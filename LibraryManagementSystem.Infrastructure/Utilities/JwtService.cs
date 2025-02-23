using System.Security.Claims;
using System.Text;
using LibraryManagementSystem.Domain.Entities;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using LibraryManagementSystem.Infrastructure.Configuration.EntitiesConfigurations;
using LibraryManagementSystem.Application.Interfaces.Utilities;

namespace LibraryManagementSystem.Infrastructure.Utilities
{
    /// <summary>
    /// Provides functionality for generating JSON Web Tokens (JWT) for user authentication and authorization.
    /// </summary>
    public class JwtService : IJwtService
    {
        private readonly JwtSettings _settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="JwtService"/> class.
        /// </summary>
        /// <param name="settings">An instance of <see cref="IOptions{JwtSettings}"/> containing the JWT configuration settings.</param>
        public JwtService(IOptions<JwtSettings> settings) => _settings = settings.Value;

        /// <summary>
        /// Generates a JWT for the given user.
        /// </summary>
        /// <param name="user">The user for whom the JWT is generated.</param>
        /// <returns>A JWT as a string.</returns>
        public string GenerateToken(User user)
        {
            // Create a symmetric security key using the secret key from configuration
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.SecretKey));

            // Create signing credentials using the symmetric key and the HMACSHA256 algorithm
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Create claims for the token
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),  // Subject claim containing the user ID
                new Claim(ClaimTypes.Role, user.UserRole.ToString())          // Role claim for user roles
            };

            // Create the JWT token with the issuer, audience, claims, expiration time, and signing credentials
            var token = new JwtSecurityToken(
                issuer: _settings.Issuer,
                audience: _settings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_settings.ExpiryInMinutes),  // Token expiration time
                signingCredentials: credentials
            );

            // Return the generated token as a string
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
