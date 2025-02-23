using FluentValidation;
using LibraryManagementSystem.Domain.DTOs.User;

namespace LibraryManagementSystem.Infrastructure.Utilities.Validator.User
{
    /// <summary>
    /// Validator for user login request (UserLoginReqDto).
    /// This class uses FluentValidation to enforce rules on the properties of the <see cref="UserLoginReqDto"/>.
    /// </summary>
    public class UserLoginReqDtoValidator : AbstractValidator<UserLoginReqDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserLoginReqDtoValidator"/> class.
        /// Defines validation rules for the <see cref="UserLoginReqDto"/> object, ensuring required fields are provided.
        /// </summary>
        public UserLoginReqDtoValidator()
        {
            // Validation rule for Username property
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username is required");

            // Validation rule for Password property
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required");
        }
    }
}
