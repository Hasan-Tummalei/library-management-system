namespace LibraryManagementSystem.Domain.DTOs.Author
{
    /// <summary>
    /// Data Transfer Object (DTO) for creating a new author.
    /// </summary>
    public record AuthorCreateDto(
        /// <summary>
        /// The name of the author.
        /// </summary>
        string Name,

        /// <summary>
        /// A brief biography of the author.
        /// </summary>
        string Bio
    );

    /// <summary>
    /// Data Transfer Object (DTO) for updating an existing author's information.
    /// </summary>
    public record AuthorUpdateDto(
        /// <summary>
        /// The updated name of the author (optional).
        /// </summary>
        string? Name,

        /// <summary>
        /// The updated biography of the author (optional).
        /// </summary>
        string? Bio
    );

    /// <summary>
    /// Data Transfer Object (DTO) representing the details of an author.
    /// </summary>
    public record AuthorResponseDto(
        /// <summary>
        /// The unique identifier of the author.
        /// </summary>
        Guid Id,

        /// <summary>
        /// The name of the author.
        /// </summary>
        string Name,

        /// <summary>
        /// A brief biography of the author.
        /// </summary>
        string Bio,

        /// <summary>
        /// The timestamp when the author was created.
        /// </summary>
        DateTime CreatedAt,

        /// <summary>
        /// The timestamp when the author's information was last updated (optional).
        /// </summary>
        DateTime? UpdatedAt
    );
}
