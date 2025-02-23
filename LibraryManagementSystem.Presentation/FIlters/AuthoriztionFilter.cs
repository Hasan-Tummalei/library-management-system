using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using LibraryManagementSystem.Infrastructure.Configuration.EntitiesConfigurations;

public static class CustomAuthorizationExtensions
{
    /// <summary>
    /// Adds custom authorization to an endpoint based on a JWT token and user roles.
    /// </summary>
    /// <param name="builder">The <see cref="RouteHandlerBuilder"/> to which the authorization filter will be added.</param>
    /// <param name="allowedRoles">An array of allowed roles that are authorized to access the endpoint.</param>
    /// <returns>A modified <see cref="RouteHandlerBuilder"/> with the custom authorization filter applied.</returns>
    public static RouteHandlerBuilder RequireCustomAuthorization(this RouteHandlerBuilder builder, params string[] allowedRoles)
    {
        return builder.AddEndpointFilter(async (context, next) =>
        {
            var httpContext = context.HttpContext;

            // Retrieve the Authorization header
            if (!httpContext.Request.Headers.TryGetValue("Authorization", out var authHeader))
            {
                return Results.Unauthorized(); // Return Unauthorized if no Authorization header is found
            }

            // Extract the token and validate it
            var token = authHeader.ToString().Replace("Bearer ", "");
            var jwtSettings = httpContext.RequestServices.GetRequiredService<IOptions<JwtSettings>>().Value;
            var tokenHandler = new JwtSecurityTokenHandler();

            ClaimsPrincipal principal;
            try
            {
                // Validate the JWT token and extract claims
                principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
                }, out SecurityToken validatedToken);
            }
            catch (Exception)
            {
                return Results.Unauthorized(); // Return Unauthorized if the token is invalid
            }

            // Check if the user role is present and valid
            var roleClaim = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            if (roleClaim == null || !allowedRoles.Contains(roleClaim.Value, StringComparer.OrdinalIgnoreCase))
            {
                return Results.Forbid(); // Return Forbidden if the user's role is not authorized
            }

            // Proceed to the next endpoint handler if authorization passes
            return await next(context);
        });
    }
}
