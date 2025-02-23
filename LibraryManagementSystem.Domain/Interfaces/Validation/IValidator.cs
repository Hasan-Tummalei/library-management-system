namespace LibraryManagementSystem.Domain.Interfaces.Validation
{
    /// <summary>
    /// Defines the contract for a validator that validates a request of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of the request to be validated.</typeparam>
    internal interface IValidator<in T>
    {
        /// <summary>
        /// Validates the given request asynchronously.
        /// </summary>
        /// <param name="request">The request to validate.</param>
        /// <returns>A task that represents the asynchronous validation operation. The task result contains the validation result.</returns>
        Task<ValidationResult> ValidateAsync(T request);
    }

    /// <summary>
    /// Represents the result of a validation operation.
    /// </summary>
    public record ValidationResult
    {
        /// <summary>
        /// Gets or sets a value indicating whether the validation was successful.
        /// </summary>
        public bool IsValid { get; init; }

        /// <summary>
        /// Gets or sets a dictionary of validation errors, where the key is the field name and the value is an array of error messages.
        /// </summary>
        public IDictionary<string, string[]> Errors { get; init; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationResult"/> record.
        /// </summary>
        /// <param name="isValid">Indicates whether the validation passed.</param>
        /// <param name="errors">A dictionary containing the validation errors.</param>
        public ValidationResult(bool isValid, IDictionary<string, string[]> errors)
        {
            IsValid = isValid;
            Errors = errors;
        }
    }
}
