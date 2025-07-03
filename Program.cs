using Microsoft.EntityFrameworkCore;
using ToDoListAPI.Context;
using ToDoListAPI.Endpoints;
using ToDoListAPI.Interfaces;
using ToDoListAPI.Services;

var builder = WebApplication.CreateBuilder(args);

string env = builder.Environment.EnvironmentName;
string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (env == "Development")
{
    builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlite(connectionString));
}
else
{
    builder.Services.AddDbContext<AppDbContext>(opt => opt.UseNpgsql(connectionString));
}

builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

app.MapUserEndpoints();

await app.RunAsync();
