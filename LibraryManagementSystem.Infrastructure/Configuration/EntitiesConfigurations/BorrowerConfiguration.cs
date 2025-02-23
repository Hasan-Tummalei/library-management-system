using LibraryManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Infrastructure.Configuration.EntitiesConfigurations
{
    /// <summary>
    /// Configures the <see cref="Borrower"/> entity for the DbContext.
    /// </summary>
    public class BorrowerConfiguration : IEntityTypeConfiguration<Borrower>
    {
        /// <summary>
        /// Configures the entity properties and relationships for the <see cref="Borrower"/> entity.
        /// </summary>
        /// <param name="borrowerBuilder">The builder used to configure the entity.</param>
        public void Configure(EntityTypeBuilder<Borrower> borrowerBuilder)
        {
            // Configure the primary key for the Borrower entity
            borrowerBuilder.HasKey(b => b.Id);

            // Configure the Name property: Required and max length of 100
            borrowerBuilder.Property(b => b.Name)
                .IsRequired()
                .HasMaxLength(100);

            // Configure the Email property: Required and max length of 100
            borrowerBuilder.Property(b => b.Email)
                .IsRequired()
                .HasMaxLength(100);

            // Configure the Phone property: Required and max length of 15
            borrowerBuilder.Property(b => b.Phone)
                .IsRequired()
                .HasMaxLength(15);

            // Configure the UpdatedAt property to have a default value of the current timestamp
            borrowerBuilder.Property(x => x.UpdatedAt)
                .HasDefaultValueSql("NOW()");

            // Configure the relationship between Borrower and User:
            // Each Borrower is linked to one User with a foreign key on UserId
            // On delete, restrict deletion of the User if they have a Borrower
            borrowerBuilder.HasOne(b => b.User)
                .WithOne()
                .HasForeignKey<Borrower>(b => b.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
