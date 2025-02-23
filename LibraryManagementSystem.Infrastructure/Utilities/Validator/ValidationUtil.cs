using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using LibraryManagementSystem.Application.Interfaces.Utilities;

namespace LibraryManagementSystem.Infrastructure.Utilities.Validator
{
    /// <summary>
    /// A service that integrates FluentValidation with the validation utility interface <see cref="IValidationUtil"/>.
    /// This class validates requests using the appropriate FluentValidator for the given request type.
    /// </summary>
    public class FluentValidationService : IValidationUtil
    {
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="FluentValidationService"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider used to resolve validators from the DI container.</param>
        public FluentValidationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Asynchronously validates a request using the appropriate FluentValidation validator.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request to be validated.</typeparam>
        /// <param name="request">The request object to be validated.</param>
        /// <returns>A task representing the result of the validation, containing a <see cref="ValidationResult"/> with error details if validation fails.</returns>
        public async Task<ValidationResult> ValidateAsync<TRequest>(TRequest request)
        {
            // Retrieve the validator for the provided request type from the DI container
            var validator = _serviceProvider.GetService<IValidator<TRequest>>();
            if (validator == null)
                // Return a successful validation result if no validator is found
                return new ValidationResult(true, new Dictionary<string, string[]>());

            // Perform the validation
            var result = await validator.ValidateAsync(request);

            if (result.IsValid)
                // Return a successful validation result if validation passes
                return new ValidationResult(true, new Dictionary<string, string[]>());

            // Group errors by property name and return them in a dictionary
            var errors = result.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(e => e.ErrorMessage).ToArray()
                );

            // Return the validation result with errors if validation fails
            return new ValidationResult(false, errors);
        }
    }
}
