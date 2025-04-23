using CoffeeShop.API.Models;
using CoffeeShop.API.Services;
using Xunit;

namespace CoffeeShop.Tests;

public class CoffeeServiceTests
{
    private readonly ICoffeeService _service;
    
    public CoffeeServiceTests()
    {
        _service = new InMemoryCoffeeService();
    }
    
    [Fact]
    public async Task GetAllCoffees_ReturnsAllCoffees()
    {

        // Act
        var coffees = await _service.GetAllCoffeesAsync();
        
        // Assert
        Assert.NotNull(coffees);
        Assert.True(coffees.Any());
    }
    
    [Fact]
    public async Task GetCoffeeById_WithValidId_ReturnsCoffee()
    {
        // Arrange
        int validId = 1;
        
        // Act
        var coffee = await _service.GetCoffeeByIdAsync(validId);
        
        // Assert
        Assert.NotNull(coffee);
        Assert.Equal(validId, coffee.Id);
    }
    
    [Fact]
    public async Task GetCoffeeById_WithInvalidId_ReturnsNull()
    {
        // Arrange
        int invalidId = 999;
        
        // Act
        var coffee = await _service.GetCoffeeByIdAsync(invalidId);
        
        // Assert
        Assert.Null(coffee);
    }
    
    [Fact]
    public async Task AddCoffee_ReturnsNewCoffeeWithId()
    {
        // Arrange
        var newCoffee = new Coffee
        {
            Name = "Mocha",
            Description = "Espresso with chocolate",
            Price = 4.50m,
            Category = "Hot"
        };
        
        // Act
        var result = await _service.AddCoffeeAsync(newCoffee);
        
        // Assert
        Assert.NotNull(result);
        Assert.True(result.Id > 0);
        Assert.Equal(newCoffee.Name, result.Name);
    }
}