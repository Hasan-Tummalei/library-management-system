using LibraryManagementSystem.Domain.DTOs.User;

namespace LibraryManagementSystem.Application.Interfaces.Services
{
    /// <summary>
    /// Represents the service interface for managing <see cref="User"/> entities.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Registers a new user asynchronously.
        /// </summary>
        /// <param name="userRegisterReqDto">The data transfer object containing the user registration details.</param>
        /// <returns>A task representing the asynchronous operation, with the result of the registration process.</returns>
        Task<UserRegisterResDto> RegisterUser(UserRegisterReqDto userRegisterReqDto);

        /// <summary>
        /// Logs in a user asynchronously.
        /// </summary>
        /// <param name="userLoginReqDto">The data transfer object containing the user login details.</param>
        /// <returns>A task representing the asynchronous operation, with the login result (e.g., authentication token or message).</returns>
        Task<string> login(UserLoginReqDto userLoginReqDto);

        /// <summary>
        /// Retrieves a user by their unique identifier asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the user.</param>
        /// <returns>A task representing the asynchronous operation, with the user details as the result, or null if not found.</returns>
        Task<UserGetResDto?> GetUserById(Guid id);

        /// <summary>
        /// Updates an existing user asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the user to update.</param>
        /// <param name="userUpdateReqDto">The data transfer object containing the updated user details.</param>
        /// <returns>A task representing the asynchronous operation, with the updated user details as the result.</returns>
        Task<UserUpdateResDto> UpdateUser(Guid id, UserUpdateReqDto userUpdateReqDto);
    }
}
