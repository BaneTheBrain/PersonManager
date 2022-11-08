using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace PersonManagerService.API.Extensions.ServiceRegistrations;

public static class RegisterAuthenticationsExtension
{
    public static void RegisterAuthentications(this IServiceCollection serviceCollection, string key)
    {
        serviceCollection.AddAuthentication(opt =>
         {
             opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
             opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
         }).AddJwtBearer(bearerOptions =>
         {
             bearerOptions.TokenValidationParameters = new TokenValidationParameters()
             {
                 ValidateIssuer = true,
                 ValidateAudience = true,
                 ValidateLifetime = true,
                 ValidateIssuerSigningKey = true,

                 ValidIssuer = "https://localhost:7080",
                 ValidAudience = "https://localhost:7080",
                 IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
             };
         });
    }
}
