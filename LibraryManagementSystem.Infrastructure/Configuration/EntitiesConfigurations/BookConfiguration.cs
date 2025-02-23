using LibraryManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Infrastructure.Configuration.EntitiesConfigurations
{
    /// <summary>
    /// Configures the <see cref="Book"/> entity for the DbContext.
    /// </summary>
    internal class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        /// <summary>
        /// Configures the entity properties and relationships for the <see cref="Book"/> entity.
        /// </summary>
        /// <param name="bookBuilder">The builder used to configure the entity.</param>
        public void Configure(EntityTypeBuilder<Book> bookBuilder)
        {
            // Configure the primary key for the Book entity
            bookBuilder.HasKey(b => b.Id);

            // Configure the Title property: Required and max length of 200
            bookBuilder.Property(b => b.Title)
                .IsRequired()
                .HasMaxLength(200);

            // Configure the ISBN property: Required, max length of 13, and fixed length
            bookBuilder.Property(b => b.ISBN)
                .IsRequired()
                .HasMaxLength(13)
                .IsFixedLength();

            // Configure the UpdatedAt property to have a default value of the current timestamp
            bookBuilder.Property(x => x.UpdatedAt)
                .HasDefaultValueSql("NOW()");

            // Configure the many-to-many relationship between Books and Authors
            bookBuilder.HasMany(b => b.Authors)
                .WithMany(a => a.Books);
        }
    }
}
