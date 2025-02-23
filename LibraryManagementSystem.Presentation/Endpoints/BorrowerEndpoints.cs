using LibraryManagementSystem.Application.Interfaces.Services;
using LibraryManagementSystem.Domain.DTOs.Borrower;
using LibraryManagementSystem.Presentation.FIlters;

namespace LibraryManagementSystem.Presentation.Endpoints
{
    /// <summary>
    /// Defines the API endpoints for managing borrowers in the library management system.
    /// </summary>
    public static class BorrowerEndpoints
    {
        /// <summary>
        /// Maps the borrower-related API endpoints to the provided web application.
        /// </summary>
        /// <param name="app">The web application to which the endpoints will be mapped.</param>
        public static void MapBorrowerEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/borrowers")
                .WithTags("Borrowers");

            // Map POST request to create a new borrower
            group.MapPost("/", async (IBorrowerService service, BorrowerCreateDto dto) =>
            {
                return Results.Ok(await service.CreateBorrower(dto));  // Return the created borrower
            })
            .AddEndpointFilter<ValidationFilter<BorrowerCreateDto>>();  // Validation filter for input validation

            // Map GET request to retrieve all borrowers
            group.MapGet("/", async (IBorrowerService service) =>
                Results.Ok(await service.GetAllBorrowers()))  // Return a list of all borrowers
                .RequireCustomAuthorization("Employee", "Manager");  // Authorization for "Employee" and "Manager" roles

            // Map GET request to retrieve a specific borrower by ID
            group.MapGet("/{id}", async (IBorrowerService service, Guid id) =>
            {
                return Results.Ok(await service.GetBorrower(id));  // Return the borrower with the specified ID
            })
            .RequireCustomAuthorization("Employee", "Manager");  // Authorization for "Employee" and "Manager" roles

            // Map PUT request to update an existing borrower by ID
            group.MapPut("/{id}", async (IBorrowerService service, Guid id, BorrowerUpdateDto dto) =>
            {
                return Results.Ok(await service.UpdateBorrower(id, dto));  // Return the updated borrower information
            })
            .AddEndpointFilter<ValidationFilter<BorrowerUpdateDto>>();  // Validation filter for input validation

            // Map DELETE request to delete a borrower by ID
            group.MapDelete("/{id}", async (IBorrowerService service, Guid id) =>
            {
                await service.DeleteBorrower(id);  // Perform deletion of the borrower
                return Results.NoContent();  // Return 204 No Content to indicate successful deletion
            })
            .RequireCustomAuthorization("Employee", "Manager");  // Authorization for "Employee" and "Manager" roles
        }
    }
}
