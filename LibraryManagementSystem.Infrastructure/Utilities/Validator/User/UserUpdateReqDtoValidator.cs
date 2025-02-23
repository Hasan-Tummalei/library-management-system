using FluentValidation;
using LibraryManagementSystem.Application.Validator;
using LibraryManagementSystem.Domain.DTOs.User;

namespace LibraryManagementSystem.Infrastructure.Utilities.Validator.User
{
    /// <summary>
    /// Validator for user update request (UserUpdateReqDto).
    /// This class uses FluentValidation to enforce rules on the properties of the <see cref="UserUpdateReqDto"/>.
    /// </summary>
    public class UserUpdateReqDtoValidator : AbstractValidator<UserUpdateReqDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserUpdateReqDtoValidator"/> class.
        /// Defines validation rules for the <see cref="UserUpdateReqDto"/> object, ensuring that the updated user information meets specific criteria.
        /// </summary>
        public UserUpdateReqDtoValidator()
        {
            // Validation rule for UserName property (applied if UserName is not null or empty)
            RuleFor(x => x.UserName)
                .Length(UserValidationContracts.Credentials.UsernameMinLength,
                        UserValidationContracts.Credentials.UsernameMaxLength)
                .WithMessage("Username must be between {MinLength} and {MaxLength} characters")
                .When(x => !string.IsNullOrEmpty(x.UserName));  // Apply rule only if UserName is not null or empty

            // Validation rule for Password property (applied if Password is not null or empty)
            RuleFor(x => x.Password)
                .MinimumLength(UserValidationContracts.Credentials.PasswordMinLength)
                .WithMessage("Password must be at least {MinLength} characters")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter")
                .Matches("[0-9]").WithMessage("Password must contain at least one number")
                .When(x => !string.IsNullOrEmpty(x.Password));  // Apply rule only if Password is not null or empty

            // Validation rule for Role property (applied if Role is provided)
            RuleFor(x => x.Role)
                .IsInEnum().WithMessage("Invalid user role")
                .When(x => x.Role.HasValue);  // Apply rule only if Role is provided
        }
    }
}
