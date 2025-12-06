using System.Diagnostics.Metrics;
using PureFix.Types;

namespace PureFix.Examples.Shared;

/// <summary>
/// Listens to PureFix metrics and periodically prints a summary to console.
/// </summary>
public sealed class ConsoleMetricsListener : IDisposable
{
    private readonly MeterListener _listener;
    private readonly Timer _printTimer;
    private readonly object _lock = new();

    // Latency tracking (microseconds)
    private double _encodeLatencySum;
    private int _encodeCount;
    private double _encodeMin = double.MaxValue;
    private double _encodeMax;

    private double _parseLatencySum;
    private int _parseCount;
    private double _parseMin = double.MaxValue;
    private double _parseMax;

    private double _sendLatencySum;
    private int _sendCount;
    private double _sendMin = double.MaxValue;
    private double _sendMax;

    // Counters
    private long _messagesSent;
    private long _messagesReceived;
    private long _bytesSent;
    private long _bytesReceived;
    private long _parseErrors;

    public ConsoleMetricsListener(TimeSpan printInterval)
    {
        _listener = new MeterListener();
        _listener.InstrumentPublished = (instrument, listener) =>
        {
            if (instrument.Meter.Name == FixMetrics.MeterName)
            {
                listener.EnableMeasurementEvents(instrument);
            }
        };

        _listener.SetMeasurementEventCallback<double>(OnMeasurement);
        _listener.SetMeasurementEventCallback<long>(OnMeasurement);

        _listener.Start();

        _printTimer = new Timer(_ => PrintStats(), null, printInterval, printInterval);
    }

    private void OnMeasurement(Instrument instrument, double measurement, ReadOnlySpan<KeyValuePair<string, object?>> tags, object? state)
    {
        lock (_lock)
        {
            switch (instrument.Name)
            {
                case "purefix.encode.latency":
                    _encodeLatencySum += measurement;
                    _encodeCount++;
                    _encodeMin = Math.Min(_encodeMin, measurement);
                    _encodeMax = Math.Max(_encodeMax, measurement);
                    break;

                case "purefix.parse.latency":
                    _parseLatencySum += measurement;
                    _parseCount++;
                    _parseMin = Math.Min(_parseMin, measurement);
                    _parseMax = Math.Max(_parseMax, measurement);
                    break;

                case "purefix.send.latency":
                    _sendLatencySum += measurement;
                    _sendCount++;
                    _sendMin = Math.Min(_sendMin, measurement);
                    _sendMax = Math.Max(_sendMax, measurement);
                    break;
            }
        }
    }

    private void OnMeasurement(Instrument instrument, long measurement, ReadOnlySpan<KeyValuePair<string, object?>> tags, object? state)
    {
        lock (_lock)
        {
            switch (instrument.Name)
            {
                case "purefix.messages.sent":
                    _messagesSent += measurement;
                    break;
                case "purefix.messages.received":
                    _messagesReceived += measurement;
                    break;
                case "purefix.bytes.sent":
                    _bytesSent += measurement;
                    break;
                case "purefix.bytes.received":
                    _bytesReceived += measurement;
                    break;
                case "purefix.parse.errors":
                    _parseErrors += measurement;
                    break;
            }
        }
    }

    private void PrintStats()
    {
        lock (_lock)
        {
            Console.WriteLine();
            Console.WriteLine("=== PureFix Metrics ===");

            if (_encodeCount > 0)
            {
                var avgEncode = _encodeLatencySum / _encodeCount;
                Console.WriteLine($"Encode:  count={_encodeCount}, avg={avgEncode:F1}us, min={_encodeMin:F1}us, max={_encodeMax:F1}us");
            }

            if (_parseCount > 0)
            {
                var avgParse = _parseLatencySum / _parseCount;
                Console.WriteLine($"Parse:   count={_parseCount}, avg={avgParse:F1}us, min={_parseMin:F1}us, max={_parseMax:F1}us");
            }

            if (_sendCount > 0)
            {
                var avgSend = _sendLatencySum / _sendCount;
                Console.WriteLine($"Send:    count={_sendCount}, avg={avgSend:F1}us, min={_sendMin:F1}us, max={_sendMax:F1}us");
            }

            Console.WriteLine($"Messages: sent={_messagesSent}, received={_messagesReceived}");
            Console.WriteLine($"Bytes:    sent={_bytesSent}, received={_bytesReceived}");

            if (_parseErrors > 0)
            {
                Console.WriteLine($"Errors:   parse={_parseErrors}");
            }

            Console.WriteLine("=======================");
            Console.WriteLine();
        }
    }

    public void Dispose()
    {
        _printTimer.Dispose();
        _listener.Dispose();

        // Print final stats
        PrintStats();
    }
}
