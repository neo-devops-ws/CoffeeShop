using CoffeeShop.API.Models;
using CoffeeShop.API.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to DI container
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { 
        Title = "Coffee Shop API", 
        Version = "v1",
        Description = "A simple API for a coffee shop",
        Contact = new OpenApiContact
        {
            Name = "Coffee Shop Team",
            Email = "support@coffeeshop.example"
        }
    });
});

// Register services
builder.Services.AddSingleton<ICoffeeService, InMemoryCoffeeService>();


var app = builder.Build();
    app.UseSwagger();
    app.UseSwaggerUI();

app.UseHttpsRedirection();

// Home endpoint
app.MapGet("/", () => "Welcome to the Coffee Shop API! Visit /swagger to explore our brew-tiful API.")
   .WithName("Home")
   .WithOpenApi();

app.MapGet("/hello", () => "Hello, from Haitham!");

// User signup
app.MapPost("/signup", (User user) =>
{
    if (string.IsNullOrEmpty(user.Name) || string.IsNullOrEmpty(user.Email))
    {
        return Results.BadRequest("Name and Email are required.");
    }

    return Results.Ok(new { Message = "Sign-up successful!", UserName = user.Name });
})
.WithName("SignUp")
.WithOpenApi();

// Coffee endpoints
app.MapGet("/coffees", async (ICoffeeService coffeeService) =>
{
    var coffees = await coffeeService.GetAllCoffeesAsync();
    return Results.Ok(coffees);
})
.WithName("GetAllCoffees")
.WithOpenApi();

app.MapGet("/coffees/{id}", async (int id, ICoffeeService coffeeService) =>
{
    var coffee = await coffeeService.GetCoffeeByIdAsync(id);
    if (coffee == null)
    {
        return Results.NotFound($"Coffee with ID {id} not found");
    }
    return Results.Ok(coffee);
})
.WithName("GetCoffeeById")
.WithOpenApi();

app.MapPost("/coffees", async (Coffee coffee, ICoffeeService coffeeService) =>
{
    var result = await coffeeService.AddCoffeeAsync(coffee);
    return Results.Created($"/coffees/{result.Id}", result);
})
.WithName("CreateCoffee")
.WithOpenApi();

app.MapPut("/coffees/{id}", async (int id, Coffee coffee, ICoffeeService coffeeService) =>
{
    var result = await coffeeService.UpdateCoffeeAsync(id, coffee);
    if (result == null)
    {
        return Results.NotFound($"Coffee with ID {id} not found");
    }
    return Results.Ok(result);
})
.WithName("UpdateCoffee")
.WithOpenApi();

app.MapDelete("/coffees/{id}", async (int id, ICoffeeService coffeeService) =>
{
    var result = await coffeeService.DeleteCoffeeAsync(id);
    if (!result)
    {
        return Results.NotFound($"Coffee with ID {id} not found");
    }
    return Results.NoContent();
})
.WithName("DeleteCoffee")
.WithOpenApi();

app.Run();

// Make Program accessible to tests
public partial class Program { }
