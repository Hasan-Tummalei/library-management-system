using LibraryManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Infrastructure.Persistance
{
    /// <summary>
    /// Represents the database context for the library management system.
    /// Provides access to the database sets of Users, Books, Authors, Borrowers, and Loans.
    /// </summary>
    public class LibraryDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LibraryDbContext"/> class with the specified options.
        /// </summary>
        /// <param name="options">The options to configure the context.</param>
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options) { }

        /// <summary>
        /// Configures the model for the context, applying entity configurations from the current assembly.
        /// </summary>
        /// <param name="modelBuilder">The model builder to apply configurations.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(LibraryDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// Gets or sets the <see cref="DbSet{User}"/> representing users in the library system.
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="DbSet{Book}"/> representing books in the library system.
        /// </summary>
        public DbSet<Book> Books { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="DbSet{Author}"/> representing authors in the library system.
        /// </summary>
        public DbSet<Author> Authors { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="DbSet{Borrower}"/> representing borrowers in the library system.
        /// </summary>
        public DbSet<Borrower> Borrowers { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="DbSet{Loan}"/> representing loans in the library system.
        /// </summary>
        public DbSet<Loan> Loans { get; set; }

        /// <summary>
        /// Saves changes made to the context.
        /// </summary>
        /// <returns>The number of affected entities.</returns>
        public override int SaveChanges()
        {
            UpdateTimestamps();
            return base.SaveChanges();
        }

        /// <summary>
        /// Asynchronously saves changes made to the context.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous save operation. The task result contains the number of affected entities.</returns>
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateTimestamps();
            return await base.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Updates the timestamps for modified entities.
        /// Sets the <see cref="BaseEntity.UpdatedAt"/> property for entities that have been modified.
        /// </summary>
        private void UpdateTimestamps()
        {
            var modifiedEntities = ChangeTracker.Entries<BaseEntity>()
                .Where(e => e.State == EntityState.Modified);

            foreach (var entry in modifiedEntities)
            {
                entry.Entity.UpdatedAt = DateTime.UtcNow;
            }
        }
    }
}
