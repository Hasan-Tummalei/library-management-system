using Microsoft.EntityFrameworkCore;
using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Application.Interfaces.Repositories;
using LibraryManagementSystem.Infrastructure.Persistance;

namespace LibraryManagementSystem.Infrastructure.Repositories
{
    /// <summary>
    /// Repository for performing CRUD operations on <see cref="Book"/> entities.
    /// Provides methods for interacting with books in the library database.
    /// </summary>
    public class BookRepository : IBookRepository
    {
        private readonly LibraryDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="BookRepository"/> class with the specified database context.
        /// </summary>
        /// <param name="context">The database context to interact with the database.</param>
        public BookRepository(LibraryDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves a <see cref="Book"/> by its ID.
        /// </summary>
        /// <param name="id">The ID of the book to retrieve.</param>
        /// <param name="includeAuthors">A flag indicating whether to include associated authors in the result.</param>
        /// <returns>The <see cref="Book"/> entity, or null if not found.</returns>
        public async Task<Book?> GetByIdAsync(Guid id, bool includeAuthors = true)
        {
            var query = _context.Books.AsQueryable();

            if (includeAuthors)
            {
                query = query.Include(b => b.Authors);
            }

            return await query.FirstOrDefaultAsync(b => b.Id == id);
        }

        /// <summary>
        /// Retrieves all <see cref="Book"/> entities.
        /// </summary>
        /// <param name="includeAuthors">A flag indicating whether to include associated authors in the result.</param>
        /// <returns>A list of <see cref="Book"/> entities.</returns>
        public async Task<List<Book>> GetAllAsync(bool includeAuthors = true)
        {
            var query = _context.Books.AsQueryable();

            if (includeAuthors)
            {
                query = query.Include(b => b.Authors);
            }

            return await query.ToListAsync();
        }

        /// <summary>
        /// Creates a new <see cref="Book"/> in the database.
        /// </summary>
        /// <param name="book">The <see cref="Book"/> entity to create.</param>
        /// <returns>The created <see cref="Book"/> entity.</returns>
        public async Task<Book> CreateAsync(Book book)
        {
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
            return book;
        }

        /// <summary>
        /// Updates an existing <see cref="Book"/> in the database.
        /// </summary>
        /// <param name="book">The <see cref="Book"/> entity to update.</param>
        /// <returns>The updated <see cref="Book"/> entity.</returns>
        /// <exception cref="KeyNotFoundException">Thrown when the book does not exist in the database.</exception>
        public async Task<Book> UpdateAsync(Book book)
        {
            var existingBook = await GetByIdAsync(book.Id);

            if (existingBook == null)
                throw new KeyNotFoundException("Book not found");

            _context.Books.Update(existingBook);
            await _context.SaveChangesAsync();
            return existingBook;
        }

        /// <summary>
        /// Deletes a <see cref="Book"/> entity from the database.
        /// </summary>
        /// <param name="book">The <see cref="Book"/> entity to delete.</param>
        public async Task DeleteAsync(Book book)
        {
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }
    }
}
