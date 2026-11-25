using System.Security.Cryptography;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using PasswordVault.Core;

namespace PasswordVault.Infrastructure;

public sealed class VaultExport
{
    public int Version { get; set; } = 1;
    public VaultHeader Header { get; set; } = new();
    public List<VaultExportRecord> Records { get; set; } = new();
}

public sealed class VaultExportRecord
{
    public string Id { get; set; } = "";
    public string EncryptedJson { get; set; } = "";
}

public sealed class VaultRepository
{
    private readonly string _databasePath;
    private static readonly JsonSerializerOptions JsonOptions = new() { WriteIndented = false };

    public VaultRepository(string databasePath)
    {
        _databasePath = Path.GetFullPath(databasePath);
        Directory.CreateDirectory(Path.GetDirectoryName(_databasePath)!);
        using var db = Open();
        db.Database.EnsureCreated();
    }

    public static string DefaultPath => Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "PasswordVaultDemo", "vault.sqlite");

    public bool Exists()
    {
        using var db = Open();
        return db.Metadata.Any(m => m.Id == 1);
    }

    public byte[] Create(string password)
    {
        if (Exists())
        {
            throw new InvalidOperationException("Sejf już istnieje.");
        }
        var (header, dek) = VaultCrypto.Create(password);
        try
        {
            using var db = Open();
            db.Metadata.Add(new VaultMetadata
            {
                Id = 1,
                HeaderJson = JsonSerializer.Serialize(header, JsonOptions)
            });
            db.SaveChanges();
            return dek;
        }
        catch
        {
            CryptographicOperations.ZeroMemory(dek);
            throw;
        }
    }

    public byte[] Unlock(string password)
    {
        using var db = Open();
        var meta = db.Metadata.SingleOrDefault(m => m.Id == 1)
            ?? throw new InvalidOperationException("Nie ma jeszcze utworzonego sejfu.");
        var header = JsonSerializer.Deserialize<VaultHeader>(meta.HeaderJson)
            ?? throw new InvalidDataException("Uszkodzony nagłówek sejfu.");
        return VaultCrypto.Unlock(password, header);
    }

    public List<VaultEntry> GetEntries(byte[] dek)
    {
        using var db = Open();
        var rows = db.Records.AsNoTracking().ToList();
        return rows.Select(row => DecryptRecord(row.Id, row.EncryptedJson, dek))
            .OrderBy(e => e.Title, StringComparer.CurrentCultureIgnoreCase).ToList();
    }

    public void Save(VaultEntry entry, byte[] dek)
    {
        if (string.IsNullOrWhiteSpace(entry.Title) || string.IsNullOrWhiteSpace(entry.Password))
        {
            throw new ArgumentException("Uzupełnij nazwę wpisu i hasło.");
        }
        if (string.IsNullOrWhiteSpace(entry.Id))
        {
            entry.Id = Guid.NewGuid().ToString("N");
        }
        string encrypted = EncryptRecord(entry, dek);
        using var db = Open();
        var row = db.Records.Find(entry.Id);
        if (row is null)
        {
            db.Records.Add(new VaultRecord { Id = entry.Id, EncryptedJson = encrypted });
        }
        else
        {
            row.EncryptedJson = encrypted;
        }
        db.SaveChanges();
    }

    public void Delete(string id)
    {
        using var db = Open();
        var row = db.Records.Find(id);
        if (row is null) return;
        db.Records.Remove(row);
        db.SaveChanges();
    }

    public void Export(string destination)
    {
        using var db = Open();
        var meta = db.Metadata.AsNoTracking().SingleOrDefault(m => m.Id == 1)
            ?? throw new InvalidOperationException("Brak sejfu.");
        var package = new VaultExport
        {
            Header = JsonSerializer.Deserialize<VaultHeader>(meta.HeaderJson)!,
            Records = db.Records.AsNoTracking()
                .Select(r => new VaultExportRecord { Id = r.Id, EncryptedJson = r.EncryptedJson })
                .ToList()
        };
        string fullPath = Path.GetFullPath(destination);
        if (string.Equals(fullPath, _databasePath, StringComparison.OrdinalIgnoreCase))
        {
            throw new IOException("Nie można eksportować do aktywnego pliku bazy.");
        }
        string temp = fullPath + ".tmp";
        try
        {
            File.WriteAllText(temp, JsonSerializer.Serialize(package, new JsonSerializerOptions { WriteIndented = true }));
            File.Move(temp, fullPath, true);
        }
        finally
        {
            if (File.Exists(temp)) File.Delete(temp);
        }
    }

    public void Import(string source, string password)
    {
        var file = new FileInfo(source);
        if (!file.Exists || file.Length > 10 * 1024 * 1024)
        {
            throw new InvalidDataException("Nie ma pliku lub plik jest zbyt duży.");
        }
        var package = JsonSerializer.Deserialize<VaultExport>(File.ReadAllText(source))
            ?? throw new InvalidDataException("Nie można odczytać eksportu.");
        if (package.Version != 1 || package.Records is null || package.Records.Count > 5000)
        {
            throw new InvalidDataException("Nieobsługiwany format eksportu.");
        }
        VaultCrypto.ValidateHeader(package.Header);
        byte[] dek = VaultCrypto.Unlock(password, package.Header);
        try
        {
            // Najpierw sprawdzam wszystkie wpisy. Baza nie jest ruszana przy błędnym eksporcie.
            var ids = new HashSet<string>(StringComparer.Ordinal);
            foreach (var record in package.Records)
            {
                if (string.IsNullOrWhiteSpace(record.Id) || !ids.Add(record.Id))
                {
                    throw new InvalidDataException("Zduplikowane lub puste ID wpisu.");
                }
                _ = DecryptRecord(record.Id, record.EncryptedJson, dek);
            }
            using var db = Open();
            using var transaction = db.Database.BeginTransaction();
            db.Records.RemoveRange(db.Records);
            db.Metadata.RemoveRange(db.Metadata);
            db.SaveChanges();
            db.Metadata.Add(new VaultMetadata
            {
                Id = 1,
                HeaderJson = JsonSerializer.Serialize(package.Header, JsonOptions)
            });
            db.Records.AddRange(package.Records.Select(r => new VaultRecord
            {
                Id = r.Id,
                EncryptedJson = r.EncryptedJson
            }));
            db.SaveChanges();
            transaction.Commit();
        }
        finally
        {
            CryptographicOperations.ZeroMemory(dek);
        }
    }

    private VaultDbContext Open()
    {
        var options = new DbContextOptionsBuilder<VaultDbContext>()
            .UseSqlite($"Data Source={_databasePath};Pooling=False")
            .Options;
        return new VaultDbContext(options);
    }

    private static string EncryptRecord(VaultEntry entry, byte[] dek)
    {
        byte[] plaintext = JsonSerializer.SerializeToUtf8Bytes(entry, JsonOptions);
        try
        {
            return JsonSerializer.Serialize(
                VaultCrypto.Encrypt(plaintext, dek, VaultCrypto.EntryAad(entry.Id)), JsonOptions);
        }
        finally
        {
            CryptographicOperations.ZeroMemory(plaintext);
        }
    }

    private static VaultEntry DecryptRecord(string id, string encryptedJson, byte[] dek)
    {
        var blob = JsonSerializer.Deserialize<EncryptedBlob>(encryptedJson)
            ?? throw new InvalidDataException("Pusty szyfrogram wpisu.");
        byte[] plaintext = VaultCrypto.Decrypt(blob, dek, VaultCrypto.EntryAad(id));
        try
        {
            var entry = JsonSerializer.Deserialize<VaultEntry>(plaintext)
                ?? throw new InvalidDataException("Pusty wpis.");
            if (entry.Id != id)
            {
                throw new InvalidDataException("Niezgodne ID wpisu.");
            }
            return entry;
        }
        finally
        {
            CryptographicOperations.ZeroMemory(plaintext);
        }
    }
}
