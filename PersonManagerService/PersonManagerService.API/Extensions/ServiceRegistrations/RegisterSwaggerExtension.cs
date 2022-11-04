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
        });
    }
}
