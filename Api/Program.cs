using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using LibraryManagementApi.Data;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddDbContext<LibraryContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Add controllers to the service container
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Customize JSON serialization to use camelCase for property names
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        
        // Example of a custom converter for DateTime (if needed)
        // options.JsonSerializerOptions.Converters.Add(new JsonDateTimeConverter());
    });

// Add CORS policy to allow requests from a specific origin
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.WithOrigins("http://localhost:4200")  // Adjust if using a different frontend origin
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials();
    });
});

// Add Swagger for API documentation in development environment
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Apply the CORS policy to the application
app.UseCors("AllowAll");

// Enable HTTPS redirection
app.UseHttpsRedirection();

// Configure Swagger UI in development environment
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Enable authorization middleware (if required)
app.UseAuthorization();

// Map controller routes
app.MapControllers();

// Run the application
app.Run();
