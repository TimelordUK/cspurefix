using System.Text;
using BenchmarkDotNet.Attributes;
using PureFix.Buffer;
using PureFix.Buffer.Ascii;
using PureFix.Dictionary.Definition;
using PureFix.Dictionary.Parser.QuickFix;
using PureFix.Types;
using PureFix.Types.Core;

namespace PureFix.Benchmarks;

/// <summary>
/// Measures string allocations from header field access (tags 8, 35, 49, 56, 50, 57).
/// These are the primary targets for string interning optimization.
///
/// Run with: dotnet run -c Release --filter "*HeaderString*"
///
/// Before optimization: Each GetString() call allocates a new string.
/// After optimization: Session-constant fields return interned strings.
/// </summary>
[MemoryDiagnoser]
[SimpleJob(warmupCount: 2, iterationCount: 5)]
public class HeaderStringAllocationBenchmarks
{
    private byte[][] _messages = null!;
    private IFixDefinitions _fixDefinitions = null!;
    private AsciiParser _parser = null!;

    // Header tags we're targeting for interning
    private const int TagBeginString = 8;
    private const int TagMsgType = 35;
    private const int TagSenderCompID = 49;
    private const int TagTargetCompID = 56;
    private const int TagSenderSubID = 50;
    private const int TagTargetSubID = 57;

    [Params(100, 1000, 10000)]
    public int MessageCount { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        // Load FIX 5.0SP2 definitions (matches tc-client.txt)
        _fixDefinitions = new FixDefinitions();
        var qf = new QuickFixXmlFileParser(_fixDefinitions);
        qf.Parse(Fix50SP2PathHelper.Fix50SP2);

        _parser = new AsciiParser(_fixDefinitions) { Delimiter = AsciiChars.Pipe };

        // Generate realistic trade capture messages
        _messages = GenerateMessages(MessageCount);
    }

    private byte[][] GenerateMessages(int count)
    {
        var messages = new byte[count][];
        var symbols = new[] { "Gold", "Silver", "Platinum", "Copper", "Steel" };

        for (int i = 0; i < count; i++)
        {
            var symbol = symbols[i % symbols.Length];
            var seqNum = i + 1;

            // Generate a TradeCaptureReport (35=AE) message
            // These have consistent headers which is what we want to test
            var msg = $"8=FIX.5.0SP2|9=0000180|35=AE|49=init-comp|56=accept-comp|34={seqNum}|" +
                      $"50=SenderSubID|57=TargetSubID|52=20241012-15:35:21.249|" +
                      $"571={100000 + i}|487=0|856=0|828=0|17={600000 + i}|570=N|" +
                      $"55={symbol}|48={symbol}|32=1000|31=100|75=20241012|" +
                      $"60=20241012-15:35:21.249|10=180|";

            messages[i] = Encoding.UTF8.GetBytes(msg);
        }

        return messages;
    }

    /// <summary>
    /// Baseline: Parse messages without accessing any string fields.
    /// This measures the raw parsing cost without string allocations.
    /// </summary>
    [Benchmark(Baseline = true)]
    public int ParseOnly()
    {
        int count = 0;
        foreach (var msg in _messages)
        {
            _parser.ParseFrom(msg, msg.Length, (_, view) =>
            {
                count++;
            });
        }
        return count;
    }

    /// <summary>
    /// Access BeginString (tag 8) - "FIX.5.0SP2" repeated N times.
    /// Currently allocates N strings. With interning: 1 allocation.
    /// </summary>
    [Benchmark]
    public int ParseAndGetBeginString()
    {
        int count = 0;
        foreach (var msg in _messages)
        {
            _parser.ParseFrom(msg, msg.Length, (_, view) =>
            {
                var beginString = view.GetString(TagBeginString);
                if (beginString != null) count++;
            });
        }
        return count;
    }

    /// <summary>
    /// Access MsgType (tag 35) - "AE" repeated N times.
    /// Note: MsgType is already interned via MsgType.Intern() in AsciiParseState.
    /// This should show minimal allocations already.
    /// </summary>
    [Benchmark]
    public int ParseAndGetMsgType()
    {
        int count = 0;
        foreach (var msg in _messages)
        {
            _parser.ParseFrom(msg, msg.Length, (_, view) =>
            {
                var msgType = view.GetString(TagMsgType);
                if (msgType != null) count++;
            });
        }
        return count;
    }

