using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;
using PureFix.Buffer;
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
        protected IMessageParser m_parser;
        protected IMessageEncoder m_encoder;
        protected bool m_manageSession;
        protected bool m_logReceivedMessages;
        protected IFixTransport m_transport;
        protected IFixConfig m_config;

        protected FixSession(IFixConfig config, IFixTransport transport, IMessageParser parser, IMessageEncoder encoder, ISessionMessageFactory messageFactory, IFixClock clock)
        {
            m_config = config;
            m_transport = transport;
            m_logReceivedMessages = true;
            m_manageSession = true;
            m_clock = clock;
            Definitions = config.Definitions;
            m_factory = messageFactory;
            m_parser = parser;
            m_encoder = encoder;
            var sessionDescription = config.Description;
            if (sessionDescription.Application == null)
                throw new InvalidDataException("no application provided in session config");
            m_me = sessionDescription.Application.Name ?? "me";
            m_logger = config.LogFactory.MakeLogger($"{m_me}:FixSession");
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

        protected Task Send(string msgType, IFixMessage message)
        {
            var storage = m_encoder.Encode(msgType, message);
            if (storage == null) return Task.CompletedTask;
            var t = m_transport.SendAsync(storage.AsBytes()).ContinueWith(_ =>
            {
                m_encoder.Return(storage);
            });
            return t;
        }

        public void OnTimer()
        {
        }

        public void OnRx(ReadOnlySpan<byte> buffer)
        {
            m_parser.ParseFrom(buffer, RxOnMsg, OnFixLog);
        }

        protected void OnFixLog(StoragePool.Storage storage)
        {
            var decoded = storage.AsString(AsciiChars.Pipe);
            var msgType = storage.GetStringAt(3);
            if (msgType == null) return;
            OnDecoded(msgType, decoded);
        }
        
        private void RxOnMsg(int i, MsgView view)
        {
            if (view.Structure == null) return;

            var msgType = view.Segment?.Set?.Name;
            if (msgType == null) return;
            if (m_logReceivedMessages)
            {
                m_logger?.Info($"{msgType}: {view}");
            }

            if (m_manageSession)
            {
                OnMsg(msgType, view);
            }
            else
            {
                CheckForwardMessage(msgType, view);
            }
        }

        private void CheckForwardMessage(string msgType, MsgView view)
        {
            m_logger?.Info($"forwarding msgType = '{msgType}' to application");
            SetState(SessionState.ActiveNormalSession);
            OnApplicationMsg(msgType, view);
        }

        public void Done()
        {
        }

        /**
           * dispatches a message into the subclass that inherits from FixSession. The view contains
           * the parsed message which has utility methods such as toObject(). The Ascii session provides
           * an implementation to handle admin level messages such as logon, hearbeat and resest request.
           * Any application messges are dispatched via onApplicationMsg where the application can action
           * the message.
           * @param msgType the string based msg type the view represents
           * @param view container for all parsed fields representing the received message.
           * @protected
           */
        protected abstract void OnMsg(string msgType, MsgView view);

        /**
         * the parsed txt recieved from the peer application.  Given the applicaton is
         * responible for maintaining the fix log, this can be used to persist all received
         * messages.
         * @param msgType the string based msg type the view represents
         * @param txt the received message where for Ascii, the wire SOH delimeter is replaced
         * with that specified in the config e.g. '|'
         * @protected
         */
        protected abstract void OnDecoded(string msgType, string txt);

        /**
         * the formatted txt sent to the peer application as an outbound message.  Given the applicaton is
         * responible for maintaining the fix log, this can be used to persist all transmitted
         * messages. use msgType for example to persist only trade capture messages to database
         * @param msgType the msg type representing the message.
         * @param txt the sent message where for Ascii, the wire SOH delimeter is replaced
         * with that specified in the config e.g. '|'
         * @protected
         */
        protected abstract void OnEncoded(string msgType, string txt);

        /**
         * typically all session level messages are handled by AsciiSession and these are
         * application level such as MarketDataRefresh. This will represent the applications main
         * work functiono where responses can be sent back to the peer. If manageSession has been set false
         * (not recommended) all messages are sent to this function
         * @param msgType the msg type representing the message.
         * @param view a wrapper containing the parsed message received.
         * @protected
         */
        protected abstract void OnApplicationMsg(string msgType, MsgView view);

        /**
         * at this point the application is ready to send messages - peer login has been achieved
         * and the session can be considered ready to use. In the case of an initiator the application
         * may at this point send for security definitions or send market data subscriptions
         * @param view the login message causing session to be ready
         * @protected
         */
        protected abstract void OnReady(MsgView view);

        /**
         * Inform application this session has now ended - either from logout or connection dropped
         * @param error if session has been terminated via an error it is provided
         * @protected
         */
        protected abstract void OnStopped(Exception error);

        /**
         * Placeholder infomring the application of a peer login attempt.
         * @param view the login message
         * @param user extracted user from message
         * @param password extracted password from the message.
         * @protected
         */

        protected abstract bool OnLogon(MsgView view, string user, string password);

        public void End()
        {
        }
    }
}
