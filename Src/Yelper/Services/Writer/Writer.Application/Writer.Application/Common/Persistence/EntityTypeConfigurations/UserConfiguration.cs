using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Writer.Domain.AggregatesModel.UsersAggregate;

namespace Writer.Application.Common.Persistence.EntityTypeConfigurations;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
	public void Configure(EntityTypeBuilder<User> builder)
	{
		builder
			.HasKey(x => x.Id);
		builder
			.HasIndex(x => x.At);
	}
}
