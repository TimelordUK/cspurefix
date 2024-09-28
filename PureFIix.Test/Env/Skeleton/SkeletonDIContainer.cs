using Arrow.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PureFix.Buffer;
using PureFix.Transport.Session;
using PureFix.Types;
using PureFix.Types.FIX44.QuickFix.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFIix.Test.Env.Skeleton
{
    internal class SkeletonDIContainer
    {
        public IHost AppHost { get; private set; }
  
        public SkeletonDIContainer(AsyncWorkQueue q, IFixClock clock, IFixConfig config)
        {
            var builder = Host.CreateApplicationBuilder();
            builder.BuildCommon(q, clock, config);
            builder.Services.AddSingleton<IFixMessageFactory, FixMessageFactory>();
            builder.Services.AddSingleton<ISessionFactory, SkeletonSessionFactory>();
            
            AppHost = builder.Build();
        }
    }
}
