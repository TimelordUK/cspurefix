using PureFix.Types;
using PureFix.Types.FIX50SP2.QuickFix;
using PureFix.Types.FIX50SP2.QuickFix.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Test.Env.TradeCapture
{
    internal class TradeFactory
    {
        private readonly IFixClock _fixClock;
        private int _nextTradeId = 100000;
        private int _nextExecId = 600000;
        private int _nextInstrumentId = 0;
        private readonly string[] _securities =
    [
      "Gold",
      "Silver",
      "Platinum",
      "Magnesium",
      "Steel"
    ];

        public TradeFactory(IFixClock clock)
        {
            _fixClock = clock;
        }

        public static TradeCaptureReportRequestAck MakeTradeCaptureReportRequestAck(TradeCaptureReportRequest tcr, int status)
        {
            return new TradeCaptureReportRequestAck
            {
                TradeRequestID = tcr.TradeRequestID,
                TradeRequestType = tcr.TradeRequestType,
                TradeRequestStatus = status,
                TradeRequestResult = TradeRequestResultValues.Successful
            };
        }

        public static TradeCaptureReportRequest MakeTradeCaptureReportRequest(string requestId, DateTime tradeDate)
        {
            return new TradeCaptureReportRequest
            {
                TradeRequestID = requestId,
                TradeRequestType = TradeRequestTypeValues.AllTrades,
                SubscriptionRequestType = SubscriptionRequestTypeValues.SnapshotAndUpdates,
                TrdCapDtGrp = new TrdCapDtGrpComponent
                {
                    NoDates = [
                        new NoDates() {
                            TransactTime = tradeDate
                        },
                        new NoDates() {
                            TransactTime = tradeDate.AddDays(1)
                        }
                    ]
                }
            };
        }

        public IReadOnlyList<TradeCaptureReport> MakeBatchOfTradeCaptureReport(int make = 10)
        {
            var l = new List<TradeCaptureReport>();

            for (var i = 0; i < make; ++i)
            {
                var t = MakeSingleTradeCaptureReport();
                l.Add(t);
            }

            return l;
        }

        public TradeCaptureReport MakeSingleTradeCaptureReport()
        {
            var tradeReportID = _nextTradeId++;
            var execId = _nextExecId++;
            var instrumentId = _nextInstrumentId++;
            _nextInstrumentId %= _securities.Length;

            return new TradeCaptureReport
            {
                TradeReportID = $"{tradeReportID}",
                TradeReportTransType = TradeReportTransTypeValues.New,
                TradeReportType = TradeReportTypeValues.Submit,
                TrdType = TrdTypeValues.RegularTrade,
                TransactTime = _fixClock.Current,
                ExecID = $"{execId}",
                PreviouslyReported = false,
                Instrument = new InstrumentComponent
                {
                    SecurityID = $"{_securities[instrumentId]}",
                    Symbol = $"{_securities[instrumentId]}"
                },
                TradeDate = DateOnly.FromDateTime(_fixClock.Current.Date),
                LastQty = 1000,
                LastPx = 100.0
            };
        }
    }
}
