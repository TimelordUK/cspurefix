using Arrow.Threading.Tasks;
using PureFix.Buffer;
using PureFix.Transport.Ascii;
using PureFix.Transport.Recovery;
using PureFix.Transport.Session;
using PureFix.Transport.Store;
using PureFix.Types;
using PureFix.Types.FIX50SP2.QuickFix;
using PureFix.Types.FIX50SP2.QuickFix.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Examples.TradeCapture
{
    internal class TradeCaptureServer : BaseApp
    {
        private readonly FixMessageFactory m_msg_factory = new();
        private readonly TradeFactory m_tradeFactory;
        public Action? OnReadyCallback { get; set; }

        public TradeCaptureServer(IFixConfig config, IFixLogRecovery fixLogRecover, ILogFactory logFactory, IFixMessageFactory fixMessageFactory, IMessageParser parser, IMessageEncoder encoder, IFixMsgStore store, AsyncWorkQueue q, IFixClock clock) : base(config, fixLogRecover, logFactory, fixMessageFactory, parser, encoder, store, q, clock)
        {
            m_logReceivedMessages = true;
            m_tradeFactory = new TradeFactory(clock);
        }

        protected override async Task OnApplicationMsg(string msgType, IMessageView view)
        {
            var res = await m_msgStore.Put(FixMsgStoreRecord.ToMsgStoreRecord(view));
            var seqNo = view.GetInt32((int)MsgTag.MsgSeqNum);
            m_logger.Info($"store state {res}");
            switch (msgType)
            {
                case MsgType.TradeCaptureReportRequest:
                    {
                        var tcr = (TradeCaptureReportRequest)m_msg_factory.ToFixMessage(view);
                        await ServiceTradeCaptureReportRequest(tcr);
                        break;
                    }

                default:
                    {
                        var reject = m_config.MessageFactory?.Reject(msgType, seqNo ?? 0, "unknown msg type.", BusinessRejectReasonValues.UnsupportedMessageType);
                        if (reject != null)
                        {
                            await Send(MsgTypeValues.Reject, reject);
                            m_logger.Info($"rejecting message type {msgType}");
                        }
                        break;
                    }
            }
        }

        private async Task ServiceTradeCaptureReportRequest(TradeCaptureReportRequest tcr)
        {
            m_logger.Info($"received tcr {tcr.TradeRequestID}");
            var ack1 = TradeFactory.MakeTradeCaptureReportRequestAck(tcr, TradeRequestStatusValues.Accepted);
            await Send(MsgTypeValues.TradeCaptureReportRequestAck, ack1);
            await CreateSendBatch(10);
            var ack2 = TradeFactory.MakeTradeCaptureReportRequestAck(tcr, TradeRequestStatusValues.Completed);
            await Send(MsgTypeValues.TradeCaptureReportRequestAck, ack2);
            if (tcr.SubscriptionRequestType == SubscriptionRequestTypeValues.SnapshotAndUpdates)
            {
                var i = 1;
                var timer = new TimerDispatcher.AsyncTimer(m_logger);
                if (m_parentToken != null)
                    await timer.Start(TimeSpan.FromSeconds(5), async () =>
                    {
                        m_logger.Info($"sending batch of trades {i}");
                        await CreateSendBatch(i);
                        i %= 8;
                        i++;
                    }, m_parentToken.Value);
            }
        }

        private async Task CreateSendBatch(int size)
        {
            var batch = m_tradeFactory.MakeBatchOfTradeCaptureReport(size);
            foreach (var tc in batch)
            {
                await Send(MsgTypeValues.TradeCaptureReport, tc);
            }
        }

        protected override bool OnLogon(IMessageView view, string user, string password)
        {
            var msg = m_msg_factory.ToFixMessage(view);
            m_logger.Info($"peer logs in user {user}");
            return true;
        }

        protected override Task OnReady(IMessageView view)
        {
            m_logger.Info("OnReady");
            OnReadyCallback?.Invoke();
            return Task.CompletedTask;
        }

        protected override void OnStopped(Exception error)
        {
            m_logger.Info("OnStopped");
        }
    }
}
