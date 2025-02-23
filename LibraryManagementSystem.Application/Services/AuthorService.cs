using LibraryManagementSystem.Application.Exceptions;
using LibraryManagementSystem.Application.Interfaces.Repositories;
using LibraryManagementSystem.Application.Interfaces.Services;
using LibraryManagementSystem.Application.Interfaces.Utilities;
using LibraryManagementSystem.Domain.DTOs.Author;
using LibraryManagementSystem.Domain.Entities;

namespace LibraryManagementSystem.Application.Services
{
    /// <summary>
    /// Represents the service for managing <see cref="Author"/> entities.
    /// </summary>
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IMapperUtil _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorService"/> class.
        /// </summary>
        /// <param name="authorRepository">The repository interface for interacting with authors.</param>
        /// <param name="bookRepository">The repository interface for interacting with books.</param>
        /// <param name="mapperUtil">The utility interface for object mapping.</param>
        public AuthorService(IAuthorRepository authorRepository, IBookRepository bookRepository, IMapperUtil mapperUtil)
        {
            _authorRepository = authorRepository;
            _bookRepository = bookRepository;
            _mapper = mapperUtil;
        }

        /// <summary>
        /// Creates a new author asynchronously.
        /// </summary>
        /// <param name="dto">The data transfer object containing the author creation details.</param>
        /// <returns>A task representing the asynchronous operation, with the created author as the result.</returns>
        /// <exception cref="ValidationException">Thrown if the creation of the author fails.</exception>
        public async Task<AuthorResponseDto> CreateAuthor(AuthorCreateDto dto)
        {
            var authorToCreate = _mapper.Map<AuthorCreateDto, Author>(dto);
            var createdAuthor = await _authorRepository.CreateAsync(authorToCreate);
            return _mapper.Map<Author, AuthorResponseDto>(createdAuthor);
        }

        /// <summary>
        /// Updates an existing author asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the author to update.</param>
        /// <param name="dto">The data transfer object containing the updated author details.</param>
        /// <returns>A task representing the asynchronous operation, with the updated author as the result.</returns>
        /// <exception cref="NotFoundException">Thrown if the author with the given ID is not found.</exception>
        /// <exception cref="ValidationException">Thrown if the update of the author fails.</exception>
        public async Task<AuthorResponseDto> UpdateAuthor(Guid id, AuthorUpdateDto dto)
        {
            var existingAuthor = await _authorRepository.GetByIdAsync(id);
            if (existingAuthor == null)
                throw new NotFoundException("Author not found", id);
            if (dto.Name != null) existingAuthor.Name = dto.Name;
            if (dto.Bio != null) existingAuthor.Bio = dto.Bio;
            var updatedAuthor = await _authorRepository.UpdateAsync(existingAuthor);
            return _mapper.Map<Author, AuthorResponseDto>(updatedAuthor);
        }

        /// <summary>
        /// Deletes an author asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the author to delete.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="NotFoundException">Thrown if the author with the given ID is not found.</exception>
        /// <exception cref="InvalidOperationException">Thrown if attempting to delete an author with associated books.</exception>
        public async Task DeleteAuthor(Guid id)
        {
            var authorExists = await _authorRepository.GetByIdAsync(id);
            if (authorExists == null) throw new NotFoundException("Author not found", id);

            var hasBooks = await _authorRepository.HasBooksAsync(id);
            if (hasBooks)
                throw new InvalidOperationException("Cannot delete an author with associated books.");

            await _authorRepository.DeleteAsync(id);
        }

        /// <summary>
        /// Retrieves an author by its unique identifier asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the author.</param>
        /// <returns>A task representing the asynchronous operation, with the author as the result.</returns>
        /// <exception cref="NotFoundException">Thrown if the author with the given ID is not found.</exception>
        public async Task<AuthorResponseDto> GetAuthor(Guid id)
        {
            var author = await _authorRepository.GetByIdAsync(id, includeBooks: true);
            if (author == null) throw new NotFoundException("Author Not Found", id);

            return _mapper.Map<Author, AuthorResponseDto>(author);
        }

        /// <summary>
        /// Retrieves all authors asynchronously.
        /// </summary>
        /// <returns>A task representing the asynchronous operation, with a list of authors as the result.</returns>
        public async Task<List<AuthorResponseDto>> GetAllAuthors()
        {
            var authors = await _authorRepository.GetAllAsync();
            return _mapper.MapList<Author, AuthorResponseDto>(authors);
        }
    }
}
