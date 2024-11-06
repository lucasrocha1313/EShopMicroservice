using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Data.Extensions;

public static class InitialData 
{
    public static IEnumerable<Customer> Customers => new List<Customer>
    {
        Customer.Create(CustomerId.Of(new Guid("34675627-bb82-432d-9524-ffaa6a6aadd1")), "Lucas", "lucas@example.com"),
        Customer.Create(CustomerId.Of(new Guid("a4b7fca3-29c2-4337-bf05-e7cb99b246b8")), "John", "john@example.com")
    };
    
    public static IEnumerable<Product> Products => new List<Product>
    {
        Product.Create(ProductId.Of(new Guid("5e061e54-5665-412e-8407-13000962b4e7")), "Iphone X", 500),
        Product.Create(ProductId.Of(new Guid("47dc42f0-00bb-499b-ae77-beaf550b3f24")), "Samsung 10", 400),
        Product.Create(ProductId.Of(new Guid("5310aeba-325f-4088-9ef5-37afc8ff40b8")), "Huawei Plus", 650),
        Product.Create(ProductId.Of(new Guid("bb8db93c-051c-4715-8f5f-972896fb98f4")), "Xiaomi Mi", 450),
    };

    public static IEnumerable<Order> OrdersWithItems
    {
        get
        {
            var address1 = Address.Of("Lucas", "Rocha", "lucas@example.com", "Rua Street Rue", "Brazil", "Minas Gerais", "10005");
            var address2 = Address.Of("john", "doe", "john@example.com", "Broadway No:1", "England", "Nottingham", "08050");

            var payment1 = Payment.Of("mehmet", "5555555555554444", "12/28", "355", 1);
            var payment2 = Payment.Of("john", "8885555555554444", "06/30", "222", 2);
            
            var customers = Customers.ToList();
            var products = Products.ToList();
            
            var order1 = Order.Create(
                OrderId.Of(Guid.NewGuid()),
                customers[0].Id,
                OrderName.Of("ORD_1"),
                shippingAddress: address1,
                billingAddress: address1,
                payment1);
            
            order1.Add(products[0].Id, 2, products[0].Price);
            order1.Add(products[1].Id, 1, products[0].Price);

            var order2 = Order.Create(
                OrderId.Of(Guid.NewGuid()),
                customers[1].Id,
                OrderName.Of("ORD_2"),
                shippingAddress: address2,
                billingAddress: address2,
                payment2);
            order2.Add(products[2].Id, 1, products[2].Price);
            order2.Add(products[3].Id, 2, products[3].Price);

            return new List<Order> { order1, order2 };
        }
    }
}