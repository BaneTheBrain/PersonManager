using PersonManagerService.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using PersonManagerService.Domain;
using PersonManagerService.API.Extensions.Midleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.RegisterDatabases(builder.Configuration);
builder.Services.RegisterRepositories();
builder.Services.RegisterApplicationMediatR();
builder.Services.RegisterMappers();


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

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    scope.ServiceProvider.GetRequiredService<PersonManagerServiceDbContext>().Database.Migrate();
}

app.Run();
