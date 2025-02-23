using LibraryManagementSystem.Application.Exceptions;
using LibraryManagementSystem.Application.Interfaces.Repositories;
using LibraryManagementSystem.Application.Interfaces.Services;
using LibraryManagementSystem.Application.Interfaces.Utilities;
using LibraryManagementSystem.Domain.DTOs.Borrower;
using LibraryManagementSystem.Domain.Entities;

namespace LibraryManagementSystem.Application.Services
{
    /// <summary>
    /// Represents the service for managing <see cref="Borrower"/> entities.
    /// </summary>
    public class BorrowerService : IBorrowerService
    {
        private readonly IBorrowerRepository _borrowerRepo;
        private readonly IUserRepository _userRepo;
        private readonly IMapperUtil _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="BorrowerService"/> class.
        /// </summary>
        /// <param name="borrowerRepo">The repository interface for interacting with borrowers.</param>
        /// <param name="userRepo">The repository interface for interacting with users.</param>
        /// <param name="mapper">The utility interface for object mapping.</param>
        public BorrowerService(
            IBorrowerRepository borrowerRepo,
            IUserRepository userRepo,
            IMapperUtil mapper)
        {
            _borrowerRepo = borrowerRepo;
            _userRepo = userRepo;
            _mapper = mapper;
        }

        /// <summary>
        /// Creates a new borrower profile asynchronously.
        /// </summary>
        /// <param name="dto">The data transfer object containing the borrower creation details.</param>
        /// <returns>A task representing the asynchronous operation, with the created borrower as the result.</returns>
        /// <exception cref="NotFoundException">Thrown if the user with the given ID is not found.</exception>
        /// <exception cref="UnauthorizedException">Thrown if the user is not a normal user.</exception>
        /// <exception cref="ConflictException">Thrown if the borrower profile already exists.</exception>
        public async Task<BorrowerResponseDto> CreateBorrower(BorrowerCreateDto dto)
        {
            var user = await _userRepo.GetUserById(dto.UserId);
            if (user == null)
                throw new NotFoundException("User Not Found", dto.UserId);
            if (user.UserRole != null)
                throw new UnauthorizedException("Only normal users can be borrowers");

            if (await _borrowerRepo.GetByUserIdAsync(dto.UserId) != null)
                throw new ConflictException("Borrower profile already exists");

            var borrower = _mapper.Map<BorrowerCreateDto, Borrower>(dto);

            var createdBorrower = await _borrowerRepo.CreateAsync(borrower);
            return _mapper.Map<Borrower, BorrowerResponseDto>(createdBorrower);
        }

        /// <summary>
        /// Retrieves all borrowers asynchronously.
        /// </summary>
        /// <returns>A task representing the asynchronous operation, with a list of borrowers as the result.</returns>
        public async Task<List<BorrowerResponseDto>?> GetAllBorrowers()
        {
            var borrowers = await _borrowerRepo.GetByAllAsync();
            return _mapper.MapList<Borrower, BorrowerResponseDto>(borrowers);
        }

        /// <summary>
        /// Retrieves a borrower by their unique identifier asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the borrower.</param>
        /// <returns>A task representing the asynchronous operation, with the borrower as the result.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the borrower with the given ID is not found.</exception>
        public async Task<BorrowerResponseDto> GetBorrower(Guid id)
        {
            var borrower = await _borrowerRepo.GetByIdAsync(id);
            if (borrower == null)
                throw new InvalidOperationException("Borrower not found.");

            return _mapper.Map<Borrower, BorrowerResponseDto>(borrower);
        }

        /// <summary>
        /// Updates an existing borrower profile asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the borrower to update.</param>
        /// <param name="borrowerUpdateDto">The data transfer object containing the updated borrower details.</param>
        /// <returns>A task representing the asynchronous operation, with the updated borrower as the result.</returns>
        /// <exception cref="KeyNotFoundException">Thrown if the borrower with the given ID is not found.</exception>
        public async Task<BorrowerResponseDto> UpdateBorrower(Guid id, BorrowerUpdateDto borrowerUpdateDto)
        {
            var foundBorrower = await _borrowerRepo.GetByIdAsync(id);

            if (foundBorrower == null) throw new KeyNotFoundException("Borrower not Found");

            if (borrowerUpdateDto.Name != null) foundBorrower.Name = borrowerUpdateDto.Name;
            if (borrowerUpdateDto.Email != null) foundBorrower.Email = borrowerUpdateDto.Email;
            if (borrowerUpdateDto.Phone != null) foundBorrower.Phone = borrowerUpdateDto.Phone;

            await _borrowerRepo.UpdateAsync(foundBorrower);
            return _mapper.Map<Borrower, BorrowerResponseDto>(foundBorrower);
        }

        /// <summary>
        /// Deletes a borrower profile asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the borrower to delete.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="NotFoundException">Thrown if the borrower with the given ID is not found.</exception>
        /// <exception cref="InvalidOperationException">Thrown if the borrower has active loans and cannot be deleted.</exception>
        public async Task DeleteBorrower(Guid id)
        {
            var foundBorrower = await _borrowerRepo.GetByIdAsync(id);

            if (foundBorrower == null)
                throw new NotFoundException("No Borrower found to delete", id);
            if (await _borrowerRepo.HasActiveLoansAsync(id))
                throw new InvalidOperationException("Cannot delete borrower with active loans");

            await _borrowerRepo.DeleteAsync(foundBorrower);
        }
    }
}
