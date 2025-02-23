namespace LibraryManagementSystem.Infrastructure.Configuration.EntitiesConfigurations
{
    /// <summary>
    /// Represents the configuration settings required for generating and validating JWT tokens.
    /// </summary>
    public class JwtSettings
    {
        /// <summary>
        /// Gets or sets the secret key used to sign the JWT token.
        /// </summary>
        public string SecretKey { get; set; }

        /// <summary>
        /// Gets or sets the issuer of the JWT token (the entity that issues the token).
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// Gets or sets the audience of the JWT token (the intended recipients of the token).
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// Gets or sets the expiry time in minutes for the JWT token.
        /// </summary>
        public int ExpiryInMinutes { get; set; }
    }
}
