using LibraryManagementSystem.Domain.Entities;

namespace LibraryManagementSystem.Application.Interfaces.Repositories
{
    /// <summary>
    /// Represents the repository interface for managing <see cref="Author"/> entities.
    /// </summary>
    public interface IAuthorRepository
    {
        /// <summary>
        /// Retrieves an author by their unique identifier asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the author.</param>
        /// <param name="includeBooks">Indicates whether to include the related books in the result. Default is true.</param>
        /// <returns>A task representing the asynchronous operation, with the author as the result.</returns>
        Task<Author?> GetByIdAsync(Guid id, bool includeBooks = true);

        /// <summary>
        /// Retrieves all authors asynchronously.
        /// </summary>
        /// <param name="includeBooks">Indicates whether to include the related books for each author. Default is true.</param>
        /// <returns>A task representing the asynchronous operation, with a list of authors as the result.</returns>
        Task<List<Author>> GetAllAsync(bool includeBooks = true);

        /// <summary>
        /// Creates a new author asynchronously.
        /// </summary>
        /// <param name="author">The author entity to create.</param>
        /// <returns>A task representing the asynchronous operation, with the created author as the result.</returns>
        Task<Author> CreateAsync(Author author);

        /// <summary>
        /// Updates an existing author asynchronously.
        /// </summary>
        /// <param name="author">The author entity to update.</param>
        /// <returns>A task representing the asynchronous operation, with the updated author as the result.</returns>
        Task<Author> UpdateAsync(Author author);

        /// <summary>
        /// Deletes an author by their unique identifier asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the author to delete.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// Checks if the author has any books associated with them asynchronously.
        /// </summary>
        /// <param name="authorId">The unique identifier of the author.</param>
        /// <returns>A task representing the asynchronous operation, with a boolean result indicating if the author has books.</returns>
        Task<bool> HasBooksAsync(Guid authorId);

        /// <summary>
        /// Retrieves authors by their unique identifiers asynchronously.
        /// </summary>
        /// <param name="authorIds">A list of unique author identifiers.</param>
        /// <returns>A task representing the asynchronous operation, with a list of authors as the result.</returns>
        Task<List<Author>> GetByIdsAsync(List<Guid> authorIds);
    }
}
