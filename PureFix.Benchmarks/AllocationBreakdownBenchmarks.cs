using System;
using System.Buffers;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using PureFix.Buffer.Ascii;
using PureFix.Buffer.Segment;
using PureFix.Dictionary.Definition;
using PureFix.Dictionary.Parser.QuickFix;
using PureFix.Types;
using PureFix.Types.Core;

namespace PureFix.Benchmarks
{
    /// <summary>
    /// Breaks down allocations in view parsing to identify where memory is spent.
    /// Run with: dotnet run -c Release --filter "*AllocationBreakdown*"
    /// </summary>
    [MemoryDiagnoser]
    [SimpleJob(warmupCount: 1, iterationCount: 5)]
    public class AllocationBreakdownBenchmarks
    {
        private byte[] _heartbeatMessage = null!;
        private byte[] _executionReportMessage = null!;

        private IFixDefinitions _fixDefinitions = null!;

        // Pre-parsed components for isolated testing
        private Tags _heartbeatTags = null!;
        private Tags _executionReportTags = null!;
        private TagPos[] _heartbeatSortedArray = null!;
        private TagPos[] _executionReportSortedArray = null!;
        private AsciiSegmentParser _segmentParser = null!;

        [GlobalSetup]
        public void Setup()
        {
            _heartbeatMessage = LoadMessage(Fix44PathHelper.HeartbeatFile);
            _executionReportMessage = LoadMessage(Fix44PathHelper.ExecutionReportFile);

            _fixDefinitions = new FixDefinitions();
            var qf = new QuickFixXmlFileParser(_fixDefinitions);
            qf.Parse(Fix44PathHelper.Fix44);

            _segmentParser = new AsciiSegmentParser(_fixDefinitions);

            // Pre-parse to get Tags for isolated tests
            _heartbeatTags = ParseToTags(_heartbeatMessage);
            _executionReportTags = ParseToTags(_executionReportMessage);

            // Pre-create sorted arrays
            _heartbeatSortedArray = CreateSortedArray(_heartbeatTags);
            _executionReportSortedArray = CreateSortedArray(_executionReportTags);
        }

        private TagPos[] CreateSortedArray(Tags tags)
        {
            var arr = new TagPos[tags.Count];
            for (var i = 0; i < tags.Count; i++)
                arr[i] = tags[i];
            Array.Sort(arr, TagPos.Compare);
            return arr;
        }

        private byte[] LoadMessage(string filename)
        {
            var message = System.IO.File.ReadAllText(filename);
            return System.Text.Encoding.UTF8.GetBytes(message);
        }

        private Tags ParseToTags(byte[] message)
        {
            Tags? result = null;
            var parser = new AsciiParser(_fixDefinitions) { Delimiter = AsciiChars.Pipe };
            parser.ParseFrom(message, message.Length, (_, view) =>
            {
                result = parser.Locations;
            });
            return new Tags(result!); // Clone to keep a copy
        }

        // ============================================================
        // HEARTBEAT - Small message (~10 fields)
        // ============================================================

        [Benchmark]
        [BenchmarkCategory("Heartbeat")]
        public void Heartbeat_1_FullViewParse()
        {
            // Full end-to-end: bytes -> view
            var parser = new AsciiParser(_fixDefinitions) { Delimiter = AsciiChars.Pipe };
            parser.ParseFrom(_heartbeatMessage, _heartbeatMessage.Length, (_, view) => { });
        }

        [Benchmark]
        [BenchmarkCategory("Heartbeat")]
        public Tags Heartbeat_2_TagsSlice()
        {
            // Just the slice operation: tags[..last]
            var last = _heartbeatTags.Count;
            var slice = _heartbeatTags.Slice(0, last);
            return _heartbeatTags; // Return to prevent dead code elimination
        }

        [Benchmark]
        [BenchmarkCategory("Heartbeat")]
        public object Heartbeat_3_TagIndexCreate()
        {
            // TagIndex creation with its copies and dictionaries
            if (!_fixDefinitions.Message.TryGetValue("0", out var msgDef))
                return null!;
            using var ti = new TagIndex(msgDef, _heartbeatTags, _heartbeatTags.Count);
            return ti;
        }

        [Benchmark]
        [BenchmarkCategory("Heartbeat")]
        public object Heartbeat_4_StructureParse()
        {
            // Full structure parsing
            var structure = _segmentParser.Parse("0", _heartbeatTags, _heartbeatTags.Count);
            return structure!;
        }

        // ============================================================
        // EXECUTION REPORT - Large message (~646 fields)
        // ============================================================

