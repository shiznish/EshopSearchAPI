using Application.Core.Data.Repository;
using Domain.Customers;
using Microsoft.EntityFrameworkCore;
using Persistance.DatabaseContext;

namespace Persistance.Repositories;
internal sealed class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
{
    public CustomerRepository(ApplicationDbContext context)
        : base(context)
    {

    }

    public async Task<bool> IsEmailUniqueAsync(string email)
    {
        return !await _dbSet.AnyAsync(c => c.Email == email);
    }
}
