using FluentValidation;
using LibraryManagementSystem.Domain.DTOs.Book;

namespace LibraryManagementSystem.Infrastructure.Utilities.Validator.Book
{
    /// <summary>
    /// Validator for creating a new book (BookCreateDto).
    /// This class uses FluentValidation to enforce rules on the properties of the <see cref="BookCreateDto"/>.
    /// </summary>
    public class BookCreateDtoValidator : AbstractValidator<BookCreateDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BookCreateDtoValidator"/> class.
        /// Defines validation rules for the <see cref="BookCreateDto"/> object.
        /// </summary>
        public BookCreateDtoValidator()
        {
            // Validation rule for Title property
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required")
                .MaximumLength(200).WithMessage("Title cannot exceed 200 characters");

            // Validation rule for ISBN property
            RuleFor(x => x.ISBN)
                .NotEmpty().WithMessage("ISBN is required")
                .Length(13).WithMessage("ISBN must be exactly 13 characters")
                .Matches(@"^\d+$").WithMessage("ISBN must contain only digits");

            // Validation rule for AuthorIds property
            RuleFor(x => x.AuthorIds)
                .NotEmpty().WithMessage("At least one author is required")
                .Must(ids => ids.Distinct().Count() == ids.Count)
                .WithMessage("Duplicate author IDs are not allowed");
        }
    }
}
