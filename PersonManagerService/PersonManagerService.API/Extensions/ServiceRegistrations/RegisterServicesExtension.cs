using PersonManagerService.Application.Abstractions;
using PersonManagerService.Application.Service;
using PersonManagerService.Domain.Abstractions;
using PersonManagerService.Domain.Configuration;
using PersonManagerService.Persistance.Repositories;

namespace PersonManagerService.API.Extensions.ServiceRegistrations;

public static class RegisterServicesExtension
{
    public static void RegisterServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped(typeof(IPersonRepository), typeof(PersonRepository));
        serviceCollection.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));

        serviceCollection.AddSingleton(typeof(IResilientService), typeof(ResilientService));
    }
}
