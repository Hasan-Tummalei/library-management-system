using FluentValidation;
using LibraryManagementSystem.Application.Validator;
using LibraryManagementSystem.Domain.DTOs.User;
using LibraryManagementSystem.Domain.Interfaces.Validation;

namespace LibraryManagementSystem.Infrastructure.Utilities.Validator.User
{
    /// <summary>
    /// Validator for user registration request (UserRegisterReqDto).
    /// This class uses FluentValidation to enforce rules on the properties of the <see cref="UserRegisterReqDto"/>.
    /// </summary>
    public class UserRegisterReqDtoValidator : AbstractValidator<UserRegisterReqDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserRegisterReqDtoValidator"/> class.
        /// Defines validation rules for the <see cref="UserRegisterReqDto"/> object, ensuring that all required fields meet specific criteria.
        /// </summary>
        public UserRegisterReqDtoValidator()
        {
            // Validation rule for Username property
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username is required")
                .Length(UserValidationContracts.Credentials.UsernameMinLength,
                        UserValidationContracts.Credentials.UsernameMaxLength)
                .WithMessage("Username must be between {MinLength} and {MaxLength} characters");

            // Validation rule for Password property
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(UserValidationContracts.Credentials.PasswordMinLength)
                .WithMessage("Password must be at least {MinLength} characters")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter")
                .Matches("[0-9]").WithMessage("Password must contain at least one number");

            // Validation rule for UserRole property (if provided)
            RuleFor(x => x.UserRole)
                .IsInEnum().WithMessage("Invalid user role")
                .When(x => x.UserRole.HasValue);  // This rule is applied only when UserRole is provided
        }
    }
}
