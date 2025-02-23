namespace LibraryManagementSystem.Application.Exceptions
{
    /// <summary>
    /// Represents an exception that occurs during validation failures.
    /// </summary>
    public class ValidationException : Exception
    {
        /// <summary>
        /// Gets the collection of validation errors.
        /// </summary>
        public IDictionary<string, string[]> Errors { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationException"/> class with validation errors.
        /// </summary>
        /// <param name="errors">The dictionary containing validation errors.</param>
        public ValidationException(IDictionary<string, string[]> errors)
            : base("Validation failed") => Errors = errors;
    }

    /// <summary>
    /// Represents an exception that occurs when an entity is not found.
    /// </summary>
    public class NotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotFoundException"/> class with the entity name and key.
        /// </summary>
        /// <param name="name">The name of the entity.</param>
        /// <param name="key">The key of the entity that was not found.</param>
        public NotFoundException(string name, object key)
            : base($"Entity \"{name}\" ({key}) was not found.") { }
    }

    /// <summary>
    /// Represents an exception that occurs when a duplicate entity is found.
    /// </summary>
    public class DuplicateException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DuplicateException"/> class with the entity name and property.
        /// </summary>
        /// <param name="entity">The name of the entity.</param>
        /// <param name="property">The property of the entity that caused the duplication.</param>
        public DuplicateException(string entity, string property)
            : base($"{entity} with this {property} already exists") { }
    }

    /// <summary>
    /// Represents an exception that occurs when the user is unauthorized.
    /// </summary>
    public class UnauthorizedException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnauthorizedException"/> class with the specified message.
        /// </summary>
        /// <param name="message">The error message.</param>
        public UnauthorizedException(string message) : base(message) { }
    }

    /// <summary>
    /// Represents an exception that occurs due to a conflict in the application.
    /// </summary>
    public class ConflictException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConflictException"/> class with the specified message.
        /// </summary>
        /// <param name="message">The conflict message.</param>
        public ConflictException(string message) : base(message) { }
    }
}
