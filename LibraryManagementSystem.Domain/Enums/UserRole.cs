namespace LibraryManagementSystem.Domain.Enums
{
    /// <summary>
    /// Represents the roles a user can have in the library management system.
    /// </summary>
    public enum UserRole
    {
        /// <summary>
        /// Represents a user with managerial privileges.
        /// Managers have access to higher-level functionality and control over system operations.
        /// </summary>
        Manager,

        /// <summary>
        /// Represents a user with standard employee privileges.
        /// Employees have limited access, generally to manage daily operations but without high-level control.
        /// </summary>
        Employee
    }
}
