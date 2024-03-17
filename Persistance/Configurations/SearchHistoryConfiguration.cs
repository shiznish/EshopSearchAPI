using Domain.Customers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.Configurations;
public class SearchHistoryConfiguration : IEntityTypeConfiguration<SearchHistory>
{

    public void Configure(EntityTypeBuilder<SearchHistory> builder)
    {

        builder.HasIndex(uq => uq.UserId);

        builder.Property(c => c.SortColumn)
              .HasMaxLength(100);

        builder.Property(c => c.SortOrder)
              .HasMaxLength(100);
    }
}
