using LibraryManagementSystem.Domain.DTOs.Author;

namespace LibraryManagementSystem.Application.Interfaces.Services
{
    /// <summary>
    /// Represents the service interface for managing <see cref="Author"/> entities.
    /// </summary>
    public interface IAuthorService
    {
        /// <summary>
        /// Creates a new author asynchronously.
        /// </summary>
        /// <param name="dto">The data transfer object containing the author creation details.</param>
        /// <returns>A task representing the asynchronous operation, with the created author as the result.</returns>
        Task<AuthorResponseDto> CreateAuthor(AuthorCreateDto dto);

        /// <summary>
        /// Updates an existing author asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the author to update.</param>
        /// <param name="dto">The data transfer object containing the updated author details.</param>
        /// <returns>A task representing the asynchronous operation, with the updated author as the result.</returns>
        Task<AuthorResponseDto> UpdateAuthor(Guid id, AuthorUpdateDto dto);

        /// <summary>
        /// Deletes an author by their unique identifier asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the author to delete.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task DeleteAuthor(Guid id);

        /// <summary>
        /// Retrieves an author by their unique identifier asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the author.</param>
        /// <returns>A task representing the asynchronous operation, with the author as the result.</returns>
        Task<AuthorResponseDto> GetAuthor(Guid id);

        /// <summary>
        /// Retrieves all authors asynchronously.
        /// </summary>
        /// <returns>A task representing the asynchronous operation, with a list of authors as the result.</returns>
        Task<List<AuthorResponseDto>> GetAllAuthors();
    }
}
