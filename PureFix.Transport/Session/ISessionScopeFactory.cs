namespace PureFix.Transport.Session
{
    /// <summary>
    /// Creates a fresh <see cref="ISessionScope"/> per connection.
    /// </summary>
    public interface ISessionScopeFactory
    {
        /// <summary>
        /// Builds an isolated set of per-connection dependencies. Every call must return
        /// newly constructed parser, encoder and session description instances - returning
        /// shared ones reintroduces cross-connection message corruption.
        /// </summary>
        ISessionScope CreateScope();
    }
}
