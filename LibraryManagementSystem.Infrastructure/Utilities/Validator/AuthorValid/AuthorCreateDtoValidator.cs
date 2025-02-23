using FluentValidation;
using LibraryManagementSystem.Domain.DTOs.Author;

namespace LibraryManagementSystem.Infrastructure.Utilities.Validator.Author
{
    /// <summary>
    /// Validator for creating an author (AuthorCreateDto).
    /// This class uses FluentValidation to enforce rules on the properties of the <see cref="AuthorCreateDto"/>.
    /// </summary>
    public class AuthorCreateDtoValidator : AbstractValidator<AuthorCreateDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorCreateDtoValidator"/> class.
        /// Defines validation rules for the <see cref="AuthorCreateDto"/> object.
        /// </summary>
        public AuthorCreateDtoValidator()
        {
            // Validation rule for Name property
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters");

            // Validation rule for Bio property
            RuleFor(x => x.Bio)
                .MaximumLength(1000).WithMessage("Bio cannot exceed 1000 characters");
        }
    }
}
