using Discounts.Grpc.Models;
using Microsoft.EntityFrameworkCore;

namespace Discounts.Grpc.Data;

/// <summary>
/// Discount Context
/// </summary>
public class DiscountContext: DbContext
{
    public DbSet<Coupon> Coupons { get; set; } = default!;

    /// <summary>
    /// Constructor For DiscountContext
    /// </summary>
    /// <param name="options"></param>
    public DiscountContext(DbContextOptions<DiscountContext> options)
        : base(options)
    {
        
    }
    
    /// <summary>
    /// Seed data for DiscountContext
    /// </summary>
    /// <param name="modelBuilder"></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Coupon>().HasData(
            new Coupon { Id = 1, ProductName = "IPhone X", Description = "IPhone Discount", Amount = 150 },
            new Coupon { Id = 2, ProductName = "Samsung 10", Description = "Samsung Discount", Amount = 100 }
        );
    }
}