        [Benchmark]
        [BenchmarkCategory("ExecutionReport")]
        public void ExecReport_1_FullViewParse()
        {
            // Full end-to-end: bytes -> view
            var parser = new AsciiParser(_fixDefinitions) { Delimiter = AsciiChars.Pipe };
            parser.ParseFrom(_executionReportMessage, _executionReportMessage.Length, (_, view) => { });
        }

        [Benchmark]
        [BenchmarkCategory("ExecutionReport")]
        public Tags ExecReport_2_TagsSlice()
        {
            // Just the slice operation: tags[..last]
            var last = _executionReportTags.Count;
            var slice = _executionReportTags.Slice(0, last);
            return _executionReportTags;
        }

        [Benchmark]
        [BenchmarkCategory("ExecutionReport")]
        public object ExecReport_3_TagIndexCreate()
        {
            // TagIndex creation with its copies and dictionaries
            if (!_fixDefinitions.Message.TryGetValue("8", out var msgDef))
                return null!;
            using var ti = new TagIndex(msgDef, _executionReportTags, _executionReportTags.Count);
            return ti;
        }

        [Benchmark]
        [BenchmarkCategory("ExecutionReport")]
        public object ExecReport_4_StructureParse()
        {
            // Full structure parsing
            var structure = _segmentParser.Parse("8", _executionReportTags, _executionReportTags.Count);
            return structure!;
        }

        // ============================================================
        // GRANULAR BREAKDOWN - Isolate specific allocation sources
        // ============================================================

        [Benchmark]
        [BenchmarkCategory("Granular")]
        public object Heartbeat_GetSpans()
        {
            // Just the _tagSpans dictionary creation
            return TagIndex.GetSpans(_heartbeatSortedArray, _heartbeatTags.Count);
        }

        [Benchmark]
        [BenchmarkCategory("Granular")]
        public object ExecReport_GetSpans()
        {
            // Just the _tagSpans dictionary creation
            return TagIndex.GetSpans(_executionReportSortedArray, _executionReportTags.Count);
        }

        [Benchmark]
        [BenchmarkCategory("Granular")]
        public object Heartbeat_DictionaryBaseline()
        {
            // Baseline: creating the empty dictionaries that TagIndex creates
            var count = _heartbeatTags.Count;
            var tagSpans = new Dictionary<int, Range>(count);
            var noOfTag2NoOfPos = new Dictionary<int, TagPos>();
            var tag2delim = new Dictionary<int, int>();
            var repeated = new HashSet<int>();
            var names = new HashSet<string>();
            var groups = new Dictionary<string, object>();
            var componentGroupWrappers = new HashSet<string>();
            var cache = new Dictionary<string, object>();
            return (tagSpans, noOfTag2NoOfPos, tag2delim, repeated, names, groups, componentGroupWrappers, cache);
        }

        [Benchmark]
        [BenchmarkCategory("Granular")]
        public object ExecReport_DictionaryBaseline()
        {
            // Baseline: creating the empty dictionaries that TagIndex creates
            var count = _executionReportTags.Count;
            var tagSpans = new Dictionary<int, Range>(count);
            var noOfTag2NoOfPos = new Dictionary<int, TagPos>();
            var tag2delim = new Dictionary<int, int>();
            var repeated = new HashSet<int>();
            var names = new HashSet<string>();
            var groups = new Dictionary<string, object>();
            var componentGroupWrappers = new HashSet<string>();
            var cache = new Dictionary<string, object>();
            return (tagSpans, noOfTag2NoOfPos, tag2delim, repeated, names, groups, componentGroupWrappers, cache);
        }

        [Benchmark]
        [BenchmarkCategory("Granular")]
        public object Heartbeat_ArrayPoolRent()
        {
            // Just the array pool rent/return overhead
            var arr = ArrayPool<TagPos>.Shared.Rent(_heartbeatTags.Count);
            ArrayPool<TagPos>.Shared.Return(arr);
            return arr;
        }

        [Benchmark]
        [BenchmarkCategory("Granular")]
        public object ExecReport_ArrayPoolRent()
        {
            // Just the array pool rent/return overhead
            var arr = ArrayPool<TagPos>.Shared.Rent(_executionReportTags.Count);
            ArrayPool<TagPos>.Shared.Return(arr);
            return arr;
        }

        [Benchmark]
        [BenchmarkCategory("Granular")]
        public object ParserCreation()
        {
            // Cost of creating a new AsciiParser instance
            return new AsciiParser(_fixDefinitions) { Delimiter = AsciiChars.Pipe };
        }
    }
}
