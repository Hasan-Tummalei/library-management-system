using LibraryManagementSystem.Domain.DTOs.Borrower;

namespace LibraryManagementSystem.Application.Interfaces.Services
{
    /// <summary>
    /// Represents the service interface for managing <see cref="Borrower"/> entities.
    /// </summary>
    public interface IBorrowerService
    {
        /// <summary>
        /// Creates a new borrower asynchronously.
        /// </summary>
        /// <param name="dto">The data transfer object containing the borrower creation details.</param>
        /// <returns>A task representing the asynchronous operation, with the created borrower as the result.</returns>
        Task<BorrowerResponseDto> CreateBorrower(BorrowerCreateDto dto);

        /// <summary>
        /// Updates an existing borrower asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the borrower to update.</param>
        /// <param name="dto">The data transfer object containing the updated borrower details.</param>
        /// <returns>A task representing the asynchronous operation, with the updated borrower as the result.</returns>
        Task<BorrowerResponseDto> UpdateBorrower(Guid id, BorrowerUpdateDto dto);

        /// <summary>
        /// Deletes a borrower by their unique identifier asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the borrower to delete.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task DeleteBorrower(Guid id);

        /// <summary>
        /// Retrieves a borrower by their unique identifier asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the borrower.</param>
        /// <returns>A task representing the asynchronous operation, with the borrower as the result.</returns>
        Task<BorrowerResponseDto> GetBorrower(Guid id);

        /// <summary>
        /// Retrieves all borrowers asynchronously.
        /// </summary>
        /// <returns>A task representing the asynchronous operation, with a list of borrowers as the result.</returns>
        Task<List<BorrowerResponseDto>?> GetAllBorrowers();
    }
}
