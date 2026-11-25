using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Konscious.Security.Cryptography;

namespace PasswordVault.Infrastructure;

public sealed class VaultHeader
{
    public int Version { get; set; } = 1;
    public int MemoryKb { get; set; } = 32768;
    public int Iterations { get; set; } = 3;
    public int Parallelism { get; set; } = 2;
    public string Salt { get; set; } = "";
    public EncryptedBlob WrappedKey { get; set; } = new();
}

public sealed class EncryptedBlob
{
    public int Version { get; set; } = 1;
    public string Nonce { get; set; } = "";
    public string Ciphertext { get; set; } = "";
    public string Tag { get; set; } = "";
}

public static class VaultCrypto
{
    private static readonly byte[] WrappedKeyAad = Encoding.UTF8.GetBytes("PasswordVault-DEK-v1");

    public static (VaultHeader Header, byte[] Key) Create(string password)
    {
        ValidateMaster(password);
        byte[] dek = RandomNumberGenerator.GetBytes(32);
        byte[] salt = RandomNumberGenerator.GetBytes(16);
        var header = new VaultHeader { Salt = Convert.ToBase64String(salt) };
        byte[] kek = Derive(password, header);
        try
        {
            header.WrappedKey = Encrypt(dek, kek, WrappedKeyAad);
            return (header, dek);
        }
        finally
        {
            CryptographicOperations.ZeroMemory(kek);
        }
    }

    public static byte[] Unlock(string password, VaultHeader header)
    {
        ValidateHeader(header);
        if (string.IsNullOrWhiteSpace(password))
        {
            throw new CryptographicException("Podaj hasło master.");
        }
        byte[] kek = Derive(password, header);
        try
        {
            return Decrypt(header.WrappedKey, kek, WrappedKeyAad);
        }
        finally
        {
            CryptographicOperations.ZeroMemory(kek);
        }
    }

    public static EncryptedBlob Encrypt(byte[] plaintext, byte[] key, byte[]? aad = null)
    {
        if (key.Length != 32)
        {
            throw new ArgumentException("Klucz AES musi mieć 32 bajty.", nameof(key));
        }
        byte[] nonce = RandomNumberGenerator.GetBytes(12);
        byte[] ciphertext = new byte[plaintext.Length];
        byte[] tag = new byte[16];
        using var aes = new AesGcm(key, 16);
        aes.Encrypt(nonce, plaintext, ciphertext, tag, aad);
        return new EncryptedBlob
        {
            Nonce = Convert.ToBase64String(nonce),
            Ciphertext = Convert.ToBase64String(ciphertext),
            Tag = Convert.ToBase64String(tag)
        };
    }

    public static byte[] Decrypt(EncryptedBlob blob, byte[] key, byte[]? aad = null)
    {
        if (blob.Version != 1 || key.Length != 32)
        {
            throw new CryptographicException("Nieobsługiwany format lub błędny klucz.");
        }
        byte[] nonce = Convert.FromBase64String(blob.Nonce);
        byte[] ciphertext = Convert.FromBase64String(blob.Ciphertext);
        byte[] tag = Convert.FromBase64String(blob.Tag);
        if (nonce.Length != 12 || tag.Length != 16)
        {
            throw new CryptographicException("Nieprawidłowe dane szyfrogramu.");
        }
        byte[] plaintext = new byte[ciphertext.Length];
        using var aes = new AesGcm(key, 16);
        aes.Decrypt(nonce, ciphertext, tag, plaintext, aad);
        return plaintext;
    }

    public static byte[] EntryAad(string id) => Encoding.UTF8.GetBytes("PasswordVault-entry-v1:" + id);

    public static void ValidateHeader(VaultHeader? header)
    {
        if (header is null || header.Version != 1 ||
            header.MemoryKb is < 19456 or > 131072 ||
            header.Iterations is < 2 or > 10 ||
            header.Parallelism is < 1 or > 8 ||
            Convert.FromBase64String(header.Salt).Length != 16 ||
            header.WrappedKey is null)
        {
            throw new InvalidDataException("Nieobsługiwany lub uszkodzony format sejfu.");
        }
    }

    private static byte[] Derive(string password, VaultHeader header)
    {
        byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
        try
        {
            using var argon = new Argon2id(passwordBytes)
            {
                Salt = Convert.FromBase64String(header.Salt),
                DegreeOfParallelism = header.Parallelism,
                Iterations = header.Iterations,
                MemorySize = header.MemoryKb
            };
            return argon.GetBytes(32);
        }
        finally
        {
            CryptographicOperations.ZeroMemory(passwordBytes);
        }
    }

    private static void ValidateMaster(string password)
    {
        if (string.IsNullOrWhiteSpace(password) || password.Length < 12)
        {
            throw new ArgumentException("Hasło master musi mieć co najmniej 12 znaków.");
        }
    }
}
