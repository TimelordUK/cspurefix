using PureFix.Buffer.Ascii;
using PureFix.Buffer;
using PureFix.Dictionary.Definition;
using PureFix.Dictionary.Parser.QuickFix;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Diagnostics;

namespace PureFIix.Test.Env
{
    internal class TestEntity
    {
        public FixDefinitions Definitions { get; }
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


        public List<AsciiView> ParseText(string s)
        {
            var views = new List<AsciiView>(10000);
            var b = Encoding.UTF8.GetBytes(s);
            Parser.ParseFrom(b, (i, view) => views.Add(view));
            return views;
        }

        public List<AsciiView> ParseTestHunks(string s)
        {
            var b = Encoding.UTF8.GetBytes(s);
            var span = new Span<byte>(b);
            var iteration = 0;
            var views = new List<AsciiView>();
            while (span.Length > 0)
            {
                var want = Math.Min(span.Length, (iteration % 10) + 1);
                Parser.ParseFrom(span[..want], (i, view) => views.Add(view));
                span = span[want..];
                ++iteration;
            }
            return views;
        }

        public static async Task<Dictionary<string,int>> GetJsonDict(string file)
        {
            using var streamReader = File.OpenText(file);
            var json = await streamReader.ReadToEndAsync();
            var values = JsonSerializer.Deserialize<Dictionary<string, int>>(json);
            return values;
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

        public List<AsciiView> ReplayText(string all, int repeats = 1)
        {
            try
            {
                if (repeats > 1)
                {
                    all = string.Concat(Enumerable.Repeat(all, repeats));
                }
                var msgs = ParseText(all);
                  return msgs;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<List<AsciiView>> Replay(string file, int repeats = 1)
        {
            var all = await GetTextAsync(file);
            var msgs = ReplayText(all, repeats);
            return msgs;
        }

        public void TimeParsePath(string description, string path, int repeats, int batch = 1)
        {
            var sw = new Stopwatch();
            var one = GetText(path);
            var all = string.Concat(Enumerable.Repeat(one, repeats));
            var count = repeats * batch;
            var b = Encoding.UTF8.GetBytes(all);
            var msgs = new List<AsciiView>(count);

            // Move the creating of the action outside the stopwatch code
            // to avoid recording the time to allocate the memory for it
            Action<int, AsciiView> action = (i, view) => msgs.Add(view);
            //_testEntity.Parser.ParseFrom(b, action);

            sw.Start();
            Parser.ParseFrom(b, action);
            //_testEntity.Parser.ParseFrom(b, null);
            sw.Stop();
            Assert.That(msgs, Has.Count.EqualTo(count));
            Console.WriteLine($"{description}[{count},{batch}]: {sw.Elapsed.TotalMilliseconds} {(decimal)sw.Elapsed.TotalMicroseconds / count}  micro/msg {Parser.ParserStats.TotalSegmentParseMicro / count} seg/msg {Parser.ParserStats}");
        }
    }
}
