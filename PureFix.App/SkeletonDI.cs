using Arrow.Threading.Tasks;
using PureFix.Test.Env.Skeleton;
using PureFix.Transport.Session;
using PureFix.Types;
using PureFix.Types.FIX44.QuickFix.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.ConsoleApp
{
    internal class SkeletonDI(AsyncWorkQueue q, ILogFactory factory, IFixClock clock, IFixConfig config) 
        : AppHost<SkeletonSessionFactory, FixMessageFactory>(q, factory, clock, config)
    {
    }
}
