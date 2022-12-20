using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Stockfolio.Shared.Abstractions.Events;

namespace Stockfolio.Shared.Infrastructure.Messaging.Outbox;

public static class Extensions
{
    public static IServiceCollection AddOutbox<T>(this IServiceCollection services, IConfiguration configuration) where T : DbContext
    {
        var outboxOptions = configuration.GetOptions<OutboxOptions>("outbox");
        if (!outboxOptions.Enabled)
        {
            return services;
        }

        services.AddTransient<IInbox, EfInbox<T>>();
        services.AddTransient<IOutbox, EfOutbox<T>>();
        services.AddTransient<EfInbox<T>>();
        services.AddTransient<EfOutbox<T>>();

        using var serviceProvider = services.BuildServiceProvider();
        serviceProvider.GetRequiredService<InboxTypeRegistry>().Register<EfInbox<T>>();
        serviceProvider.GetRequiredService<OutboxTypeRegistry>().Register<EfOutbox<T>>();

        return services;
    }

    public static IServiceCollection AddOutbox(this IServiceCollection services, IConfiguration configuration)
    {
        var outboxOptions = configuration.GetOptions<OutboxOptions>("outbox");
        services.AddSingleton(outboxOptions);
        services.AddSingleton(new InboxTypeRegistry());
        services.AddSingleton(new OutboxTypeRegistry());
        services.AddSingleton<IOutboxBroker, OutboxBroker>();
        if (!outboxOptions.Enabled)
        {
            return services;
        }

        services.TryDecorate(typeof(IEventHandler<>), typeof(InboxEventHandlerDecorator<>));
        services.AddHostedService<OutboxProcessor>();
        services.AddHostedService<InboxCleanupProcessor>();
        services.AddHostedService<OutboxCleanupProcessor>();

        return services;
    }
}