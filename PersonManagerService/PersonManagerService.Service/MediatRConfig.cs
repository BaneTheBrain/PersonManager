using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace PersonManagerService.Domain;

public static class MediatRConfig
{
    public static void RegisterApplicationMediatR(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddMediatR(Assembly.GetExecutingAssembly());
    }
}
