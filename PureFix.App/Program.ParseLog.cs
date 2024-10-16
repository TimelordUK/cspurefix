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
            var parser = new FixLogParser(options.DictPath, (byte)options.Delimiter);
            var factory = FactoryHelper.GetFactory(options.DictPath);
            var fixPath = options.FixLogPath;
            var filter = options.MsgTypes.ToHashSet() ?? [];
            var counts = new Dictionary<string, int>();

            parser.OnView = view =>
            {
                if (filter.Count > 0)
                {
                    var isContained = filter.Contains(view.MsgType() ?? "");
                    if (options.Exclude)
                    {
                        if (isContained) return;
                    } else
                    {
                        if (!isContained) return;
                    }                    
                }
                switch (options.OutputFormat)
                {
                    case "counts":
                        var msgType = view.MsgType() ?? "";
                        var count = counts.GetValueOrDefault(msgType, 0);
                        counts[msgType] = count + 1;
                        break;
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
                if (counts.Count > 0)
                {
                    foreach ( var kv in counts)
                    {
                        Console.WriteLine($"{kv.Key,-3} {kv.Value,-3} {parser.Definitions.Message.GetValueOrDefault(kv.Key)?.Name}");
                    }
                }
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
