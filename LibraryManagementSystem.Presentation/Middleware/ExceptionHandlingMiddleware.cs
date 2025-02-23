using LibraryManagementSystem.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace LibraryManagementSystem.Presentation.Middleware
{
    /// <summary>
    /// Middleware for handling exceptions that occur during the request processing pipeline.
    /// Catches exceptions, logs them, and returns appropriate HTTP responses with problem details.
    /// </summary>
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        private readonly IHostEnvironment _env;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionHandlingMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next delegate in the pipeline.</param>
        /// <param name="logger">The logger to log exceptions.</param>
        /// <param name="env">The environment information to determine whether the app is in development.</param>
        public ExceptionHandlingMiddleware(
            RequestDelegate next,
            ILogger<ExceptionHandlingMiddleware> logger,
            IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        /// <summary>
        /// Invokes the next middleware in the pipeline and catches exceptions that occur.
        /// </summary>
        /// <param name="context">The HTTP context for the current request.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Invoke the next middleware in the pipeline
                await _next(context);
            }
            catch (Exception ex)
            {
                // Handle any exception that occurs
                await HandleExceptionAsync(context, ex);
            }
        }

        /// <summary>
        /// Handles the exception and creates a response with the appropriate status code and details.
        /// </summary>
        /// <param name="context">The HTTP context for the current request.</param>
        /// <param name="exception">The exception that was thrown.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var problemDetails = CreateProblemDetails(context, exception);
            LogException(exception, problemDetails);

            // Write the problem details to the response
            await WriteProblemDetailsResponse(context, problemDetails);
        }

        /// <summary>
        /// Creates a <see cref="ProblemDetails"/> object based on the exception.
        /// </summary>
        /// <param name="context">The HTTP context for the current request.</param>
        /// <param name="exception">The exception that was thrown.</param>
        /// <returns>A <see cref="ProblemDetails"/> object containing the exception details.</returns>
        private ProblemDetails CreateProblemDetails(HttpContext context, Exception exception)
        {
            var problemDetails = new ProblemDetails
            {
                Instance = context.Request.Path,
                Extensions = { ["traceId"] = context.TraceIdentifier }
            };

            // Determine the appropriate problem details based on the exception type
            switch (exception)
            {
                case ValidationException validationEx:
                    problemDetails.Title = "Validation Error";
                    problemDetails.Status = StatusCodes.Status400BadRequest;
                    problemDetails.Detail = "One or more validation errors occurred";
                    problemDetails.Extensions["errors"] = validationEx.Errors;
                    break;

                case NotFoundException notFoundEx:
                    problemDetails.Title = "Resource Not Found";
                    problemDetails.Status = StatusCodes.Status404NotFound;
                    problemDetails.Detail = notFoundEx.Message;
                    break;

                case DuplicateException duplicateEx:
                    problemDetails.Title = "Conflict";
                    problemDetails.Status = StatusCodes.Status409Conflict;
                    problemDetails.Detail = duplicateEx.Message;
                    break;

                case UnauthorizedException unauthorizedEx:
                    problemDetails.Title = "Unauthorized";
                    problemDetails.Status = StatusCodes.Status401Unauthorized;
                    problemDetails.Detail = unauthorizedEx.Message;
                    break;

                default:
                    problemDetails.Title = "Internal Server Error";
                    problemDetails.Status = StatusCodes.Status500InternalServerError;
                    problemDetails.Detail = _env.IsDevelopment()
                        ? exception.Message
                        : "An unexpected error occurred. Please try again later.";
                    break;
            }

            return problemDetails;
        }

        /// <summary>
        /// Logs the exception based on its severity and status code.
        /// </summary>
        /// <param name="exception">The exception that was thrown.</param>
        /// <param name="problemDetails">The problem details that describe the exception.</param>
        private void LogException(Exception exception, ProblemDetails problemDetails)
        {
            // Log server errors with error level, and client errors with warning level
            if (problemDetails.Status >= 500)
            {
                _logger.LogError(exception, "Server Error: {Title} - {Detail}",
                    problemDetails.Title, problemDetails.Detail);
            }
            else
            {
                _logger.LogWarning(exception, "Client Error: {Title} - {Detail}",
                    problemDetails.Title, problemDetails.Detail);
            }
        }

        /// <summary>
        /// Writes the <see cref="ProblemDetails"/> response to the HTTP context.
        /// </summary>
        /// <param name="context">The HTTP context for the current request.</param>
        /// <param name="problemDetails">The problem details to include in the response.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        private async Task WriteProblemDetailsResponse(HttpContext context, ProblemDetails problemDetails)
        {
            // Set the response status code and content type
            context.Response.StatusCode = problemDetails.Status ?? StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/problem+json";

            // Serialize the problem details to JSON with appropriate formatting
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = _env.IsDevelopment()
            };

            // Write the JSON response
            await context.Response.WriteAsync(JsonSerializer.Serialize(problemDetails, options));
        }
    }
}
