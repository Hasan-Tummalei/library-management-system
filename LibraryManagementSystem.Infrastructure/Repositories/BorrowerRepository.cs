using LibraryManagementSystem.Application.Interfaces.Repositories;
using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Infrastructure.Repositories
{
    /// <summary>
    /// Repository for performing CRUD operations on <see cref="Borrower"/> entities.
    /// Provides methods for interacting with borrowers in the library database.
    /// </summary>
    public class BorrowerRepository : IBorrowerRepository
    {
        private readonly LibraryDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="BorrowerRepository"/> class with the specified database context.
        /// </summary>
        /// <param name="context">The database context to interact with the database.</param>
        public BorrowerRepository(LibraryDbContext context) => _context = context;

        /// <summary>
        /// Retrieves a <see cref="Borrower"/> by its associated User ID.
        /// </summary>
        /// <param name="userId">The ID of the user to find the borrower.</param>
        /// <returns>The <see cref="Borrower"/> entity, or null if not found.</returns>
        public async Task<Borrower?> GetByUserIdAsync(Guid userId)
        {
            return await _context.Borrowers
                .FirstOrDefaultAsync(b => b.UserId == userId);
        }

        /// <summary>
        /// Retrieves a <see cref="Borrower"/> by its ID.
        /// </summary>
        /// <param name="id">The ID of the borrower to retrieve.</param>
        /// <returns>The <see cref="Borrower"/> entity, or null if not found.</returns>
        public async Task<Borrower?> GetByIdAsync(Guid id)
        {
            return await _context.Borrowers
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        /// <summary>
        /// Creates a new <see cref="Borrower"/> in the database.
        /// </summary>
        /// <param name="borrower">The <see cref="Borrower"/> entity to create.</param>
        /// <returns>The created <see cref="Borrower"/> entity.</returns>
        public async Task<Borrower> CreateAsync(Borrower borrower)
        {
            await _context.Borrowers.AddAsync(borrower);
            await _context.SaveChangesAsync();
            return borrower;
        }

        /// <summary>
        /// Retrieves all <see cref="Borrower"/> entities.
        /// </summary>
        /// <returns>A list of <see cref="Borrower"/> entities.</returns>
        public async Task<List<Borrower>?> GetByAllAsync()
        {
            return await _context.Borrowers.ToListAsync();
        }

        /// <summary>
        /// Updates an existing <see cref="Borrower"/> in the database.
        /// </summary>
        /// <param name="borrower">The <see cref="Borrower"/> entity to update.</param>
        /// <returns>The updated <see cref="Borrower"/> entity.</returns>
        public async Task<Borrower> UpdateAsync(Borrower borrower)
        {
            _context.Borrowers.Update(borrower);
            await _context.SaveChangesAsync();
            return borrower;
        }

        /// <summary>
        /// Deletes a <see cref="Borrower"/> entity from the database.
        /// </summary>
        /// <param name="borrower">The <see cref="Borrower"/> entity to delete.</param>
        public async Task DeleteAsync(Borrower borrower)
        {
            _context.Borrowers.Remove(borrower);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Checks if a borrower has any active loans that have not yet been returned.
        /// </summary>
        /// <param name="borrowerId">The ID of the borrower to check for active loans.</param>
        /// <returns><c>true</c> if the borrower has active loans, otherwise <c>false</c>.</returns>
        public async Task<bool> HasActiveLoansAsync(Guid borrowerId)
        {
            return await _context.Loans
                .AnyAsync(l => l.BorrowerId == borrowerId && l.ReturnDate > DateOnly.FromDateTime(DateTime.UtcNow));
        }
    }
}
