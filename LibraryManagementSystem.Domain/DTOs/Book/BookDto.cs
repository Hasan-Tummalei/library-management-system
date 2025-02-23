namespace LibraryManagementSystem.Domain.DTOs.Book
{
    /// <summary>
    /// Data Transfer Object (DTO) for creating a new book.
    /// </summary>
    public record BookCreateDto(
        /// <summary>
        /// The title of the book.
        /// </summary>
        string Title,

        /// <summary>
        /// The International Standard Book Number (ISBN) of the book.
        /// </summary>
        string ISBN,

        /// <summary>
        /// The publication date of the book.
        /// </summary>
        DateOnly PublishedDate,

        /// <summary>
        /// List of author GUIDs associated with the book.
        /// </summary>
        List<Guid> AuthorIds
    );

    /// <summary>
    /// Data Transfer Object (DTO) for updating an existing book's information.
    /// </summary>
    public record BookUpdateDto(
        /// <summary>
        /// The updated title of the book (optional).
        /// </summary>
        string? Title,

        /// <summary>
        /// The updated ISBN of the book (optional).
        /// </summary>
        string? ISBN,

        /// <summary>
        /// The updated publication date of the book (optional).
        /// </summary>
        DateOnly? PublishedDate,

        /// <summary>
        /// The updated list of author GUIDs associated with the book (optional).
        /// </summary>
        List<Guid>? AuthorIds
    );

    /// <summary>
    /// Data Transfer Object (DTO) representing the details of a book.
    /// </summary>
    public record BookResponseDto(
        /// <summary>
        /// The unique identifier of the book.
        /// </summary>
        Guid Id,

        /// <summary>
        /// The title of the book.
        /// </summary>
        string Title,

        /// <summary>
        /// The ISBN of the book.
        /// </summary>
        string ISBN,

        /// <summary>
        /// The publication date of the book.
        /// </summary>
        DateOnly PublishedDate,

        /// <summary>
        /// The timestamp when the book was created.
        /// </summary>
        DateTime CreatedAt,

        /// <summary>
        /// The timestamp when the book's information was last updated (optional).
        /// </summary>
        DateTime? UpdatedAt
    );
}
