using System.Security.Cryptography;
using System.Text;

namespace ExpenseManagement.Base.Extensions.Encryption;

/// <summary>
/// Provides extension methods for creating SHA256 hashes from input strings.
/// </summary>
public static class PasswordHashExtension
{
    /// <summary>
    /// Creates an SHA256 hash from the specified input string.
    /// </summary>
    /// <param name="input">The input string to hash.</param>
    /// <returns>The SHA256 hash as a hexadecimal string.</returns>
    public static string CreateSHA256(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }
    }
    /// <summary>
    /// Gets an SHA256 hash for the input string by invoking the CreateSHA256 method twice.
    /// </summary>
    /// <param name="input">The input string to hash.</param>
    /// <returns>The final SHA256 hash as a lowercase hexadecimal string.</returns>
    public static string GetSHA256Hash(this string input)
    {
        var hash = CreateSHA256(input);
        return CreateSHA256(hash);
    }
}