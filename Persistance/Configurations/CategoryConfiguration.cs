using Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.Configurations;
public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(prop => prop.Name)
            .HasMaxLength(256)
            .IsRequired();

        builder.HasMany(prop => prop.Products)
                    .WithOne(prop => prop.Category)
                    .HasForeignKey(p => p.CategoryId); ;
    }
}
