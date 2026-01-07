using PureFix.Examples.Shared;
using PureFix.Transport.Session;
using PureFix.Types;
using PureFix.Types.FIX44;

namespace PureFix.Examples.Skeleton;

internal class SkeletonDI(ILogFactory factory, IFixClock clock, IFixConfig config)
    : AppHost<SkeletonSessionFactory, FixMessageFactory, Fix44SessionMessageFactory>(factory, clock, config)
{
}
