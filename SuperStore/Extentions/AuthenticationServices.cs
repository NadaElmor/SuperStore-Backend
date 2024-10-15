using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SuperStore.Core.Entities.User;
using SuperStore.Core.Services.Contracts;
using SuperStore.Repositery.Identity;
using SuperStore.Services;
using System.Text;

namespace SuperStore.Extentions
{
    public static class AuthenticationServices
    {
        public static IServiceCollection AddAuthServices(this IServiceCollection Services,IConfiguration configuration)
        {
            //handle authorization 
            Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => {
                // Configure JWT bearer authentication options
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    // Set other validation parameters...
                    ValidIssuer = configuration["JWT:ValidIssuer"],
                    ValidAudience = configuration["JWT:ValidAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"])),
                    ClockSkew = TimeSpan.FromDays(double.Parse(configuration["JWT:DurationInDays"]))
                };

            });

            //allow DI for IAuthService
            Services.AddScoped(typeof(IAuthService), typeof(AuthService));
          
           Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AppIdentityDbContext>();
            
            return Services;
        }
       
    }
}
