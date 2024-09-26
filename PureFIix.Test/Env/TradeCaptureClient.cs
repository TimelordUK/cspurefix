using Arrow.Threading.Tasks;
using PureFix.Buffer;
using PureFix.Buffer.Ascii;
using PureFix.Transport.Ascii;
using PureFix.Transport.Session;
using PureFix.Transport.Store;
using PureFix.Types;
using PureFix.Types.FIX50SP2.QuickFix;
using PureFix.Types.FIX50SP2.QuickFix.Types;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFIix.Test.Env
{
    internal class TradeCaptureClient : BaseApp
    { 
        private readonly FixMessageFactory m_msg_factory = new();      
        private readonly Dictionary<string, TradeCaptureReport> m_reports = [];
        private readonly TradeFactory m_tradeFactory;

        public TradeCaptureClient(IFixConfig config, IMessageTransport transport, IFixMessageFactory fixMessageFactory, IMessageParser parser, IMessageEncoder encoder, AsyncWorkQueue q, IFixClock clock) : base(config, transport, fixMessageFactory, parser, encoder, q, clock)
        {
            m_logReceivedMessages = true;
            m_tradeFactory = new TradeFactory(clock);
        }

        protected override async Task OnApplicationMsg(string msgType, MsgView view)
        {
            var res = await m_msgStore.Put(FixMsgStoreRecord.ToMsgStoreRecord(view));
            m_logger.Info($"store state {res}");
            switch (msgType)
            {
                case MsgType.TradeCaptureReport:
                    {
                        var tc = (TradeCaptureReport)m_msg_factory.ToFixMessage(view);
                        m_reports[tc.TradeReportID] = tc;
                        m_logger.Info($"[reports: {m_reports.Count}] received tc ExecID = {tc.ExecID} TradeReportID = {tc.TradeReportID} Symbol = {tc.Instrument.Symbol} {tc.LastQty} @ ${tc.LastPx}");
                        break;
                    }

                case MsgType.TradeCaptureReportRequestAck:
                    {
                        var tca = (TradeCaptureReportRequestAck)m_msg_factory.ToFixMessage(view);
                        m_logger.Info($"received tcr ack {tca.TradeRequestID} {tca.TradeRequestStatus}");
                        break;
                    }
            }
        }

        protected override bool OnLogon(MsgView view, string user, string password)
        {
            var msg = m_msg_factory.ToFixMessage(view);
            m_logger.Info($"peer logs in user {user}");
            return true;
        }

        protected override async Task OnReady(MsgView view)
        {
            m_logger.Info("OnReady");
            var tcr = TradeFactory.MakeTradeCaptureReportRequest("all-trades", m_clock.Current);
            await Send(MsgTypeValues.TradeCaptureReportRequest, tcr);
        }

        protected override void OnStopped(Exception error)
        {
            m_logger.Info("OnStopped");
        }
    }
}
