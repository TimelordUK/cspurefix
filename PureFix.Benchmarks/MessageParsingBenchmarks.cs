using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BenchmarkDotNet.Attributes;
using CommandLine;
using PureFix.Buffer.Ascii;
using PureFix.Dictionary.Definition;
using PureFix.Dictionary.Parser.QuickFix;

namespace PureFix.Benchmarks
{
    [MemoryDiagnoser]
    public class MessageParsingBenchmarks
    {
        private const string DataDict = "FIX44.xml";

        private readonly byte[] _HeartbeatMessage;
        private readonly byte[] _LogonMessage;
        private readonly byte[] _ExecutionReportMessage;        
        private readonly byte[] _MarketDataSnapshotMessage;
        private readonly byte[] _OrderCancelRejectMessage;
        private readonly byte[] _QuoteRequestMessage;

        private readonly FixDefinitions _FixDefinitions;
        private readonly AsciiParser _Parser;

        private readonly Action<int, AsciiView> NoAction = (index, view) => {};

        public MessageParsingBenchmarks()
        {
            _HeartbeatMessage = LoadMessage(Fix44PathHelper.HeartbeatFile);
            _LogonMessage = LoadMessage(Fix44PathHelper.LogonFile);
            _ExecutionReportMessage = LoadMessage(Fix44PathHelper.ExecutionReportFile);
            _MarketDataSnapshotMessage = LoadMessage(Fix44PathHelper.MarketDataSnapshotFile);
            _OrderCancelRejectMessage = LoadMessage(Fix44PathHelper.OrderCancelRejectFile);
            _QuoteRequestMessage = LoadMessage(Fix44PathHelper.QuoteRequestFile);

            _FixDefinitions = new FixDefinitions();
            var qf = new QuickFixXmlFileParser(_FixDefinitions);
            qf.Parse(Fix44PathHelper.Fix44);
            
            _Parser = new AsciiParser(_FixDefinitions) 
            { 
                Delimiter = AsciiChars.Pipe 
            };
        }

        private byte[] LoadMessage(string filename)
        {
            var message = System.IO.File.ReadAllText(filename);
            return System.Text.Encoding.UTF8.GetBytes(message);
        }

        [Benchmark]
        public void Heartbeat()
        {
            _Parser.ParseFrom(_HeartbeatMessage, NoAction);
        }

        [Benchmark]
        public void Logon()
        {
            _Parser.ParseFrom(_LogonMessage, NoAction);
        }

        [Benchmark]
        public void ExecutionReport()
        {
            _Parser.ParseFrom(_ExecutionReportMessage, NoAction);
        }

        [Benchmark]
        public void MarketDataSnapshot()
        {
            _Parser.ParseFrom(_MarketDataSnapshotMessage, NoAction);
        }

        [Benchmark]
        public void OrderCancelReject()
        {
            _Parser.ParseFrom(_OrderCancelRejectMessage, NoAction);
        }

        [Benchmark]
        public void QuoteRequest()
        {
            _Parser.ParseFrom(_QuoteRequestMessage, NoAction);
        }
    }
}
