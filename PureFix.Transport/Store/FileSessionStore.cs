using System.Globalization;
using System.Text;

namespace PureFix.Transport.Store;

/// <summary>
/// QuickFix-compatible file-based session store.
///
/// File format:
/// - .seqnums: "SSSSSSSSSSSSSSSSSSSS : TTTTTTTTTTTTTTTTTTTT" (20-char right-justified sender : target)
/// - .session: "YYYYMMDD-HH:MM:SS.ffffff" (session creation time)
/// - .header: "seqnum,offset,length" per line (index into body)
/// - .body: concatenated raw FIX messages (no delimiters)
/// </summary>
public sealed class FileSessionStore : IFixSessionStore
{
    private const string SeqNumsFormat = "{0,20} : {1,20}";
    private const string SessionTimeFormat = "yyyyMMdd-HH:mm:ss.ffffff";

    private readonly object _lock = new();
    private readonly ISessionStreamProvider _streamProvider;
    private readonly bool _ownsProvider;

    private int _senderSeqNum;
    private int _targetSeqNum;
    private DateTime _creationTime;

    // Streams - managed by provider or opened directly
    private Stream? _bodyStream;
    private StreamWriter? _headerWriter;

    // In-memory index: seqnum -> (offset, length)
    private readonly Dictionary<int, (long Offset, int Length)> _headerIndex = new();

    /// <summary>
    /// Creates a FileSessionStore with the default file-based stream provider.
    /// </summary>
    public FileSessionStore(SessionId sessionId, string directory)
        : this(sessionId, new FileSessionStreamProvider(sessionId, directory), ownsProvider: true)
    {
    }

    /// <summary>
    /// Creates a FileSessionStore with a custom stream provider.
    /// Useful for testing with in-memory streams.
    /// </summary>
    public FileSessionStore(SessionId sessionId, ISessionStreamProvider streamProvider, bool ownsProvider = false)
    {
        SessionId = sessionId;
        _streamProvider = streamProvider;
        _ownsProvider = ownsProvider;
    }

    public SessionId SessionId { get; }

    /// <summary>
    /// Gets the stream provider for direct access (useful for testing).
    /// </summary>
    public ISessionStreamProvider StreamProvider => _streamProvider;

    #region Sequence Numbers

    public int SenderSeqNum
    {
        get { lock (_lock) return _senderSeqNum; }
    }

    public int TargetSeqNum
    {
        get { lock (_lock) return _targetSeqNum; }
    }

    public async Task SetSenderSeqNum(int value)
    {
        lock (_lock) _senderSeqNum = value;
        await PersistSeqNums();
    }

    public async Task SetTargetSeqNum(int value)
    {
        lock (_lock) _targetSeqNum = value;
        await PersistSeqNums();
    }

    public async Task<int> NextSenderSeqNum()
    {
        int next;
        lock (_lock) next = ++_senderSeqNum;
        await PersistSeqNums();
        return next;
    }

    public async Task<int> NextTargetSeqNum()
    {
        int next;
        lock (_lock) next = ++_targetSeqNum;
        await PersistSeqNums();
        return next;
    }

    #endregion

    #region Session

    public DateTime CreationTime
    {
        get { lock (_lock) return _creationTime; }
    }

    public async Task Reset()
    {
        lock (_lock)
        {
            _senderSeqNum = 1;
            _targetSeqNum = 1;
            _creationTime = DateTime.UtcNow;
            _headerIndex.Clear();
        }

        // Close existing streams
        await CloseStreams();

        // Reset provider (clears files/memory)
        await _streamProvider.ResetAsync();

        // Persist new state
        await PersistSeqNums();
        await PersistSessionTime();

        // Reopen streams
        OpenStreams();
    }

    #endregion

    #region Message Operations

    public async Task Put(IFixMsgStoreRecord record)
    {
        if (record.Encoded == null)
            throw new ArgumentException("Record must have Encoded content", nameof(record));

        var bytes = Encoding.UTF8.GetBytes(record.Encoded);
        long offset;
        int length = bytes.Length;

        lock (_lock)
        {
            if (_bodyStream == null)
                throw new InvalidOperationException("Store not initialized");

            offset = _bodyStream.Position;
            _headerIndex[record.SeqNum] = (offset, length);
        }

        // Write to body stream
        await _bodyStream!.WriteAsync(bytes);
        await _bodyStream.FlushAsync();

        // Write to header
        await _headerWriter!.WriteLineAsync($"{record.SeqNum},{offset},{length}");
        await _headerWriter.FlushAsync();
    }

    public Task<IFixMsgStoreRecord?> Get(int seqNum)
    {
        lock (_lock)
        {
            if (!_headerIndex.TryGetValue(seqNum, out var entry))
                return Task.FromResult<IFixMsgStoreRecord?>(null);

            return ReadMessageAsync(seqNum, entry.Offset, entry.Length);
        }
    }

    public async Task<IReadOnlyList<IFixMsgStoreRecord>> GetRange(int fromSeqNum, int toSeqNum)
    {
        var results = new List<IFixMsgStoreRecord>();

        for (int seq = fromSeqNum; seq <= toSeqNum; seq++)
        {
            var record = await Get(seq);
            if (record != null)
                results.Add(record);
        }

        return results;
    }

