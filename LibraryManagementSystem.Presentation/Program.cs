using LibraryManagementSystem.Infrastructure.Configuration.MapsterConfiguration;
using LibraryManagementSystem.Infrastructure.Persistance;
using LibraryManagementSystem.Presentation.Endpoints;
using Microsoft.EntityFrameworkCore;
using LibraryManagementSystem.Presentation.Middleware;
using LibraryManagementSystem.Infrastructure.Configuration.EntitiesConfigurations;

namespace LibraryManagementSystem.Presentation
{
    /// <summary>
    /// The entry point of the application where the web application is configured and started.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The main method that configures and runs the web application.
        /// </summary>
        /// <param name="args">Command line arguments passed to the application.</param>
        public static void Main(string[] args)
        {
            // Create a WebApplication builder
            var builder = WebApplication.CreateBuilder(args);

            // Retrieve the connection string for the database from the configuration
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            // Add the DbContext for library data with PostgreSQL provider
            builder.Services.AddDbContext<LibraryDbContext>(options => options.UseNpgsql(connectionString));

            // Configure mappings for DTOs to entities using Mapster
            MapsterConfig.ConfigureMappings();

            // Configure JWT settings from configuration
            builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

            // Register the application services (e.g., repositories, services) and authentication services
            builder.Services.AddApplicationServices(builder.Configuration);
            builder.Services.AddAuthenticationServices(builder.Configuration);

            // Build the application
            var app = builder.Build();

            // Add middleware for exception handling
            app.UseMiddleware<ExceptionHandlingMiddleware>();

            // Enable HTTPS redirection
            app.UseHttpsRedirection();

            // Map API endpoints for users, books, authors, borrowers, and loans
            app.MapUserEndpoint();
            app.MapBookEndpoints();
            app.MapAuthorEndpoints();
            app.MapBorrowerEndpoints();
            app.MapLoanEndpoints();

            // Run the application
            app.Run();
        }
    }
}
