using Application.Core.Abstractions.Common;
using Domain.Common;
using Domain.Customers;
using Domain.Orders;
using Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Reflection;

namespace Persistance.DatabaseContext;
public class ApplicationDbContext : DbContext
{
    private readonly IDateTime _dateTime;
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IDateTime dateTime) : base(options)
    {
        _dateTime = dateTime;
    }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<LineItem> LineItems { get; set; }
    public DbSet<Customer> Customers { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }

    private void UpdateAuditableEntities(DateTime utcNow)
    {
        foreach (EntityEntry<BaseEntity> entityEntry in ChangeTracker.Entries<BaseEntity>())
        {
            if (entityEntry.State == EntityState.Added)
            {
                entityEntry.Property(nameof(BaseEntity.CreatedDate)).CurrentValue = utcNow;
            }

            if (entityEntry.State == EntityState.Modified)
            {
                entityEntry.Property(nameof(BaseEntity.LastModifiedDate)).CurrentValue = utcNow;
            }
        }
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        //var domainEvents = ChangeTracker.Entries<BaseEntity>()
        //    .Select(e => e.Entity)
        //    .Where(e => e.GetDomainEvents().Any())
        //    .SelectMany(e => e.GetDomainEvents());

        //var result = await base.SaveChangesAsync(cancellationToken);

        //foreach (var domainEvent in domainEvents)
        //{
        //    await _publisher.Publish(domainEvent, cancellationToken);
        //}
        DateTime utcNow = _dateTime.UtcNow;

        UpdateAuditableEntities(utcNow);

        return await base.SaveChangesAsync(cancellationToken);
    }


}
