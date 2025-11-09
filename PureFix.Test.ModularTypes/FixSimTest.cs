using PureFix.Buffer.Ascii;
using PureFix.Test.ModularTypes.Helpers;
using PureFix.Transport;
using PureFix.Types;
using PureFix.Types.FIX44;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DIs = NUnit.DeepObjectCompare.Is;
using QuickLookup = PureFix.Test.ModularTypes.Helpers.QuickLookup;
using JsonHelper = PureFix.Types.JsonHelper;
using MsgType = PureFix.Types.MsgType;

namespace PureFix.Test.ModularTypes
{
    public class FixSimTest
    {
        private TestEntity _testEntity;      
        private List<AsciiView> _views;

        [OneTimeSetUp]
        public async Task OnceSetup()
        {
            _testEntity = new TestEntity();         
            var sw = new Stopwatch();
            sw.Start();
            _views = await _testEntity.Replay(Fix44PathHelper.FixSimExamplePath);
        }

        [SetUp]
        public void Setup()
        {
            _testEntity.Prepare();
        }

        [Test]
        public void Check_Replay_View_Count_Test()
        {
            Assert.That(_views, Has.Count.EqualTo(46));
        }

        /*
         * ❯ ./PureFix.ConsoleApp -d FIX44.xml -f Data/examples/FIX.4.4/fixsim-examples.txt -o counts
A   2   Logon
0   38  Heartbeat
6   1   IOI
1   1   TestRequest
V   1   MarketDataRequest
D   1   NewOrderSingle
8   1   ExecutionReport
R   1   QuoteRequest
         */

        [TestCase("A", 2)]
        [TestCase("0", 38)]
        [TestCase("6", 1)]
        [TestCase("1", 1)]
        [TestCase("V", 1)]
        [TestCase("D", 1)]
        [TestCase("8", 1)]
        [TestCase("R", 1)]
        public void Check_Frequency_Count_Test(string type, int expected)
        {
            var f = _views.Where(v => v.MsgType() == type).ToList();
            Assert.That(f, Has.Count.EqualTo(expected));
        }

        private static ExecutionReport ExecutionReport()
        {
            var json = """
                {
                  "StandardHeader": {
                    "BeginString": "FIX.4.4",
                    "BodyLength": 261,
                    "MsgType": "8",
                    "SenderCompID": "FIXSIMDEMO",
                    "TargetCompID": "sjames8888@gmail_com",
                    "MsgSeqNum": 6,
                    "SendingTime": "2024-10-13T15:15:00.746+01:00"
                  },
                  "OrderID": "638644257007460000",
                  "ExecID": "41638644256253512000",
                  "ExecType": "2",
                  "OrdStatus": "2",
                  "Instrument": {
                    "SecurityIDSource": "5",
                    "Symbol": "VOD.L",
                    "SecurityID": "VOD.L"
                  },
                  "Side": "1",
                  "OrderQtyData": {
                    "OrderQty": 100
                  },
                  "OrdType": "2",
                  "Price": 100,
                  "Currency": "GBP",
                  "TimeInForce": "0",
                  "ExecInst": "VOD.L",
                  "LastQty": 100,
                  "LastPx": 100,
                  "LeavesQty": 0,
                  "CumQty": 100,
                  "AvgPx": 100,
                  "TransactTime": "2024-10-13T15:15:00.746+01:00",
                  "HandlInst": "2",
                  "StandardTrailer": {
                    "CheckSum": "207"
                  }
                }                
                """;
            var er = JsonHelper.FromJson<ExecutionReport>(json);
            return er;
        }

        [Test]
        public void Parse_Execution_Report()
        {
            var v = _views.FirstOrDefault(v => v.MsgType() == MsgType.ExecutionReport);
            Assert.That(v, Is.Not.Null);
            var factory = new FixMessageFactory();
            var execReport = (ExecutionReport)factory.ToFixMessage(v);
            Assert.That(execReport, Is.Not.Null);
            var expected = ExecutionReport();
            Assert.That(execReport, DIs.DeepEqualTo(expected));
        }
    }
}
