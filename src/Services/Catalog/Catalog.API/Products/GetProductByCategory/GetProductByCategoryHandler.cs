
using Catalog.API.Models;

namespace Catalog.API.Products.GetProductByCategory
{
    public record GetProductByCategoryQuery(string Category): IQuery<GetProductByCategoryResult>;
    public record class GetProductByCategoryResult(IEnumerable<Product> Products);
    internal class GetProductByCategoryHandler(IDocumentSession session, ILogger<GetProductByCategoryHandler> logger) :
        IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
    {
        public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting product by category from GetProductByCategoryHandler class called @{query}", query);
            var productByCategory = await session.Query<Product>().Where(pro => pro.Category.Contains(query.Category)).ToListAsync(cancellationToken);

            if (productByCategory.Count is 0)
            {
                throw new ProductNotFoundException(query.Category);
            }
            return new GetProductByCategoryResult(productByCategory);
        }
    }
}
