namespace PureFix.Transport.Session
{
    public interface ISessionFactory
    {
        /// <summary>
        /// Creates a session bound to a single connection's dependencies.
        /// </summary>
        /// <remarks>
        /// Implementations must pass <see cref="ISessionScope.Config"/>,
        /// <see cref="ISessionScope.Parser"/> and <see cref="ISessionScope.Encoder"/> through to
        /// the session rather than dependencies captured from the container. An acceptor calls
        /// this once per accepted connection, so a factory that hands out container singletons
        /// gives every concurrent counterparty the same parse buffer and outbound sequence number.
        /// </remarks>
        FixSession MakeSession(ISessionScope scope);
    }
}
