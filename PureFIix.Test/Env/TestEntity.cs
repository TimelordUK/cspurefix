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


        public List<AsciiView> ParseText(byte[] b)
        {
            var views = new List<AsciiView>(10000);
            Parser.ParseFrom(b, (i, view) => views.Add(view));
            return views;
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
    }
}
