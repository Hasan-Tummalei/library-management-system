namespace LibraryManagementSystem.Domain.Entities
{
    /// <summary>
    /// Represents an author in the library system.
    /// </summary>
    public class Author : BaseEntity
    {
        /// <summary>
        /// Gets or sets the name of the author.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the biography of the author.
        /// </summary>
        public string Bio { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the collection of books associated with the author.
        /// </summary>
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
