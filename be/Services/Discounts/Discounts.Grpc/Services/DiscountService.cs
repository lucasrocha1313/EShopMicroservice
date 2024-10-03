using Discount.Grpc;
using Discounts.Grpc.Data;
using Discounts.Grpc.Models;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;

namespace Discounts.Grpc.Services;

/// <summary>
/// Discount service
/// </summary>
public class DiscountService(DiscountContext dbContext, ILogger<DiscountService> logger): 
    Discount.Grpc.DiscountService.DiscountServiceBase
{
    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var coupon = await dbContext.Coupons.FirstOrDefaultAsync(c => c.ProductName == request.ProductName) ?? new Coupon
        {
            ProductName = "No Discount",
            Description = "No Discount",
            Amount = 0
        };

        logger.LogInformation("Discount is retrieved for ProductName: {ProductName}, Amount: {Amount}",
            coupon.ProductName, coupon.Amount);

        var couponModel = new CouponModel
        {
            Id = coupon.Id,
            ProductName = coupon.ProductName,
            Description = coupon.Description,
            Amount = coupon.Amount
        };
        
        return couponModel;
    }

    public override Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        return base.CreateDiscount(request, context);
    }
    
    public override Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        return base.UpdateDiscount(request, context);
    }
    
    public override Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        return base.DeleteDiscount(request, context);
    }
}