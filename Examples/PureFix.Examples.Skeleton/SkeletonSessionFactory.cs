using Arrow.Threading.Tasks;
using PureFix.Buffer;
using PureFix.Transport.Recovery;
using PureFix.Transport.Session;
using PureFix.Types;


namespace PureFix.Examples.Skeleton;

public class SkeletonSessionFactory(
    IFixConfig config,
    IFixLogRecovery? fixLogRecovery,
    ILogFactory logFactory,
    IFixMessageFactory fixMessageFactory,
    IMessageParser parser,
    IMessageEncoder encoder,
    AsyncWorkQueue q,
    IFixClock clock)
    : ISessionFactory
{
    public FixSession MakeSession()
    {
        return new TestAsciiSkeleton(config, fixLogRecovery, logFactory, fixMessageFactory, parser, encoder, q, clock);
    }
}
