using E_Commerce.Services;
using Microsoft.EntityFrameworkCore;
using AssignmentTaskECommerce.Models;
using AssignmentTaskECommerce.Data;
using AssignmentTaskECommerce.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
var configuration = builder.Configuration;

// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency injection for services
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<ICartService, CartService>();  // Register CartService

// CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod());
});


// Configure DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(configuration.GetConnectionString("DefaultConnection"),
        sqliteOptions => sqliteOptions.CommandTimeout(300))  // 300 seconds timeout
            .EnableDetailedErrors()
            .EnableSensitiveDataLogging());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Enable CORS
app.UseCors("AllowSpecificOrigin");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Seed database (optional)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();

    try
    {
        context.Database.Migrate();  // Apply migrations

        // Seed initial data only if the database is empty
        if (!context.Products.Any())
        {
            SeedDatabase(context);
        }
    }
    catch (Exception ex)
    {
        var logger = app.Services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating or seeding the database.");
    }
}

try
{
    app.Run();
}
catch (Exception ex)
{
    var logger = app.Services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred while starting the application.");
}


void SeedDatabase(ApplicationDbContext context)
{
    var products = new List<Product>
    {
        new Product { ProductName = "Laptop", Price = 999.99m },
        new Product { ProductName = "Smartphone", Price = 450.00m },
        new Product { ProductName = "Tablet", Price = 299.99m }
    };

    context.Products.AddRange(products);
    context.SaveChanges();
}
