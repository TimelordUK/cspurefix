using PureFix.Buffer.Ascii;
using PureFix.Buffer;
using PureFix.Dictionary.Definition;
using PureFix.Dictionary.Parser.QuickFix;
using PureFix.Transport;
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
        public ElasticBuffer Buffer { get; }
        public FixDuplex<MsgView> Duplex { get; private set;}
        public AsciiParser Parser { get; private set; }
        public string RootData { get; } = Path.Join(Directory.GetCurrentDirectory(), "..", "..", "..", "..", "Data");

        public TestEntity(string dataDict = "FIX44.xml")
        {
            Definitions = new FixDefinitions();
            var qf = new QuickFixXmlFileParser(Definitions);
            qf.Parse(Path.Join(RootData, dataDict));
            Buffer = new ElasticBuffer(160 * 1024, 160 * 1024);
        }

        public void Prepare()
        {
            Buffer.Reset();
            Duplex = new FixDuplex<MsgView>();
            Parser = new AsciiParser(Definitions, Duplex, Buffer) { Delimiter = AsciiChars.Pipe };
        }

        public void ParseTest(string s)
        {
            var b = Encoding.UTF8.GetBytes(s);
            Parser.ParseFrom(b);
            Duplex.Writer.Complete();
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
            Duplex.Writer.Complete();
        }

        public async Task<Dictionary<string,int>> GetJsonDict(string file)
        {
            using var streamReader = File.OpenText(file);
            var json = await streamReader.ReadToEndAsync();
            var values = JsonSerializer.Deserialize<Dictionary<string, int>>(json);
            return values;
        }

        public async Task<List<AsciiView>> Replay(string file)
        {
            List<AsciiView> msgs = [];
            try
            {
                using var streamReader = File.OpenText(file);
                var all = await streamReader.ReadToEndAsync();
                ParseTest(all);
                await foreach (var view in Duplex.Reader.ReadAllAsync())
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
    }
}
