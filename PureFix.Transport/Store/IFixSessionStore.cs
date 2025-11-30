namespace PureFix.Transport.Store;

/// <summary>
/// Unified session store interface for FIX message persistence and sequence number management.
/// Coordinates all persistence for a single FIX session:
/// - Message storage (.body + .header files)
/// - Sequence numbers (.seqnums file)
/// - Session metadata (.session file)
///
/// QuickFix-compatible file format for interoperability.
/// </summary>
public interface IFixSessionStore : IAsyncDisposable
{
    /// <summary>
    /// The session identifier for this store.
    /// </summary>
    SessionId SessionId { get; }

    #region Message Operations

    /// <summary>
    /// Stores a message. All messages including session messages are stored.
    /// </summary>
    Task Put(IFixMsgStoreRecord record);

    /// <summary>
    /// Retrieves a message by sequence number.
    /// </summary>
    Task<IFixMsgStoreRecord?> Get(int seqNum);

    /// <summary>
    /// Retrieves a range of messages for resend requests.
    /// </summary>
    Task<IReadOnlyList<IFixMsgStoreRecord>> GetRange(int fromSeqNum, int toSeqNum);

    #endregion

    #region Sequence Number Operations

    /// <summary>
    /// Current sender (outgoing) sequence number.
    /// </summary>
    int SenderSeqNum { get; }

    /// <summary>
    /// Current target (incoming) sequence number.
    /// </summary>
    int TargetSeqNum { get; }

    /// <summary>
    /// Sets the sender sequence number and persists to storage.
    /// </summary>
    Task SetSenderSeqNum(int value);

    /// <summary>
    /// Sets the target sequence number and persists to storage.
    /// </summary>
    Task SetTargetSeqNum(int value);

    /// <summary>
    /// Atomically increments and returns the next sender sequence number.
    /// Persists the new value.
    /// </summary>
    Task<int> NextSenderSeqNum();

    /// <summary>
    /// Atomically increments and returns the next target sequence number.
    /// Persists the new value.
    /// </summary>
    Task<int> NextTargetSeqNum();

    #endregion

    #region Session Operations

    /// <summary>
    /// Session creation/reset timestamp.
    /// </summary>
    DateTime CreationTime { get; }

    /// <summary>
    /// Resets the session - clears all messages, resets sequence numbers to 1,
    /// and updates the creation time.
    /// </summary>
    Task Reset();

    #endregion

    #region Lifecycle

    /// <summary>
    /// Initializes the store - loads existing state from files if present.
    /// Must be called before any other operations.
    /// </summary>
    Task Initialize();

    /// <summary>
    /// Flushes any pending writes to disk.
    /// </summary>
    Task Flush();

    #endregion
}
