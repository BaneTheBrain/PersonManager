using PersonManagerService.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using PersonManagerService.Domain;
using PersonManagerService.API.Extensions.ServiceRegistrations;
using PersonManagerService.API.Extensions.Middleware;
using System.Reflection;
using Serilog;
using static System.Net.Mime.MediaTypeNames;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.RegisterSwagger();
builder.Services.RegisterDatabases(builder.Configuration);
builder.Services.RegisterRepositories();
builder.Services.RegisterMiddlewares();
builder.Services.RegisterRequestValidators();
builder.Services.AddMediatR(typeof(PersonManagerService.Application.AssemblyReference).Assembly);

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
    var database = scope.ServiceProvider.GetRequiredService<PersonManagerServiceDbContext>().Database;
    if (database.IsRelational())
    {
        database.Migrate();
    }
}

app.Run();
