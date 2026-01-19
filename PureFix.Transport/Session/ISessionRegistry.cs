using PureFix.Transport.Store;

namespace PureFix.Transport.Session
{
    /// <summary>
    /// Tracks active sessions by their SessionId.
    /// When a new session with the same ID is registered, the old one is stopped first.
    /// This prevents stale sessions from running after a client reconnects.
    /// </summary>
    public interface ISessionRegistry
    {
        /// <summary>
        /// Registers a session. If there's an existing session with the same ID, stops it first.
        /// </summary>
        /// <param name="sessionId">The unique identifier for this session.</param>
        /// <param name="session">The session to register.</param>
        /// <returns>True if an old session was stopped, false if this is a new registration.</returns>
        bool Register(SessionId sessionId, FixSession session);

        /// <summary>
        /// Unregisters a session when it stops.
        /// Only removes the session if it's the currently registered one (prevents race conditions).
        /// </summary>
        /// <param name="sessionId">The unique identifier for this session.</param>
        /// <param name="session">The session to unregister (must match the registered one).</param>
        void Unregister(SessionId sessionId, FixSession session);
    }
}
