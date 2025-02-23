namespace LibraryManagementSystem.Application.Validator
{
    /// <summary>
    /// Contains validation constants used for user-related operations such as username, password, and role constraints.
    /// </summary>
    public static class UserValidationContracts
    {
        /// <summary>
        /// Defines constants for username and password validation constraints.
        /// </summary>
        public static class Credentials
        {
            /// <summary>
            /// The minimum allowed length for a username.
            /// </summary>
            public const int UsernameMinLength = 3;

            /// <summary>
            /// The maximum allowed length for a username.
            /// </summary>
            public const int UsernameMaxLength = 50;

            /// <summary>
            /// The minimum allowed length for a password.
            /// </summary>
            public const int PasswordMinLength = 8;
        }

        /// <summary>
        /// Defines constants for user role validation.
        /// </summary>
        public static class Role
        {
            /// <summary>
            /// The role assigned to a manager user.
            /// </summary>
            public const string Manager = "Manager";

            /// <summary>
            /// The role assigned to an employee user.
            /// </summary>
            public const string Employee = "Employee";
        }
    }
}
