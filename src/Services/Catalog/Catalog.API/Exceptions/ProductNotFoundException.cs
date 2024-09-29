namespace Catalog.API.Exceptions
{
    public class ProductNotFoundException :Exception
    {
        public ProductNotFoundException(Guid Id) : base($"Product with ID {Id} was not found") { }
        
        public ProductNotFoundException(string cat) : base($"Product with category {cat} was not found")
        {

        }
    }
}
