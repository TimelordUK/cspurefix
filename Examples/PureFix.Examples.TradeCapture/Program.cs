using PureFix.Examples.Shared;
using PureFix.Examples.TradeCapture;
using PureFix.Transport.Session;
using PureFix.Types;


Console.WriteLine("FIX Protocol Trade Capture Example");
Console.WriteLine("===================================");
Console.WriteLine("This example demonstrates a FIX 5.0 SP2 trade capture workflow");
Console.WriteLine();

// Start metrics listener - prints stats every 5 seconds
using var metricsListener = new ConsoleMetricsListener(TimeSpan.FromSeconds(5));

// Paths for session configuration files
var dictRootPath = AppContext.BaseDirectory; // Dictionary files are copied via PureFix.Data reference
var sessionRootPath = Path.Join(dictRootPath, "Session");
var acceptorConfig = Path.Join(sessionRootPath, "test-qf52-acceptor.json");
var initiatorConfig = Path.Join(sessionRootPath, "test-qf52-initiator.json");

Console.WriteLine("Starting FIX sessions (Trade Capture Client and Server)...");
Console.WriteLine("Logging and store are configured via JSON config.");
Console.WriteLine();

await Runner.Run(
    acceptorConfig,
    initiatorConfig,
    dictRootPath,
    MakeTradeCaptureHost);

Console.WriteLine();
Console.WriteLine("Trade Capture example completed");
return;

static BaseAppDI MakeTradeCaptureHost(IFixConfig config)
{
    // Create log factory from config - reads "logging" section from JSON
    var factory = new ConsoleLogFactory(config.Description);
    var clock = new RealtimeClock();
    return new TradeCaptureDI(factory, clock, config);
}
