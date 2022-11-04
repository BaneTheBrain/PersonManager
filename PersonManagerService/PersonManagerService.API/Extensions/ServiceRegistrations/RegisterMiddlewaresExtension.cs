using PersonManagerService.API.Extensions.Middleware;

namespace PersonManagerService.API.Extensions.ServiceRegistrations;

public static class RegisterMiddlewaresExtension
{
    public static void RegisterMiddlewares(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<GlobalExceptionHandlerMiddleware>();
    }
}
