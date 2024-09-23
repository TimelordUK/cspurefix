using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using PureFix.Buffer;
using PureFix.Buffer.Ascii;
using PureFix.Dictionary.Contained;
using PureFix.Dictionary.Definition;
using PureFix.Types;
using PureFix.Types.Config;

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
        protected string m_requestLogonType;
        protected bool m_checkMsgIntegrity;
        protected ILogger? m_sessionLogger;
        protected ISessionMessageFactory m_factory;
        protected IFixDefinitions Definitions { get; set; }
        protected IFixClock m_clock;
        protected IMessageParser m_parser;
        protected IMessageEncoder m_encoder;
        protected bool m_manageSession;
        protected bool m_logReceivedMessages;
        protected IMessageTransport? m_transport;
        protected IFixConfig m_config;
        private CancellationToken? m_token;
        private readonly List<MsgView> _messages = new();

        protected FixSession(IFixConfig config, IMessageTransport transport, IMessageParser parser, IMessageEncoder encoder, IFixClock clock)
        {
            if (config.Definitions == null)
            {
                throw new ArgumentException("config had been supplied with no definitions");
            }
            if (config.MessageFactory == null)
            {
                throw new ArgumentException("config had been supplied with no message factory");
            }
            if (config.LogFactory == null)
            {
                throw new ArgumentException("config had been supplied with no log factory");
            }
            m_config = config;
            m_transport = transport;
            m_logReceivedMessages = true;
            m_manageSession = true;
            m_clock = clock;
            Definitions = config.Definitions;
            m_factory = config.MessageFactory;
            m_parser = parser;
            m_encoder = encoder;
            var sessionDescription = config.Description;
            if (sessionDescription?.Application == null)
                throw new InvalidDataException("no application provided in session config");
            m_me = sessionDescription.Application.Name ?? "me";
            m_sessionLogger = config.LogFactory.MakeLogger($"{m_me}:FixSession");
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
            if (state != m_sessionState.State)
            {
                var currentState = m_sessionState.State;
                m_sessionLogger?.Info($"current state {currentState} ({currentState} moves to {state})");
                m_sessionState.State = state;
            }
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
                        m_sessionLogger?.Info($"ignoring request to change stat e as now already in {currentState}");
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

        public async Task SendLogon()
        {
            if (m_requestLogonType == null)
                throw new InvalidDataException("app session needs to assign m_requestLogonType");
            var lo = m_factory.Logon();
            if (lo != null)
            {
                await Send(m_requestLogonType, lo);
            }
        }

        protected async Task SendLogout(string msg)
        {
            if (m_requestLogoutType == null)
                throw new InvalidDataException("session needs m_requestLogoutType assigned");
            m_sessionLogger?.Info($"sending logout with {msg}");
            var lo = m_factory?.Logout(msg);
            if (lo != null)
            {
                await Send(m_requestLogoutType, lo);
            }
        }
        
        protected async Task PeerLogout(MsgView view) {
            var msg = view.GetString((int)MsgTag.Text);
            var state = m_sessionState.State;
            switch (state) {
                case SessionState.WaitingLogoutConfirm:
                {
                    m_sessionLogger?.Info($"peer confirms logout Text = '{msg}'");
                    Stop();
                    break;
                }

                case SessionState.InitiationLogonResponse:
                case SessionState.ActiveNormalSession:
                case SessionState.InitiationLogonReceived:
                {
                    SetState(SessionState.ConfirmingLogout);
                    m_sessionLogger?.Info($"peer initiates logout Text = '{msg}'");
                    await SessionLogout();
                    break;
                }
            }
        }

        protected async Task SessionLogout() {
            if (m_sessionState.LogoutSentAt != null)
            {
                return;
            }

            switch (m_sessionState.State) {
                case SessionState.ActiveNormalSession:
                case SessionState.InitiationLogonResponse:
                case SessionState.InitiationLogonReceived: {
                    // this instance initiates logout
                    SetState(SessionState.WaitingLogoutConfirm);
                    m_sessionState.LogoutSentAt = m_clock.Current;
                    var msg = $"{m_me} initiate logout";
                    m_sessionLogger?.Info(msg);
                    await SendLogout(msg);
                    break;
                }

                case SessionState.ConfirmingLogout: {
                    // this instance responds to log out
                    SetState(SessionState.ConfirmingLogout);
                    m_sessionState.LogoutSentAt = m_clock.Current;
                    var msg = $"{m_me} confirming logout";
                    m_sessionLogger?.Info(msg);
                    await SendLogout(msg);
                    break;
                }

                default:
                {
                    m_sessionLogger?.Info($"sessionLogout ignored as in state {m_sessionState.State}");
                    break;
                }
            }
        }

        protected void Stop(Exception? error = null) {
            if (m_sessionState.State == SessionState.Stopped)
            {
                return;
            }
            // this.stopTimer();
            m_sessionLogger?.Info("stop: kill transport");
            if (error != null)
            {
                m_sessionLogger?.Info($"stop: emit error ${error}");
            }
            
            SetState(SessionState.Stopped);
            OnStopped(error);
            m_transport = null;
        }

        protected async Task Send(string msgType, IFixMessage message)
        {
            if (m_transport == null) return;
            switch (m_sessionState.State)
            {
                case SessionState.Stopped:
                    m_sessionLogger?.Info($"can't send {msgType}, state is now {m_sessionState.State}");
                    break;

                default:
                    {
                        if (m_token == null) return;
                        if (m_transport == null) return;
                        var storage = m_encoder.Encode(msgType, message);
                        if (storage == null) return;
                        m_sessionLogger?.Debug($"sending {msgType}, pos = {storage.Buffer.Pos}");
                        await m_transport.SendAsync(storage.AsBytes(), m_token.Value);
                        m_encoder.Return(storage);
                        break;
                    }
            }
        }

        public async Task OnTimer()
        {
            await Tick();
        }

        public async Task OnRx(byte[] buffer)
        {
            _messages.Clear();
            m_sessionLogger?.Debug($"OnRx {buffer.Length}");
            m_parser.ParseFrom(buffer, (p, v) => _messages.Add(v), OnFixLog);
            if (_messages.Count == 0) return;
            m_sessionLogger?.Info($"received {_messages.Count}");
            foreach (var msg in _messages)
            {
                await RxOnMsg(msg);
                m_parser.Return(((AsciiView)msg).Storage);
            }
        }

        protected void OnFixLog(StoragePool.Storage storage)
        {
            var decoded = storage.AsString(AsciiChars.Pipe);
            var msgType = storage.GetStringAt(3);
            if (msgType == null) return;
            OnDecoded(msgType, decoded);
        }
        
        private async Task RxOnMsg(MsgView view)
        {
            if (view.Structure == null) return;

            var msgType = view.GetString((int)MsgTag.MsgType);
            if (msgType == null) return;
            if (m_logReceivedMessages)
            {
                m_sessionLogger?.Info($"{msgType}: {view}");
            }

            if (m_manageSession)
            {
                await OnMsg(msgType, view);
            }
            else
            {
                await CheckForwardMessage(msgType, view);
            }
        }

        private async Task ReveiveMessages(EventDispatcher dispatcher, CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                var msg = await dispatcher.WaitRead();
                if (msg.Data == null)
                {
                    await OnTimer();
                }
                else
                {
                    await OnRx(msg.Data);
                }
            }
        }

        private async Task InitiatorLogon() {
            if (m_transport != null)
            {
                m_sessionLogger?.Info("reset from previous transport");
            }
         
            if (m_initiator)
            {
                m_sessionLogger?.Debug($"initiator sending logon state = {m_sessionState.State}");
                await SendLogon();
                SetState(SessionState.InitiationLogonSent);
            }
            else
            {
                m_sessionLogger?.Debug($"acceptor waits for logon state = {m_sessionState.State}");
                SetState(SessionState.WaitingForALogon);
            }
        }

        public async Task Run(IMessageTransport transport, CancellationToken token)
        {
            m_token = token;
            await InitiatorLogon();            
            var dispatcher = new EventDispatcher(transport);
            // start sending events to the channel on which this session listens.
            await dispatcher.Dispatch(TimeSpan.FromMilliseconds(100), token);
            await ReveiveMessages(dispatcher, token);   
        }

        private async Task CheckForwardMessage(string msgType, MsgView view)
        {
            m_sessionLogger?.Info($"forwarding msgType = '{msgType}' to application");
            SetState(SessionState.ActiveNormalSession);
            await OnApplicationMsg(msgType, view);
        }

        public async Task Done()
        {
            switch (m_sessionState.State)
            {
                case SessionState.InitiationLogonResponse:
                case SessionState.ActiveNormalSession:
                case SessionState.InitiationLogonReceived:
                {
                    await SessionLogout();
                    break;
                }

                case SessionState.Stopped:
                    m_sessionLogger?.Info("done. session is now stopped");
                    break;
           
                default:
                {
                    Stop();
                    break;
                }
            }

            m_sessionLogger?.Info($"done.check logout sequence state ${ m_sessionState.State}");
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
        protected abstract Task OnMsg(string msgType, MsgView view);

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
        protected abstract Task OnApplicationMsg(string msgType, MsgView view);

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
        protected abstract void OnStopped(Exception? error);

        /**
         * Placeholder infomring the application of a peer login attempt.
         * @param view the login message
         * @param user extracted user from message
         * @param password extracted password from the message.
         * @protected
         */

        protected abstract bool OnLogon(MsgView view, string? user, string? password);

        protected abstract Task Tick();
       public void End()
        {
        }
    }
}
