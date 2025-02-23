using LibraryManagementSystem.Application.Interfaces.Services;
using LibraryManagementSystem.Domain.DTOs.User;
using LibraryManagementSystem.Presentation.FIlters;

namespace LibraryManagementSystem.Presentation.Endpoints
{
    /// <summary>
    /// Defines the API endpoints for managing users in the library management system.
    /// </summary>
    public static class UserEndpoint
    {
        /// <summary>
        /// Maps the user-related API endpoints to the provided web application.
        /// </summary>
        /// <param name="app">The web application to which the endpoints will be mapped.</param>
        public static void MapUserEndpoint(this WebApplication app)
        {
            var group = app.MapGroup("/users").WithTags("Users");

            // Map POST request for user registration
            group.MapPost("/register", async (IUserService userService, UserRegisterReqDto userRegisterReqDTo) =>
            {
                // Register the user and return the created response with the user's username
                var response = await userService.RegisterUser(userRegisterReqDTo);
                return Results.Created($"/users/{response.Username}", response);
            })
            .AddEndpointFilter<ValidationFilter<UserRegisterReqDto>>();  // Validation filter for input validation

            // Map POST request for user login
            group.MapPost("/login", async (IUserService userService, UserLoginReqDto userLoginReqDto) =>
            {
                // Login the user and return the response (e.g., JWT token)
                var response = await userService.login(userLoginReqDto);
                return Results.Ok(response);
            })
            .AddEndpointFilter<ValidationFilter<UserLoginReqDto>>();  // Validation filter for input validation

            // Map PUT request to update a user's details by ID
            group.MapPut("/update/{id}", async (IUserService userService, Guid id, UserUpdateReqDto userUpdateReqDto, HttpContext context) =>
            {
                // Update the user's details and return the updated response
                var response = await userService.UpdateUser(id, userUpdateReqDto);
                return Results.Ok(response);
            })
            .AddEndpointFilter<ValidationFilter<UserUpdateReqDto>>()  // Validation filter for input validation
            .RequireCustomAuthorization("Manager");  // Authorization for "Manager" role

            // Map GET request to retrieve a user by ID
            group.MapGet("/{id}", async (IUserService userService, Guid id, HttpContext context) =>
            {
                // Retrieve the user's details by ID and return the response
                var response = await userService.GetUserById(id);
                return Results.Ok(response);
            })
            .RequireCustomAuthorization("Manager");  // Authorization for "Manager" role
        }
    }
}
