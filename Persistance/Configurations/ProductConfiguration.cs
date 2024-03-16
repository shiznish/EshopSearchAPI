using Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.Configurations;
public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(prop => prop.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(prop => prop.ShortDescription)
            .HasMaxLength(200);

        builder.OwnsOne(p => p.Price, priceBuilder =>
        {
            priceBuilder.Property(m => m.Currency).HasMaxLength(3);
            priceBuilder.Property(m => m.Amount)
                 .HasColumnType("decimal(18,2)");
        });

        builder.Property(prop => prop.CategoryId)
          .IsRequired();

        builder.HasOne(p => p.Category)
               .WithMany(c => c.Products)
               .HasForeignKey(p => p.CategoryId);

        builder.Property(p => p.Popularity)
                .IsRequired()
                .HasDefaultValue(0);

        builder.HasIndex(p => p.Popularity);
    }
}
