
using Marten.Pagination;

namespace Catalog.API.Products.GetProducts
{
    public record GetProductsQuery(int? pageNumber = 1 , int? pageSize = 5): IQuery<GetProductsResult>;
    public record GetProductsResult (IEnumerable<Product> Products);
    internal class GetProductsQueryHandler(IDocumentSession session  ): IQueryHandler<GetProductsQuery, GetProductsResult>
    {
        public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            var products = await session.Query<Product>().ToPagedListAsync(query.pageNumber?? 1 , query.pageSize??5 , cancellationToken);
            return new GetProductsResult(products);
        }

       
    }
}
