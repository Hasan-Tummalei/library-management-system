using LibraryManagementSystem.Application.Interfaces.Repositories;
using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Infrastructure.Repositories
{
    /// <summary>
    /// Repository for performing CRUD operations on <see cref="User"/> entities.
    /// Provides methods for interacting with users in the library system.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly LibraryDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class with the specified database context.
        /// </summary>
        /// <param name="context">The database context to interact with the database.</param>
        public UserRepository(LibraryDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves a <see cref="User"/> by its unique identifier.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>The <see cref="User"/> entity, or <c>null</c> if no user was found with the specified ID.</returns>
        public async Task<User?> GetUserById(Guid id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        /// <summary>
        /// Retrieves a <see cref="User"/> by their username, typically used for login.
        /// </summary>
        /// <param name="username">The username of the user to retrieve.</param>
        /// <returns>The <see cref="User"/> entity, or <c>null</c> if no user was found with the specified username.</returns>
        public async Task<User?> Login(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        /// <summary>
        /// Registers a new user by adding them to the database.
        /// </summary>
        /// <param name="user">The <see cref="User"/> entity to register.</param>
        /// <returns>The created <see cref="User"/> entity.</returns>
        public async Task<User> RegisterUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        /// <summary>
        /// Updates an existing user's information in the database.
        /// </summary>
        /// <param name="user">The <see cref="User"/> entity with updated information.</param>
        public async Task UpdateUser(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
