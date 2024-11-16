using BuildingBlocks.Exceptions;

namespace Catalog.API.Exceptions
{
    public class ProductNotFoundException :NotFoundEx
    {
        public ProductNotFoundException(Guid Id) : base($"Product with ID {Id} was not found") { }
        
        public ProductNotFoundException(string cat) : base($"Product with category {cat} was not found")
        {

        }
    }
}
