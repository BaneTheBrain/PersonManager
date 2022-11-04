using PersonManagerService.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using PersonManagerService.Domain;
using PersonManagerService.API.Extensions.ServiceRegistrations;
using PersonManagerService.API.Extensions.Middleware;
using System.Reflection;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.RegisterSwagger();
builder.Services.RegisterDatabases(builder.Configuration);
builder.Services.RegisterRepositories();
builder.Services.RegisterMiddlewares();
builder.Services.RegisterApplicationMediatR();
builder.Services.RegisterMappers();
builder.RegisterLoggers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//add authentication
app.UseAuthorization();

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    scope.ServiceProvider.GetRequiredService<PersonManagerServiceDbContext>().Database.Migrate();
}

app.Run();
