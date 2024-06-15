using Microsoft.EntityFrameworkCore;
using MTKDotNetCore.ConsoleApp.Services;
using MTKDotNetCore.RestApi.Database;
using MTKDotNetCore.Shared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// get connection string from appsettings
string connectionString = builder.Configuration.GetConnectionString("DbConnection")!;

// inject connection string to AppDbContext
// always use transient injection for AppDbContext
builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(connectionString);
},
ServiceLifetime.Transient,
ServiceLifetime.Transient);

// use Scoped dependency injection pattern for each service
// automatically add connection string for everytime an instance is created
builder.Services.AddScoped(n => new AdoDotNetService(connectionString));
builder.Services.AddScoped(n => new DapperService(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
