using PersonManagerService.Domain.Abstractions;
using PersonManagerService.Persistance.Repositories;

namespace PersonManagerService.API.Extensions.Midleware;

public static class RegisterRepositoriesExtension
{
    public static void RegisterRepositories(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped(typeof(IPersonRepository), typeof(PersonRepository));
        serviceCollection.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
    }
}
