using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;

namespace PersonManagerService.API.Extensions.ServiceRegistrations;

public static class RegisterSwaggerExtension
{
    public static void RegisterSwagger(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSwaggerGen(swg =>
        {
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            swg.IncludeXmlComments(xmlPath);

            swg.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Description = "Standard Authorization header using the Bearer scheme",
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });
            swg.OperationFilter<SecurityRequirementsOperationFilter>();
        });
    }
}
