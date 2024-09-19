using Infrastructure.Persistence.Constants;
using Infrastructure.Persistence.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

/// <summary>
/// Configuration for the "OutboxMessage" entity in the database context.
/// </summary>
internal sealed class OutboxMessageConfigurations : IEntityTypeConfiguration<OutboxMessage>
{
    /// <summary>
    /// Configures the "OutboxMessage" entity, including table name and primary key.
    /// </summary>
    /// <param name="builder">The entity type builder for the "OutboxMessage" entity.</param>
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        // Set the table name for the "OutboxMessage" entity.
        builder.ToTable(TableNames.OutboxMessages);

        // Define the primary key for the "OutboxMessage" entity.
        builder.HasKey(om => om.Id);
    }
}
