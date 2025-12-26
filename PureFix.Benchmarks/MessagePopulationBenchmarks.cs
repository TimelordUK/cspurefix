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
using PureFix.Types.FIX44;

namespace PureFix.Benchmarks
{
    /// <summary>
    /// Benchmarks for populating message objects from a pre-parsed view.
    /// This measures the cost of extracting field values and creating typed message objects.
    /// </summary>
    [MemoryDiagnoser]
    public class MessagePopulationBenchmarks
    {
        private readonly IFixDefinitions _FixDefinitions;

        private readonly AsciiView _HeartbeatView;
        private readonly AsciiView _LogonView;
        private readonly AsciiView _ExecutionReportView;
        private readonly AsciiView _OrderCancelRejectView;
        private readonly AsciiView _QuoteRequestView;

        // Pooled message instances for Reset() benchmarks
        private readonly Heartbeat _PooledHeartbeat = new();
        private readonly Logon _PooledLogon = new();
        private readonly ExecutionReport _PooledExecutionReport = new();
        private readonly OrderCancelReject _PooledOrderCancelReject = new();
        private readonly QuoteRequest _PooledQuoteRequest = new();

        public MessagePopulationBenchmarks()
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
            parser.ParseFrom(message, message .Length, (_, v) => view = (AsciiView)v);

            return view!;
        }

        private byte[] LoadMessage(string filename)
        {
            var message = System.IO.File.ReadAllText(filename);
            return System.Text.Encoding.UTF8.GetBytes(message);
        }

        // --- New allocation benchmarks (creates new message each time) ---

        [Benchmark]
        [BenchmarkCategory("NewAllocation")]
        public Heartbeat Heartbeat_New()
        {
            var message = new Heartbeat();
            ((IFixParser)message).Parse(_HeartbeatView);
            return message;
        }

        [Benchmark]
        [BenchmarkCategory("NewAllocation")]
        public Logon Logon_New()
        {
            var message = new Logon();
            ((IFixParser)message).Parse(_LogonView);
            return message;
        }

        [Benchmark]
        [BenchmarkCategory("NewAllocation")]
        public ExecutionReport ExecutionReport_New()
        {
            var message = new ExecutionReport();
            ((IFixParser)message).Parse(_ExecutionReportView);
            return message;
        }

        [Benchmark]
        [BenchmarkCategory("NewAllocation")]
        public OrderCancelReject OrderCancelReject_New()
        {
            var message = new OrderCancelReject();
            ((IFixParser)message).Parse(_OrderCancelRejectView);
            return message;
        }

        [Benchmark]
        [BenchmarkCategory("NewAllocation")]
        public QuoteRequest QuoteRequest_New()
        {
            var message = new QuoteRequest();
            ((IFixParser)message).Parse(_QuoteRequestView);
            return message;
        }

        // --- Pooled benchmarks (reuses message with Reset()) ---

        [Benchmark]
        [BenchmarkCategory("Pooled")]
        public Heartbeat Heartbeat_Pooled()
        {
            ((IFixReset)_PooledHeartbeat).Reset();
            ((IFixParser)_PooledHeartbeat).Parse(_HeartbeatView);
            return _PooledHeartbeat;
        }

        [Benchmark]
        [BenchmarkCategory("Pooled")]
        public Logon Logon_Pooled()
        {
            ((IFixReset)_PooledLogon).Reset();
            ((IFixParser)_PooledLogon).Parse(_LogonView);
            return _PooledLogon;
        }

        [Benchmark]
        [BenchmarkCategory("Pooled")]
        public ExecutionReport ExecutionReport_Pooled()
        {
            ((IFixReset)_PooledExecutionReport).Reset();
            ((IFixParser)_PooledExecutionReport).Parse(_ExecutionReportView);
            return _PooledExecutionReport;
        }

        [Benchmark]
        [BenchmarkCategory("Pooled")]
        public OrderCancelReject OrderCancelReject_Pooled()
        {
            ((IFixReset)_PooledOrderCancelReject).Reset();
            ((IFixParser)_PooledOrderCancelReject).Parse(_OrderCancelRejectView);
            return _PooledOrderCancelReject;
        }

        [Benchmark]
        [BenchmarkCategory("Pooled")]
        public QuoteRequest QuoteRequest_Pooled()
        {
            ((IFixReset)_PooledQuoteRequest).Reset();
            ((IFixParser)_PooledQuoteRequest).Parse(_QuoteRequestView);
            return _PooledQuoteRequest;
        }
    }
}
