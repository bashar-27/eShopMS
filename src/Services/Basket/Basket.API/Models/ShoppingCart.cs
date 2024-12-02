namespace Basket.API.Models
{
    public class ShoppingCart
    {
        public string UserName { get; set; } = default!;
        public List<ShoppingCartItem> Items { get; set; } = new();
        public decimal TotalPrice => Items.Sum(x => x.Price * x.Quantity);
    
        public ShoppingCart(string name)
        {
            UserName = name;
        }
        //Required for mapping
        public ShoppingCart()
        {
        }
    }
}
