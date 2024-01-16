namespace ExpenseManagement.Base.Extensions.Encryption;

/// <summary>
/// Provides extension methods for creating MD5 hashes from input strings.
/// </summary>
public static class Md5Extension
{
    /// <summary>
    /// Creates an MD5 hash from the specified input string.
    /// </summary>
    /// <param name="input">The input string to hash.</param>
    /// <returns>The MD5 hash as a lowercase hexadecimal string.</returns>
    public static string CreateMd5(string input)
    {
        using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
        {
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            return Convert.ToHexString(hashBytes).ToLower();

        }
    }
    /// <summary>
    /// Gets an MD5 hash for the input string by invoking the CreateMd5 method twice.
    /// </summary>
    /// <param name="input">The input string to hash.</param>
    /// <returns>The final MD5 hash as a lowercase hexadecimal string.</returns>
    public static string GetMd5Hash(this string input)
    {
        var hash = CreateMd5(input);
        return CreateMd5(hash);
    }
}