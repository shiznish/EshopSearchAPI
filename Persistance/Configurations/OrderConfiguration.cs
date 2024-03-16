using Domain.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.Configurations;
public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {

        builder.HasKey(o => o.Id);

        builder.Property(o => o.InvoiceNo)
               .IsRequired();

        builder.Property(o => o.CustomerId)
               .IsRequired();

        builder.HasOne(o => o.Customer)
               .WithMany()
               .HasForeignKey(o => o.CustomerId);

        builder.OwnsOne(o => o.OrderAmount, amountBuilder =>
        {
            amountBuilder.Property(m => m.Currency)
                .HasMaxLength(3);

            amountBuilder.Property(m => m.Amount)
                .HasColumnType("decimal(18,2)");
        });

        builder.OwnsOne(o => o.ShippingAddress, addressBuilder =>
        {
            addressBuilder.Property(a => a.Street)
                .HasMaxLength(100);

            addressBuilder.Property(a => a.City)
                .HasMaxLength(50);
        });

        builder.HasMany(o => o.LineItems)
               .WithOne()
               .HasForeignKey(li => li.OrderId);
    }
}
