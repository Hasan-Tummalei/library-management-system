using LibraryManagementSystem.Domain.Enums;

namespace LibraryManagementSystem.Domain.Entities
{
    /// <summary>
    /// Represents a user in the library management system.
    /// A user can have different roles like Manager, Employee, etc.
    /// </summary>
    public class User : BaseEntity
    {
        /// <summary>
        /// Gets or sets the username of the user.
        /// This is a unique identifier for the user.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password of the user.
        /// The password should be securely hashed before storing.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the role of the user in the system.
        /// This defines the level of access and privileges for the user.
        /// </summary>
        public UserRole? UserRole { get; set; }
    }
}
