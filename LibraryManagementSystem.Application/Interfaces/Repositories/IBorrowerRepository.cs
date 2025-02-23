using LibraryManagementSystem.Domain.Entities;

namespace LibraryManagementSystem.Application.Interfaces.Repositories
{
    /// <summary>
    /// Represents the repository interface for managing <see cref="Borrower"/> entities.
    /// </summary>
    public interface IBorrowerRepository
    {
        /// <summary>
        /// Retrieves a borrower by their user identifier asynchronously.
        /// </summary>
        /// <param name="userId">The unique identifier of the borrower (user).</param>
        /// <returns>A task representing the asynchronous operation, with the borrower as the result, or null if not found.</returns>
        Task<Borrower?> GetByUserIdAsync(Guid userId);

        /// <summary>
        /// Retrieves a borrower by their unique identifier asynchronously.
        /// </summary>
        /// <param name="userId">The unique identifier of the borrower.</param>
        /// <returns>A task representing the asynchronous operation, with the borrower as the result, or null if not found.</returns>
        Task<Borrower?> GetByIdAsync(Guid userId);

        /// <summary>
        /// Retrieves all borrowers asynchronously.
        /// </summary>
        /// <returns>A task representing the asynchronous operation, with a list of borrowers as the result, or null if no borrowers exist.</returns>
        Task<List<Borrower>?> GetByAllAsync();

        /// <summary>
        /// Creates a new borrower asynchronously.
        /// </summary>
        /// <param name="borrower">The borrower entity to create.</param>
        /// <returns>A task representing the asynchronous operation, with the created borrower as the result.</returns>
        Task<Borrower> CreateAsync(Borrower borrower);

        /// <summary>
        /// Updates an existing borrower asynchronously.
        /// </summary>
        /// <param name="borrower">The borrower entity to update.</param>
        /// <returns>A task representing the asynchronous operation, with the updated borrower as the result.</returns>
        Task<Borrower> UpdateAsync(Borrower borrower);

        /// <summary>
        /// Deletes a borrower asynchronously.
        /// </summary>
        /// <param name="borrower">The borrower entity to delete.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task DeleteAsync(Borrower borrower);

        /// <summary>
        /// Checks if the borrower has any active loans asynchronously.
        /// </summary>
        /// <param name="borrowerId">The unique identifier of the borrower.</param>
        /// <returns>A task representing the asynchronous operation, with a boolean result indicating if the borrower has active loans.</returns>
        Task<bool> HasActiveLoansAsync(Guid borrowerId);
    }
}
