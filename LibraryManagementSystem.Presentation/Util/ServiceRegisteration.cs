using FluentValidation;
using System.Text;
using LibraryManagementSystem.Application.Interfaces.Repositories;
using LibraryManagementSystem.Application.Interfaces.Services;
using LibraryManagementSystem.Application.Interfaces.Utilities;
using LibraryManagementSystem.Application.Services;
using LibraryManagementSystem.Infrastructure.Repositories;
using LibraryManagementSystem.Infrastructure.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using LibraryManagementSystem.Infrastructure.Utilities.Validator;
using LibraryManagementSystem.Infrastructure.Utilities.Validator.User;
using Microsoft.IdentityModel.Tokens;
using LibraryManagementSystem.Infrastructure.Utilities.Mapping;
using LibraryManagementSystem.Infrastructure.Utilities.Hashing;

namespace LibraryManagementSystem.Presentation
{
    /// <summary>
    /// A static class that contains methods for registering application services and authentication services in the DI container.
    /// </summary>
    public static class ServiceRegistration
    {
        /// <summary>
        /// Registers the application services required by the system, including repositories, services, and utility classes.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to register services into.</param>
        /// <param name="configuration">The configuration instance containing application settings.</param>
        public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Register utility services
            services.AddSingleton<IMapperUtil, MapperUtil>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();

            // Register repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IBorrowerRepository, BorrowerRepository>();
            services.AddScoped<ILoanRepository, LoanRepository>();

            // Register application services
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<IBorrowerService, BorrowerService>();
            services.AddScoped<ILoanService, LoanService>();

            // Register validation services
            services.AddScoped<IValidationUtil, FluentValidationService>();
            services.AddValidatorsFromAssemblyContaining<UserRegisterReqDtoValidator>();
        }

        /// <summary>
        /// Configures JWT authentication services for the application.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add authentication services into.</param>
        /// <param name="configuration">The configuration instance containing JWT settings.</param>
        public static void AddAuthenticationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Add JWT Bearer authentication
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["JwtSettings:Issuer"],
                        ValidAudience = configuration["JwtSettings:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"]))
                    };
                });
        }
    }
}
