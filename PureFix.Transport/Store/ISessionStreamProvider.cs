namespace PureFix.Transport.Store;

/// <summary>
/// Provides stream access for session store operations.
/// Allows abstraction of file I/O for testing with in-memory streams.
/// </summary>
public interface ISessionStreamProvider
{
    /// <summary>
    /// Creates or opens a read-write stream for the message body file.
    /// Stream should support seeking.
    /// </summary>
    Stream OpenBodyStream();

    /// <summary>
    /// Creates a StreamWriter for the header index file.
    /// </summary>
    StreamWriter OpenHeaderWriter();

    /// <summary>
    /// Reads all text from the sequence numbers file.
    /// Returns null if the file doesn't exist.
    /// </summary>
    Task<string?> ReadSeqNumsAsync();

    /// <summary>
    /// Writes all text to the sequence numbers file.
    /// </summary>
    Task WriteSeqNumsAsync(string content);

    /// <summary>
    /// Reads all text from the session time file.
    /// Returns null if the file doesn't exist.
    /// </summary>
    Task<string?> ReadSessionTimeAsync();

    /// <summary>
    /// Writes all text to the session time file.
    /// </summary>
    Task WriteSessionTimeAsync(string content);

    /// <summary>
    /// Reads all lines from the header index file.
    /// Returns empty array if file doesn't exist.
    /// </summary>
    Task<string[]> ReadHeaderLinesAsync();

    /// <summary>
    /// Resets all streams/files for a new session.
    /// Called during store Reset().
    /// </summary>
    Task ResetAsync();

    /// <summary>
    /// Gets the body stream directly for reading.
    /// Returns the same stream as OpenBodyStream but allows explicit access.
    /// </summary>
    Stream? GetBodyStream();
}
