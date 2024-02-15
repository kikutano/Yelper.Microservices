using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Writer.Domain.AggregatesModel.YelpsAggregate;

namespace Writer.Application.Common.Persistence.EntityTypeConfigurations;

internal class YelpConfiguration : IEntityTypeConfiguration<Yelp>
{
    public void Configure(EntityTypeBuilder<Yelp> builder)
    {
        builder
            .HasKey(x => x.Id);
    }
}
