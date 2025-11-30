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
    private const int SeqNumWidth = 20;
    private const string SeqNumsFormat = "{0,20} : {1,20}";
    private const string SessionTimeFormat = "yyyyMMdd-HH:mm:ss.ffffff";

    private readonly string _directory;
    private readonly object _lock = new();

    private int _senderSeqNum;
    private int _targetSeqNum;
    private DateTime _creationTime;

    // File streams - kept open for performance
    private FileStream? _bodyStream;
    private StreamWriter? _headerWriter;

    // In-memory index: seqnum -> (offset, length)
    private readonly Dictionary<int, (long Offset, int Length)> _headerIndex = new();

    public FileSessionStore(SessionId sessionId, string directory)
    {
        SessionId = sessionId;
        _directory = directory;
    }

    public SessionId SessionId { get; }

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

        // Delete existing files
        DeleteFileIfExists("seqnums");
        DeleteFileIfExists("session");
        DeleteFileIfExists("header");
        DeleteFileIfExists("body");

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

        // Write to body file
        await _bodyStream!.WriteAsync(bytes);
        await _bodyStream.FlushAsync();

        // Write to header file
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
        Directory.CreateDirectory(_directory);

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
    }

    #endregion

    #region Private - File Operations

    private string GetFilePath(string extension) => SessionId.GetFilePath(_directory, extension);

    private void DeleteFileIfExists(string extension)
    {
        var path = GetFilePath(extension);
        if (File.Exists(path))
            File.Delete(path);
    }

    private void OpenStreams()
    {
        _bodyStream = new FileStream(
            GetFilePath("body"),
            FileMode.OpenOrCreate,
            FileAccess.ReadWrite,
            FileShare.Read);
        _bodyStream.Seek(0, SeekOrigin.End); // Append mode

        var headerStream = new FileStream(
            GetFilePath("header"),
            FileMode.OpenOrCreate,
            FileAccess.Write,
            FileShare.Read);
        headerStream.Seek(0, SeekOrigin.End); // Append mode
        _headerWriter = new StreamWriter(headerStream, Encoding.UTF8) { AutoFlush = false };
    }

    private async Task CloseStreams()
    {
        if (_headerWriter != null)
        {
            await _headerWriter.DisposeAsync();
            _headerWriter = null;
        }
        if (_bodyStream != null)
        {
            await _bodyStream.DisposeAsync();
            _bodyStream = null;
        }
    }

    private async Task PersistSeqNums()
    {
        var content = string.Format(SeqNumsFormat, _senderSeqNum, _targetSeqNum);
        await File.WriteAllTextAsync(GetFilePath("seqnums"), content);
    }

    private async Task LoadSeqNums()
    {
        var path = GetFilePath("seqnums");
        if (!File.Exists(path))
        {
            _senderSeqNum = 1;
            _targetSeqNum = 1;
            return;
        }

        var content = await File.ReadAllTextAsync(path);
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
        await File.WriteAllTextAsync(GetFilePath("session"), content);
    }

    private async Task LoadSessionTime()
    {
        var path = GetFilePath("session");
        if (!File.Exists(path))
        {
            _creationTime = DateTime.UtcNow;
            await PersistSessionTime();
            return;
        }

        var content = await File.ReadAllTextAsync(path);
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
        var path = GetFilePath("header");
        if (!File.Exists(path))
            return;

        var lines = await File.ReadAllLinesAsync(path);
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
