using Catalog.API.Enums;
using Catalog.API.Models;
using Marten;
using Marten.Schema;

namespace Catalog.API.Data;

public class CatalogInitialData: IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        await using var session = store.LightweightSession();        
            
        //TODO - should check if Prod env
        if(await session.Query<Product>().AnyAsync(token: cancellation))
            return;
        
        session.Store(GetPreConfiguredProducts());
        await session.SaveChangesAsync(cancellation);
    }

    private IEnumerable<Product> GetPreConfiguredProducts()
    {
        return new List<Product>
        {
            new()
            {
                Id = new Guid("50a7ed07-42df-4655-bc29-1cdd2b728d39"),
                Name = "Iphone X",
                Category = [CategoryEnum.SmartPhone],
                Description = "This phone is company's biggest change to its flagship smartphone in years. It includes a borderless.",
                ImageFile = "product-1.png",
                Price = 950.00m,
            },
            new()
            {
                Id = new Guid("150fcb97-a877-4edd-8a65-03eae4f79be7"),
                Name = "Samsung 10",
                Category = [CategoryEnum.SmartPhone],
                Description = "This phone is company's biggest change to its flagship smartphone in years. It includes a borderless.",
                ImageFile = "product-2.png",
                Price = 840.00m,
            },
            new()
            {
                Id = new Guid("5192c488-ee35-45c2-9091-2d8152e24518"),
                Name = "Huawei Plus",
                Category = [CategoryEnum.SmartPhone],
                Description = "This phone is company's biggest change to its flagship smartphone in years. It includes a borderless.",
                ImageFile = "product-3.png",
                Price = 650.00m,
            },
            new()
            {
                Id = new Guid("72e74f09-f26d-41e2-a71d-5ac117f05821"),
                Name = "Xiaomi Mi 9",
                Category = [CategoryEnum.SmartPhone],
                Description = "This phone is company's biggest change to its flagship smartphone in years. It includes a borderless.",
                ImageFile = "product-4.png",
                Price = 470.00m,
            },
            new()
            {
                Id = new Guid("d4f42590-7c3d-4459-ae14-9b4deb4a3b60"),
                Name = "Sony Xperia XZ3",
                Category = [CategoryEnum.SmartPhone],
                Description = "This phone is company's biggest change to its flagship smartphone in years. It includes a borderless.",
                ImageFile = "product-5.png",
                Price = 580.00m,
            },
            new()
            {
                Id = new Guid("a1a35567-40ed-4de6-8a00-967948726c94"),
                Name = "LG G7 ThinQ",
                Category = [CategoryEnum.SmartPhone],
                Description = "This phone is company's biggest change to its flagship smartphone in years. It includes a borderless.",
                ImageFile = "product-6.png",
                Price = 470.00m,
            },
            new()
            {
                Id = new Guid("9b141155-ae41-4612-ab3e-da11534de586"),
                Name = "OnePlus 6T",
                Category = [CategoryEnum.SmartPhone],
                Description = "This phone is company's biggest change to its flagship smartphone in years. It includes a borderless.",
                ImageFile = "product-7.png",
                Price = 550.00m,
            },
        };
    }
}