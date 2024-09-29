using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BenchmarkDotNet.Attributes;

using PureFix.Buffer.Ascii;
using PureFix.Dictionary.Definition;
using PureFix.Dictionary.Parser.QuickFix;
using PureFix.Types;
using PureFix.Types.FIX44.QuickFix;

namespace PureFix.Benchmarks
{
    [MemoryDiagnoser]
    public class ParseFromViewBenchmarks
    {
        private readonly IFixDefinitions _FixDefinitions;

        private readonly AsciiView _HeartbeatView;
        private readonly AsciiView _LogonView;
        private readonly AsciiView _ExecutionReportView;
        private readonly AsciiView _OrderCancelRejectView;
        private readonly AsciiView _QuoteRequestView;

        public ParseFromViewBenchmarks()
        {
            _FixDefinitions = new FixDefinitions();
            var qf = new QuickFixXmlFileParser(_FixDefinitions);
            qf.Parse(Fix44PathHelper.Fix44);

            _HeartbeatView = MakeView(LoadMessage(Fix44PathHelper.HeartbeatFile));
            _LogonView = MakeView(LoadMessage(Fix44PathHelper.LogonFile));
            _ExecutionReportView = MakeView(LoadMessage(Fix44PathHelper.ExecutionReportFile));
            _OrderCancelRejectView = MakeView(LoadMessage(Fix44PathHelper.OrderCancelRejectFile));
            _QuoteRequestView = MakeView(LoadMessage(Fix44PathHelper.QuoteRequestFile));
        }

        private AsciiView MakeView(byte[] message)
        {
            AsciiView? view = null;

            var parser = new AsciiParser(_FixDefinitions);
            parser.ParseFrom(message, (_, v) => view = (AsciiView)v);

            return view!;
        }

        private byte[] LoadMessage(string filename)
        {
            var message = System.IO.File.ReadAllText(filename);
            return System.Text.Encoding.UTF8.GetBytes(message);
        }

        [Benchmark]
        public void HeartbeatView()
        {
            var message = new Heartbeat();
            ((IFixParser)message).Parse(_HeartbeatView);
        }

        [Benchmark]
        public void LogonView()
        {
            var message = new Logon();
            ((IFixParser)message).Parse(_LogonView);
        }

        [Benchmark]
        public void ExecutionReportView()
        {
            var message = new ExecutionReport();
            ((IFixParser)message).Parse(_ExecutionReportView);
        }

        [Benchmark]
        public void OrderCancelRejectView()
        {
            var message = new OrderCancelReject();
            ((IFixParser)message).Parse(_OrderCancelRejectView);
        }

        [Benchmark]
        public void QuoteRequestView()
        {
            var message = new QuoteRequest();
            ((IFixParser)message).Parse(_QuoteRequestView);
        }
    }
}
