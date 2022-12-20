using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Stockfolio.Shared.Abstractions.Messaging;
using Stockfolio.Shared.Infrastructure.Messaging.Brokers;
using Stockfolio.Shared.Infrastructure.Messaging.Contexts;
using Stockfolio.Shared.Infrastructure.Messaging.Dispatchers;

namespace Stockfolio.Shared.Infrastructure.Messaging;

public static class Extensions
{
    private const string SectionName = "messaging";

    public static IServiceCollection AddMessaging(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IMessageBroker, InMemoryMessageBroker>();
        services.AddTransient<IAsyncMessageDispatcher, AsyncMessageDispatcher>();
        services.AddSingleton<IMessageChannel, MessageChannel>();
        services.AddSingleton<IMessageContextProvider, MessageContextProvider>();
        services.AddSingleton<IMessageContextRegistry, MessageContextRegistry>();

        var messagingOptions = configuration.GetOptions<MessagingOptions>(SectionName);
        services.AddSingleton(messagingOptions);

        if (messagingOptions.UseAsyncDispatcher)
        {
            services.AddHostedService<AsyncDispatcherJob>();
        }

        return services;
    }
}