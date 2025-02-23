using LibraryManagementSystem.Domain.Enums;

namespace LibraryManagementSystem.Domain.DTOs.User
{
    /// <summary>
    /// Data Transfer Object (DTO) for returning user information in response (usually for API responses).
    /// </summary>
    public class UserGetResDto
    {
        /// <summary>
        /// The unique identifier for the user.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The username of the user.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// The role assigned to the user (e.g., Manager, Employee, etc.).
        /// </summary>
        public UserRole UserRole { get; set; }
    }
}
