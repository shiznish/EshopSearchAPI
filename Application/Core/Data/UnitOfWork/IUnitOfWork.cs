using Application.Core.Data.Repository;
using Domain.Common;

namespace Application.Core.Data.UnitOfWork;
public interface IUnitOfWork : IDisposable
{
    ICustomerRepository CustomerRepository { get; }
    IProductRepository ProductRepository { get; }
    IOrderRepository OrderRepository { get; }
    IGenericRepository<T> GetRepository<T>()
        where T : BaseEntity;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
