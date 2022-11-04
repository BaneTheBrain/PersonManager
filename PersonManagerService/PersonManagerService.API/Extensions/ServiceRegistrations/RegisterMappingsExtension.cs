using AutoMapper;
using PersonManagerService.Domain.Mapping;

namespace PersonManagerService.API.Extensions.ServiceRegistrations;

public static class RegisterMappingsExtension
{
    public static void RegisterMappers(this IServiceCollection serviceCollection)
    {
        var mapperConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new PersonProfile());
        });

        IMapper mapper = mapperConfig.CreateMapper();
        serviceCollection.AddSingleton(mapper);
    }
}
