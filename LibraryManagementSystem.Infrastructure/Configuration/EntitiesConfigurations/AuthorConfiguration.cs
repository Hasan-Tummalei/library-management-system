using LibraryManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Infrastructure.Configuration.EntitiesConfigurations
{
    /// <summary>
    /// Configures the <see cref="Author"/> entity for the DbContext.
    /// </summary>
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        /// <summary>
        /// Configures the entity properties and relationships for the <see cref="Author"/> entity.
        /// </summary>
        /// <param name="authorBuilder">The builder used to configure the entity.</param>
        public void Configure(EntityTypeBuilder<Author> authorBuilder)
        {
            // Configure the primary key for the Author entity
            authorBuilder.HasKey(a => a.Id);

            // Configure the Name property: Required and max length of 100
            authorBuilder.Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(100);

            // Configure the Bio property: Max length of 1000
            authorBuilder.Property(a => a.Bio)
                .HasMaxLength(1000);

            // Configure the UpdatedAt property to have a default value of the current timestamp
            authorBuilder.Property(x => x.UpdatedAt)
                .HasDefaultValueSql("NOW()");
        }
    }
}
