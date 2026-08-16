using EmployeeAPI.Data;
using EmployeeAPI.Extensions;
using EmployeeAPI.Middleware;
using System.Text.Json.Serialization;

// Enable legacy timestamp behavior for Npgsql to handle DateTime seamlessly
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddSwaggerConfiguration();
builder.Services.AddCorsConfiguration();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Seed database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        await SeedData.InitializeAsync(context);
        logger.LogInformation("Database migration and seeding completed successfully.");
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred during database migration / seeding: {Message}", ex.Message);
    }
}

// Middleware pipeline
app.UseMiddleware<ExceptionMiddleware>();

// Always enable Swagger for easy testing
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Employee Management API v1");
    c.RoutePrefix = "swagger";
});

app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();

// Root and /api health check endpoints
app.MapGet("/", () => Results.Ok(new
{
    status = "Healthy",
    message = "Employee Management System API is running successfully!",
    swagger = "/swagger",
    timestamp = DateTime.UtcNow
}));

app.MapGet("/api", () => Results.Ok(new
{
    status = "Healthy",
    message = "Employee Management API Base Endpoint",
    endpoints = new[] { "/api/auth/login", "/api/dashboard/stats", "/api/employees", "/api/departments", "/api/roles" },
    swagger = "/swagger",
    timestamp = DateTime.UtcNow
}));

app.MapControllers();

app.Run();
