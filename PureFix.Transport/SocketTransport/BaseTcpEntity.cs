using PureFix.Transport.Session;
using PureFix.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Transport.SocketTransport
{
    internal abstract class BaseTcpEntity
    {
      
        protected readonly ILogger m_logger;
        protected readonly ISessionFactory m_sessionFactory;

        public BaseTcpEntity(ISessionFactory sessionFactory, IFixConfig config, IFixClock clock, ILogFactory logFactory)
        {
            ArgumentNullException.ThrowIfNull(sessionFactory);
            ArgumentNullException.ThrowIfNull(config);
            ArgumentNullException.ThrowIfNull(clock);
            ArgumentNullException.ThrowIfNull(logFactory);
           
            m_logger = logFactory.MakeLogger("BaseTcpEntity");
            m_sessionFactory = sessionFactory;
        }

        public abstract Task Start(CancellationToken cancellationToken);
    }
}
