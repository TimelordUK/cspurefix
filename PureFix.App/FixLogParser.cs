using PureFix.Buffer.Ascii;
using PureFix.Dictionary.Definition;
using PureFix.Dictionary.Parser.QuickFix;
using PureFix.Types;
using PureFix.Types.FIX50SP2.QuickFix.Types;
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

        private static void GetViews(string dict, string file, Action<IMessageView> onView, byte delim = (byte)'|')
        {
            var definitions = new FixDefinitions();
            var qf = new QuickFixXmlFileParser(definitions);
            qf.Parse(dict);
            var asciiParser = new AsciiParser(definitions) { Delimiter = delim };
            using var streamReader = File.OpenText(file);
            while (streamReader != null && !streamReader.EndOfStream)
            {
                var line = streamReader.ReadLine();
                if (line != null)
                {
                    var b = Encoding.UTF8.GetBytes(line);
                    asciiParser.ParseFrom(b, b.Length, (p, v) => onView(v));
                }
            }
        }

        public FixLogParser(string dict, IFixMessageFactory factory, string file, string format, byte delim = (byte)'|')
        {
            _mf = factory;
            Action<IMessageView> onView = format == "tags" ? WriteOutAsTags : WriteOutAsJson;
            GetViews(dict, file, onView, delim);
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
