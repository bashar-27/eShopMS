namespace Catalog.API.Products.GetProductById
{
    public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;
    
    public record GetProductByIdResult(Product Product);

internal class GetProductByIdHandler(IDocumentSession session ,ILogger<GetProductByIdHandler> logger): IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult > Handle (GetProductByIdQuery query , CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting product by ID from GetProductByIdHandler class called @{query}",query);
            var product = await session.Query<Product>().FirstOrDefaultAsync(pro=>pro.Id == query.Id, cancellationToken);
            if(product is null)
            {
                throw new ProductNotFoundException(query.Id);
            }
            return new GetProductByIdResult(product);
        }
    }
}
