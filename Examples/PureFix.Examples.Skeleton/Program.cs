using Arrow.Threading.Tasks;
using PureFix.Examples.Shared;
using PureFix.Examples.Skeleton;
using PureFix.Transport.Session;
using PureFix.Types;

Console.WriteLine("FIX Protocol Skeleton Example");
Console.WriteLine("============================");
Console.WriteLine("This example demonstrates a simple FIX client/server connection");
Console.WriteLine();

// Paths for session configuration files
var dictRootPath = Directory.GetCurrentDirectory(); // Dictionary files are copied via PureFix.Data reference
var sessionRootPath = Path.Join(dictRootPath, "Session");
var acceptorConfig = Path.Join(sessionRootPath, "test-qf44-acceptor.json");
var initiatorConfig = Path.Join(sessionRootPath, "test-qf44-initiator.json");

Console.WriteLine("Starting FIX sessions (Initiator and Acceptor)...");
Console.WriteLine();

var clock = new RealtimeClock();
await Runner.Run(
    acceptorConfig,
    initiatorConfig,
    dictRootPath,
    clock,
    MakeSkeletonHost);

Console.WriteLine();
Console.WriteLine("Skeleton example completed");

static BaseAppDI MakeSkeletonHost(IFixClock clock, IFixConfig config)
{
    var factory = new ConsoleLogFactory(clock);
    var queue = new AsyncWorkQueue();
    return new SkeletonDI(queue, factory, clock, config);
}
