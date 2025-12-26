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
    /// Storage is returned to pool after each parse to show steady-state allocation.
    /// </summary>
    [MemoryDiagnoser]
    public class ViewParsingBenchmarks
    {
        private byte[] _HeartbeatMessage = null!;
        private byte[] _LogonMessage = null!;
        private byte[] _ExecutionReportMessage = null!;
        private byte[] _OrderCancelRejectMessage = null!;
        private byte[] _QuoteRequestMessage = null!;

        private IFixDefinitions _FixDefinitions = null!;

        // Separate parser per message type to avoid state accumulation
        private AsciiParser _HeartbeatParser = null!;
        private AsciiParser _LogonParser = null!;
        private AsciiParser _ExecutionReportParser = null!;
        private AsciiParser _OrderCancelRejectParser = null!;
        private AsciiParser _QuoteRequestParser = null!;

        // Store last view to return to pool
        private AsciiView? _lastView;

        [GlobalSetup]
        public void Setup()
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

            // Warmup: parse once and return to pre-populate pools
            WarmupParser(_HeartbeatParser, _HeartbeatMessage);
            WarmupParser(_LogonParser, _LogonMessage);
            WarmupParser(_ExecutionReportParser, _ExecutionReportMessage);
            WarmupParser(_OrderCancelRejectParser, _OrderCancelRejectMessage);
            WarmupParser(_QuoteRequestParser, _QuoteRequestMessage);
        }

        private void WarmupParser(AsciiParser parser, byte[] message)
        {
            AsciiView? view = null;
            parser.ParseFrom(message, message.Length, (_, v) => view = (AsciiView)v);
            if (view != null)
            {
                parser.Return(view.Storage);
            }
        }

        private AsciiParser CreateParser()
        {
            return new AsciiParser(_FixDefinitions) { Delimiter = AsciiChars.Pipe };
        }

        private static byte[] LoadMessage(string filename)
        {
            var message = System.IO.File.ReadAllText(filename);
            return System.Text.Encoding.UTF8.GetBytes(message);
        }

        [Benchmark]
        public void Heartbeat()
        {
            _HeartbeatParser.ParseFrom(_HeartbeatMessage, _HeartbeatMessage.Length, (_, v) => _lastView = (AsciiView)v);
            if (_lastView != null)
            {
                _HeartbeatParser.Return(_lastView.Storage);
                _lastView = null;
            }
        }

        [Benchmark]
        public void Logon()
        {
            _LogonParser.ParseFrom(_LogonMessage, _LogonMessage.Length, (_, v) => _lastView = (AsciiView)v);
            if (_lastView != null)
            {
                _LogonParser.Return(_lastView.Storage);
                _lastView = null;
            }
        }

        [Benchmark]
        public void ExecutionReport()
        {
            _ExecutionReportParser.ParseFrom(_ExecutionReportMessage, _ExecutionReportMessage.Length, (_, v) => _lastView = (AsciiView)v);
            if (_lastView != null)
            {
                _ExecutionReportParser.Return(_lastView.Storage);
                _lastView = null;
            }
        }

        [Benchmark]
        public void OrderCancelReject()
        {
            _OrderCancelRejectParser.ParseFrom(_OrderCancelRejectMessage, _OrderCancelRejectMessage.Length, (_, v) => _lastView = (AsciiView)v);
            if (_lastView != null)
            {
                _OrderCancelRejectParser.Return(_lastView.Storage);
                _lastView = null;
            }
        }

        [Benchmark]
        public void QuoteRequest()
        {
            _QuoteRequestParser.ParseFrom(_QuoteRequestMessage, _QuoteRequestMessage.Length, (_, v) => _lastView = (AsciiView)v);
            if (_lastView != null)
            {
                _QuoteRequestParser.Return(_lastView.Storage);
                _lastView = null;
            }
        }
    }
}
