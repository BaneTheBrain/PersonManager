using PersonManagerService.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using PersonManagerService.Domain;
using PersonManagerService.API.Extensions.ServiceRegistrations;
using PersonManagerService.API.Extensions.Middleware;
using System.Reflection;
using Serilog;
using static System.Net.Mime.MediaTypeNames;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PersonManagerService.Domain.Abstractions;
using PersonManagerService.Persistance.Repositories;
using PersonManagerService.Application.Service;
using PersonManagerService.Domain.Configuration;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.RegisterSwagger();
builder.Services.RegisterDatabases(builder.Configuration);
builder.Services.Configure<ResilientOptions>(builder.Configuration.GetSection(nameof(ResilientOptions)));
builder.Services.RegisterServices();
builder.Services.RegisterMiddlewares();
builder.Services.RegisterRequestValidators();
builder.Services.AddMediatR(typeof(PersonManagerService.Application.AssemblyReference).Assembly);
builder.Services.RegisterAuthentications(builder.Configuration.GetSection("SecurityKey").Value);
builder.Services.RegisterMappers();
builder.RegisterLoggers();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "personPortalCors",
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:4200");
                      });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
app.UseCors("personPortalCors");
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