    /// <summary>
    /// Access SenderCompID (tag 49) - "init-comp" repeated N times.
    /// Currently allocates N strings. With interning: 1 allocation per session.
    /// </summary>
    [Benchmark]
    public int ParseAndGetSenderCompID()
    {
        int count = 0;
        foreach (var msg in _messages)
        {
            _parser.ParseFrom(msg, msg.Length, (_, view) =>
            {
                var sender = view.GetString(TagSenderCompID);
                if (sender != null) count++;
            });
        }
        return count;
    }

    /// <summary>
    /// Access TargetCompID (tag 56) - "accept-comp" repeated N times.
    /// Currently allocates N strings. With interning: 1 allocation per session.
    /// </summary>
    [Benchmark]
    public int ParseAndGetTargetCompID()
    {
        int count = 0;
        foreach (var msg in _messages)
        {
            _parser.ParseFrom(msg, msg.Length, (_, view) =>
            {
                var target = view.GetString(TagTargetCompID);
                if (target != null) count++;
            });
        }
        return count;
    }

    /// <summary>
    /// Access all header strings together (typical usage pattern).
    /// This is what a real Parse() method does.
    /// </summary>
    [Benchmark]
    public int ParseAndGetAllHeaderStrings()
    {
        int count = 0;
        foreach (var msg in _messages)
        {
            _parser.ParseFrom(msg, msg.Length, (_, view) =>
            {
                var beginString = view.GetString(TagBeginString);
                var msgType = view.GetString(TagMsgType);
                var sender = view.GetString(TagSenderCompID);
                var target = view.GetString(TagTargetCompID);
                var senderSub = view.GetString(TagSenderSubID);
                var targetSub = view.GetString(TagTargetSubID);

                if (beginString != null && msgType != null && sender != null && target != null)
                    count++;
            });
        }
        return count;
    }

    /// <summary>
    /// Use span-based comparison instead of GetString() - zero allocation.
    /// This is the existing zero-alloc API for routing/filtering.
    /// </summary>
    [Benchmark]
    public int ParseAndCheckSpan()
    {
        int count = 0;
        foreach (var msg in _messages)
        {
            _parser.ParseFrom(msg, msg.Length, (_, view) =>
            {
                if (view is AsciiView asciiView)
                {
                    // Use span API - no string allocation
                    if (asciiView.IsTagEqual(TagBeginString, "FIX.5.0SP2"u8) &&
                        asciiView.IsTagEqual(TagMsgType, "AE"u8))
                    {
                        count++;
                    }
                }
            });
        }
        return count;
    }

    /// <summary>
    /// Access all header strings with string interning enabled.
    /// Uses SessionStringStore for CompID interning + static pool for BeginString.
    /// Expected: Same allocations for first message, then near-zero for subsequent.
    /// </summary>
    [Benchmark]
    public int ParseAndGetAllHeaderStrings_WithInterning()
    {
        // Create parser with string store - simulates per-session parser
        var stringStore = new SessionStringStore();
        var parserWithStore = new AsciiParser(_fixDefinitions, stringStore) { Delimiter = AsciiChars.Pipe };

        int count = 0;
        foreach (var msg in _messages)
        {
            parserWithStore.ParseFrom(msg, msg.Length, (_, view) =>
            {
                var beginString = view.GetString(TagBeginString);
                var msgType = view.GetString(TagMsgType);
                var sender = view.GetString(TagSenderCompID);
                var target = view.GetString(TagTargetCompID);
                var senderSub = view.GetString(TagSenderSubID);
                var targetSub = view.GetString(TagTargetSubID);

                if (beginString != null && msgType != null && sender != null && target != null)
                    count++;
            });
        }
        return count;
    }
}

/// <summary>
/// Helper to locate FIX 5.0SP2 dictionary path
/// </summary>
internal static class Fix50SP2PathHelper
{
    public static string Fix50SP2 => FindDictPath("FIX50SP2.xml");

    private static string FindDictPath(string filename)
    {
        var dir = AppContext.BaseDirectory;
        for (int i = 0; i < 8; i++)
        {
            // Check directly in Data folder (repo structure)
            var path = Path.Combine(dir, "Data", filename);
            if (File.Exists(path)) return path;

            // Check in bin output (copied by build)
            path = Path.Combine(dir, filename);
            if (File.Exists(path)) return path;

            dir = Path.GetDirectoryName(dir) ?? dir;
        }

        throw new FileNotFoundException($"Could not find {filename}. Searched from {AppContext.BaseDirectory}");
    }
}
