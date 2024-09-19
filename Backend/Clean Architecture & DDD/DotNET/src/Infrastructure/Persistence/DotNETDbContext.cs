using Domain.UserAggregate;
using Infrastructure.Persistence.Outbox;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Persistence;

public sealed class DotNETDbContext : DbContext
{
    public DotNETDbContext(
        DbContextOptions<DotNETDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("dotnet");

        modelBuilder
            .ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }

    /// <summary>
    /// Gets or sets the DbSet for the OutboxMessage entities (used for the Outbox Pattern).
    /// </summary>
    public DbSet<OutboxMessage> OutboxMessages { get; set; }

    /// <summary>
    /// Gets or sets the DbSet for the OutboxMessageConsumer entities (used for the Outbox Pattern).
    /// </summary>
    public DbSet<OutboxMessageConsumer> OutboxMessageConsumers { get; set; }

    public DbSet<User> Users { get; set; }
}
