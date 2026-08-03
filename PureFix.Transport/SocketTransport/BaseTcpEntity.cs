using PureFix.Transport.Session;
using PureFix.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PureFix.Transport.SocketTransport
{
    public abstract class BaseTcpEntity : ITcpEntity
    {
        protected readonly ILogger m_logger;
        protected readonly ISessionFactory m_sessionFactory;
        protected readonly IFixConfig m_config;
        protected readonly IFixClock m_clock;
        protected readonly ILogFactory m_logFactory;
        protected readonly ISessionScopeFactory m_scopeFactory;

        protected BaseTcpEntity(
            ISessionFactory sessionFactory,
            IFixConfig config,
            IFixClock clock,
            ILogFactory logFactory,
            ISessionScopeFactory? scopeFactory = null)
        {
            ArgumentNullException.ThrowIfNull(sessionFactory);
            ArgumentNullException.ThrowIfNull(config);
            ArgumentNullException.ThrowIfNull(clock);
            ArgumentNullException.ThrowIfNull(logFactory);

            m_logger = logFactory.MakeLogger("BaseTcpEntity");
            m_sessionFactory = sessionFactory;
            m_config = config;
            m_clock = clock;
            m_logFactory = logFactory;

            // Fall back to the built-in scope factory when the application has not
            // registered one, so a connection never shares parser/encoder state by default.
            m_scopeFactory = scopeFactory ?? new DefaultSessionScopeFactory(config, clock);
        }

        public abstract Task Start(CancellationToken cancellationToken);
    }
}
