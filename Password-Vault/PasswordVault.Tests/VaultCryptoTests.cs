using System.Security.Cryptography;
using System.Text;
using PasswordVault.Core;
using PasswordVault.Infrastructure;
using Xunit;

namespace PasswordVault.Tests;

public sealed class VaultCryptoTests
{
    private const string Master = "przykladowe mocne haslo testowe 123";
    private readonly VaultCryptoService _service = new();

    [Fact]
    public void PoprawneHasloOdszyfrowujeDane()
    {
        byte[] data = Encoding.UTF8.GetBytes("konto: test@example.org; haslo: fikcyjne");
        VaultFile file = _service.Encrypt(data, Master);

        Assert.Equal(data, _service.Decrypt(file, Master));
    }

    [Fact]
    public void BledneHasloNieOtwieraSejfu()
    {
        VaultFile file = _service.Encrypt(Encoding.UTF8.GetBytes("sekret"), Master);

        Assert.Throws<CryptographicException>(() =>
            _service.Decrypt(file, "inne haslo"));
    }

    [Fact]
    public void UszkodzonySzyfrogramJestOdrzucany()
    {
        VaultFile file = _service.Encrypt(Encoding.UTF8.GetBytes("test"), Master);
        byte[] data = Convert.FromBase64String(file.Data);
        data[0] ^= 1;
        VaultFile damaged = file with { Data = Convert.ToBase64String(data) };

        Assert.Throws<CryptographicException>(() => _service.Decrypt(damaged, Master));
    }

    [Fact]
    public void UszkodzonyKluczJestOdrzucany()
    {
        VaultFile file = _service.Encrypt(Encoding.UTF8.GetBytes("test"), Master);
        byte[] data = Convert.FromBase64String(file.WrappedKey);
        data[0] ^= 1;
        VaultFile damaged = file with { WrappedKey = Convert.ToBase64String(data) };

        Assert.Throws<CryptographicException>(() => _service.Decrypt(damaged, Master));
    }

    [Fact]
    public void DwaSzyfrowaniaMajaRozneLosoweWartosci()
    {
        byte[] data = Encoding.UTF8.GetBytes("te same dane");
        VaultFile first = _service.Encrypt(data, Master);
        VaultFile second = _service.Encrypt(data, Master);

        Assert.NotEqual(first.Salt, second.Salt);
        Assert.NotEqual(first.KeyNonce, second.KeyNonce);
        Assert.NotEqual(first.DataNonce, second.DataNonce);
        Assert.NotEqual(first.Data, second.Data);
    }

    [Fact]
    public void ZapisJsonIPonownyOdczytDziala()
    {
        byte[] data = Encoding.UTF8.GetBytes("haslo testowe: zażółć gęślą jaźń");
        VaultFile file = _service.Encrypt(data, Master);

        string json = VaultFileJson.Serialize(file);
        VaultFile loaded = VaultFileJson.Deserialize(json);

        Assert.DoesNotContain("zażółć", json);
        Assert.Equal(data, _service.Decrypt(loaded, Master));
    }

    [Fact]
    public void FormatV2NieJestAkceptowanyBezImplementacji()
    {
        VaultFile file = _service.Encrypt(Encoding.UTF8.GetBytes("test"), Master);
        Assert.Throws<NotSupportedException>(() =>
            _service.Decrypt(file with { Version = 2 }, Master));
    }

    [Fact]
    public void NieMoznaZmniejszycKosztuKdfWPliku()
    {
        VaultFile file = _service.Encrypt(Encoding.UTF8.GetBytes("test"), Master);
        Assert.Throws<NotSupportedException>(() =>
            _service.Decrypt(file with { MemoryKiB = 8 }, Master));
    }

    [Theory]
    [InlineData("")]
    [InlineData("  ")]
    public void PusteHasloJestOdrzucane(string master)
    {
        Assert.Throws<ArgumentException>(() =>
            _service.Encrypt(Encoding.UTF8.GetBytes("test"), master));
    }

    [Fact]
    public void PustySejfMoznaZaszyfrowac()
    {
        VaultFile file = _service.Encrypt(Array.Empty<byte>(), Master);
        Assert.Empty(_service.Decrypt(file, Master));
    }

    [Fact]
    public void BlednyNaglowekJestOdrzucany()
    {
        VaultFile file = _service.Encrypt(Encoding.UTF8.GetBytes("test"), Master);
        Assert.Throws<FormatException>(() =>
            _service.Decrypt(file with { KeyNonce = "AA==" }, Master));
    }
}
