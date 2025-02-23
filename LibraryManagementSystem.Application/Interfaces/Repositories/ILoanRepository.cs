using LibraryManagementSystem.Domain.Entities;

namespace LibraryManagementSystem.Application.Interfaces.Repositories
{
    /// <summary>
    /// Represents the repository interface for managing <see cref="Loan"/> entities.
    /// </summary>
    public interface ILoanRepository
    {
        /// <summary>
        /// Retrieves a loan by its unique identifier asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the loan.</param>
        /// <returns>A task representing the asynchronous operation, with the loan as the result, or null if not found.</returns>
        Task<Loan?> GetByIdAsync(Guid id);

        /// <summary>
        /// Retrieves all loans asynchronously.
        /// </summary>
        /// <returns>A task representing the asynchronous operation, with a list of loans as the result.</returns>
        Task<List<Loan>> GetAllAsync();

        /// <summary>
        /// Creates a new loan asynchronously.
        /// </summary>
        /// <param name="loan">The loan entity to create.</param>
        /// <returns>A task representing the asynchronous operation, with the created loan as the result.</returns>
        Task<Loan> CreateAsync(Loan loan);

        /// <summary>
        /// Updates an existing loan asynchronously.
        /// </summary>
        /// <param name="loan">The loan entity to update.</param>
        /// <returns>A task representing the asynchronous operation, with the updated loan as the result.</returns>
        Task<Loan> UpdateAsync(Loan loan);

        /// <summary>
        /// Checks if a book is available for loan based on the given date asynchronously.
        /// </summary>
        /// <param name="bookId">The unique identifier of the book.</param>
        /// <param name="toLoanDate">The date to check for the loan availability.</param>
        /// <returns>A task representing the asynchronous operation, with a boolean result indicating if the book is available.</returns>
        Task<bool> IsBookAvailableAsync(Guid bookId, DateOnly toLoanDate);

        /// <summary>
        /// Checks if a book is still loaned out asynchronously.
        /// </summary>
        /// <param name="bookId">The unique identifier of the book.</param>
        /// <returns>A task representing the asynchronous operation, with a boolean result indicating if the book is still loaned out.</returns>
        Task<bool> IsStillLoaned(Guid bookId);
    }
}
