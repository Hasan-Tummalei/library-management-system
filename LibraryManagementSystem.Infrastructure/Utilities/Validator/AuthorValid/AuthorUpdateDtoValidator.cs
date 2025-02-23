using FluentValidation;
using LibraryManagementSystem.Domain.DTOs.Author;

namespace LibraryManagementSystem.Infrastructure.Utilities.Validator.Author
{
    /// <summary>
    /// Validator for updating an author (AuthorUpdateDto).
    /// This class uses FluentValidation to enforce rules on the properties of the <see cref="AuthorUpdateDto"/>.
    /// </summary>
    public class AuthorUpdateDtoValidator : AbstractValidator<AuthorUpdateDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorUpdateDtoValidator"/> class.
        /// Defines validation rules for the <see cref="AuthorUpdateDto"/> object.
        /// </summary>
        public AuthorUpdateDtoValidator()
        {
            // Validation rule for Name property
            When(x => x.Name != null, () =>
            {
                RuleFor(x => x.Name)
                    .NotEmpty().WithMessage("Name cannot be empty")
                    .MaximumLength(100).WithMessage("Name cannot exceed 100 characters");
            });

            // Validation rule for Bio property
            When(x => x.Bio != null, () =>
            {
                RuleFor(x => x.Bio)
                    .MaximumLength(1000).WithMessage("Bio cannot exceed 1000 characters");
            });
        }
    }
}
