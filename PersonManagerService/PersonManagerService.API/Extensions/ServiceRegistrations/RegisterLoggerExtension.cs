using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PersonManagerService.Infrastructure.Contexts;
using Serilog;

namespace PersonManagerService.API.Extensions.ServiceRegistrations
{
    public static class RegisterLoggingExtension
    {
        public static void RegisterLoggers(this WebApplicationBuilder builder)
        {
            var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .Enrich.FromLogContext()
                .CreateLogger();

            builder.Logging.ClearProviders();
            builder.Logging.AddSerilog(logger);
        }
    }
}
