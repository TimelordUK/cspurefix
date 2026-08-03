using Microsoft.Extensions.DependencyInjection;
using PureFix.Transport.Session;
using PureFix.Types;
using PureFix.Types.Config;


namespace PureFix.Examples.Shared;

public class AppHost<T, U, V> : BaseAppDI
    where T : class, ISessionFactory
    where U : class, IFixMessageFactory
    where V : class, ISessionMessageFactory
{
    protected AppHost(ILogFactory factory, IFixClock clock, IFixConfig config)
        : base(factory, clock, config)
    {
        _builder.Services.AddSingleton<ISessionFactory, T>();
        _builder.Services.AddSingleton<IFixMessageFactory, U>();
        _builder.Services.AddSingleton<ISessionMessageFactory, V>();

        // Hands each connection its own parser, encoder, session description and session
        // message factory. The message factory has to be per-scope because it reads
        // SenderCompID/TargetCompID off the description whenever it builds a header - one
        // shared instance would stamp a wildcard acceptor's most recent peer onto every
        // other live session's outbound messages.
        _builder.Services.AddSingleton<ISessionScopeFactory>(sp =>
            new DefaultSessionScopeFactory(
                sp.GetRequiredService<IFixConfig>(),
                sp.GetRequiredService<IFixClock>(),
                description => ActivatorUtilities.CreateInstance<V>(sp, description)));

        AppHost = _builder.Build();

        // Set the MessageFactory on the config from DI
        var sessionMessageFactory = AppHost.Services.GetRequiredService<ISessionMessageFactory>();
        if (config is PureFix.Transport.FixConfig fixConfig)
        {
            fixConfig.MessageFactory = sessionMessageFactory;
        }
    }
}
