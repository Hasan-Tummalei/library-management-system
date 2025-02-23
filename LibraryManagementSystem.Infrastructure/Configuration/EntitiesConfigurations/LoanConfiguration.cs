using LibraryManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementSystem.Infrastructure.Configuration.EntitiesConfigurations
{
    /// <summary>
    /// Configures the Loan entity in the context of the database.
    /// </summary>
    public class LoanConfiguration : IEntityTypeConfiguration<Loan>
    {
        /// <summary>
        /// Configures the Loan entity's properties and relationships in the model.
        /// </summary>
        /// <param name="loanBuilder">The builder used to configure the Loan entity.</param>
        public void Configure(EntityTypeBuilder<Loan> loanBuilder)
        {
            // Define the primary key for the Loan entity
            loanBuilder.HasKey(l => l.Id);

            // Configure the LoanDate property as required
            loanBuilder.Property(l => l.LoanDate)
                .IsRequired();

            // Configure the ReturnDate property as required
            loanBuilder.Property(l => l.ReturnDate)
                .IsRequired();

            // Configure the relationship between Loan and Book
            loanBuilder.HasOne(l => l.Book)
                .WithMany(b => b.Loans)
                .HasForeignKey(l => l.BookId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure the relationship between Loan and Borrower
            loanBuilder.HasOne(l => l.Borrower)
                .WithMany(b => b.Loans)
                .HasForeignKey(l => l.BorrowerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
