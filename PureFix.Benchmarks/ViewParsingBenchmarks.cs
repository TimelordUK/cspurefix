using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BenchmarkDotNet.Attributes;
using PureFix.Buffer.Ascii;
using PureFix.Dictionary.Definition;
using PureFix.Dictionary.Parser.QuickFix;

namespace PureFix.Benchmarks
{
    /// <summary>
    /// Benchmarks for parsing raw bytes into an indexed view (tokenization).
    /// This measures the cost of creating the view structure from a FIX message buffer.
    /// </summary>
    [MemoryDiagnoser]
    public class ViewParsingBenchmarks
    {
        private readonly byte[] _HeartbeatMessage;
        private readonly byte[] _LogonMessage;
        private readonly byte[] _ExecutionReportMessage;
        private readonly byte[] _OrderCancelRejectMessage;
        private readonly byte[] _QuoteRequestMessage;

        private readonly IFixDefinitions _FixDefinitions;

        // Separate parser per message type to avoid state accumulation
        private readonly AsciiParser _HeartbeatParser;
        private readonly AsciiParser _LogonParser;
        private readonly AsciiParser _ExecutionReportParser;
        private readonly AsciiParser _OrderCancelRejectParser;
        private readonly AsciiParser _QuoteRequestParser;

        private readonly Action<int, MsgView> NoAction = (index, view) => {};

        public ViewParsingBenchmarks()
        {
            _HeartbeatMessage = LoadMessage(Fix44PathHelper.HeartbeatFile);
            _LogonMessage = LoadMessage(Fix44PathHelper.LogonFile);
            _ExecutionReportMessage = LoadMessage(Fix44PathHelper.ExecutionReportFile);
            _OrderCancelRejectMessage = LoadMessage(Fix44PathHelper.OrderCancelRejectFile);
            _QuoteRequestMessage = LoadMessage(Fix44PathHelper.QuoteRequestFile);

            _FixDefinitions = new FixDefinitions();
            var qf = new QuickFixXmlFileParser(_FixDefinitions);
            qf.Parse(Fix44PathHelper.Fix44);

            _HeartbeatParser = CreateParser();
            _LogonParser = CreateParser();
            _ExecutionReportParser = CreateParser();
            _OrderCancelRejectParser = CreateParser();
            _QuoteRequestParser = CreateParser();
        }

        private AsciiParser CreateParser()
        {
            return new AsciiParser(_FixDefinitions) { Delimiter = AsciiChars.Pipe };
        }

        private byte[] LoadMessage(string filename)
        {
            var message = System.IO.File.ReadAllText(filename);
            return System.Text.Encoding.UTF8.GetBytes(message);
        }

        [Benchmark]
        public void Heartbeat()
        {
            _HeartbeatParser.ParseFrom(_HeartbeatMessage, _HeartbeatMessage.Length, NoAction);
        }

        [Benchmark]
        public void Logon()
        {
            _LogonParser.ParseFrom(_LogonMessage, _LogonMessage.Length, NoAction);
        }

        [Benchmark]
        public void ExecutionReport()
        {
            _ExecutionReportParser.ParseFrom(_ExecutionReportMessage, _ExecutionReportMessage.Length, NoAction);
        }

        [Benchmark]
        public void OrderCancelReject()
        {
            _OrderCancelRejectParser.ParseFrom(_OrderCancelRejectMessage, _OrderCancelRejectMessage.Length, NoAction);
        }

        [Benchmark]
        public void QuoteRequest()
        {
            _QuoteRequestParser.ParseFrom(_QuoteRequestMessage, _QuoteRequestMessage.Length, NoAction);
        }
    }
}