    private async Task<IFixMsgStoreRecord?> ReadMessageAsync(int seqNum, long offset, int length)
    {
        if (_bodyStream == null)
            return null;

        var buffer = new byte[length];
        var originalPosition = _bodyStream.Position;

        try
        {
            _bodyStream.Seek(offset, SeekOrigin.Begin);
            var bytesRead = await _bodyStream.ReadAsync(buffer.AsMemory(0, length));

            if (bytesRead != length)
                return null;

            var encoded = Encoding.UTF8.GetString(buffer);

            // Extract MsgType from the message (tag 35)
            var msgType = ExtractTag(encoded, "35");

            // Extract SendingTime from the message (tag 52)
            var sendingTimeStr = ExtractTag(encoded, "52");
            var timestamp = DateTime.MinValue;
            if (sendingTimeStr != null)
            {
                // Try to parse FIX timestamp format: YYYYMMDD-HH:MM:SS.sss
                DateTime.TryParseExact(sendingTimeStr, "yyyyMMdd-HH:mm:ss.fff",
                    CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out timestamp);
            }

            return new FixMsgStoreRecord(msgType ?? "", timestamp, seqNum, encoded);
        }
        finally
        {
            _bodyStream.Seek(originalPosition, SeekOrigin.Begin);
        }
    }

    private static string? ExtractTag(string message, string tag)
    {
        // FIX tags are delimited by SOH (0x01)
        // Search for SOH + tag= or tag= at start of message
        var tagPrefix = $"{tag}=";
        var sohTagPrefix = $"\x01{tag}=";

        int startIndex;

        // First check if tag is at the very start of the message
        if (message.StartsWith(tagPrefix, StringComparison.Ordinal))
        {
            startIndex = tagPrefix.Length;
        }
        else
        {
            // Otherwise search for SOH + tag=
            var sohIndex = message.IndexOf(sohTagPrefix, StringComparison.Ordinal);
            if (sohIndex < 0) return null;
            startIndex = sohIndex + sohTagPrefix.Length;
        }

        var endIndex = message.IndexOf('\x01', startIndex);
        if (endIndex < 0) endIndex = message.Length;

        return message.Substring(startIndex, endIndex - startIndex);
    }

    #endregion

    #region Lifecycle

    public async Task Initialize()
    {
        // Load sequence numbers
        await LoadSeqNums();

        // Load session time
        await LoadSessionTime();

        // Load header index
        await LoadHeaderIndex();

        // Open streams for writing
        OpenStreams();
    }

    public async Task Flush()
    {
        if (_bodyStream != null)
            await _bodyStream.FlushAsync();
        if (_headerWriter != null)
            await _headerWriter.FlushAsync();
    }

    public async ValueTask DisposeAsync()
    {
        await CloseStreams();

        if (_ownsProvider && _streamProvider is IAsyncDisposable disposable)
        {
            await disposable.DisposeAsync();
        }
    }

    #endregion

    #region Private - Stream Operations

    private void OpenStreams()
    {
        _bodyStream = _streamProvider.OpenBodyStream();
        _headerWriter = _streamProvider.OpenHeaderWriter();
    }

    private async Task CloseStreams()
    {
        // We don't dispose the streams themselves as they're owned by the provider
        // Just flush and clear our references
        if (_headerWriter != null)
        {
            await _headerWriter.FlushAsync();
            _headerWriter = null;
        }
        if (_bodyStream != null)
        {
            await _bodyStream.FlushAsync();
            _bodyStream = null;
        }
    }

    private async Task PersistSeqNums()
    {
        var content = string.Format(SeqNumsFormat, _senderSeqNum, _targetSeqNum);
        await _streamProvider.WriteSeqNumsAsync(content);
    }

    private async Task LoadSeqNums()
    {
        var content = await _streamProvider.ReadSeqNumsAsync();
        if (content == null)
        {
            _senderSeqNum = 1;
            _targetSeqNum = 1;
            return;
        }

        // Format: "SSSSSSSSSSSSSSSSSSSS : TTTTTTTTTTTTTTTTTTTT"
        var parts = content.Split(':');
        if (parts.Length == 2)
        {
            int.TryParse(parts[0].Trim(), out _senderSeqNum);
            int.TryParse(parts[1].Trim(), out _targetSeqNum);
        }
    }

    private async Task PersistSessionTime()
    {
        var content = _creationTime.ToString(SessionTimeFormat, CultureInfo.InvariantCulture);
        await _streamProvider.WriteSessionTimeAsync(content);
    }

    private async Task LoadSessionTime()
    {
        var content = await _streamProvider.ReadSessionTimeAsync();
        if (content == null)
        {
            _creationTime = DateTime.UtcNow;
            await PersistSessionTime();
            return;
        }

        if (DateTime.TryParseExact(content.Trim(), SessionTimeFormat,
            CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var time))
        {
            _creationTime = time;
        }
        else
        {
            _creationTime = DateTime.UtcNow;
        }
    }

    private async Task LoadHeaderIndex()
    {
        var lines = await _streamProvider.ReadHeaderLinesAsync();
        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            var parts = line.Split(',');
            if (parts.Length == 3 &&
                int.TryParse(parts[0], out var seqNum) &&
                long.TryParse(parts[1], out var offset) &&
                int.TryParse(parts[2], out var length))
            {
                _headerIndex[seqNum] = (offset, length);
            }
        }
    }

    #endregion
}
