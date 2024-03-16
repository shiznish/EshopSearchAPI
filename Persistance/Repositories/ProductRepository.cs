using Application.Core.Data.Repository;
using Domain.Products;
using Persistance.DatabaseContext;

namespace Persistance.Repositories;
internal sealed class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(ApplicationDbContext dbcontext)
        : base(dbcontext)
    {

    }

}
