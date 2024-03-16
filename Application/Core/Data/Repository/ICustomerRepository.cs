using Domain.Customers;

namespace Application.Core.Data.Repository;
public interface ICustomerRepository : IGenericRepository<Customer>
{
    Task<bool> IsEmailUniqueAsync(string email);
}
