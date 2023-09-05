using Identity.Domain.AggregatesModel.UserAggregate;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Persistence;

public class IdentityDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
}
