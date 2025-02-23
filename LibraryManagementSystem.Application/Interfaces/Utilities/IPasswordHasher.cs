namespace LibraryManagementSystem.Application.Interfaces.Utilities
{
    /// <summary>
    /// Represents the utility interface for hashing and verifying passwords.
    /// </summary>
    public interface IPasswordHasher
    {
        /// <summary>
        /// Hashes a password to securely store it.
        /// </summary>
        /// <param name="toBeHasedPassword">The plain-text password to be hashed.</param>
        /// <returns>A string representing the hashed password.</returns>
        string HashPassword(string toBeHasedPassword);

        /// <summary>
        /// Verifies if a given password matches a hashed password.
        /// </summary>
        /// <param name="password">The plain-text password to verify.</param>
        /// <param name="hashedPassowrd">The hashed password to compare against.</param>
        /// <returns>True if the password matches the hashed password; otherwise, false.</returns>
        bool VerifyPassword(string password, string hashedPassowrd);
    }
}
