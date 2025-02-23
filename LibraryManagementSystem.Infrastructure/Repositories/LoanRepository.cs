using LibraryManagementSystem.Application.Interfaces.Repositories;
using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Infrastructure.Repositories
{
    /// <summary>
    /// Repository for performing CRUD operations on <see cref="Loan"/> entities.
    /// Provides methods for interacting with book loans in the library database.
    /// </summary>
    public class LoanRepository : ILoanRepository
    {
        private readonly LibraryDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoanRepository"/> class with the specified database context.
        /// </summary>
        /// <param name="context">The database context to interact with the database.</param>
        public LoanRepository(LibraryDbContext context) => _context = context;

        /// <summary>
        /// Checks if a book is available for loan at a specific date.
        /// </summary>
        /// <param name="bookId">The ID of the book to check for availability.</param>
        /// <param name="toLoanDate">The date to check the availability of the book.</param>
        /// <returns><c>true</c> if the book is available for loan on the specified date, otherwise <c>false</c>.</returns>
        public async Task<bool> IsBookAvailableAsync(Guid bookId, DateOnly toLoanDate)
        {
            return !await _context.Loans
                .AnyAsync(l => l.BookId == bookId && l.ReturnDate > toLoanDate);
        }

        /// <summary>
        /// Checks if a book is still loaned out and has not yet been returned.
        /// </summary>
        /// <param name="bookId">The ID of the book to check.</param>
        /// <returns><c>true</c> if the book is still loaned, otherwise <c>false</c>.</returns>
        public async Task<bool> IsStillLoaned(Guid bookId)
        {
            return await _context.Loans
                .AnyAsync(l => l.BookId == bookId && l.ReturnDate > DateOnly.FromDateTime(DateTime.UtcNow));
        }

        /// <summary>
        /// Creates a new <see cref="Loan"/> in the database.
        /// </summary>
        /// <param name="loan">The <see cref="Loan"/> entity to create.</param>
        /// <returns>The created <see cref="Loan"/> entity.</returns>
        public async Task<Loan> CreateAsync(Loan loan)
        {
            await _context.Loans.AddAsync(loan);
            await _context.SaveChangesAsync();
            return loan;
        }

        /// <summary>
        /// Retrieves all <see cref="Loan"/> entities from the database.
        /// </summary>
        /// <returns>A list of <see cref="Loan"/> entities.</returns>
        public async Task<List<Loan>> GetAllAsync()
        {
            return await _context.Loans.ToListAsync();
        }

        /// <summary>
        /// Retrieves a <see cref="Loan"/> by its ID.
        /// </summary>
        /// <param name="id">The ID of the loan to retrieve.</param>
        /// <returns>The <see cref="Loan"/> entity, or null if not found.</returns>
        public async Task<Loan?> GetByIdAsync(Guid id)
        {
            return await _context.Loans.FirstOrDefaultAsync(l => l.Id == id);
        }

        /// <summary>
        /// Updates an existing <see cref="Loan"/> in the database.
        /// </summary>
        /// <param name="loan">The <see cref="Loan"/> entity to update.</param>
        /// <returns>The updated <see cref="Loan"/> entity.</returns>
        public async Task<Loan> UpdateAsync(Loan loan)
        {
            _context.Update(loan);
            await _context.SaveChangesAsync();
            return loan;
        }
    }
}
