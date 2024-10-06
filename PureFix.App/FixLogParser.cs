using CommandLine;
using PureFix.Buffer;
using PureFix.Buffer.Ascii;
using PureFix.Dictionary.Definition;
using PureFix.Dictionary.Parser.QuickFix;
using PureFix.Types;
using PureFix.Types.FIX44.QuickFix.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.ConsoleApp
{
    internal class FixLogParser
    {
        readonly IFixMessageFactory _mf;
        readonly IMessageParser _asciiParser;
        readonly Action<IMessageView> _onView;
        readonly Options _options;

        private void GetViews(string file)
        { 
            using var streamReader = File.OpenText(file);
            while (streamReader != null && !streamReader.EndOfStream)
            {
                var line = streamReader.ReadLine();
                if (line != null)
                {
                    Parse(line);                
                }
            }
        }

        public void Parse(string txt)
        {
            var b = Encoding.UTF8.GetBytes(txt);
            _asciiParser.ParseFrom(b, b.Length, (p, v) => _onView(v));
        }

        public FixLogParser(Options options, byte delim = (byte)'|')
        {
            _options = options;
            string dict = options.DictPath;
            var definitions = new FixDefinitions();
            var qf = new QuickFixXmlFileParser(definitions);
            _asciiParser = new AsciiParser(definitions) { Delimiter = delim };
            qf.Parse(dict);
            var factory = options.GetFactory();           
            var format = options.OutputFormat;
            _mf = factory;
            _onView = format == "tags" ? WriteOutAsTags : WriteOutAsJson;          
        }

        public void Snapshot()
        {
            var file = _options.FixLogPath;
            GetViews(file);
        }

        public void Tail()
        {
            var follower = new FollowingTail(new FileInfo(_options.FixLogPath),
            Encoding.ASCII, Parse);
            while (true)
            {
                Thread.Sleep(1000);
            }
        }

        private void WriteOutAsTags(IMessageView v)
        {
            Console.WriteLine(v.ToString());
            Console.WriteLine();
        }

        private void WriteOutAsJson(IMessageView v)
        {
            var f = _mf.ToFixMessage(v);
            if (f == null) return;
            Console.WriteLine(JsonHelper.ToJson(f, f.GetType()));
            Console.WriteLine();
        }
    }
}
