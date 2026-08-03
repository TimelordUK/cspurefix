using PureFix.Transport.Recovery;
using PureFix.Transport.Session;
using PureFix.Types;


namespace PureFix.Examples.Skeleton;

/// <summary>
/// Builds a session per connection. Config, parser and encoder come from the scope, never
/// from the container - see <see cref="ISessionScope"/>.
/// </summary>
public class SkeletonSessionFactory(
    IFixLogRecovery? fixLogRecovery,
    ILogFactory logFactory,
    IFixMessageFactory fixMessageFactory,
    IFixClock clock)
    : ISessionFactory
{
    public FixSession MakeSession(ISessionScope scope)
    {
        return new TestAsciiSkeleton(
            scope.Config,
            fixLogRecovery,
            logFactory,
            fixMessageFactory,
            scope.Parser,
            scope.Encoder,
            clock);
    }
}
