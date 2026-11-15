namespace PasswordVault.Core;

// Nagłówek i zaszyfrowana zawartość. W tym obiekcie nie ma jawnego hasła.
public sealed record VaultFile(
    int Version,
    string Kdf,
    int MemoryKiB,
    int Iterations,
    int Parallelism,
    string Salt,
    string KeyNonce,
    string WrappedKey,
    string KeyTag,
    string DataNonce,
    string Data,
    string DataTag);
