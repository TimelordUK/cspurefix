using Arrow.Threading.Tasks;
using PureFix.Examples.Shared;
using PureFix.Transport.Session;
using PureFix.Types;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Examples.Skeleton;

internal class SkeletonDI(AsyncWorkQueue q, ILogFactory factory, IFixClock clock, IFixConfig config)
    : AppHost<SkeletonSessionFactory, FixMessageFactory>(q, factory, clock, config)
{
}
