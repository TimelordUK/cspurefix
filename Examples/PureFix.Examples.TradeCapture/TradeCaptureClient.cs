using Arrow.Threading.Tasks;
using PureFix.Buffer;
using PureFix.Transport.Ascii;
using PureFix.Transport.Recovery;
using PureFix.Transport.Session;
using PureFix.Types;
using PureFix.Types.Core;
using PureFix.Types.FIX50SP2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Examples.TradeCapture
{
    internal class TradeCaptureClient : BaseApp
    {
        private readonly FixMessageFactory m_msg_factory = new();
        private readonly Dictionary<string, TradeCaptureReport> m_reports = [];
        public Action? OnReadyCallback { get; set; }

        public TradeCaptureClient(IFixConfig config, IFixLogRecovery? fixLogRecover, ILogFactory logFactory, IFixMessageFactory fixMessageFactory, IMessageParser parser, IMessageEncoder encoder, AsyncWorkQueue q, IFixClock clock) : base(config, fixLogRecover, logFactory, fixMessageFactory, parser, encoder, q, clock)
        {
            m_logReceivedMessages = true;
        }

        protected override Task OnApplicationMsg(string msgType, IMessageView view)
        {

            switch (msgType)
            {
                case MsgType.TradeCaptureReport:
                    {
                        var tc = (TradeCaptureReport)m_msg_factory.ToFixMessage(view)!;
                        m_reports[tc?.TradeReportID ?? ""] = tc;
                        m_logger.Info($"{JsonHelper.ToJson(tc)}");
                        m_logger.Info($"[reports: {m_reports.Count}] received tc ExecID = {tc?.ExecID} TradeReportID = {tc?.TradeReportID} Symbol = {tc?.Instrument?.Symbol} {tc?.LastQty} @ ${tc?.LastPx}");
                        break;
                    }

                case MsgType.TradeCaptureReportRequestAck:
                    {
                        var tca = (TradeCaptureReportRequestAck)m_msg_factory.ToFixMessage(view)!;
                        m_logger.Info($"received tcr ack {tca?.TradeRequestID} {tca?.TradeRequestStatus}");
                        break;
                    }
            }

            return Task.CompletedTask;
        }

        protected override bool OnLogon(IMessageView view, string user, string password)
        {
            var msg = m_msg_factory.ToFixMessage(view);
            m_logger.Info($"peer logs in user {user} {msg}");
            return true;
        }

        protected override async Task OnReady(IMessageView view)
        {
            m_logger.Info("OnReady");
            OnReadyCallback?.Invoke();
            var tcr = TradeFactory.MakeTradeCaptureReportRequest("all-trades", m_clock.Current);
            await Send(MsgTypeValues.TradeCaptureReportRequest, tcr);
        }

        protected override void OnStopped(Exception error)
        {
            m_logger.Info("OnStopped");
        }
    }
}
