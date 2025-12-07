using Arrow.Threading.Tasks;
using PureFix.Examples.Shared;
using PureFix.Examples.Skeleton;
using PureFix.Transport;
using PureFix.Transport.Session;
using PureFix.Transport.Store;
using PureFix.Types;

Console.WriteLine("FIX Protocol Skeleton Example");
Console.WriteLine("============================");

// Parse command line arguments
var clientOnly = false;
var dryRun = false;
var noReset = false;
string? customConfig = null;
string? storeDirectory = null;

for (var i = 0; i < args.Length; i++)
{
    switch (args[i])
    {
        case "--client" or "-c":
            clientOnly = true;
            if (i + 1 < args.Length && !args[i + 1].StartsWith("-"))
            {
                customConfig = args[++i];
            }
            break;
        case "--store" or "-s":
            if (i + 1 < args.Length && !args[i + 1].StartsWith("-"))
            {
                storeDirectory = args[++i];
            }
            break;
        case "--no-reset" or "-n":
            noReset = true;
            break;
        case "--dry-run" or "-d":
            dryRun = true;
            clientOnly = true; // dry-run implies client mode
            break;
        case "--help" or "-h":
            PrintHelp();
            return;
    }
}

// Paths for session configuration files
var dictRootPath = AppContext.BaseDirectory; // Dictionary files are copied via PureFix.Data reference
var sessionRootPath = Path.Join(dictRootPath, "Session");

var clock = new RealtimeClock();

