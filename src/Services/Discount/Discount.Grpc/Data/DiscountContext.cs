using Discount.Grpc.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data
{
    public class DiscountContext :DbContext
    {
        public DiscountContext(DbContextOptions<DiscountContext> options) : base(options) { }
        public DbSet<Coupon> coupons { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Coupon>().HasData(
                new Coupon { Id = 1, ProductName = "IPhone 16 Pro", Description = "IPhone Description", Amount = 1500 },
                new Coupon { Id = 2, ProductName = "SAMSUNG S24", Description = "SAMSUNG Description", Amount = 1000 }

                );
        }
    }
}
