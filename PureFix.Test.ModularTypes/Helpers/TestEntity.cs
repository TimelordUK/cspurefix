using PureFix.Buffer.Ascii;
using PureFix.Dictionary.Definition;
using PureFix.Dictionary.Parser.QuickFix;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Test.ModularTypes.Helpers
{
    internal class TestEntity
    {
        public IFixDefinitions Definitions { get; }
        public AsciiParser Parser { get; private set; }

        public TestEntity(string dataDict = "FIX44.xml")
        {
            Definitions = new FixDefinitions();
            var qf = new QuickFixXmlFileParser(Definitions);
            qf.Parse(Path.Join(Fix44PathHelper.DataDictRootPath, dataDict));
            Prepare();
        }

        public void Prepare()
        {
            Parser = new AsciiParser(Definitions) { Delimiter = AsciiChars.Pipe };
        }

        public async Task<List<AsciiView>> Replay(string path)
        {
            var views = new List<AsciiView>();

            // Read the file and parse FIX messages
            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"Replay file not found: {path}");
            }

            var content = await File.ReadAllTextAsync(path);
            var bytes = Encoding.UTF8.GetBytes(content);

            Parser.ParseFrom(bytes, bytes.Length, (i, view) => views.Add((AsciiView)view));

            return views;
        }

        public List<AsciiView> ParseText(string s)
        {
            var views = new List<AsciiView>(10000);
            var b = Encoding.UTF8.GetBytes(s);
            Parser.ParseFrom(b, b.Length, (i, view) => views.Add((AsciiView)view));
            return views;
        }

        public static async Task<string> GetTextAsync(string file)
        {
            using var streamReader = File.OpenText(file);
            var all = await streamReader.ReadToEndAsync();
            return all;
        }

        public static string GetText(string file)
        {
            using var streamReader = File.OpenText(file);
            var all = streamReader.ReadToEnd();
            return all;
        }

        public void TimeParsePath(string description, string path, int repeats, int batch = 1)
        {
            var sw = new System.Diagnostics.Stopwatch();
            var one = GetText(path);
            var all = string.Concat(Enumerable.Repeat(one, repeats));
            var count = repeats * batch;
            var b = Encoding.UTF8.GetBytes(all);
            var msgs = new List<AsciiView>(count);

            void Action(int i, MsgView view) => msgs.Add((AsciiView)view);

            sw.Start();
            Parser.ParseFrom(b, b.Length, Action);
            sw.Stop();

            Console.WriteLine($"{description}[{count},{batch}]: {sw.Elapsed.TotalMilliseconds} {(decimal)sw.Elapsed.TotalMicroseconds / count}  micro/msg {Parser.ParserStats.TotalSegmentParseMicro / count} seg/msg {Parser.ParserStats}");
        }
    }
}
