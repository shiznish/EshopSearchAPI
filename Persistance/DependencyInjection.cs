using Application.Core.Data.Repository;
using Application.Core.Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistance.DatabaseContext;
using Persistance.Infrastructure;
using Persistance.Repositories;
using Persistance.UnitOfWrok;

namespace Persistance;
public static class DependencyInjection
{
    public static IServiceCollection AddPersistance(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString(ConnectionString.SettingsKey);

        services.AddSingleton(new ConnectionString(connectionString));

        // Add DbContext
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));

        // Register repositories
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();

        // Register unit of work
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
