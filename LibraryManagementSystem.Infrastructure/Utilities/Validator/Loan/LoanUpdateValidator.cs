using FluentValidation;
using LibraryManagementSystem.Domain.DTOs.Loan;

namespace LibraryManagementSystem.Infrastructure.Utilities.Validator.Loan
{
    /// <summary>
    /// Validator for updating a loan (LoanUpdateDto).
    /// This class uses FluentValidation to enforce rules on the properties of the <see cref="LoanUpdateDto"/>.
    /// </summary>
    public class LoanUpdateDtoValidator : AbstractValidator<LoanUpdateDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoanUpdateDtoValidator"/> class.
        /// Defines validation rules for the <see cref="LoanUpdateDto"/> object when the return date is provided.
        /// </summary>
        public LoanUpdateDtoValidator()
        {
            // Validation rule for ReturnDate property
            When(x => x.ReturnDate.HasValue, () =>
            {
                RuleFor(x => x.ReturnDate)
                    .GreaterThanOrEqualTo(x => DateOnly.FromDateTime(DateTime.Today))
                    .WithMessage("Return date cannot be in the past");
            });
        }
    }
}
