using Application.Core.Data.Repository;
using Domain.Orders;
using Persistance.DatabaseContext;

namespace Persistance.Repositories;
internal sealed class OrderRepository : GenericRepository<Order>, IOrderRepository
{
    public OrderRepository(ApplicationDbContext context)
           : base(context)
    {

    }
}
