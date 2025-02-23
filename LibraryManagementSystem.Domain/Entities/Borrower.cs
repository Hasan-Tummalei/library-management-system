namespace LibraryManagementSystem.Domain.Entities
{
    /// <summary>
    /// Represents a borrower in the library system.
    /// </summary>
    public class Borrower : BaseEntity
    {
        /// <summary>
        /// Gets or sets the ID of the user associated with the borrower.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the user associated with the borrower.
        /// </summary>
        public User User { get; set; } = null!;

        /// <summary>
        /// Gets or sets the name of the borrower.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the email of the borrower.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the phone number of the borrower.
        /// </summary>
        public string Phone { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the collection of loans associated with the borrower.
        /// </summary>
        public List<Loan> Loans { get; set; } = new();
    }
}
