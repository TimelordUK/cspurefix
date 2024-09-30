using PureFix.Buffer.Ascii;
using PureFix.Dictionary.Definition;
using PureFix.Dictionary.Parser.QuickFix;
using PureFix.Types;
using PureFix.Types.FIX50SP2.QuickFix.Types;
using PureFix.Types.FIX50SP2.QuickFix;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.ConsoleApp
{
    internal class FixLogParser
    {
        private IReadOnlyList<MsgView> GetViews(string dict, string file, byte delim = (byte)'|')
        {
            var definitions = new FixDefinitions();
            var qf = new QuickFixXmlFileParser(definitions);
            qf.Parse(dict);
            var asciiParser = new AsciiParser(definitions) { Delimiter = delim };
            using var streamReader = File.OpenText(file);
            var all = streamReader.ReadToEnd();
            var views = new List<MsgView>();
            var b = Encoding.UTF8.GetBytes(all);
           
            asciiParser.ParseFrom(b, (p, v) => views.Add(v));
            return views;
        }

        public FixLogParser(string dict, string file, string format, byte delim = (byte)'|')
        {
            var views = GetViews(dict, file, delim);
            WriteOut(views, format);
        }

        private void WriteOut(IReadOnlyList<MsgView> views, string format)
        {
            var mf = new FixMessageFactory();
            foreach (var v in views)
            {
                switch (format)
                {
                    case "tags":
                        Console.WriteLine(v.ToString());
                        break;
                    default:
                    {
                        var f = mf.ToFixMessage(v);
                            if (f == null) continue;
                            Console.WriteLine(JsonHelper.ToJson(f, f.GetType()));
                            break;
                        }
                }
                Console.WriteLine();
            }
        }
    }
}
