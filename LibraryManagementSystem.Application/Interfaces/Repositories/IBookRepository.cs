using LibraryManagementSystem.Domain.Entities;

namespace LibraryManagementSystem.Application.Interfaces.Repositories
{
    /// <summary>
    /// Represents the repository interface for managing <see cref="Book"/> entities.
    /// </summary>
    public interface IBookRepository
    {
        /// <summary>
        /// Retrieves a book by its unique identifier asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the book.</param>
        /// <param name="includeAuthors">Indicates whether to include the related authors in the result. Default is true.</param>
        /// <returns>A task representing the asynchronous operation, with the book as the result.</returns>
        Task<Book?> GetByIdAsync(Guid id, bool includeAuthors = true);

        /// <summary>
        /// Retrieves all books asynchronously.
        /// </summary>
        /// <param name="includeAuthors">Indicates whether to include the related authors for each book. Default is true.</param>
        /// <returns>A task representing the asynchronous operation, with a list of books as the result.</returns>
        Task<List<Book>> GetAllAsync(bool includeAuthors = true);

        /// <summary>
        /// Creates a new book asynchronously.
        /// </summary>
        /// <param name="book">The book entity to create.</param>
        /// <returns>A task representing the asynchronous operation, with the created book as the result.</returns>
        Task<Book> CreateAsync(Book book);

        /// <summary>
        /// Updates an existing book asynchronously.
        /// </summary>
        /// <param name="book">The book entity to update.</param>
        /// <returns>A task representing the asynchronous operation, with the updated book as the result.</returns>
        Task<Book> UpdateAsync(Book book);

        /// <summary>
        /// Deletes a book asynchronously.
        /// </summary>
        /// <param name="book">The book entity to delete.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task DeleteAsync(Book book);
    }
}
