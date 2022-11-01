using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PersonManagerService.Infrastructure.Contexts;

namespace PersonManagerService.API.Extensions.Midleware;

public static class RegisterDatabasesExtension
{
    public static void RegisterDatabases(this IServiceCollection serviceCollection, ConfigurationManager configuration)
    {
        serviceCollection.AddDbContext<PersonManagerServiceDbContext>(options =>
        options.UseSqlServer(new SqlConnection(configuration.GetSection("DatabaseOptions:ConnectionString").Value)));
    }
}
