namespace LibraryManagementSystem.Application.Interfaces.Utilities
{
    /// <summary>
    /// Represents the utility interface for validating requests asynchronously.
    /// </summary>
    public interface IValidationUtil
    {
        /// <summary>
        /// Validates a request asynchronously.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request to validate.</typeparam>
        /// <param name="request">The request object to validate.</param>
        /// <returns>A task representing the asynchronous operation, with the result of the validation as the outcome.</returns>
        Task<ValidationResult> ValidateAsync<TRequest>(TRequest request);
    }

    /// <summary>
    /// Represents the result of a validation operation.
    /// </summary>
    /// <param name="IsValid">Indicates whether the validation was successful.</param>
    /// <param name="Errors">A dictionary containing error details, where the key is the field name and the value is an array of error messages.</param>
    public record ValidationResult(bool IsValid, IDictionary<string, string[]> Errors);
}
