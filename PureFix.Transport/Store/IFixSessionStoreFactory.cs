namespace PureFix.Transport.Store;

/// <summary>
/// Factory for creating session stores.
/// </summary>
public interface IFixSessionStoreFactory
{
    /// <summary>
    /// Creates a session store for the given session identifier.
    /// </summary>
    IFixSessionStore Create(SessionId sessionId);
}
