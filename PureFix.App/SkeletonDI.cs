using Arrow.Threading.Tasks;
using PureFIix.Test.Env.Skeleton;
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
    internal class SkeletonDI : AppHost<SkeletonSessionFactory, FixMessageFactory>
    {
        public SkeletonDI(AsyncWorkQueue q, ILogFactory factory, IFixClock clock, IFixConfig config) : base(q, factory, clock, config)
        {
        }
    }
}
