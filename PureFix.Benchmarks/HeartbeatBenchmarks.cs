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
    public class HeartbeatBenchmarks
    {
        private const string DataDict = "FIX44.xml";

        private readonly string _HeartbeatMessage;
        private readonly byte[] _HeartbeatMessageAsBytes;
        private readonly FixDefinitions _FixDefinitions;
        private readonly AsciiParser _Parser;

        public HeartbeatBenchmarks()
        {
            _HeartbeatMessage = System.IO.File.ReadAllText(Fix44PathHelper.HeartbeatFile);
            _HeartbeatMessageAsBytes = System.Text.Encoding.UTF8.GetBytes(_HeartbeatMessage);

            _FixDefinitions = new FixDefinitions();
            var qf = new QuickFixXmlFileParser(_FixDefinitions);
            qf.Parse(Fix44PathHelper.Fix44);
            
            _Parser = new AsciiParser(_FixDefinitions) 
            { 
                Delimiter = AsciiChars.Pipe 
            };
        }

        [Benchmark]
        public void ParseHeartbeatMessage()
        {
            _Parser.ParseFrom(_HeartbeatMessageAsBytes, (index, view) => {});
        }
    }
}
