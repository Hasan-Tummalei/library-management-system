using LibraryManagementSystem.Application.Exceptions;
using LibraryManagementSystem.Application.Interfaces.Repositories;
using LibraryManagementSystem.Application.Interfaces.Services;
using LibraryManagementSystem.Application.Interfaces.Utilities;
using LibraryManagementSystem.Domain.DTOs.Loan;
using LibraryManagementSystem.Domain.Entities;

namespace LibraryManagementSystem.Application.Services
{
    /// <summary>
    /// Represents the service for managing <see cref="Loan"/> entities.
    /// </summary>
    public class LoanService : ILoanService
    {
        private readonly ILoanRepository _loanRepo;
        private readonly IBookRepository _bookRepo;
        private readonly IBorrowerRepository _borrowerRepo;
        private readonly IMapperUtil _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoanService"/> class.
        /// </summary>
        /// <param name="loanRepo">The repository interface for interacting with loans.</param>
        /// <param name="bookRepo">The repository interface for interacting with books.</param>
        /// <param name="borrowerRepo">The repository interface for interacting with borrowers.</param>
        /// <param name="mapper">The utility interface for object mapping.</param>
        public LoanService(
            ILoanRepository loanRepo,
            IBookRepository bookRepo,
            IBorrowerRepository borrowerRepo,
            IMapperUtil mapper)
        {
            _loanRepo = loanRepo;
            _bookRepo = bookRepo;
            _borrowerRepo = borrowerRepo;
            _mapper = mapper;
        }

        /// <summary>
        /// Creates a new loan asynchronously.
        /// </summary>
        /// <param name="dto">The data transfer object containing the loan creation details.</param>
        /// <returns>A task representing the asynchronous operation, with the created loan as the result.</returns>
        /// <exception cref="NotFoundException">Thrown if the book or borrower with the given ID is not found.</exception>
        /// <exception cref="ConflictException">Thrown if the book is already loaned for the given period.</exception>
        public async Task<LoanResponseDto> CreateLoan(LoanCreateDto dto)
        {
            var bookFound = await _bookRepo.GetByIdAsync(dto.BookId);
            if (bookFound == null)
                throw new NotFoundException("Book not found", dto.BookId);

            var foundBorrower = await _borrowerRepo.GetByIdAsync(dto.BorrowerId);
            if (foundBorrower == null)
                throw new NotFoundException("Borrower not found", dto.BorrowerId);

            if (!await _loanRepo.IsBookAvailableAsync(dto.BookId, dto.LoanDate))
                throw new ConflictException("Book is already loaned for this period");

            var loan = _mapper.Map<LoanCreateDto, Loan>(dto);
            var createdLoan = await _loanRepo.CreateAsync(loan);
            return _mapper.Map<Loan, LoanResponseDto>(createdLoan);
        }

        /// <summary>
        /// Retrieves all loans asynchronously.
        /// </summary>
        /// <returns>A task representing the asynchronous operation, with a list of loans as the result.</returns>
        public async Task<List<LoanResponseDto>> GetAllLoans()
        {
            var loans = await _loanRepo.GetAllAsync();
            return _mapper.MapList<Loan, LoanResponseDto>(loans);
        }

        /// <summary>
        /// Updates an existing loan asynchronously.
        /// </summary>
        /// <param name="loanId">The unique identifier of the loan to update.</param>
        /// <param name="dto">The data transfer object containing the updated loan details.</param>
        /// <returns>A task representing the asynchronous operation, with the updated loan as the result.</returns>
        /// <exception cref="NotFoundException">Thrown if the loan with the given ID is not found.</exception>
        /// <exception cref="DuplicateException">Thrown if the return date is before the loan date.</exception>
        public async Task<LoanResponseDto> UpdateLoan(Guid loanId, LoanUpdateDto dto)
        {
            var foundLoan = await _loanRepo.GetByIdAsync(loanId);
            if (foundLoan == null)
                throw new NotFoundException("Book not found", loanId);

            if (foundLoan.LoanDate > dto.ReturnDate)
                throw new DuplicateException("Return Date can't be before loan Date", $"{dto.ReturnDate}");

            foundLoan.ReturnDate = dto.ReturnDate;
            var updatedLoan = await _loanRepo.UpdateAsync(foundLoan);
            return _mapper.Map<Loan, LoanResponseDto>(updatedLoan);
        }
    }
}
