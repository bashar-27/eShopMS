using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services
{
    public class DiscountService(DiscountContext dbContext , ILogger<DiscountService> logger) : DiscountProtoService.DiscountProtoServiceBase
    {
        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await dbContext.coupons.FirstOrDefaultAsync(x=>x.ProductName == request.ProductName);

            if (coupon is null) 
                coupon = new Coupon { ProductName = "No Discount", Amount = 0 , Description="" };

            logger.LogInformation("Discount is retrieved for ProductName : {productName} , Amount: {amount}", coupon.ProductName, coupon.Amount);
                
                    var couponModel = coupon.Adapt<CouponModel>();
                    return couponModel;
        }

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>();
        if (coupon is null)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object."));

        dbContext.coupons.Add(coupon);
        await dbContext.SaveChangesAsync();

        logger.LogInformation("Discount is successfully created. ProductName : {ProductName}", coupon.ProductName);

        var couponModel = coupon.Adapt<CouponModel>();
        return couponModel;
        
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {

           var coupon = request.Coupon.Adapt<Coupon>();
            if(coupon is null)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object."));
            }
            dbContext.coupons.Update(coupon);
            await dbContext.SaveChangesAsync();
            logger.LogInformation("Discount is successfully updated. ProductName : {ProductName}", coupon.ProductName);
           
            var couponModel = coupon.Adapt<CouponModel>();
            return couponModel;
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var coupon = await dbContext.coupons.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);
            if (coupon is null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount with ProductName={request.ProductName} is not found."));
            }
            dbContext.coupons.Remove(coupon);
            await dbContext.SaveChangesAsync();
            
            return new DeleteDiscountResponse { Success = true};
        }
    }
}
