using LibraryManagementSystem.Application.Interfaces.Repositories;
using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Infrastructure.Repositories
{
    /// <summary>
    /// Repository for performing CRUD operations on <see cref="Author"/> entities.
    /// Provides methods for interacting with authors in the library database.
    /// </summary>
    public class AuthorRepository : IAuthorRepository
    {
        private readonly LibraryDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorRepository"/> class with the specified database context.
        /// </summary>
        /// <param name="context">The database context to interact with the database.</param>
        public AuthorRepository(LibraryDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves an <see cref="Author"/> by its ID.
        /// </summary>
        /// <param name="id">The ID of the author to retrieve.</param>
        /// <param name="includeBooks">A flag indicating whether to include associated books in the result.</param>
        /// <returns>The <see cref="Author"/> entity, or null if not found.</returns>
        public async Task<Author?> GetByIdAsync(Guid id, bool includeBooks = true)
        {
            var query = _context.Authors.AsQueryable();
            if (includeBooks)
                query = query.Include(a => a.Books);

            return await query.FirstOrDefaultAsync(a => a.Id == id);
        }

        /// <summary>
        /// Retrieves all <see cref="Author"/> entities.
        /// </summary>
        /// <param name="includeBooks">A flag indicating whether to include associated books in the result.</param>
        /// <returns>A list of <see cref="Author"/> entities.</returns>
        public async Task<List<Author>> GetAllAsync(bool includeBooks = true)
        {
            var query = _context.Authors.AsQueryable();
            if (includeBooks)
                query = query.Include(a => a.Books);
            return await query.ToListAsync();
        }

        /// <summary>
        /// Creates a new <see cref="Author"/> in the database.
        /// </summary>
        /// <param name="author">The <see cref="Author"/> entity to create.</param>
        /// <returns>The created <see cref="Author"/> entity.</returns>
        public async Task<Author> CreateAsync(Author author)
        {
            await _context.Authors.AddAsync(author);
            await _context.SaveChangesAsync();
            return author;
        }

        /// <summary>
        /// Updates an existing <see cref="Author"/> in the database.
        /// </summary>
        /// <param name="author">The <see cref="Author"/> entity to update.</param>
        /// <returns>The updated <see cref="Author"/> entity.</returns>
        public async Task<Author> UpdateAsync(Author author)
        {
            _context.Authors.Update(author);
            await _context.SaveChangesAsync();
            return author;
        }

        /// <summary>
        /// Deletes an <see cref="Author"/> by its ID.
        /// </summary>
        /// <param name="id">The ID of the <see cref="Author"/> entity to delete.</param>
        /// <returns>A task representing the asynchronous delete operation.</returns>
        public async Task DeleteAsync(Guid id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author != null)
            {
                _context.Authors.Remove(author);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Checks whether the author has any associated books.
        /// </summary>
        /// <param name="authorId">The ID of the author to check.</param>
        /// <returns>True if the author has any books; otherwise, false.</returns>
        public async Task<bool> HasBooksAsync(Guid authorId)
        {
            return await _context.Books.AnyAsync(b => b.Authors.Any(a => a.Id == authorId));
        }

        /// <summary>
        /// Retrieves a list of authors by their IDs.
        /// </summary>
        /// <param name="authorIds">A list of author IDs to retrieve.</param>
        /// <returns>A list of <see cref="Author"/> entities matching the specified IDs.</returns>
        public async Task<List<Author>> GetByIdsAsync(List<Guid> authorIds)
        {
            return await _context.Authors
                .Where(a => authorIds.Contains(a.Id))
                .ToListAsync();
        }
    }
}
