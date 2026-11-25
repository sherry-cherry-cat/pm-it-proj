using System.Security.Cryptography;

namespace PasswordVault.Core;
//komentarz by zrobić commit
public static class PasswordGenerator
{
    private const string Alphabet = "ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz23456789!@#$%*_-";

    public static string Generate(int length = 20)
    {
        if (length < 12 || length > 128)
        {
            throw new ArgumentOutOfRangeException(nameof(length), "Długość musi wynosić 12–128 znaków.");
        }

        return new string(Enumerable.Range(0, length)
            .Select(_ => Alphabet[RandomNumberGenerator.GetInt32(Alphabet.Length)])
            .ToArray());
    }
}
