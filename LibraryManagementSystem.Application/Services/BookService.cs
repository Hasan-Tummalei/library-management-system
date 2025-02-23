using LibraryManagementSystem.Application.Interfaces.Services;
using LibraryManagementSystem.Application.Interfaces.Repositories;
using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.DTOs.Book;
using LibraryManagementSystem.Application.Interfaces.Utilities;
using LibraryManagementSystem.Application.Exceptions;

namespace LibraryManagementSystem.Application.Services
{
    /// <summary>
    /// Represents the service for managing <see cref="Book"/> entities.
    /// </summary>
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapperUtil _mapper;
        private readonly ILoanRepository _loanRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="BookService"/> class.
        /// </summary>
        /// <param name="bookRepository">The repository interface for interacting with books.</param>
        /// <param name="authorRepository">The repository interface for interacting with authors.</param>
        /// <param name="loanRepository">The repository interface for interacting with loans.</param>
        /// <param name="mapper">The utility interface for object mapping.</param>
        public BookService(
            IBookRepository bookRepository,
            IAuthorRepository authorRepository,
            ILoanRepository loanRepository,
            IMapperUtil mapper)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _loanRepository = loanRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Creates a new book asynchronously.
        /// </summary>
        /// <param name="bookDto">The data transfer object containing the book creation details.</param>
        /// <returns>A task representing the asynchronous operation, with the created book as the result.</returns>
        /// <exception cref="NotFoundException">Thrown if any of the authors are invalid.</exception>
        public async Task<BookResponseDto> CreateBook(BookCreateDto bookDto)
        {
            var validAuthors = await ValidateAuthorsExist(bookDto.AuthorIds);

            var toCreateBook = _mapper.Map<BookCreateDto, Book>(bookDto);

            toCreateBook.Authors = validAuthors;
            await _bookRepository.CreateAsync(toCreateBook);

            return _mapper.Map<Book, BookResponseDto>(toCreateBook);
        }

        /// <summary>
        /// Updates an existing book asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the book to update.</param>
        /// <param name="bookDto">The data transfer object containing the updated book details.</param>
        /// <returns>A task representing the asynchronous operation, with the updated book as the result.</returns>
        /// <exception cref="NotFoundException">Thrown if the book with the given ID is not found.</exception>
        public async Task<BookResponseDto> UpdateBook(Guid id, BookUpdateDto bookDto)
        {
            var existingBook = await _bookRepository.GetByIdAsync(id);
            if (existingBook == null)
                throw new NotFoundException("Book not found", id);

            if (bookDto.AuthorIds != null)
            {
                var validAuthors = await ValidateAuthorsExist(bookDto.AuthorIds);
                existingBook.Authors = validAuthors;
            }
            if (bookDto.ISBN != null)
            {
                existingBook.ISBN = bookDto.ISBN;
            }
            if (bookDto.PublishedDate != null)
            {
                existingBook.PublishedDate = (DateOnly)bookDto.PublishedDate;
            }
            if (bookDto.Title != null)
            {
                existingBook.Title = bookDto.Title;
            }

            var updatedBook = await _bookRepository.UpdateAsync(existingBook);
            return _mapper.Map<Book, BookResponseDto>(updatedBook);
        }

        /// <summary>
        /// Deletes a book asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the book to delete.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="NotFoundException">Thrown if the book with the given ID is not found.</exception>
        /// <exception cref="ConflictException">Thrown if the book is still loaned out and cannot be deleted.</exception>
        public async Task DeleteBook(Guid id)
        {
            var foundBook = await _bookRepository.GetByIdAsync(id);
            if (foundBook == null)
                throw new NotFoundException("Book not found", id);

            if (await _loanRepository.IsStillLoaned(id))
                throw new ConflictException("Book is still loaned, you can't delete it");

            await _bookRepository.DeleteAsync(foundBook);
        }

        /// <summary>
        /// Retrieves a book by its unique identifier asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the book.</param>
        /// <returns>A task representing the asynchronous operation, with the book as the result.</returns>
        public async Task<BookResponseDto> GetBook(Guid id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            return _mapper.Map<Book, BookResponseDto>(book);
        }

        /// <summary>
        /// Retrieves all books asynchronously.
        /// </summary>
        /// <returns>A task representing the asynchronous operation, with a list of books as the result.</returns>
        public async Task<List<BookResponseDto>> GetAllBooks()
        {
            var books = await _bookRepository.GetAllAsync();
            return _mapper.MapList<Book, BookResponseDto>(books);
        }

        /// <summary>
        /// Validates if the provided author IDs exist.
        /// </summary>
        /// <param name="authorIds">The list of author IDs to validate.</param>
        /// <returns>A task representing the asynchronous operation, with a list of valid authors as the result.</returns>
        /// <exception cref="NotFoundException">Thrown if any of the author IDs do not exist.</exception>
        private async Task<List<Author>> ValidateAuthorsExist(List<Guid> authorIds)
        {
            var existingAuthors = await _authorRepository.GetByIdsAsync(authorIds);
            if (existingAuthors.Count != authorIds.Count)
            {
                var missingIds = authorIds.Except(existingAuthors.Select(a => a.Id));
                throw new NotFoundException($"Invalid author IDs: {string.Join(", ", missingIds)}", missingIds);
            }
            return existingAuthors;
        }
    }
}
