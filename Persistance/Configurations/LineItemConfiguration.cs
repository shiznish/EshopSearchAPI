using Domain.Orders;
using Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.Configurations;
public class LineItemConfiguration : IEntityTypeConfiguration<LineItem>
{
    public void Configure(EntityTypeBuilder<LineItem> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne<Product>()
           .WithMany()
           .HasForeignKey(li => li.ProductId);

        builder.OwnsOne(li => li.Price)
            .Property(p => p.Amount)
            .HasColumnType("decimal(18,2)"); ;


    }
}