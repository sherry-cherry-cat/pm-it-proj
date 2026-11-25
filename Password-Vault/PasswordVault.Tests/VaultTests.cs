using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using PasswordVault.Core;
using PasswordVault.Infrastructure;
using Xunit;

namespace PasswordVault.Tests;

public sealed class VaultTests
{
    private const string Master = "DuzeHasloTestowe!2026";

    private static string TempPath() => Path.Combine(Path.GetTempPath(),
        "pv-test-" + Guid.NewGuid().ToString("N"), "vault.sqlite");

    [Fact]
    public void KluczMa32Bajty()
    {
        var (header, key) = VaultCrypto.Create(Master);
        Assert.Equal(32, key.Length);
        Assert.Equal(1, header.Version);
    }

    [Fact]
    public void MasterOtwieraTenSamDek()
    {
        var (header, key) = VaultCrypto.Create(Master);
        byte[] unwrapped = VaultCrypto.Unlock(Master, header);
        Assert.Equal(key, unwrapped);
    }

    [Fact]
    public void ZleMasterNieOtwieraSejfu()
    {
        var (header, _) = VaultCrypto.Create(Master);
        Assert.Throws<CryptographicException>(() => VaultCrypto.Unlock("InneHasloTestowe!", header));
    }

    [Fact]
    public void AesGcmSzyfrujeIOdszyfrowuje()
    {
        byte[] key = RandomNumberGenerator.GetBytes(32);
        byte[] plain = Encoding.UTF8.GetBytes("tajne dane testowe");
        var blob = VaultCrypto.Encrypt(plain, key);
        Assert.Equal(plain, VaultCrypto.Decrypt(blob, key));
    }

    [Fact]
    public void AesGcmWykrywaZmianeDanych()
    {
        byte[] key = RandomNumberGenerator.GetBytes(32);
        var blob = VaultCrypto.Encrypt(Encoding.UTF8.GetBytes("abc"), key);
        blob.Ciphertext = Convert.ToBase64String(Encoding.UTF8.GetBytes("xyz"));
        Assert.Throws<CryptographicException>(() => VaultCrypto.Decrypt(blob, key));
    }

    [Fact]
    public void KolejneSzyfrowaniaMajaRozneNonce()
    {
        byte[] key = RandomNumberGenerator.GetBytes(32);
        var first = VaultCrypto.Encrypt([1, 2, 3], key);
        var second = VaultCrypto.Encrypt([1, 2, 3], key);
        Assert.NotEqual(first.Nonce, second.Nonce);
    }

    [Fact]
    public void GeneratorMaPoprawnaDlugosc()
    {
        Assert.Equal(24, PasswordGenerator.Generate(24).Length);
        Assert.NotEqual(PasswordGenerator.Generate(20), PasswordGenerator.Generate(20));
    }

    [Fact]
    public void GeneratorOdrzucaZlaDlugosc()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => PasswordGenerator.Generate(5));
    }

    [Fact]
    public void BazaZachowujeWpisPoPonownymOtwarciu()
    {
        string path = TempPath();
        try
        {
            var store = new VaultRepository(path);
            byte[] key = store.Create(Master);
            store.Save(new VaultEntry { Title = "Przykład", Username = "test", Password = "Haslo!123" }, key);
            var again = new VaultRepository(path);
            byte[] unlocked = again.Unlock(Master);
            var result = Assert.Single(again.GetEntries(unlocked));
            Assert.Equal("Haslo!123", result.Password);
        }
        finally { Cleanup(path); }
    }

    [Fact]
    public void EdycjaIUsuniecieDzialaja()
    {
        string path = TempPath();
        try
        {
            var store = new VaultRepository(path);
            byte[] key = store.Create(Master);
            var entry = new VaultEntry { Title = "Stara", Password = "123" };
            store.Save(entry, key);
            entry.Title = "Nowa";
            store.Save(entry, key);
            Assert.Equal("Nowa", Assert.Single(store.GetEntries(key)).Title);
            store.Delete(entry.Id);
            Assert.Empty(store.GetEntries(key));
        }
        finally { Cleanup(path); }
    }

    [Fact]
    public void EksportImportOdtwarzaWpisy()
    {
        string source = TempPath();
        string destination = TempPath();
        string export = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".pvault");
        try
        {
            var a = new VaultRepository(source);
            byte[] key = a.Create(Master);
            a.Save(new VaultEntry { Title = "Konto", Username = "user", Password = "tajne" }, key);
            a.Export(export);
            var b = new VaultRepository(destination);
            b.Import(export, Master);
            Assert.Equal("tajne", Assert.Single(b.GetEntries(b.Unlock(Master))).Password);
            Assert.DoesNotContain("tajne", File.ReadAllText(export));
        }
        finally
        {
            Cleanup(source); Cleanup(destination); if (File.Exists(export)) File.Delete(export);
        }
    }

    [Fact]
    public void BlednyImportNieNadpisujeIstniejacegoSejfu()
    {
        string source = TempPath();
        string destination = TempPath();
        string export = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".pvault");
        try
        {
            var a = new VaultRepository(source);
            a.Create(Master);
            a.Export(export);
            var b = new VaultRepository(destination);
            byte[] key = b.Create("MojeInneHaslo!456");
            b.Save(new VaultEntry { Title = "Ocalały", Password = "sekret" }, key);
            Assert.Throws<CryptographicException>(() => b.Import(export, "ZleHasloUzytkownika"));
            Assert.Equal("Ocalały", Assert.Single(b.GetEntries(b.Unlock("MojeInneHaslo!456"))).Title);
        }
        finally
        {
            Cleanup(source); Cleanup(destination); if (File.Exists(export)) File.Delete(export);
        }
    }

    [Fact]
    public void ResetUsuwaLokalnySejfIPozwalaUtworzycNowy()
    {
        string path = TempPath();
        string export = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".pvault");
        try
        {
            var store = new VaultRepository(path);
            byte[] oldKey = store.Create(Master);
            store.Save(new VaultEntry { Title = "Testowy serwis", Password = "TajneHaslo123" }, oldKey);
            store.Export(export);
            string exportedBefore = File.ReadAllText(export);

            store.Reset();

            Assert.False(store.Exists());
            Assert.Equal(exportedBefore, File.ReadAllText(export));
            byte[] newKey = store.Create("NoweHasloMaster!2026");
            Assert.Empty(store.GetEntries(newKey));
            Assert.Throws<CryptographicException>(() => store.Unlock(Master));
            CryptographicOperations.ZeroMemory(oldKey);
            CryptographicOperations.ZeroMemory(newKey);
        }
        finally
        {
            Cleanup(path);
            if (File.Exists(export)) File.Delete(export);
        }
    }

    private static void Cleanup(string path)
    {
        string? folder = Path.GetDirectoryName(path);
        if (folder is not null && Directory.Exists(folder))
        {
            Directory.Delete(folder, true);
        }
    }
}
