using LibraryManagementSystem.Application.Interfaces.Services;
using LibraryManagementSystem.Domain.DTOs.Book;
using LibraryManagementSystem.Presentation.FIlters;

namespace LibraryManagementSystem.Presentation.Endpoints
{
    /// <summary>
    /// Defines the API endpoints for managing books in the library management system.
    /// </summary>
    public static class BookEndpoints
    {
        /// <summary>
        /// Maps the book-related API endpoints to the provided web application.
        /// </summary>
        /// <param name="app">The web application to which the endpoints will be mapped.</param>
        public static void MapBookEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/books")
                .WithTags("Books");

            // Map GET request to retrieve all books
            group.MapGet("/", async (IBookService service) =>
                Results.Ok(await service.GetAllBooks()));

            // Map GET request to retrieve a specific book by ID
            group.MapGet("/{id}", async (IBookService service, Guid id) =>
                Results.Ok(await service.GetBook(id)));

            // Map POST request to create a new book
            group.MapPost("/", async (IBookService service, BookCreateDto dto, HttpContext context) =>
            {
                await service.CreateBook(dto);
                return Results.Created();  // Return a 201 Created response
            })
            .AddEndpointFilter<ValidationFilter<BookCreateDto>>()  // Validation filter for input validation
            .RequireCustomAuthorization("Manager");  // Custom authorization for "Manager" role

            // Map PUT request to update an existing book by ID
            group.MapPut("/{id}", async (IBookService service, Guid id, BookUpdateDto dto, HttpContext context) =>
            {
                var userId = context.User.FindFirst("uid")?.Value;
                return Results.Ok(await service.UpdateBook(id, dto));  // Return updated book information
            })
            .AddEndpointFilter<ValidationFilter<BookUpdateDto>>()  // Validation filter for input validation
            .RequireCustomAuthorization("Manager");  // Custom authorization for "Manager" role

            // Map DELETE request to delete a book by ID
            group.MapDelete("/{id}", async (IBookService service, Guid id) =>
            {
                await service.DeleteBook(id);
                return Results.NoContent();  // Return 204 No Content on successful deletion
            })
            .RequireCustomAuthorization("Manager");  // Custom authorization for "Manager" role
        }
    }
}
