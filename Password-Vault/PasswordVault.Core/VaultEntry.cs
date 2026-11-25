namespace PasswordVault.Core;

// Jeden zapisany login. W bazie szyfrowany jest cały obiekt, nie tylko hasło.
public sealed class VaultEntry
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string Title { get; set; } = "";
    public string Url { get; set; } = "";
    public string Username { get; set; } = "";
    public string Password { get; set; } = "";
    public string Notes { get; set; } = "";

    public override string ToString() => Title;
}
