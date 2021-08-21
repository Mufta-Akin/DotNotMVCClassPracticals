
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace SMS.Web
{
    public static class Extensions
    {
        // -------------------------- VIEW Authorisation Helper -------------------------//
        // ClaimsPrincipal - HasOneOfRoles extension method to check if a user has any of the roles in a comma separated string
        public static bool HasOneOfRoles(this ClaimsPrincipal claims, string rolesString)
        {
            // split string into an array of roles
            var roles = rolesString.Split(",");

            // linq query to check that ClaimsPrincipal has one of these roles
            return roles.FirstOrDefault(role => claims.IsInRole(role)) != null;
        }

        // ----------------------------- AUTHENTICATION --------------------------------//

        // IServiceCollection extension method adding cookie authentication 
        public static void AddCookieAuthentication(this IServiceCollection services, 
                                                        string notAuthorised = "/User/ErrorNotAuthorised", 
                                                        string notAuthenticated= "/User/ErrorNotAuthenticated")
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(options => {
                        options.AccessDeniedPath = notAuthorised;
                        options.LoginPath = notAuthenticated;
            });
        }

         // IServiceCollection extension method adding JwtAuthentication
        public static void AddJwtAuthentication(this IServiceCollection services, string secret){                  
            var key = Encoding.ASCII.GetBytes(secret);
            
            services.AddAuthentication(x => {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x => {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }

        // IServiceCollection extension method adding both Cookie and JwtAuthentication
        // API routes need to specify that Jwt should be used as the default
        // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public static void AddCookieAndJwtAuthentication(this IServiceCollection services, string secret,
                                        string notAuthorised = "/User/ErrorNotAuthorised", 
                                        string notAuthenticated= "/User/ErrorNotAuthenticated")
        {                  
            var key = Encoding.ASCII.GetBytes(secret);

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => {
                        options.AccessDeniedPath = notAuthorised;
                        options.LoginPath = notAuthenticated;
                })
                .AddJwtBearer(x => {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
        }
        
    }
}
