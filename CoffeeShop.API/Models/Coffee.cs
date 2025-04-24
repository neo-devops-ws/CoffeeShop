using System.ComponentModel.DataAnnotations;

namespace CoffeeShop.API.Models;

public class Coffee
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
    
    [StringLength(500)]
    public string Description { get; set; } = string.Empty;
    
    [Required]
    [Range(0.01, 100.00)]
    public decimal Price { get; set; }
    
    public bool IsAvailable { get; set; } = true;
    
    [StringLength(50)]
    public string Category { get; set; } = string.Empty;
}