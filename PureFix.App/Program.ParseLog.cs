using PureFix.Transport;
using PureFix.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.ConsoleApp
{
    internal partial class Program
    {
        private static void ParseLog(CommandOptions options)
        {
            var parser = new FixLogParser(options.DictPath);
            var factory = FactoryHelper.GetFactory(options.DictPath);
            var fixPath = options.FixLogPath;
            var filter = options.MsgTypes?.ToHashSet() ?? [];

            parser.OnView = (view) =>
            {
                if (filter.Count > 0)
                {
                    if (!filter.Contains(view.MsgType() ?? "")) return;
                }
                switch (options.OutputFormat)
                {
                    case "tags":
                        WriteOutAsTags(view);
                        break;
                    default:
                        WriteOutAsJson(factory, view);
                        break;
                }
            };

            if (options.Tail)
            {
                parser.Tail(fixPath);
            }
            else
            {
                parser.Snapshot(fixPath);
            }
        }

        private static void WriteOutAsTags(IMessageView v)
        {
            Console.WriteLine(v.ToString());
            Console.WriteLine();
        }

        private static void WriteOutAsJson(IFixMessageFactory f, IMessageView v)
        {
            var o = f.ToFixMessage(v);
            if (o == null) return;
            Console.WriteLine(JsonHelper.ToJson(o, o.GetType()));
            Console.WriteLine();
        }
    }
}
