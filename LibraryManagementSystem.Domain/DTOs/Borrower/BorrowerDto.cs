namespace LibraryManagementSystem.Domain.DTOs.Borrower
{
    /// <summary>
    /// Data Transfer Object (DTO) for creating a new borrower.
    /// </summary>
    public record BorrowerCreateDto(
        /// <summary>
        /// The unique identifier of the user associated with the borrower.
        /// </summary>
        Guid UserId,

        /// <summary>
        /// The name of the borrower.
        /// </summary>
        string Name,

        /// <summary>
        /// The email address of the borrower.
        /// </summary>
        string Email,

        /// <summary>
        /// The phone number of the borrower.
        /// </summary>
        string Phone
    );

    /// <summary>
    /// Data Transfer Object (DTO) for updating an existing borrower's information.
    /// </summary>
    public record BorrowerUpdateDto(
        /// <summary>
        /// The updated name of the borrower (optional).
        /// </summary>
        string? Name,

        /// <summary>
        /// The updated email address of the borrower (optional).
        /// </summary>
        string? Email,

        /// <summary>
        /// The updated phone number of the borrower (optional).
        /// </summary>
        string? Phone
    );

    /// <summary>
    /// Data Transfer Object (DTO) representing the details of a borrower.
    /// </summary>
    public record BorrowerResponseDto(
        /// <summary>
        /// The unique identifier of the borrower.
        /// </summary>
        Guid Id,

        /// <summary>
        /// The unique identifier of the user associated with the borrower.
        /// </summary>
        Guid UserId,

        /// <summary>
        /// The name of the borrower.
        /// </summary>
        string Name,

        /// <summary>
        /// The email address of the borrower.
        /// </summary>
        string Email,

        /// <summary>
        /// The phone number of the borrower.
        /// </summary>
        string Phone,

        /// <summary>
        /// The timestamp when the borrower profile was created.
        /// </summary>
        DateTime CreatedAt,

        /// <summary>
        /// The timestamp when the borrower's information was last updated (optional).
        /// </summary>
        DateTime? UpdatedAt
    );
}
