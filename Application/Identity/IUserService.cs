using Domain.Customers;

namespace Application.Identity;
public interface IUserService
{
    Task<List<Customer>> GetCustomers();
    Task<Customer> GetCustomer(string customerId);
}
