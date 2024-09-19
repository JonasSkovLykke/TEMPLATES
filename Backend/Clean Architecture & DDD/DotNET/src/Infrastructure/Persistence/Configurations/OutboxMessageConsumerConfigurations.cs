using Infrastructure.Persistence.Constants;
using Infrastructure.Persistence.Outbox;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Configurations;

/// <summary>
/// Configuration for the "OutboxMessageConsumer" entity in the database context.
/// </summary>
internal sealed class OutboxMessageConsumerConfigurations : IEntityTypeConfiguration<OutboxMessageConsumer>
{
    /// <summary>
    /// Configures the "OutboxMessageConsumer" entity, including table name and key settings.
    /// </summary>
    /// <param name="builder">The entity type builder for the "OutboxMessageConsumer" entity.</param>
    public void Configure(EntityTypeBuilder<OutboxMessageConsumer> builder)
    {
        // Set the table name for the "OutboxMessageConsumer" entity.
        builder.ToTable(TableNames.OutboxMessageConsumers);

        // Define the composite primary key for the "OutboxMessageConsumer" entity.
        builder.HasKey(omc => new
        {
            omc.Id,
            omc.Name
        });
    }
}
