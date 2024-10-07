// See https://aka.ms/new-console-template for more information

using Arrow.Threading.Tasks;
using PureFix.Transport.Session;
using PureFix.Types;

namespace PureFix.ConsoleApp;

internal partial class Program
{
    // have different makehosts based on application type
    private static BaseAppDI MakeTradeCaptureAppHost(IFixClock clock, IFixConfig config)
    {
        var factory = new ConsoleLogFactory(config.Name());
        var queue = new AsyncWorkQueue();
        var app = new TradeCaptureDI(queue, factory, clock, config);
        return app;
    }

    private static BaseAppDI MakeSkeletonAppHost(IFixClock clock, IFixConfig config)
    {
        var factory = new ConsoleLogFactory(config.Name());
        var queue = new AsyncWorkQueue();
        var app = new SkeletonDI(queue, factory, clock, config);
        return app;
    }
}