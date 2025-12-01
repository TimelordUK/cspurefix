using System.Text;

namespace PureFix.Transport.Store;

/// <summary>
/// In-memory implementation of ISessionStreamProvider for testing.
/// Stores all data in memory streams and dictionaries for inspection.
/// </summary>
public sealed class MemorySessionStreamProvider : ISessionStreamProvider, IAsyncDisposable
{
    private MemoryStream? _bodyStream;
    private MemoryStream? _headerStream;
    private StreamWriter? _headerWriter;
    private string? _seqNumsContent;
    private string? _sessionTimeContent;

    /// <summary>
    /// Gets a copy of the body stream content as a byte array.
    /// Useful for inspecting the exact bytes written.
    /// </summary>
    public byte[] GetBodyBytes() => _bodyStream?.ToArray() ?? [];

    /// <summary>
    /// Gets the body stream content as a UTF-8 string.
    /// </summary>
    public string GetBodyString() => Encoding.UTF8.GetString(GetBodyBytes());

    /// <summary>
    /// Gets the header stream content as a UTF-8 string.
    /// </summary>
    public string GetHeaderString()
    {
        if (_headerStream == null) return string.Empty;
        _headerWriter?.Flush();
        return Encoding.UTF8.GetString(_headerStream.ToArray());
    }

    /// <summary>
    /// Gets the header lines for inspection.
    /// </summary>
    public string[] GetHeaderLines()
    {
        var content = GetHeaderString();
        if (string.IsNullOrEmpty(content)) return [];
        return content.Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .Select(l => l.TrimEnd('\r'))
            .ToArray();
    }

    /// <summary>
    /// Gets the current sequence numbers content.
    /// </summary>
    public string? SeqNumsContent => _seqNumsContent;

    /// <summary>
    /// Gets the current session time content.
    /// </summary>
    public string? SessionTimeContent => _sessionTimeContent;

    public Stream OpenBodyStream()
    {
        _bodyStream ??= new MemoryStream();
        return _bodyStream;
    }

    public StreamWriter OpenHeaderWriter()
    {
        if (_headerWriter != null)
            return _headerWriter;

        _headerStream = new MemoryStream();
        // Use UTF8 without BOM to match file format exactly
        _headerWriter = new StreamWriter(_headerStream, new UTF8Encoding(false), leaveOpen: true) { AutoFlush = false };
        return _headerWriter;
    }

    public Task<string?> ReadSeqNumsAsync()
    {
        return Task.FromResult(_seqNumsContent);
    }

    public Task WriteSeqNumsAsync(string content)
    {
        _seqNumsContent = content;
        return Task.CompletedTask;
    }

    public Task<string?> ReadSessionTimeAsync()
    {
        return Task.FromResult(_sessionTimeContent);
    }

    public Task WriteSessionTimeAsync(string content)
    {
        _sessionTimeContent = content;
        return Task.CompletedTask;
    }

    public Task<string[]> ReadHeaderLinesAsync()
    {
        return Task.FromResult(GetHeaderLines());
    }

    public Task ResetAsync()
    {
        _headerWriter?.Dispose();
        _headerWriter = null;
        _headerStream?.Dispose();
        _headerStream = null;
        _bodyStream?.Dispose();
        _bodyStream = null;
        _seqNumsContent = null;
        _sessionTimeContent = null;
        return Task.CompletedTask;
    }

    public Stream? GetBodyStream() => _bodyStream;

    public ValueTask DisposeAsync()
    {
        _headerWriter?.Dispose();
        _headerStream?.Dispose();
        _bodyStream?.Dispose();
        return ValueTask.CompletedTask;
    }
}
