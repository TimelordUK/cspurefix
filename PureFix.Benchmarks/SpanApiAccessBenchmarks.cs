using System;
using BenchmarkDotNet.Attributes;
using PureFix.Buffer.Ascii;
using PureFix.Dictionary.Definition;
using PureFix.Dictionary.Parser.QuickFix;

namespace PureFix.Benchmarks
{
    /// <summary>
    /// Benchmarks comparing zero-allocation span-based API vs string-based API for field access.
    /// These benchmarks measure the cost of accessing tag values once a view is already parsed.
    /// </summary>
    [MemoryDiagnoser]
    public class SpanApiAccessBenchmarks
    {
        private AsciiView _logonView = null!;
        private AsciiView _execView = null!;
        private IFixDefinitions _FixDefinitions = null!;
        private AsciiParser _parser = null!;

        [GlobalSetup]
        public void Setup()
        {
            _FixDefinitions = new FixDefinitions();
            var qf = new QuickFixXmlFileParser(_FixDefinitions);
            qf.Parse(Fix44PathHelper.Fix44);

            _parser = new AsciiParser(_FixDefinitions) { Delimiter = AsciiChars.Pipe };

            // Parse logon and keep the view
            var logonBytes = LoadMessage(Fix44PathHelper.LogonFile);
            _parser.ParseFrom(logonBytes, logonBytes.Length, (_, v) => _logonView = ((AsciiView)v).Clone());

            // Parse execution report and keep the view
            var execBytes = LoadMessage(Fix44PathHelper.ExecutionReportFile);
            _parser.ParseFrom(execBytes, execBytes.Length, (_, v) => _execView = ((AsciiView)v).Clone());
        }

        private static byte[] LoadMessage(string filename)
        {
            var message = System.IO.File.ReadAllText(filename);
            return System.Text.Encoding.UTF8.GetBytes(message);
        }

        // ===== String-based API (allocates) =====

        [Benchmark(Baseline = true)]
        public string? MsgType_GetString()
        {
            return _logonView.GetString(35);
        }

        [Benchmark]
        public bool MsgType_IsEqual_String()
        {
            var msgType = _logonView.GetString(35);
            return msgType == "A";
        }

        // ===== Span-based API (zero allocation) =====

        [Benchmark]
        public bool MsgType_IsTagEqual_Bytes()
        {
            return _logonView.IsTagEqual(35, "A"u8);
        }

        [Benchmark]
        public bool MsgType_IsTagEqual_String()
        {
            return _logonView.IsTagEqual(35, "A");
        }

        [Benchmark]
        public int MsgType_GetSpan_Length()
        {
            var span = _logonView.GetSpan(35);
            return span.Length;
        }

        // ===== MatchTag (routing scenario) =====

        [Benchmark]
        public int Route_With_GetString()
        {
            var msgType = _logonView.GetString(35);
            return msgType switch
            {
                "0" => 0,
                "A" => 1,
                "5" => 2,
                _ => -1
            };
        }

        [Benchmark]
        public int Route_With_MatchTag()
        {
            return _logonView.MatchTag(35, "0"u8, "A"u8, "5"u8);
        }

        // ===== TryGet vs nullable pattern =====

        [Benchmark]
        public int? BodyLength_GetInt32_Nullable()
        {
            return _logonView.GetInt32(9);
        }

        [Benchmark]
        public int BodyLength_TryGetInt32()
        {
            _logonView.TryGetInt32(9, out var value);
            return value;
        }

        // ===== BeginString prefix check =====

        [Benchmark]
        public bool BeginString_GetString_StartsWith()
        {
            var beginString = _logonView.GetString(8);
            return beginString?.StartsWith("FIX") ?? false;
        }

        [Benchmark]
        public bool BeginString_TagStartsWith()
        {
            return _logonView.TagStartsWith(8, "FIX"u8);
        }

        // ===== Multi-field access scenario =====

        [Benchmark]
        public bool ValidateLogon_GetString()
        {
            var msgType = _logonView.GetString(35);
            var beginString = _logonView.GetString(8);
            return msgType == "A" && beginString == "FIX4.4";
        }

        [Benchmark]
        public bool ValidateLogon_IsTagEqual()
        {
            return _logonView.IsTagEqual(35, "A"u8) && _logonView.IsTagEqual(8, "FIX4.4"u8);
        }

        // ===== Position-based access scenario =====

        [Benchmark]
        public int CountAndSum_Tags()
        {
            int sum = 0;
            foreach (var pos in _logonView.EnumerateTagPositions(553)) // Username (may have 0-1 occurrences)
            {
                var span = _logonView.GetSpanAtPosition(pos);
                sum += span.Length;
            }
            return sum;
        }
    }
}
