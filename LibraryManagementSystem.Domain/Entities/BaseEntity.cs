namespace LibraryManagementSystem.Domain.Entities
{
    /// <summary>
    /// Represents the base entity for all domain entities in the library system.
    /// Provides common properties such as Id, CreatedAt, and UpdatedAt.
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// Gets or sets the unique identifier for the entity.
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Gets or sets the date and time when the entity was created.
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the date and time when the entity was last updated.
        /// This can be null if the entity hasn't been updated yet.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }
    }
}
