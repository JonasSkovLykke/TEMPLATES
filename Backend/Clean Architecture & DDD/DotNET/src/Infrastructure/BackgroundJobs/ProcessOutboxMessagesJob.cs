using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Polly;
using Quartz;
using SharedKernel.Interfaces;
using SharedKernel.Primitives;

namespace Infrastructure.BackgroundJobs;

/// <summary>
/// A Quartz.NET job responsible for processing outbox messages and publishing domain events.
/// </summary>
[DisallowConcurrentExecution]
public class ProcessOutboxMessagesJob(
    DotNETDbContext _dbContext,
    IPublisher _publisher,
    IDateTimeProvider _dateTimeProvider) : IJob
{
    private static readonly JsonSerializerSettings JsonSerializerSettings = new()
    {
        TypeNameHandling = TypeNameHandling.All
    };

    /// <summary>
    /// Executes the job to process and publish outbox messages.
    /// </summary>
    /// <param name="context">The Quartz.NET job execution context.</param>
    public async Task Execute(IJobExecutionContext context)
    {
        var messages = await _dbContext
            .OutboxMessages
            .Where(m => m.ProcessedOnDateTimeOffset == null &&
                        m.Error == null)
            .OrderBy(m => m.OccurredOnDateTimeOffset)
            .Take(20)
            .ToListAsync(context.CancellationToken);

        foreach (var outboxMessage in messages)
        {
            var domainEvent = JsonConvert
                .DeserializeObject<IDomainEvent>(
                    outboxMessage.Content,
                    JsonSerializerSettings);

            if (domainEvent is null)
            {
                continue;
            }

            var policy = Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(
                    3,
                    attempt => TimeSpan.FromMilliseconds(50 * attempt));

            var result = await policy.ExecuteAndCaptureAsync(() =>
                _publisher.Publish(
                    domainEvent,
                    context.CancellationToken));

            outboxMessage.Error = result.FinalException?.ToString();
            outboxMessage.ProcessedOnDateTimeOffset = _dateTimeProvider.Now;

            await _dbContext.SaveChangesAsync();
        }
    }
}
