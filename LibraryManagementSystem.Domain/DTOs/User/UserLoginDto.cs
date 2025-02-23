namespace LibraryManagementSystem.Domain.DTOs.User
{
    /// <summary>
    /// Data Transfer Object (DTO) for handling user login request.
    /// </summary>
    public class UserLoginReqDto
    {
        /// <summary>
        /// The username of the user attempting to log in.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// The password of the user attempting to log in.
        /// </summary>
        public string Password { get; set; }
    }
}
