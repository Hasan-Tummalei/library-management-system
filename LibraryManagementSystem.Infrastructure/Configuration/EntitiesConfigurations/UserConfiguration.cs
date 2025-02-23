using LibraryManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementSystem.Infrastructure.Configuration.EntitiesConfigurations
{
    /// <summary>
    /// Configures the User entity in the context of the database.
    /// </summary>
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        /// <summary>
        /// Configures the User entity's properties and relationships in the model.
        /// </summary>
        /// <param name="userBuilder">The builder used to configure the User entity.</param>
        public void Configure(EntityTypeBuilder<User> userBuilder)
        {
            // Define the primary key for the User entity
            userBuilder.HasKey(x => x.Id);

            // Configure the Username property as required with a max length of 50
            userBuilder.Property(x => x.Username)
                .HasMaxLength(50)
                .IsRequired();

            // Configure the Password property as required
            userBuilder.Property(x => x.Password)
                .IsRequired();

            // Configure the UpdatedAt property to have a default value of the current time
            userBuilder.Property(x => x.UpdatedAt)
                .HasDefaultValueSql("NOW()");

            // Configure the UserRole property (no additional constraints here)
            userBuilder.Property(x => x.UserRole);
        }
    }
}
