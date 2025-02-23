namespace LibraryManagementSystem.Domain.Entities
{
    /// <summary>
    /// Represents a book in the library system.
    /// </summary>
    public class Book : BaseEntity
    {
        /// <summary>
        /// Gets or sets the title of the book.
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the ISBN (International Standard Book Number) of the book.
        /// </summary>
        public string ISBN { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the published date of the book.
        /// </summary>
        public DateOnly PublishedDate { get; set; }

        /// <summary>
        /// Gets or sets the collection of authors who wrote the book.
        /// </summary>
        public ICollection<Author> Authors { get; set; } = new List<Author>();

        /// <summary>
        /// Gets or sets the collection of loans associated with the book.
        /// </summary>
        public ICollection<Loan> Loans { get; set; } = new List<Loan>();
    }
}
