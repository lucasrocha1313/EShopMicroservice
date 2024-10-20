using Ordering.Domain.Abstractions;
using Ordering.Domain.ValueObjects;

namespace Ordering.Domain.Models;

public class Product: Entity<ProductId>
{
    public string Name { get; private set; } = default!;
    public decimal Price { get; private set; }
    
    public static Product Create(ProductId id, string name, decimal price)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price, nameof(price));
        
        return new Product
        {
            Id = id,
            Name = name,
            Price = price
        };
    }
}