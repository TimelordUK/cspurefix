using PureFix.Transport.Recovery;
using PureFix.Transport.Session;
using PureFix.Types;


namespace PureFix.Examples.TradeCapture;

/// <summary>
/// Builds a session per connection. Config, parser and encoder come from the scope, never
/// from the container - see <see cref="ISessionScope"/>.
/// </summary>
public class TradeCaptureSessionFactory : ISessionFactory
{
    private readonly IFixLogRecovery? _fixLogRecovery;
    private readonly ILogFactory _logFactory;
    private readonly IFixMessageFactory _fixMessageFactory;
    private readonly IFixClock _clock;

    public TradeCaptureSessionFactory(
        IFixLogRecovery? fixLogRecovery,
        ILogFactory logFactory,
        IFixMessageFactory fixMessageFactory,
        IFixClock clock)
    {
        _fixLogRecovery = fixLogRecovery;
        _logFactory = logFactory;
        _fixMessageFactory = fixMessageFactory;
        _clock = clock;
    }

    public FixSession MakeSession(ISessionScope scope)
    {
        if (scope.Config.IsInitiator())
        {
            return new TradeCaptureClient(
                scope.Config, _fixLogRecovery, _logFactory, _fixMessageFactory, scope.Parser, scope.Encoder, _clock);
        }

        return new TradeCaptureServer(
            scope.Config, _fixLogRecovery, _logFactory, _fixMessageFactory, scope.Parser, scope.Encoder, _clock);
    }
}
