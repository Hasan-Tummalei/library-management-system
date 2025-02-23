using LibraryManagementSystem.Domain.Entities;

namespace LibraryManagementSystem.Application.Interfaces.Repositories
{
    /// <summary>
    /// Represents the repository interface for managing <see cref="User"/> entities.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Registers a new user asynchronously.
        /// </summary>
        /// <param name="user">The user entity to register.</param>
        /// <returns>A task representing the asynchronous operation, with the registered user as the result.</returns>
        Task<User> RegisterUser(User user);

        /// <summary>
        /// Logs in a user by their username asynchronously.
        /// </summary>
        /// <param name="username">The username of the user attempting to log in.</param>
        /// <returns>A task representing the asynchronous operation, with the user as the result, or null if the user was not found.</returns>
        Task<User?> Login(string username);

        /// <summary>
        /// Updates an existing user asynchronously.
        /// </summary>
        /// <param name="user">The user entity to update.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task UpdateUser(User user);

        /// <summary>
        /// Retrieves a user by their unique identifier asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the user.</param>
        /// <returns>A task representing the asynchronous operation, with the user as the result, or null if not found.</returns>
        Task<User?> GetUserById(Guid id);
    }
}
