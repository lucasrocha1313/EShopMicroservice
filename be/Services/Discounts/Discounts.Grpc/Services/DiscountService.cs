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
    /// <summary>
    /// Get discount
    /// </summary>
    /// <param name="request">Product name to be searched</param>
    /// <param name="context"></param>
    /// <returns>Coupon found if any</returns>
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

    /// <summary>
    /// Get all discounts
    /// </summary>
    /// <param name="request">No params</param>
    /// <param name="context"></param>
    /// <returns>All discounts</returns>
    public override async Task<GetAllDiscountsResponse> GetAllDiscounts(GetAllDiscountsRequest request, ServerCallContext context)
    {
        var coupons = await dbContext.Coupons.ToListAsync();

        var response = new GetAllDiscountsResponse();
        response.Coupons.AddRange(coupons.Select(BuildCouponModel));

        return response;
    }

    /// <summary>
    /// Create discount
    /// </summary>
    /// <param name="request">Coupon data</param>
    /// <param name="context"></param>
    /// <returns>Model created</returns>
    /// <exception cref="RpcException">If cupon date is NULL, it will not be created</exception>
    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var coupon = new Coupon
        {
            ProductName = request.ProductName,
            Description = request.Description,
            Amount = request.Amount
        };

        if (coupon is null)
        {
            throw new RpcException(new Status(StatusCode.Internal, "Coupon could not be created"));
        }
        
        dbContext.Coupons.Add(coupon);
        await dbContext.SaveChangesAsync();
        
        logger.LogInformation("Discount is successfully created. ProductName: {ProductName}", coupon.ProductName);
        
        return BuildCouponModel(coupon);
    }
    
    /// <summary>
    /// Update discount
    /// </summary>
    /// <param name="request">Coupon data to update</param>
    /// <param name="context"></param>
    /// <returns></returns>
    /// <exception cref="RpcException">If cupon date is NULL, it will not be updated</exception>
    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var coupon = new Coupon
        {
            Id = request.Id,
            ProductName = request.ProductName,
            Description = request.Description,
            Amount = request.Amount
        };

        if (coupon is null)
        {
            throw new RpcException(new Status(StatusCode.Internal, "Coupon could not be created"));
        }
        
        dbContext.Coupons.Update(coupon);
        await dbContext.SaveChangesAsync();
        
        logger.LogInformation("Discount is successfully updated. ProductName: {ProductName}", coupon.ProductName);
        
        return BuildCouponModel(coupon);
    }

    private static CouponModel BuildCouponModel(Coupon coupon)
    {
        var couponModel = new CouponModel
        {
            Id = coupon.Id,
            ProductName = coupon.ProductName,
            Description = coupon.Description,
            Amount = coupon.Amount
        };

        return couponModel;
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        var coupon = await dbContext.Coupons.FirstOrDefaultAsync(c => c.Id == request.Id);

        if (coupon is null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, "Coupon not found"));
        }

        dbContext.Coupons.Remove(coupon);
        await dbContext.SaveChangesAsync();

        logger.LogInformation("Discount is successfully deleted. ProductName: {ProductName}", coupon.ProductName);

        return new DeleteDiscountResponse
        {
            Success = true
        };
    }
}