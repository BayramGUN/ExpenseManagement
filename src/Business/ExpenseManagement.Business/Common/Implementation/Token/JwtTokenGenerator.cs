using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ExpenseManagement.Base.Token;
using ExpenseManagement.Business.Common.Interfaces.Token;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ExpenseManagement.Business.Common.Implementation.Token;

/// <summary>
/// Service responsible for generating JWT tokens for authenticated users.
/// </summary>
public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly JwtSettings jwtSettings;

    /// <summary>
    /// Initializes a new instance of the <see cref="JwtTokenGenerator"/> class.
    /// </summary>
    /// <param name="jwtSettings">The JWT configuration options.</param>
    public JwtTokenGenerator(IOptions<JwtSettings> jwtSettings)
    {
        this.jwtSettings = jwtSettings.Value;
    }
    
    /// <summary>
    /// Generates a JWT token for the specified user.
    /// </summary>
    /// <param name="user">The user for whom the token is generated.</param>
    /// <returns>The generated JWT token as a string.</returns>
    public string GenerateToken(AppUser user)
    {
        var createdSingingCredentials = CreateSigningCredentials();
        var createdClaims = CreateClaims(user);
        var securityToken = new JwtSecurityToken(
            issuer: jwtSettings.Issuer,
            audience: jwtSettings.Audience,
            expires: DateTime.Now.AddHours(jwtSettings.AccessTokenExpiration),
            claims: createdClaims,
            signingCredentials: createdSingingCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }

    /// <summary>
    /// Creates the signing credentials required for JWT token generation.
    /// </summary>
    /// <returns>The signing credentials.</returns>
    private SigningCredentials CreateSigningCredentials() => 
        new (
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings.Secret)
            ),
            SecurityAlgorithms.HmacSha256
        );
       
    /// <summary>
    /// Creates the claims for the JWT token based on the specified user.
    /// </summary>
    /// <param name="user">The user for whom claims are created.</param>
    /// <returns>An array of claims.</returns>
    private Claim[] CreateClaims(AppUser user) => 
        new Claim[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString() ?? null!),
            new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName ?? null!),
            new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName ?? null!),
            new Claim(JwtRegisteredClaimNames.Email, user.Email ?? null!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()!),
            new Claim(ClaimTypes.Role, user.Role.ToString() ?? null!),
        };
}