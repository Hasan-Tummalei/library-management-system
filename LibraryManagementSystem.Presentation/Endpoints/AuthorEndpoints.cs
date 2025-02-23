using LibraryManagementSystem.Application.Interfaces.Services;
using LibraryManagementSystem.Domain.DTOs.Author;
using LibraryManagementSystem.Presentation.FIlters;

namespace LibraryManagementSystem.Presentation.Endpoints
{
    /// <summary>
    /// Defines the API endpoints for managing authors in the library management system.
    /// </summary>
    public static class AuthorEndpoints
    {
        /// <summary>
        /// Maps the author-related API endpoints to the provided web application.
        /// </summary>
        /// <param name="app">The web application to which the endpoints will be mapped.</param>
        public static void MapAuthorEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/authors")
                .WithTags("Authors");

            // Map GET request to retrieve all authors
            group.MapGet("/", async (IAuthorService service) =>
                Results.Ok(await service.GetAllAuthors()));

            // Map GET request to retrieve a specific author by ID
            group.MapGet("/{id}", async (IAuthorService service, Guid id) =>
                Results.Ok(await service.GetAuthor(id)));

            // Map POST request to create a new author
            group.MapPost("/", async (IAuthorService service, AuthorCreateDto dto) =>
            {
                return Results.Ok(await service.CreateAuthor(dto));
            })
            .AddEndpointFilter<ValidationFilter<AuthorCreateDto>>()  // Validation filter for input validation
            .RequireCustomAuthorization("Manager");  // Custom authorization for "Manager" role

            // Map PUT request to update an existing author by ID
            group.MapPut("/{id}", async (IAuthorService service, Guid id, AuthorUpdateDto dto) =>
            {
                return Results.Ok(await service.UpdateAuthor(id, dto));
            })
            .AddEndpointFilter<ValidationFilter<AuthorUpdateDto>>()  // Validation filter for input validation
            .RequireCustomAuthorization("Manager");  // Custom authorization for "Manager" role

            // Map DELETE request to delete an author by ID
            group.MapDelete("/{id}", async (IAuthorService service, Guid id) =>
            {
                await service.DeleteAuthor(id);
                return Results.NoContent();  // Return 204 No Content on successful deletion
            })
            .RequireCustomAuthorization("Manager");  // Custom authorization for "Manager" role
        }
    }
}
