using Arrow.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
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
    internal class SkeletonDI : BaseAppDI
    {
        public SkeletonDI(AsyncWorkQueue q, ILogFactory factory, IFixClock clock, IFixConfig config) : base(q, factory, clock, config)
        {
            _builder.Services.AddSingleton<ISessionFactory, SkeletonSessionFactory>();
            _builder.Services.AddSingleton<IFixMessageFactory, FixMessageFactory>();
            AppHost = _builder.Build();
        }
    }
}
