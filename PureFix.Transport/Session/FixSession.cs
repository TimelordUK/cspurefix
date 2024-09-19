using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;
using PureFix.Buffer.Ascii;
using PureFix.Dictionary.Definition;
using PureFix.Types;
using PureFix.Types.Config;

namespace PureFix.Transport.Session
{
    public abstract class FixSession : ISessionEventReciever
    {
        protected string? m_me;
        protected bool m_initiator;
        protected bool m_acceptor;
        protected readonly FixSessionState  m_sessionState;
        protected string? m_requestLogoutType;
        protected string? m_respondLogoutType;
        protected string m_requestLogonType;
        protected bool m_checkMsgIntegrity;
        protected ILogger? m_logger;
        protected ISessionMessageFactory m_factory;
        protected IFixDefinitions Definitions { get; set; }
        protected IFixClock m_clock;
        protected AsciiParser m_parser;

        protected FixSession(IFixDefinitions definitions, SessionDescription sessionDescription, AsciiParser parser, ISessionMessageFactory messageFactory, IFixClock clock, ILogFactory logFactory)
        {
            m_clock = clock;
            Definitions = definitions;
            m_factory = messageFactory;
            m_parser = parser;
            if (sessionDescription.Application == null)
                throw new InvalidDataException("no application provided in session config");
            m_me = sessionDescription.Application.Name ?? "me";
            m_logger = logFactory.MakeLogger($"{m_me}:FixSession");
            m_initiator = sessionDescription.Application.Type == "initiator";
            m_acceptor = !m_initiator;
            m_checkMsgIntegrity = m_acceptor;
            m_sessionState = new FixSessionState(new FixSessionStateArgs
            {
                HeartBeat = sessionDescription.HeartBtInt ?? 30,
                LastPeerMsgSeqNum = sessionDescription.LastReceivedSeqNum
            });
            if (!Definitions.Message.TryGetValue("Logon", out var logonDefinition))
            {
                throw new InvalidDataException("definitions must contain Logon definition.");
            }
            if (!Definitions.Message.TryGetValue("Logout", out var logoutDefinition))
            {
                throw new InvalidDataException("definitions must contain Logout definition.");
            }
            m_requestLogonType = logonDefinition.MsgType;
            m_requestLogoutType = logoutDefinition.MsgType;
            m_respondLogoutType = logoutDefinition.MsgType;
        }

        private void AssignState(SessionState state)
        {
            var currentState = m_sessionState.State;
            m_logger?.Info($"current state {currentState} ({currentState} moves to {state}");
            m_sessionState.State = state;
        }

        public void SetState(SessionState state)
        {
            var currentState = m_sessionState.State;
            if (currentState == state) return;
            switch (currentState)
            {
                case SessionState.ConfirmingLogout:
                case SessionState.Stopped:
                    if (currentState != SessionState.NetworkConnectionEstablished)
                    {
                        m_logger?.Info($"ignoring request to change stat e as now already in {currentState}");
                    }
                    else
                    {
                        AssignState(state);
                    }
                    break;

                default:
                    AssignState(state);
                    break;
            }
        }

        public void SendLogon()
        {
            if (m_requestLogonType == null)
                throw new InvalidDataException("app session needs to assign m_requestLogonType");
            var lo = m_factory.Logon();
            if (lo != null)
            {
                Send(m_requestLogonType, lo);
            }
        }

        protected void Send(string msgType, IFixMessage message)  {
        }

        public void OnTimer()
        {
        }

        public void OnRx(ReadOnlySpan<byte> buffer)
        {
            m_parser.ParseFrom(buffer, OnMsg, OnFixLog);
        }
        protected abstract void OnFixLog(AsciiParser.Pool.Storage storage);
        
        private void OnMsg(int i, AsciiView view)
        {
        }

        public void Done()
        {
        }

        public void End()
        {
        }
    }
}
