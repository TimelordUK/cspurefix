using Arrow.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using PureFix.Transport.Session;
using PureFix.Types;

namespace PureFix.Examples.Shared;

public class AppHost<T, U> : BaseAppDI
    where T : class, ISessionFactory
    where U : class, IFixMessageFactory
{
    public AppHost(AsyncWorkQueue q, ILogFactory factory, IFixClock clock, IFixConfig config)
        : base(q, factory, clock, config)
    {
        _builder.Services.AddSingleton<ISessionFactory, T>();
        _builder.Services.AddSingleton<IFixMessageFactory, U>();
        AppHost = _builder.Build();
    }
}
