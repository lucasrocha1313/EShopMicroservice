using Ordering.Domain.Exceptions;

namespace Ordering.Domain.ValueObjects;

public record ProductId
{
    public Guid Value { get;}
    
    private ProductId(Guid value) => Value = value;

    public static ProductId Of(Guid productIdValue)
    {
        ArgumentNullException.ThrowIfNull(productIdValue, nameof(ProductId));
        if (productIdValue == Guid.Empty)
            throw new DomainException("Product id cannot be empty");
        
        return new ProductId(productIdValue);
    }
}