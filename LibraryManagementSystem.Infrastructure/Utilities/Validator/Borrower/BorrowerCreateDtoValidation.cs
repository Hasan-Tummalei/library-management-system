using FluentValidation;
using LibraryManagementSystem.Domain.DTOs.Borrower;

namespace LibraryManagementSystem.Infrastructure.Utilities.Validator.Borrower
{
    /// <summary>
    /// Validator for creating a new borrower (BorrowerCreateDto).
    /// This class uses FluentValidation to enforce rules on the properties of the <see cref="BorrowerCreateDto"/>.
    /// </summary>
    public class BorrowerCreateDtoValidator : AbstractValidator<BorrowerCreateDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BorrowerCreateDtoValidator"/> class.
        /// Defines validation rules for the <see cref="BorrowerCreateDto"/> object.
        /// </summary>
        public BorrowerCreateDtoValidator()
        {
            // Validation rule for Name property
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters");

            // Validation rule for Email property
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format")
                .MaximumLength(100).WithMessage("Email cannot exceed 100 characters");

            // Validation rule for Phone property
            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Phone number is required")
                .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Invalid phone number format")
                .MaximumLength(15).WithMessage("Phone number cannot exceed 15 characters");
        }
    }
}
