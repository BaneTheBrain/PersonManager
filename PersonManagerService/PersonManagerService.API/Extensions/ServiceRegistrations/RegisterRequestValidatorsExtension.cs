using FluentValidation;
using MediatR;
using PersonManagerService.Application.Behaviors;
using PersonManagerService.Domain.Abstractions;
using PersonManagerService.Persistance.Repositories;

namespace PersonManagerService.API.Extensions.ServiceRegistrations
{
    public static class RegisterRequestValidatorsExtension
    {
        public static void RegisterRequestValidators(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestValidationPipelineBehavior<,>));
            serviceCollection.AddValidatorsFromAssembly(typeof(Application.AssemblyReference).Assembly, includeInternalTypes: true);
        }
    }
}
