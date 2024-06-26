﻿using Microsoft.EntityFrameworkCore;
using Writer.Application.Common.Persistence.EntityTypeConfigurations;
using Writer.Domain.AggregatesModel.UsersAggregate;
using Writer.Domain.AggregatesModel.YelpsAggregate;

namespace Writer.Application.Common.Persistence;

public class WriterDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Yelp> Yelps { get; set; }

    public WriterDbContext(DbContextOptions<WriterDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new UserConfiguration());
    }
}
