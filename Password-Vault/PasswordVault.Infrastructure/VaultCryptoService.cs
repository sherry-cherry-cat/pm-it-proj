using System.Security.Cryptography;
using System.Text;
using Konscious.Security.Cryptography;
using PasswordVault.Core;

namespace PasswordVault.Infrastructure;

// Pierwszy format testowy sejfu. Obsługa wpisów i SQLite będą w Sprincie 2.
public sealed class VaultCryptoService : IVaultCryptoService
{
    private const int FormatVersion = 1;
    private const string KdfName = "Argon2id";
    private const int KeySize = 32;
    private const int SaltSize = 16;
    private const int NonceSize = 12;
    private const int TagSize = 16;
    private const int MemoryKiB = 65536;
    private const int Iterations = 3;
    private const int Parallelism = 1;
    private const int MaxDataBytes = 1024 * 1024;

    private static readonly byte[] KeyContext = Encoding.UTF8.GetBytes("PasswordVault:v1:wrapped-key");
    private static readonly byte[] DataContext = Encoding.UTF8.GetBytes("PasswordVault:v1:data");

    public VaultFile Encrypt(byte[] data, string masterPassword)
    {
        ArgumentNullException.ThrowIfNull(data);
        ValidatePassword(masterPassword);
        if (data.Length > MaxDataBytes)
            throw new ArgumentException("Dane testowe są za duże.", nameof(data));

        byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
        byte[] kek = DeriveKey(masterPassword, salt);
        byte[] dek = RandomNumberGenerator.GetBytes(KeySize);

        try
        {
            byte[] keyNonce = RandomNumberGenerator.GetBytes(NonceSize);
            byte[] wrappedKey = new byte[KeySize];
            byte[] keyTag = new byte[TagSize];

            using (var aes = new AesGcm(kek, TagSize))
                aes.Encrypt(keyNonce, dek, wrappedKey, keyTag, KeyContext);

            byte[] dataNonce = RandomNumberGenerator.GetBytes(NonceSize);
            byte[] encryptedData = new byte[data.Length];
            byte[] dataTag = new byte[TagSize];

            using (var aes = new AesGcm(dek, TagSize))
                aes.Encrypt(dataNonce, data, encryptedData, dataTag, DataContext);

            return new VaultFile(
                FormatVersion, KdfName, MemoryKiB, Iterations, Parallelism,
                Convert.ToBase64String(salt),
                Convert.ToBase64String(keyNonce),
                Convert.ToBase64String(wrappedKey),
                Convert.ToBase64String(keyTag),
                Convert.ToBase64String(dataNonce),
                Convert.ToBase64String(encryptedData),
                Convert.ToBase64String(dataTag));
        }
        finally
        {
            CryptographicOperations.ZeroMemory(kek);
            CryptographicOperations.ZeroMemory(dek);
        }
    }

    public byte[] Decrypt(VaultFile file, string masterPassword)
    {
        ArgumentNullException.ThrowIfNull(file);
        ValidatePassword(masterPassword);
        ValidateHeader(file);

        byte[] salt = ReadBase64(file.Salt, SaltSize);
        byte[] keyNonce = ReadBase64(file.KeyNonce, NonceSize);
        byte[] wrappedKey = ReadBase64(file.WrappedKey, KeySize);
        byte[] keyTag = ReadBase64(file.KeyTag, TagSize);
        byte[] dataNonce = ReadBase64(file.DataNonce, NonceSize);
        byte[] dataTag = ReadBase64(file.DataTag, TagSize);
        byte[] encryptedData = ReadData(file.Data);

        byte[] kek = DeriveKey(masterPassword, salt);
        byte[] dek = new byte[KeySize];

        try
        {
            using (var aes = new AesGcm(kek, TagSize))
                aes.Decrypt(keyNonce, wrappedKey, keyTag, dek, KeyContext);

            byte[] plaintext = new byte[encryptedData.Length];
            try
            {
                using var aes = new AesGcm(dek, TagSize);
                aes.Decrypt(dataNonce, encryptedData, dataTag, plaintext, DataContext);
                return plaintext;
            }
            catch
            {
                CryptographicOperations.ZeroMemory(plaintext);
                throw;
            }
        }
        finally
        {
            CryptographicOperations.ZeroMemory(kek);
            CryptographicOperations.ZeroMemory(dek);
        }
    }

    private static byte[] DeriveKey(string password, byte[] salt)
    {
        byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
        try
        {
            using var argon2 = new Argon2id(passwordBytes)
            {
                Salt = salt,
                MemorySize = MemoryKiB,
                Iterations = Iterations,
                DegreeOfParallelism = Parallelism
            };
            return argon2.GetBytes(KeySize);
        }
        finally
        {
            CryptographicOperations.ZeroMemory(passwordBytes);
        }
    }

    private static void ValidatePassword(string? password)
    {
        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentException("Podaj hasło master.", nameof(password));
    }

    private static void ValidateHeader(VaultFile file)
    {
        // W v1 parametry są stałe. Nie ufamy wartościom KDF z obcego pliku.
        if (file.Version != FormatVersion || file.Kdf != KdfName ||
            file.MemoryKiB != MemoryKiB || file.Iterations != Iterations ||
            file.Parallelism != Parallelism)
            throw new NotSupportedException("Nieobsługiwany format sejfu.");
    }

    private static byte[] ReadBase64(string? value, int expectedLength)
    {
        if (value is null || value.Length > 256)
            throw new FormatException("Błędny nagłówek sejfu.");
        byte[] bytes = Convert.FromBase64String(value);
        if (bytes.Length != expectedLength)
            throw new FormatException("Błędna długość pola w nagłówku sejfu.");
        return bytes;
    }

    private static byte[] ReadData(string? value)
    {
        if (value is null || value.Length > ((MaxDataBytes + 2) / 3) * 4 + 4)
            throw new FormatException("Błędny rozmiar danych sejfu.");
        byte[] bytes = Convert.FromBase64String(value);
        if (bytes.Length > MaxDataBytes)
            throw new FormatException("Zbyt duże dane sejfu.");
        return bytes;
    }
}
