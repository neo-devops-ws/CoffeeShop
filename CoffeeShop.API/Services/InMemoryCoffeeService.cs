using CoffeeShop.API.Models;

namespace CoffeeShop.API.Services;

public class InMemoryCoffeeService : ICoffeeService
{
    private readonly List<Coffee> _coffees = new()
    {
        new Coffee { Id = 1, Name = "Espresso", Description = "Strong black coffee", Price = 2.50m, Category = "Hot" },
        new Coffee { Id = 2, Name = "Cappuccino", Description = "Espresso with steamed milk and foam", Price = 3.50m, Category = "Hot" },
        new Coffee { Id = 3, Name = "Latte", Description = "Espresso with steamed milk", Price = 3.75m, Category = "Hot" },
        new Coffee { Id = 4, Name = "Cold Brew", Description = "Coffee brewed with cold water", Price = 4.25m, Category = "Cold" }
    };

    public Task<IEnumerable<Coffee>> GetAllCoffeesAsync()
    {
        return Task.FromResult(_coffees.AsEnumerable());
    }

    public Task<Coffee?> GetCoffeeByIdAsync(int id)
    {
        var coffee = _coffees.FirstOrDefault(c => c.Id == id);
        return Task.FromResult(coffee);
    }

    public Task<Coffee> AddCoffeeAsync(Coffee coffee)
    {
        coffee.Id = _coffees.Max(c => c.Id) + 1;
        _coffees.Add(coffee);
        return Task.FromResult(coffee);
    }

    public Task<Coffee?> UpdateCoffeeAsync(int id, Coffee coffee)
    {
        var existingCoffee = _coffees.FirstOrDefault(c => c.Id == id);
        if (existingCoffee == null)
        {
            return Task.FromResult<Coffee?>(null);
        }

        existingCoffee.Name = coffee.Name;
        existingCoffee.Description = coffee.Description;
        existingCoffee.Price = coffee.Price;
        existingCoffee.IsAvailable = coffee.IsAvailable;
        existingCoffee.Category = coffee.Category;

        return Task.FromResult<Coffee?>(existingCoffee);
    }

    public Task<bool> DeleteCoffeeAsync(int id)
    {
        var coffee = _coffees.FirstOrDefault(c => c.Id == id);
        if (coffee == null)
        {
            return Task.FromResult(false);
        }

        _coffees.Remove(coffee);
        return Task.FromResult(true);
    }
}