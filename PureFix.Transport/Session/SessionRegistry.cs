using System.Collections.Concurrent;
using PureFix.Transport.Store;
using PureFix.Types;

namespace PureFix.Transport.Session
{
    /// <summary>
    /// Thread-safe registry for tracking active FIX sessions.
    /// Ensures only one session per SessionId is active at a time.
    /// When a new connection arrives for the same SessionId, the old session is stopped.
    /// </summary>
    public class SessionRegistry : ISessionRegistry
    {
        private readonly ConcurrentDictionary<string, FixSession> _sessions = new();
        private readonly ILogger? _logger;

        public SessionRegistry(ILogFactory? logFactory = null)
        {
            _logger = logFactory?.MakeLogger("SessionRegistry");
        }

        /// <inheritdoc />
        public bool Register(SessionId sessionId, FixSession session)
        {
            var key = sessionId.ToString();
            var stoppedOld = false;

            _logger?.Info("Register called for SessionId={SessionId}, ActiveSessions={Count}", key, _sessions.Count);

            // Use AddOrUpdate to atomically handle the case where an old session exists
            _sessions.AddOrUpdate(
                key,
                // Add case - no existing session
                addValueFactory: _ =>
                {
                    _logger?.Info("No existing session found - registering new session: {SessionId}", key);
                    return session;
                },
                // Update case - existing session found
                updateValueFactory: (_, existingSession) =>
                {
                    if (existingSession != session)
                    {
                        _logger?.Info("FOUND EXISTING SESSION for {SessionId} - this is a reconnection scenario!", key);
                        _logger?.Info("Stopping old session to prevent stale transport writes...");
                        existingSession.RequestStop("Replaced by new connection - stopping old session with stale transport");
                        stoppedOld = true;
                        _logger?.Info("Old session stop requested, registering new session");
                    }
                    else
                    {
                        _logger?.Debug("Same session instance already registered for {SessionId}", key);
                    }
                    return session;
                });

            _logger?.Info("Register complete for {SessionId}: StoppedOldSession={StoppedOld}, TotalActiveSessions={Count}",
                key, stoppedOld, _sessions.Count);
            return stoppedOld;
        }

        /// <inheritdoc />
        public void Unregister(SessionId sessionId, FixSession session)
        {
            var key = sessionId.ToString();

            _logger?.Info("Unregister called for SessionId={SessionId}", key);

            // Only remove if the session matches (prevents race where new session registered before old one unregisters)
            var removed = _sessions.TryRemove(new KeyValuePair<string, FixSession>(key, session));

            if (removed)
            {
                _logger?.Info("Successfully unregistered session: {SessionId}, RemainingActiveSessions={Count}", key, _sessions.Count);
            }
            else
            {
                // This is expected when a new session has already replaced this one
                _logger?.Info("Session NOT unregistered (already replaced by new connection or not found): {SessionId}", key);
            }
        }
    }
}
