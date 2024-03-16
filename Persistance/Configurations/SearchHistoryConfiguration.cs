using Domain.Customers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.Configurations;
public class SearchHistoryConfiguration : IEntityTypeConfiguration<SearchHistory>
{

    public void Configure(EntityTypeBuilder<SearchHistory> builder)
    {
        builder.HasOne(uq => uq.Customer)
             .WithMany()
             .HasForeignKey(uq => uq.CustomerId)
             .IsRequired();

        builder.HasIndex(uq => uq.CustomerId);

        builder.Property(c => c.SortColumn)
              .HasMaxLength(100);

        builder.Property(c => c.SortOrder)
              .HasMaxLength(100);
    }
}
