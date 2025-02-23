using FluentValidation;
using LibraryManagementSystem.Domain.DTOs.Loan;

namespace LibraryManagementSystem.Infrastructure.Utilities.Validator.Loan
{
    /// <summary>
    /// Validator for creating a new loan (LoanCreateDto).
    /// This class uses FluentValidation to enforce rules on the properties of the <see cref="LoanCreateDto"/>.
    /// </summary>
    public class LoanCreateDtoValidator : AbstractValidator<LoanCreateDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoanCreateDtoValidator"/> class.
        /// Defines validation rules for the <see cref="LoanCreateDto"/> object.
        /// </summary>
        public LoanCreateDtoValidator()
        {
            // Validation rule for BookId property
            RuleFor(x => x.BookId)
                .NotEmpty().WithMessage("Book ID is required");

            // Validation rule for BorrowerId property
            RuleFor(x => x.BorrowerId)
                .NotEmpty().WithMessage("Borrower ID is required");

            // Validation rule for LoanDate property
            RuleFor(x => x.LoanDate)
                .NotEmpty().WithMessage("Loan date is required")
                .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today))
                .WithMessage("Loan date cannot be in the future");

            // Validation rule for ReturnDate property
            RuleFor(x => x.ReturnDate)
                .NotEmpty().WithMessage("Return date is required")
                .GreaterThan(x => x.LoanDate)
                .WithMessage("Return date must be after loan date");
        }
    }
}
