using LibraryManagementSystem.Application.Interfaces.Services;
using LibraryManagementSystem.Domain.DTOs.Loan;
using LibraryManagementSystem.Presentation.FIlters;

namespace LibraryManagementSystem.Presentation.Endpoints
{
    /// <summary>
    /// Defines the API endpoints for managing loans in the library management system.
    /// </summary>
    public static class LoanEndpoints
    {
        /// <summary>
        /// Maps the loan-related API endpoints to the provided web application.
        /// </summary>
        /// <param name="app">The web application to which the endpoints will be mapped.</param>
        public static void MapLoanEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/loans")
                .WithTags("Loans");

            // Map POST request to create a new loan
            group.MapPost("/", async (ILoanService service, LoanCreateDto dto) =>
            {
                return Results.Ok(await service.CreateLoan(dto));  // Return the created loan
            })
            .AddEndpointFilter<ValidationFilter<LoanCreateDto>>();  // Validation filter for input validation

            // Map PUT request to update an existing loan by ID
            group.MapPut("/{id}", async (ILoanService service, Guid id, LoanUpdateDto dto) =>
            {
                return Results.Ok(await service.UpdateLoan(id, dto));  // Return the updated loan information
            })
            .AddEndpointFilter<ValidationFilter<LoanUpdateDto>>();  // Validation filter for input validation

            // Map GET request to retrieve all loans
            group.MapGet("/", async (ILoanService service) =>
                Results.Ok(await service.GetAllLoans()))  // Return a list of all loans
                .RequireCustomAuthorization("Employee", "Manager");  // Authorization for "Employee" and "Manager" roles
        }
    }
}
