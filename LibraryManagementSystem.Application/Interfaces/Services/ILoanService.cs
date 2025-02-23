using LibraryManagementSystem.Domain.DTOs.Loan;

namespace LibraryManagementSystem.Application.Interfaces.Services
{
    /// <summary>
    /// Represents the service interface for managing <see cref="Loan"/> entities.
    /// </summary>
    public interface ILoanService
    {
        /// <summary>
        /// Creates a new loan asynchronously.
        /// </summary>
        /// <param name="dto">The data transfer object containing the loan creation details.</param>
        /// <returns>A task representing the asynchronous operation, with the created loan as the result.</returns>
        Task<LoanResponseDto> CreateLoan(LoanCreateDto dto);

        /// <summary>
        /// Retrieves all loans asynchronously.
        /// </summary>
        /// <returns>A task representing the asynchronous operation, with a list of loans as the result.</returns>
        Task<List<LoanResponseDto>> GetAllLoans();

        /// <summary>
        /// Updates an existing loan asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the loan to update.</param>
        /// <param name="updateLoanDto">The data transfer object containing the updated loan details.</param>
        /// <returns>A task representing the asynchronous operation, with the updated loan as the result.</returns>
        Task<LoanResponseDto> UpdateLoan(Guid id, LoanUpdateDto updateLoanDto);
    }
}
