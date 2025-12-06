using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace PureFix.Types;

/// <summary>
/// Centralized metrics for FIX engine instrumentation using System.Diagnostics.Metrics.
/// Provides low-overhead histograms and counters for measuring latency and throughput.
/// </summary>
public static class FixMetrics
{
    private static readonly Meter s_meter = new("PureFix", "1.0.0");

    // Latency histograms (in microseconds for precision)
    public static readonly Histogram<double> EncodeLatency = s_meter.CreateHistogram<double>(
        "purefix.encode.latency",
        unit: "us",
        description: "Time to encode a FIX message");

    public static readonly Histogram<double> ParseLatency = s_meter.CreateHistogram<double>(
        "purefix.parse.latency",
        unit: "us",
        description: "Time to parse FIX messages from buffer");

    public static readonly Histogram<double> SendLatency = s_meter.CreateHistogram<double>(
        "purefix.send.latency",
        unit: "us",
        description: "Time to send message over transport");

    public static readonly Histogram<double> ReceiveLatency = s_meter.CreateHistogram<double>(
        "purefix.receive.latency",
        unit: "us",
        description: "Time to receive message from transport");

    // Counters for throughput
    public static readonly Counter<long> MessagesSent = s_meter.CreateCounter<long>(
        "purefix.messages.sent",
        unit: "messages",
        description: "Total messages sent");

    public static readonly Counter<long> MessagesReceived = s_meter.CreateCounter<long>(
        "purefix.messages.received",
        unit: "messages",
        description: "Total messages received");

    public static readonly Counter<long> BytesSent = s_meter.CreateCounter<long>(
        "purefix.bytes.sent",
        unit: "bytes",
        description: "Total bytes sent");

    public static readonly Counter<long> BytesReceived = s_meter.CreateCounter<long>(
        "purefix.bytes.received",
        unit: "bytes",
        description: "Total bytes received");

    public static readonly Counter<long> ParseErrors = s_meter.CreateCounter<long>(
        "purefix.parse.errors",
        unit: "errors",
        description: "Total parse errors");

    /// <summary>
    /// Gets the meter name for listeners to subscribe to.
    /// </summary>
    public static string MeterName => s_meter.Name;
}

/// <summary>
/// Low-overhead scope for measuring latency. Uses Stopwatch timestamp for precision.
/// </summary>
public readonly struct LatencyScope : IDisposable
{
    private readonly long _startTicks;
    private readonly Histogram<double> _histogram;
    private readonly KeyValuePair<string, object?>[] _tags;

    public LatencyScope(Histogram<double> histogram, params KeyValuePair<string, object?>[] tags)
    {
        _histogram = histogram;
        _tags = tags;
        _startTicks = Stopwatch.GetTimestamp();
    }

    public void Dispose()
    {
        var elapsed = Stopwatch.GetElapsedTime(_startTicks);
        if (_tags.Length > 0)
        {
            _histogram.Record(elapsed.TotalMicroseconds, _tags);
        }
        else
        {
            _histogram.Record(elapsed.TotalMicroseconds);
        }
    }
}

/// <summary>
/// Extension methods for creating latency scopes.
/// </summary>
public static class LatencyScopeExtensions
{
    /// <summary>
    /// Creates a latency scope for the given histogram. Dispose to record the elapsed time.
    /// </summary>
    public static LatencyScope Time(this Histogram<double> histogram)
    {
        return new LatencyScope(histogram);
    }

    /// <summary>
    /// Creates a latency scope with tags for the given histogram.
    /// </summary>
    public static LatencyScope Time(this Histogram<double> histogram, string tagName, object? tagValue)
    {
        return new LatencyScope(histogram, new KeyValuePair<string, object?>(tagName, tagValue));
    }

    /// <summary>
    /// Creates a latency scope with multiple tags for the given histogram.
    /// </summary>
    public static LatencyScope Time(this Histogram<double> histogram, params KeyValuePair<string, object?>[] tags)
    {
        return new LatencyScope(histogram, tags);
    }
}