if (clientOnly)
{
    // Client-only mode - connect to external server/broker
    var configPath = customConfig ?? Path.Join(sessionRootPath, "test-qf44-initiator.json");

    if (!File.Exists(configPath))
    {
        Console.WriteLine($"Error: Config file not found: {configPath}");
        return;
    }

    // Load config to get session details
    var config = FixApp.MakeConfig(configPath, dictRootPath, storeDirectory);
    var desc = config.Description;

    Console.WriteLine();
    Console.WriteLine("Session Configuration:");
    Console.WriteLine($"  Config file:    {configPath}");
    Console.WriteLine($"  BeginString:    {desc?.BeginString}");
    Console.WriteLine($"  SenderCompID:   {desc?.SenderCompID}");
    Console.WriteLine($"  TargetCompID:   {desc?.TargetCompID}");
    Console.WriteLine($"  Host:           {desc?.Application?.Tcp?.Host}:{desc?.Application?.Tcp?.Port}");
    Console.WriteLine($"  TLS Enabled:    {desc?.Application?.Tcp?.Tls?.Enabled ?? false}");
    if (desc?.Application?.Tcp?.Tls?.Enabled == true)
    {
        Console.WriteLine($"  TLS Cert:       {desc?.Application?.Tcp?.Tls?.Certificate}");
        Console.WriteLine($"  TLS TargetHost: {desc?.Application?.Tcp?.Tls?.TargetHost ?? desc?.Application?.Tcp?.Host}");
    }
    Console.WriteLine();

    if (dryRun)
    {
        // Dry run - just show store status without connecting
        Console.WriteLine("=== DRY RUN MODE ===");
        Console.WriteLine();

        if (storeDirectory == null)
        {
            Console.WriteLine("Store: in-memory (no persistence, will start fresh)");
            Console.WriteLine("  Sender SeqNum: 1");
            Console.WriteLine("  Target SeqNum: 1");
        }
        else
        {
            Console.WriteLine($"Store: {storeDirectory} (QuickFix-compatible file store)");

            // Create session ID to check for existing files
            var sessionId = new SessionId(
                desc?.BeginString ?? "FIX.4.4",
                desc?.SenderCompID ?? "UNKNOWN",
                desc?.TargetCompID ?? "UNKNOWN");

            Console.WriteLine($"  Session ID:     {sessionId}");

            // Check for existing files (read-only - don't create anything)
            var seqnumsPath = sessionId.GetFilePath(storeDirectory, "seqnums");
            var sessionPath = sessionId.GetFilePath(storeDirectory, "session");
            var headerPath = sessionId.GetFilePath(storeDirectory, "header");
            var bodyPath = sessionId.GetFilePath(storeDirectory, "body");

            var seqnumsExists = File.Exists(seqnumsPath);
            var sessionExists = File.Exists(sessionPath);
            var headerExists = File.Exists(headerPath);
            var bodyExists = File.Exists(bodyPath);

            Console.WriteLine();
            Console.WriteLine("Store Files:");
            Console.WriteLine($"  .seqnums: {(seqnumsExists ? "EXISTS" : "missing")} -> {seqnumsPath}");
            Console.WriteLine($"  .session: {(sessionExists ? "EXISTS" : "missing")} -> {sessionPath}");
            Console.WriteLine($"  .header:  {(headerExists ? "EXISTS" : "missing")} -> {headerPath}");
            Console.WriteLine($"  .body:    {(bodyExists ? "EXISTS" : "missing")} -> {bodyPath}");

            Console.WriteLine();
            Console.WriteLine("Session State:");

            // Read state from files directly (read-only, no file creation)
            if (seqnumsExists)
            {
                var seqContent = await File.ReadAllTextAsync(seqnumsPath);
                var parts = seqContent.Split(':');
                if (parts.Length == 2)
                {
                    int.TryParse(parts[0].Trim(), out var sender);
                    int.TryParse(parts[1].Trim(), out var target);
                    Console.WriteLine($"  Sender SeqNum:  {sender}");
                    Console.WriteLine($"  Target SeqNum:  {target}");
                }
            }
            else
            {
                Console.WriteLine($"  Sender SeqNum:  1 (will start fresh)");
                Console.WriteLine($"  Target SeqNum:  1 (will start fresh)");
            }

            if (sessionExists)
            {
                var sessionContent = await File.ReadAllTextAsync(sessionPath);
                Console.WriteLine($"  Creation Time:  {sessionContent.Trim()} UTC");
            }
            else
            {
                Console.WriteLine($"  Creation Time:  (new session)");
            }

            if (headerExists)
            {
                var headerLines = await File.ReadAllLinesAsync(headerPath);
                var msgCount = headerLines.Count(l => !string.IsNullOrWhiteSpace(l));
                Console.WriteLine($"  Stored Messages: {msgCount}");

                if (msgCount > 0)
                {
                    Console.WriteLine();
                    Console.WriteLine("Recent Messages (last 5 from header index):");
                    foreach (var line in headerLines.TakeLast(5))
                    {
                        if (string.IsNullOrWhiteSpace(line)) continue;
                        var parts = line.Split(',');
                        if (parts.Length >= 1)
                        {
                            Console.WriteLine($"    [SeqNum {parts[0]}] offset={parts.ElementAtOrDefault(1)}, len={parts.ElementAtOrDefault(2)}");
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine($"  Stored Messages: 0");
            }
        }

        Console.WriteLine();
        Console.WriteLine("=== DRY RUN COMPLETE (no connection made) ===");
        return;
    }

    // Normal run
    Console.WriteLine($"Starting FIX client (connecting to external server)...");
    if (storeDirectory != null)
    {
        Console.WriteLine($"Store:  {storeDirectory} (QuickFix-compatible file store)");
    }
    else
    {
        Console.WriteLine($"Store:  in-memory (no persistence)");
    }
    Console.WriteLine();

    await Runner.RunSingle(configPath, dictRootPath, clock, MakeSkeletonHost, storeDirectory);
}
else
{
    // Default: run both acceptor and initiator for local testing
    string acceptorConfig, initiatorConfig;
    if (noReset)
    {
        // Use configs with ResetSeqNumFlag=false for resume testing
        acceptorConfig = Path.Join(sessionRootPath, "test-qf44-acceptor-noresetseq.json");
        initiatorConfig = Path.Join(sessionRootPath, "test-qf44-initiator-noresetseq.json");
    }
    else
    {
        acceptorConfig = Path.Join(sessionRootPath, "test-qf44-acceptor.json");
        initiatorConfig = Path.Join(sessionRootPath, "test-qf44-initiator.json");
    }

    Console.WriteLine("Starting FIX sessions (Initiator and Acceptor)...");
    if (noReset)
    {
        Console.WriteLine("Mode:   NO-RESET (ResetSeqNumFlag=false, HeartBtInt=5s)");
    }
    if (storeDirectory != null)
    {
        Console.WriteLine($"Store:  {storeDirectory} (QuickFix-compatible file store)");
        Console.WriteLine();
        Console.WriteLine("Expected store files:");
        Console.WriteLine($"  Acceptor:  FIX.4.4-accept-comp-init-comp.{{seqnums,session,header,body}}");
        Console.WriteLine($"  Initiator: FIX.4.4-init-comp-accept-comp.{{seqnums,session,header,body}}");
    }
    else
    {
        Console.WriteLine("Store:  in-memory (no persistence)");
    }
    Console.WriteLine();

    await Runner.Run(acceptorConfig, initiatorConfig, dictRootPath, clock, MakeSkeletonHost, storeDirectory);
}

Console.WriteLine();
Console.WriteLine("Skeleton example completed");

static BaseAppDI MakeSkeletonHost(IFixClock clock, IFixConfig config)
{
    var factory = new ConsoleLogFactory(clock);
    var queue = new AsyncWorkQueue();
    return new SkeletonDI(queue, factory, clock, config);
}

static void PrintHelp()
{
    Console.WriteLine("Usage: PureFix.Examples.Skeleton [options]");
    Console.WriteLine();
    Console.WriteLine("Options:");
    Console.WriteLine("  --client, -c [config]  Run client only (connect to external server)");
    Console.WriteLine("                         Optional: path to session config JSON");
    Console.WriteLine("  --store, -s <dir>      Use QuickFix-compatible file store in <dir>");
    Console.WriteLine("                         Store files created per-session based on comp IDs");
    Console.WriteLine("  --no-reset, -n         Use configs with ResetSeqNumFlag=false (for resume testing)");
    Console.WriteLine("                         Also uses faster heartbeat (5s vs 30s)");
    Console.WriteLine("  --dry-run, -d          Show session/store status without connecting");
    Console.WriteLine("                         Use to verify QuickFix store recovery before connecting");
    Console.WriteLine("  --help, -h             Show this help message");
    Console.WriteLine();
    Console.WriteLine("Examples:");
    Console.WriteLine("  dotnet run                                    # Run both locally (in-memory)");
    Console.WriteLine("  dotnet run -- -s ./teststore                  # Run both with file persistence");
    Console.WriteLine("  dotnet run -- -s ./teststore -n               # Run with store, no seq reset");
    Console.WriteLine("  dotnet run -- --client                        # Client with default config");
    Console.WriteLine("  dotnet run -- -c broker.json                  # Client with custom config");
    Console.WriteLine("  dotnet run -- -c broker.json -s ./store       # Client with file persistence");
    Console.WriteLine("  dotnet run -- -c broker.json -s ./store -d    # Dry run - verify store state");
    Console.WriteLine();
    Console.WriteLine("Store Resume Testing:");
    Console.WriteLine("  1. Delete store folder:  rm -rf ./teststore");
    Console.WriteLine("  2. First run:            dotnet run -- -s ./teststore -n");
    Console.WriteLine("  3. Let heartbeats run, then Ctrl+C");
    Console.WriteLine("  4. Check store:          cat ./teststore/*.seqnums");
    Console.WriteLine("  5. Restart:              dotnet run -- -s ./teststore -n");
    Console.WriteLine("  6. Sessions should resume from last sequence numbers");
}
