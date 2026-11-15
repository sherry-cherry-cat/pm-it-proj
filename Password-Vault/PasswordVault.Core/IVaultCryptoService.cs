namespace PasswordVault.Core;

public interface IVaultCryptoService
{
    VaultFile Encrypt(byte[] data, string masterPassword);
    byte[] Decrypt(VaultFile file, string masterPassword);
}
