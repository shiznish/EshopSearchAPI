using Domain.Customers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.Configurations;
public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {

        builder.HasKey(o => o.Id);

        builder.Property(c => c.IdentityGuid)
               .IsRequired();

        builder.Property(c => c.Name)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(c => c.Email).HasMaxLength(255);

        builder.OwnsOne(c => c.Address, address =>
        {
            address.Property(a => a.Street)
                   .IsRequired()
                   .HasMaxLength(100)
                   .HasColumnType("nvarchar(100)");

            address.Property(a => a.City)
                   .IsRequired()
                   .HasMaxLength(50)
                   .HasColumnType("nvarchar(50)");
        });
    }
}