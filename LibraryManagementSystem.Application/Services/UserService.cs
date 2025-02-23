using LibraryManagementSystem.Application.Exceptions;
using LibraryManagementSystem.Application.Interfaces.Repositories;
using LibraryManagementSystem.Application.Interfaces.Services;
using LibraryManagementSystem.Application.Interfaces.Utilities;
using LibraryManagementSystem.Domain.DTOs.User;
using LibraryManagementSystem.Domain.Entities;

namespace LibraryManagementSystem.Application.Services
{
    /// <summary>
    /// Represents the service for managing <see cref="User"/> entities.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapperUtil _mapper;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtService _jwtService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="userRepository">The repository interface for interacting with users.</param>
        /// <param name="mapper">The utility interface for object mapping.</param>
        /// <param name="passwordHasher">The password hashing service.</param>
        /// <param name="jwtService">The JWT token generation service.</param>
        public UserService(IUserRepository userRepository, IMapperUtil mapper, IPasswordHasher passwordHasher, IJwtService jwtService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
        }

        /// <summary>
        /// Retrieves a user by their ID asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the user.</param>
        /// <returns>A task representing the asynchronous operation, with the user details as the result.</returns>
        /// <exception cref="NotFoundException">Thrown if the user with the given ID is not found.</exception>
        public async Task<UserGetResDto?> GetUserById(Guid id)
        {
            var user = await _userRepository.GetUserById(id);
            return user == null ? throw new NotFoundException("User Not found", id) : _mapper.Map<User, UserGetResDto>(user);
        }

        /// <summary>
        /// Logs in a user and generates a JWT token if the credentials are valid.
        /// </summary>
        /// <param name="userLoginReqDto">The data transfer object containing the login request details.</param>
        /// <returns>A task representing the asynchronous operation, with the JWT token as the result.</returns>
        /// <exception cref="NotFoundException">Thrown if the user with the given username is not found.</exception>
        /// <exception cref="KeyNotFoundException">Thrown if the password does not match the stored password.</exception>
        public async Task<string> login(UserLoginReqDto userLoginReqDto)
        {
            var userFound = await _userRepository.Login(userLoginReqDto.Username);
            if (userFound == null) throw new NotFoundException("User not found", userLoginReqDto.Username);
            if (!_passwordHasher.VerifyPassword(userLoginReqDto.Password, userFound.Password)) throw new KeyNotFoundException("Invalid Credentials");
            return _jwtService.GenerateToken(userFound);
        }

        /// <summary>
        /// Registers a new user asynchronously.
        /// </summary>
        /// <param name="userRegisterReqDto">The data transfer object containing the registration details.</param>
        /// <returns>A task representing the asynchronous operation, with the registered user details as the result.</returns>
        /// <exception cref="DuplicateException">Thrown if the username is already in use.</exception>
        public async Task<UserRegisterResDto> RegisterUser(UserRegisterReqDto userRegisterReqDto)
        {
            var userFound = await _userRepository.Login(userRegisterReqDto.Username);
            if (userFound != null) throw new DuplicateException("Username is already in use.", userRegisterReqDto.Username);
            userRegisterReqDto.Password = _passwordHasher.HashPassword(userRegisterReqDto.Password);
            var userToBeCreated = _mapper.Map<UserRegisterReqDto, User>(userRegisterReqDto);
            var createdUser = await _userRepository.RegisterUser(userToBeCreated);
            return _mapper.Map<User, UserRegisterResDto>(createdUser);
        }

        /// <summary>
        /// Updates an existing user's details asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the user to update.</param>
        /// <param name="userUpdateReqDto">The data transfer object containing the updated user details.</param>
        /// <returns>A task representing the asynchronous operation, with the updated user details as the result.</returns>
        /// <exception cref="NotFoundException">Thrown if the user with the given ID is not found.</exception>
        public async Task<UserUpdateResDto> UpdateUser(Guid id, UserUpdateReqDto userUpdateReqDto)
        {
            var foundUser = await _userRepository.GetUserById(id);
            if (foundUser == null) throw new NotFoundException("Username is not found", id);
            if (userUpdateReqDto.Password != null) foundUser.Password = _passwordHasher.HashPassword(userUpdateReqDto.Password);
            if (userUpdateReqDto.Role != null) foundUser.UserRole = userUpdateReqDto.Role;
            if (userUpdateReqDto.UserName != null) foundUser.Username = userUpdateReqDto.UserName;
            await _userRepository.UpdateUser(foundUser);
            return _mapper.Map<User, UserUpdateResDto>(foundUser);
        }
    }
}
