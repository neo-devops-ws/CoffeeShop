using CoffeeShop.API.Models;

namespace CoffeeShop.API.Services;

public interface ICoffeeService
{
    Task<IEnumerable<Coffee>> GetAllCoffeesAsync();
    Task<Coffee?> GetCoffeeByIdAsync(int id);
    Task<Coffee> AddCoffeeAsync(Coffee coffee);
    Task<Coffee?> UpdateCoffeeAsync(int id, Coffee coffee);
    Task<bool> DeleteCoffeeAsync(int id);
}