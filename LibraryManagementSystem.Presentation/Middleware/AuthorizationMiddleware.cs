//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;

//namespace LibraryManagementSystem.Presentation.Middleware
//{
//    public class AuthorizationMiddleware
//    {
//        private readonly RequestDelegate _next;
//        private readonly string _secretKey;
//        private readonly string _issuer;
//        private readonly string _audience;

//        public AuthorizationMiddleware(RequestDelegate next, IConfiguration configuration)
//        {
//            _next = next;
//            _secretKey = configuration["JwtSettings:SecretKey"];
//            _issuer = configuration["JwtSettings:Issuer"];
//            _audience = configuration["JwtSettings:Audience"];
//        }

//        public async Task InvokeAsync(HttpContext context)
//        {
//            var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
//            if (string.IsNullOrEmpty(token))
//            {
//                context.Response.StatusCode = 401;
//                await context.Response.WriteAsync("Unauthorized: No token provided.");
//                return;
//            }

//            try
//            {
//                var handler = new JwtSecurityTokenHandler();
//                var key = Encoding.UTF8.GetBytes(_secretKey);
//                var tokenValidationParameters = new TokenValidationParameters
//                {
//                    ValidateIssuer = true,
//                    ValidateAudience = true,
//                    ValidateLifetime = true,
//                    ValidateIssuerSigningKey = true,
//                    ValidIssuer = _issuer,
//                    ValidAudience = _audience,
//                    IssuerSigningKey = new SymmetricSecurityKey(key),
//                };

//                var principal = handler.ValidateToken(token, tokenValidationParameters, out var validatedToken);
//                context.User = principal;

//                var requiredRoles = context.Items["RequiredRoles"] as string[];
//                if (requiredRoles != null && requiredRoles.Length > 0)
//                {
//                    var role = context.User.FindFirst(ClaimTypes.Role)?.Value;
//                    if (role == null || !Array.Exists(requiredRoles, r => r.Equals(role, StringComparison.OrdinalIgnoreCase)))
//                    {
//                        context.Response.StatusCode = 403;
//                        await context.Response.WriteAsync("Forbidden: Insufficient role.");
//                        return;
//                    }
//                }

//                await _next(context);
//            }
//            catch (Exception)
//            {
//                context.Response.StatusCode = 401;
//                await context.Response.WriteAsync("Unauthorized: Invalid token.");
//            }
//        }
//    }
//}
