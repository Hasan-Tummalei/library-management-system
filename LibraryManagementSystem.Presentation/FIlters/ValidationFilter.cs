using LibraryManagementSystem.Application.Exceptions;
using LibraryManagementSystem.Application.Interfaces.Utilities;

namespace LibraryManagementSystem.Presentation.FIlters
{
    /// <summary>
    /// A custom filter that performs validation on a request object of type <typeparamref name="T"/> before processing the endpoint.
    /// </summary>
    /// <typeparam name="T">The type of the request object that will be validated.</typeparam>
    public class ValidationFilter<T> : IEndpointFilter where T : class
    {
        private readonly IValidationUtil _validationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationFilter{T}"/> class.
        /// </summary>
        /// <param name="validationService">The validation service to use for validating the request object.</param>
        public ValidationFilter(IValidationUtil validationService)
        {
            _validationService = validationService;
        }

        /// <summary>
        /// Validates the request object of type <typeparamref name="T"/> before allowing the endpoint to process the request.
        /// If the validation fails, it throws a <see cref="ValidationException"/> with the validation errors.
        /// </summary>
        /// <param name="context">The context for the endpoint filter invocation, which contains the arguments passed to the endpoint.</param>
        /// <param name="next">The delegate to the next endpoint filter in the pipeline.</param>
        /// <returns>The result of the next filter or endpoint handler.</returns>
        /// <exception cref="ValidationException">Thrown if the request object does not pass validation.</exception>
        public async ValueTask<object?> InvokeAsync(
            EndpointFilterInvocationContext context,
            EndpointFilterDelegate next)
        {
            // Extract the request object of type T from the context arguments
            var request = context.Arguments
                .OfType<T>()
                .FirstOrDefault();

            // If no request object is found, continue to the next filter or handler
            if (request is null)
                return await next(context);

            // Validate the request object
            var validationResult = await _validationService.ValidateAsync(request);

            // If validation fails, throw a ValidationException
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            // Proceed to the next filter or endpoint handler if validation is successful
            return await next(context);
        }
    }
}
