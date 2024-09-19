using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types;

namespace PureFix.Transport.Session
{
    public abstract class FixSession
    {
        protected string? m_me;
        protected bool m_initiator;
        protected bool m_acceptor;
        protected readonly FixSessionState  m_sessionState;
        protected string? m_requestLogoutType;
        protected string? m_respondLogoutType;
        protected string? m_requestLogonType;
        protected ILogger? m_logger;

        public FixSession(ILogFactory logFactory)
        {
            m_logger = logFactory.MakeLogger<FixSession>();
        }
    }
}
