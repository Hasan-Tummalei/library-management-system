namespace LibraryManagementSystem.Domain.DTOs.Loan
{
    /// <summary>
    /// Data Transfer Object (DTO) for creating a new loan.
    /// </summary>
    public record LoanCreateDto(
        /// <summary>
        /// The unique identifier of the book being loaned.
        /// </summary>
        Guid BookId,

        /// <summary>
        /// The unique identifier of the borrower who is loaning the book.
        /// </summary>
        Guid BorrowerId,

        /// <summary>
        /// The date when the loan starts.
        /// </summary>
        DateOnly LoanDate,

        /// <summary>
        /// The date when the loan is expected to be returned.
        /// </summary>
        DateOnly ReturnDate
    );

    /// <summary>
    /// Data Transfer Object (DTO) for updating a loan's information.
    /// </summary>
    public record LoanUpdateDto(
        /// <summary>
        /// The updated return date of the loan (optional).
        /// </summary>
        DateOnly? ReturnDate
    );

    /// <summary>
    /// Data Transfer Object (DTO) representing the details of a loan.
    /// </summary>
    public record LoanResponseDto(
        /// <summary>
        /// The unique identifier of the loan.
        /// </summary>
        Guid Id,

        /// <summary>
        /// The unique identifier of the book being loaned.
        /// </summary>
        Guid BookId,

        /// <summary>
        /// The unique identifier of the borrower who borrowed the book.
        /// </summary>
        Guid BorrowerId,

        /// <summary>
        /// The date when the loan starts.
        /// </summary>
        DateOnly LoanDate,

        /// <summary>
        /// The date when the loan was returned (optional).
        /// </summary>
        DateOnly? ReturnDate,

        /// <summary>
        /// The timestamp when the loan record was created.
        /// </summary>
        DateTime CreatedAt,

        /// <summary>
        /// The timestamp when the loan record was last updated (optional).
        /// </summary>
        DateTime? UpdatedAt
    );
}
