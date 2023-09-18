using Identity.Application.Common.Persistence.EntityTypeConfigurations;
using Identity.Domain.AggregatesModel.FollowerAggregate;
using Identity.Domain.AggregatesModel.FollowingAggregate;
using Identity.Domain.AggregatesModel.SecurityAggregate;
using Identity.Domain.AggregatesModel.UserAggregate;
using Microsoft.EntityFrameworkCore;

namespace Identity.Application.Common.Persistence;

public class IdentityDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Security> Securities { get; set; }
    public DbSet<Follower> Followers { get; set; }
    public DbSet<Following> Followings { get; set; }

    public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new FollowerConfiguration());
        modelBuilder.ApplyConfiguration(new FollowingConfiguration());
    }
}
