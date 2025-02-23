using LibraryManagementSystem.Domain.Enums;

namespace LibraryManagementSystem.Domain.DTOs.User
{
    /// <summary>
    /// Data Transfer Object (DTO) for handling user registration request.
    /// </summary>
    public class UserRegisterReqDto
    {
        /// <summary>
        /// The password for the new user to be registered.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// The username for the new user to be registered.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// The role to be assigned to the new user (e.g., Manager, Employee).
        /// If null, the default role will be assigned.
        /// </summary>
        public UserRole? UserRole { get; set; }
    }

    /// <summary>
    /// Data Transfer Object (DTO) for the response after a user registration request.
    /// </summary>
    public class UserRegisterResDto
    {
        /// <summary>
        /// The username of the newly registered user.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// The role of the newly registered user.
        /// </summary>
        public UserRole? UserRole { get; set; }
    }
}
