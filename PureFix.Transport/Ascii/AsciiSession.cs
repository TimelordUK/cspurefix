using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;
using PureFix.Buffer;
using PureFix.Buffer.Ascii;
using PureFix.Transport.Session;
using PureFix.Types;


namespace PureFix.Transport.Ascii
{
    public abstract class AsciiSession : FixSession
    {
        public bool Heartbeat { get; set; }
        protected AsciiSession(IFixConfig config, IMessageTransport transport, IMessageParser parser, IMessageEncoder encoder, IFixClock clock) : base(config, transport, parser, encoder, clock)
        {
        }

        private async Task SendTestRequest()
        {
            SetState(SessionState.AwaitingProcessingResponseToTestRequest);
            var now = m_clock.Current.ToUniversalTime().ToString(CultureInfo.InvariantCulture);
            var text = $"test-req-{now}";
            var tr = m_factory?.TestRequest(text);
            if (tr != null)
            {
                await Send(MsgType.TestRequest, tr);
            }
        }

        private async Task SendHeartbeat()
        {
            var now = m_clock.Current.ToUniversalTime().ToString(CultureInfo.InvariantCulture);
            var text = $"heartbeat-{now}";
            var tr = m_factory?.Heartbeat(text);
            if (tr != null)
            {
                await Send(MsgType.Heartbeat, tr);
            }
        }

        private async Task Tick()
        {
            if (m_transport == null) return;
            var action = m_sessionState.CalcAction(m_clock.Current);
            switch (action)
            {
                case TickAction.Nothing:
                    break;

                case TickAction.TestRequest:
                    {
                        m_sessionLogger?.Debug($"sending test req. state = {m_sessionState.State}");
                        await SendTestRequest();
                        break;
                    }

                case TickAction.Heartbeat:
                    {
                        m_sessionLogger?.Debug($"sending heartbeat. state = {m_sessionState.State}");
                        await SendHeartbeat();
                        break;
                    }

                case TickAction.TerminateOnError:
                    {
                        m_sessionLogger?.Warn(m_sessionState.ToString());
                        break;
                    }
            }
        }

        private async Task PeerLogon(MsgView view)
        {
            var logger = m_sessionLogger;
            var heartBtInt = view.HeartBtInt();
            var peerCompId = view.SenderCompID();
            var userName = view.Username();
            var password = view.Password();
            logger?.Info($"peerLogon Username = {userName}, heartBtInt = {heartBtInt}, peerCompId = {peerCompId}, userName = {userName}");
            var state = m_sessionState;
            state.PeerHeartBeatSecs = view.MsgSeqNum();
            state.PeerCompID = peerCompId;
            var res = OnLogon(view, userName, password);
            // currently not using this.
            logger?.Info("peerLogon onLogon returns {res}");
            if (m_acceptor)
            {
                SetState(SessionState.InitiationLogonResponse);
                logger?.Info("acceptor responds to logon request");
                await SendLogon();  // if res send response else reject, terminate
            }
            else
            { // as an initiator the acceptor has responded
                logger?.Info("initiator receives logon response");
                SetState(SessionState.InitiationLogonReceived);
            }
            if (Heartbeat)
            {
                //   this.startTimer();
            }
            logger?.Info("system ready, inform app");
            OnReady(view);
        }

        private async Task<bool> CheckSeqNo(string msgType, MsgView view)
        {
            switch (msgType)
            {
                case MsgType.SequenceReset:
                    {
                        return true;
                    }

                default:
                    {
                        var state = m_sessionState;
                        var lastSeq = state.LastPeerMsgSeqNum;
                        var seqNo = view.MsgSeqNum();
                        bool ret = false;
                        var seqDelta = seqNo - lastSeq;
                        if (seqDelta <= 0)
                        {
                            // serious problem ... drop immediately
                            m_sessionLogger?.Warn($"terminate as seqDelta(${seqDelta}) < 0 lastSeq = ${lastSeq} seqNo = ${seqNo}");
                            Stop();
                        }
                        else if (seqDelta > 1)
                        {
                            // resend request required as have missed messages.
                            // We process a Logon beforehand to confirm the connection even we out of sync
                            if (msgType == MsgType.Logon)
                            {
                                await PeerLogon(view);
                            }
                            // If the out of sync message is a resend request itself, then we handle it first in order
                            // to avoid triggering an endless loop of both sides sending resend requests in response to resend requests.
                            if (msgType == MsgType.ResendRequest)
                            {
                               // await OnResendRequest(view);
                            }
                           // SendResendRequest(lastSeq, seqNo);
                        }
                        else
                        {
                            ret = true;
                            state.LastPeerMsgSeqNum = seqNo;
                        }
                        return ret;
                    }
            }
        }

        /**
   * Override to resend stored messages following a sequence reset.
   * @protected
   */
     
        /*
        protected void OnResendRequest(MsgView view) {
            // if no records are in store then send a gap fill for entire sequence
            SetState(SessionState.HandleResendRequest);
            var beginSeqNo = view.BeginSeqNo();
            var requestedEndSeqNo = view.EndSeqNo();
            var endSeqNo = requestedEndSeqNo == 0
      ? m_sessionState.LastSentSeqNum
      : requestedEndSeqNo;
            m_sessionLogger?.Info($"onResendRequest getResendRequest beginSeqNo = {beginSeqNo}, endSeqNo = {endSeqNo}");
            this.resender.getResendRequest(beginSeqNo, endSeqNo).then((records: IFixMsgStoreRecord[]) => {
            const validRecords = records.filter(rec => rec.obj !== null)
            this.sessionLogger.info(`sending ${ validRecords.length}`)
            validRecords.forEach(rec => {
                if (rec.obj)
                {
                    this.send(rec.msgType, rec.obj)
                }
            });
                SetState(SessionState.ActiveNormalSession);
    }).catch((e: Error) => {
    this.sessionLogger.error(e)
    })
  }

        protected void CheckForwardMsg (string msgType, MsgView view) {
            var okToForward = ValidStateApplicationMsg();
            if (okToForward) {
                m_sessionLogger?.Info($"ascii forwarding msgType = '{msgType}' to application");
                SetState(SessionState.ActiveNormalSession);
                OnApplicationMsg(msgType, view);
            } else {
                Terminate(new Exception($"msgType ${msgType} received in state {m_sessionState.State}"));
            }
        }
    }*/
    }
}
