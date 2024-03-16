namespace Application.Core.Abstractions.Sort.Relevance.Product;
using Domain.Products;
public interface IProductSearchService
{
    Task<IEnumerable<Product>> SearchAndSortProductsAsync(IEnumerable<Product> products, string searchTerm);
}
