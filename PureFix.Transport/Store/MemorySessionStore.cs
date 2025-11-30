namespace PureFix.Transport.Store;

/// <summary>
/// In-memory session store for testing and development.
/// Not persistent - all data lost on dispose.
/// </summary>
public sealed class MemorySessionStore : IFixSessionStore
{
    private readonly object _lock = new();
    private readonly Dictionary<int, IFixMsgStoreRecord> _messages = new();

    private int _senderSeqNum = 1;
    private int _targetSeqNum = 1;
    private DateTime _creationTime = DateTime.UtcNow;

    public MemorySessionStore(SessionId sessionId)
    {
        SessionId = sessionId;
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

    public Task SetSenderSeqNum(int value)
    {
        lock (_lock) _senderSeqNum = value;
        return Task.CompletedTask;
    }

    public Task SetTargetSeqNum(int value)
    {
        lock (_lock) _targetSeqNum = value;
        return Task.CompletedTask;
    }

    public Task<int> NextSenderSeqNum()
    {
        lock (_lock) return Task.FromResult(++_senderSeqNum);
    }

    public Task<int> NextTargetSeqNum()
    {
        lock (_lock) return Task.FromResult(++_targetSeqNum);
    }

    #endregion

    #region Session

    public DateTime CreationTime
    {
        get { lock (_lock) return _creationTime; }
    }

    public Task Reset()
    {
        lock (_lock)
        {
            _senderSeqNum = 1;
            _targetSeqNum = 1;
            _creationTime = DateTime.UtcNow;
            _messages.Clear();
        }
        return Task.CompletedTask;
    }

    #endregion

    #region Message Operations

    public Task Put(IFixMsgStoreRecord record)
    {
        lock (_lock)
        {
            _messages[record.SeqNum] = record.Clone();
        }
        return Task.CompletedTask;
    }

    public Task<IFixMsgStoreRecord?> Get(int seqNum)
    {
        lock (_lock)
        {
            if (_messages.TryGetValue(seqNum, out var record))
                return Task.FromResult<IFixMsgStoreRecord?>(record.Clone());
            return Task.FromResult<IFixMsgStoreRecord?>(null);
        }
    }

    public Task<IReadOnlyList<IFixMsgStoreRecord>> GetRange(int fromSeqNum, int toSeqNum)
    {
        var results = new List<IFixMsgStoreRecord>();
        lock (_lock)
        {
            for (int seq = fromSeqNum; seq <= toSeqNum; seq++)
            {
                if (_messages.TryGetValue(seq, out var record))
                    results.Add(record.Clone());
            }
        }
        return Task.FromResult<IReadOnlyList<IFixMsgStoreRecord>>(results);
    }

    #endregion

    #region Lifecycle

    public Task Initialize() => Task.CompletedTask;

    public Task Flush() => Task.CompletedTask;

    public ValueTask DisposeAsync() => ValueTask.CompletedTask;

    #endregion
}
