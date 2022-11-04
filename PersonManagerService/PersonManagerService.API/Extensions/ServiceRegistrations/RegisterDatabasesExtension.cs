using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PersonManagerService.Infrastructure.Contexts;

namespace PersonManagerService.API.Extensions.ServiceRegistrations;

public static class RegisterDatabasesExtension
{
    public static void RegisterDatabases(this IServiceCollection serviceCollection, ConfigurationManager configuration)
    {
        var dbConnectionString = configuration.GetSection("DatabaseOptions:ConnectionString").Value;
        if (dbConnectionString is not null)
        {
            serviceCollection.AddDbContext<PersonManagerServiceDbContext>(options => options.UseSqlServer(dbConnectionString));
        }
        else
        {
            serviceCollection.AddDbContext<PersonManagerServiceDbContext>(c => c.UseInMemoryDatabase("PersonDbInMemory"));
            //to do: seed in memory db
        }
    }
}
