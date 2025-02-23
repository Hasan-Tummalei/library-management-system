namespace LibraryManagementSystem.Domain.Entities
{
    /// <summary>
    /// Represents a loan transaction for a book in the library system.
    /// </summary>
    public class Loan : BaseEntity
    {
        /// <summary>
        /// Gets or sets the ID of the book being loaned.
        /// </summary>
        public Guid BookId { get; set; }

        /// <summary>
        /// Gets or sets the ID of the borrower who loaned the book.
        /// </summary>
        public Guid BorrowerId { get; set; }

        /// <summary>
        /// Gets or sets the date the book was loaned.
        /// </summary>
        public DateOnly LoanDate { get; set; }

        /// <summary>
        /// Gets or sets the date the book was returned.
        /// Null if the book has not been returned yet.
        /// </summary>
        public DateOnly? ReturnDate { get; set; }

        /// <summary>
        /// Gets or sets the book being loaned.
        /// </summary>
        public Book Book { get; set; } = null!;

        /// <summary>
        /// Gets or sets the borrower who loaned the book.
        /// </summary>
        public Borrower Borrower { get; set; } = null!;
    }
}
