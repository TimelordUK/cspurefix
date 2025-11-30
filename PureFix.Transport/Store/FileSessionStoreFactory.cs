namespace PureFix.Transport.Store;

/// <summary>
/// Factory for creating file-based session stores.
/// </summary>
public sealed class FileSessionStoreFactory : IFixSessionStoreFactory
{
    private readonly string _directory;

    /// <summary>
    /// Creates a factory that stores files in the specified directory.
    /// </summary>
    /// <param name="directory">Base directory for store files</param>
    public FileSessionStoreFactory(string directory)
    {
        _directory = directory;
    }

    public IFixSessionStore Create(SessionId sessionId)
    {
        return new FileSessionStore(sessionId, _directory);
    }
}

/// <summary>
/// Factory for creating in-memory session stores.
/// </summary>
public sealed class MemorySessionStoreFactory : IFixSessionStoreFactory
{
    public IFixSessionStore Create(SessionId sessionId)
    {
        return new MemorySessionStore(sessionId);
    }
}
