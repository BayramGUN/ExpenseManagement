namespace ExpenseManagement.Business.Common;

/// <summary>
/// Interface for generating JWT tokens based on AppUser information.
/// </summary>
public interface IJwtTokenGenerator
{
    /// <summary>
    /// Generates a JWT token for the specified AppUser.
    /// </summary>
    /// <param name="user">The AppUser for whom the token is generated.</param>
    /// <returns>The generated JWT token.</returns>
    string GenerateToken(AppUser user);
}