using System.Text.Json;
using PasswordVault.Core;

namespace PasswordVault.Infrastructure;

public static class VaultFileJson
{
    private static readonly JsonSerializerOptions Options = new()
    {
        WriteIndented = true,
        PropertyNameCaseInsensitive = false
    };

    public static string Serialize(VaultFile file)
    {
        ArgumentNullException.ThrowIfNull(file);
        return JsonSerializer.Serialize(file, Options);
    }

    public static VaultFile Deserialize(string json)
    {
        if (string.IsNullOrWhiteSpace(json))
            throw new JsonException("Pusty plik sejfu.");
        return JsonSerializer.Deserialize<VaultFile>(json, Options)
            ?? throw new JsonException("Nie można odczytać sejfu.");
    }
}
