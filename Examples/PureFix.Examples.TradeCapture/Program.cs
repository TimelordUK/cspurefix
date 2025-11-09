using Arrow.Threading.Tasks;
using PureFix.Examples.Shared;
using PureFix.Examples.TradeCapture;
using PureFix.Transport.Session;
using PureFix.Types;

Console.WriteLine("FIX Protocol Trade Capture Example");
Console.WriteLine("===================================");
Console.WriteLine("This example demonstrates a FIX 5.0 SP2 trade capture workflow");
Console.WriteLine();

// Paths for session configuration files
var dictRootPath = AppContext.BaseDirectory; // Dictionary files are copied via PureFix.Data reference
var sessionRootPath = Path.Join(dictRootPath, "Session");
var acceptorConfig = Path.Join(sessionRootPath, "test-qf52-acceptor.json");
var initiatorConfig = Path.Join(sessionRootPath, "test-qf52-initiator.json");

Console.WriteLine("Starting FIX sessions (Trade Capture Client and Server)...");
Console.WriteLine();

var clock = new RealtimeClock();
await Runner.Run(
    acceptorConfig,
    initiatorConfig,
    dictRootPath,
    clock,
    MakeTradeCaptureHost);

Console.WriteLine();
Console.WriteLine("Trade Capture example completed");
return;

static BaseAppDI MakeTradeCaptureHost(IFixClock clock, IFixConfig config)
{
    var factory = new ConsoleLogFactory(clock);
    var queue = new AsyncWorkQueue();
    return new TradeCaptureDI(queue, factory, clock, config);
}
