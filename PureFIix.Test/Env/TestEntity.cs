using PureFix.Buffer.Ascii;
using PureFix.Buffer;
using PureFix.Dictionary.Definition;
using PureFix.Dictionary.Parser.QuickFix;
using PureFix.Transport;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace PureFIix.Test.Env
{
    internal class TestEntity
    {
        public FixDefinitions Definitions { get; }
        public ElasticBuffer Buffer { get; }
        public FixDuplex<MsgView> Duplex { get; private set;}
        public AsciiParser Parser { get; private set; }
        
        public TestEntity(string dataDict = "FIX44.xml")
        {
            Definitions = new FixDefinitions();
            var qf = new QuickFixXmlFileParser(Definitions);
            qf.Parse(Path.Join(Fix44PathHelper.DataDictRootPath, dataDict));
            Buffer = new ElasticBuffer(160 * 1024, 160 * 1024);
            Prepare();
        }

        public void Prepare()
        {
            Buffer.Reset();
            Duplex = new ChannelDuplex<MsgView>();
            Parser = new AsciiParser(Definitions, Duplex, Buffer) { Delimiter = AsciiChars.Pipe };
        }

        public void ParseTest(string s)
        {
            var b = Encoding.UTF8.GetBytes(s);
            Parser.ParseFrom(b);
            Duplex.Complete();
        }

        public void ParseTestHunks(string s)
        {
            var b = Encoding.UTF8.GetBytes(s);
            var span = new Span<byte>(b);
            var iteration = 0;
            while (span.Length > 0)
            {
                var want = Math.Min(span.Length, (iteration % 10) + 1);
                Parser.ParseFrom(span[..want].ToArray());
                span = span[want..];
                ++iteration;
            }
            Duplex.Complete();
        }

        public static async Task<Dictionary<string,int>> GetJsonDict(string file)
        {
            using var streamReader = File.OpenText(file);
            var json = await streamReader.ReadToEndAsync();
            var values = JsonSerializer.Deserialize<Dictionary<string, int>>(json);
            return values;
        }

        public static async Task<string> GetText(string file)
        {
            using var streamReader = File.OpenText(file);
            var all = await streamReader.ReadToEndAsync();
            return all;
        }

        public async Task<List<AsciiView>> ReplayText(string all, int repeats = 1)
        {
            var msgs = new List<AsciiView>(10000);
            try
            {
                if (repeats > 1)
                {
                    all = string.Concat(Enumerable.Repeat(all, repeats));
                }

                ParseTest(all);
                await foreach (var view in Duplex.ReadAllAsync())
                {
                    msgs.Add((AsciiView)view);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return msgs;
        }

        public async Task<List<AsciiView>> Replay(string file, int repeats = 1)
        {
            var all = await GetText(file);
            var msgs = await ReplayText(all, repeats);
            return msgs;
        }
    }
}
