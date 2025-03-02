using Catalog.API.Enums;

namespace Catalog.API.Responses;

public class ProductResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public List<CategoryEnum> Category { get; set; } = [];
    public string Description { get; set; } = default!;
    public string ImageFile { get; set; } = default!;
    public decimal Price { get; set; }
}
