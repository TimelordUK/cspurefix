using Arrow.Threading.Tasks;
using PureFix.Examples.Shared;
using PureFix.Transport.Session;
using PureFix.Types;
using PureFix.Types.FIX44;

namespace PureFix.Examples.Skeleton;

internal class SkeletonDI(AsyncWorkQueue q, ILogFactory factory, IFixClock clock, IFixConfig config)
    : AppHost<SkeletonSessionFactory, FixMessageFactory, Fix44SessionMessageFactory>(q, factory, clock, config)
{
}
