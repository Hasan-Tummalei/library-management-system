using LibraryManagementSystem.Domain.Entities;

namespace LibraryManagementSystem.Application.Interfaces.Utilities
{
    /// <summary>
    /// Represents the service interface for handling JSON Web Tokens (JWT) operations.
    /// </summary>
    public interface IJwtService
    {
        /// <summary>
        /// Generates a JWT token for a given user.
        /// </summary>
        /// <param name="user">The user entity for whom the token is to be generated.</param>
        /// <returns>A string representing the generated JWT token.</returns>
        string GenerateToken(User user);
    }
}
