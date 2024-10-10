using Arrow.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using PureFix.Transport.Session;
using PureFix.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.ConsoleApp
{
    internal class AppHost<T,U> : BaseAppDI where T: class, ISessionFactory where U : class, IFixMessageFactory
    {
        public AppHost(AsyncWorkQueue q, ILogFactory factory, IFixClock clock, IFixConfig config) : base(q, factory, clock, config)
        {
            _builder.Services.AddSingleton<ISessionFactory, T>();
            _builder.Services.AddSingleton<IFixMessageFactory, U>();
            AppHost = _builder.Build();
        }
    }
}
