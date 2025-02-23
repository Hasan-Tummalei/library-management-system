using LibraryManagementSystem.Domain.DTOs.Book;

namespace LibraryManagementSystem.Application.Interfaces.Services
{
    /// <summary>
    /// Represents the service interface for managing <see cref="Book"/> entities.
    /// </summary>
    public interface IBookService
    {
        /// <summary>
        /// Creates a new book asynchronously.
        /// </summary>
        /// <param name="dto">The data transfer object containing the book creation details.</param>
        /// <returns>A task representing the asynchronous operation, with the created book as the result.</returns>
        Task<BookResponseDto> CreateBook(BookCreateDto dto);

        /// <summary>
        /// Updates an existing book asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the book to update.</param>
        /// <param name="dto">The data transfer object containing the updated book details.</param>
        /// <returns>A task representing the asynchronous operation, with the updated book as the result.</returns>
        Task<BookResponseDto> UpdateBook(Guid id, BookUpdateDto dto);

        /// <summary>
        /// Deletes a book by its unique identifier asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the book to delete.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task DeleteBook(Guid id);

        /// <summary>
        /// Retrieves a book by its unique identifier asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the book.</param>
        /// <returns>A task representing the asynchronous operation, with the book as the result.</returns>
        Task<BookResponseDto> GetBook(Guid id);

        /// <summary>
        /// Retrieves all books asynchronously.
        /// </summary>
        /// <returns>A task representing the asynchronous operation, with a list of books as the result.</returns>
        Task<List<BookResponseDto>> GetAllBooks();
    }
}
