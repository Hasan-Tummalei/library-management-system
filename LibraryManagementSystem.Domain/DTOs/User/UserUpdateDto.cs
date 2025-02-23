using LibraryManagementSystem.Domain.Enums;

namespace LibraryManagementSystem.Domain.DTOs.User
{
    /// <summary>
    /// Data Transfer Object (DTO) for handling user update request.
    /// </summary>
    public class UserUpdateReqDto
    {
        /// <summary>
        /// The password to update for the user. This is optional, and only provided if the user wishes to change their password.
        /// </summary>
        public string? Password { get; set; }

        /// <summary>
        /// The username to update for the user. This is optional, and only provided if the user wishes to change their username.
        /// </summary>
        public string? UserName { get; set; }

        /// <summary>
        /// The role to update for the user. This is optional, and only provided if the user wishes to change their role.
        /// </summary>
        public UserRole? Role { get; set; }
    }

    /// <summary>
    /// Data Transfer Object (DTO) for the response after a user update request.
    /// </summary>
    public class UserUpdateResDto
    {
        /// <summary>
        /// The updated password of the user. This is not nullable as it represents the user's current password after update.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// The updated username of the user.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// The updated role of the user (e.g., Manager, Employee).
        /// </summary>
        public UserRole Role { get; set; }
    }
}
