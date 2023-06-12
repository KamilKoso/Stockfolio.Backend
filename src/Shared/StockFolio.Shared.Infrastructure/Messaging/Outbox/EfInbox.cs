using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Stockfolio.Shared.Abstractions.Time;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Stockfolio.Shared.Infrastructure.Messaging.Outbox;

internal sealed class EfInbox<T> : IInbox where T : DbContext
{
    private readonly T _dbContext;
    private readonly DbSet<InboxMessage> _set;
    private readonly IClock _clock;
    private readonly ILogger<EfInbox<T>> _logger;

    public bool Enabled { get; }

    public EfInbox(T dbContext, IClock clock, OutboxOptions outboxOptions, ILogger<EfInbox<T>> logger)
    {
        _dbContext = dbContext;
        _set = dbContext.Set<InboxMessage>();
        _clock = clock;
        _logger = logger;
        Enabled = outboxOptions.Enabled;
    }

    public async Task HandleAsync(Guid messageId, string name, Func<Task> handler)
    {
        var module = _dbContext.GetModuleName();
        if (!Enabled)
        {
            _logger.LogWarning("Outbox is disabled ('{Module}'), incoming messages won't be processed.", module);
            return;
        }

        _logger.LogTrace("Received a message with ID: '{MessageId}' to be processed ('{Module}').", messageId, module);
        if (await _set.AnyAsync(m => m.Id == messageId && m.ProcessedAt != null))
        {
            _logger.LogTrace("Message with ID: '{MessageId}' was already processed ('{Module}').", messageId, module);
            return;
        }

        _logger.LogTrace("Processing a message with ID: '{MessageId}' ('{Module}')...", messageId, module);

        var inboxMessage = new InboxMessage
        {
            Id = messageId,
            Name = name,
            ReceivedAt = _clock.CurrentDate()
        };

        var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            await handler();
            inboxMessage.ProcessedAt = _clock.CurrentDate();
            await _set.AddAsync(inboxMessage);
            await _dbContext.SaveChangesAsync();

            if (transaction is not null)
            {
                await transaction.CommitAsync();
            }

            _logger.LogTrace("Processed a message with ID: '{MessageId}' ('{Module}').", messageId, module);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "There was an error when processing a message with ID: '{MessageId}' ('{Module}').", messageId, module);
            if (transaction is not null)
            {
                await transaction.RollbackAsync();
            }

            throw;
        }
        finally
        {
            {
                await transaction.DisposeAsync();
            }
        }
    }

    public async Task CleanupAsync(DateTime? to = null)
    {
        var module = _dbContext.GetModuleName();
        if (!Enabled)
        {
            _logger.LogWarning("Outbox is disabled ('{Module}'), incoming messages won't be cleaned up.", module);
            return;
        }

        var dateTo = to ?? _clock.CurrentDate();
        var sentMessages = await _set.Where(x => x.ReceivedAt <= dateTo).ToListAsync();
        if (!sentMessages.Any())
        {
            _logger.LogTrace("No received messages found in inbox ('{Module}') till: {DateTo}.", module, dateTo);
            return;
        }

        _logger.LogInformation("Found {SentMessagesCount} received messages in inbox ('{Module}') till: {DateTo}, cleaning up...", sentMessages.Count, module, dateTo);
        _set.RemoveRange(sentMessages);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("Removed {SentMessagesCount} received messages from inbox ('{Module}') till: {DateTo}.", sentMessages.Count, module, dateTo);
    }
}