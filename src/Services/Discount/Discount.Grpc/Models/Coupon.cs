namespace Discount.Grpc.Models
{
    public class Coupon
    {
        public string Id { get; set; } = default!;
        public string ProductName { get; set; } = default!;
        public string Description { get; set; } = default!;
        public int Amount { get; set; } = default!;
    }
}
