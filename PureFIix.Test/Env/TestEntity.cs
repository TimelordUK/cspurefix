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
            var rootFolder = Directory.GetCurrentDirectory();
            Definitions = new FixDefinitions();
            var qf = new QuickFixXmlFileParser(Definitions);
            qf.Parse(Path.Join(rootFolder, "..", "..", "..", "..", "Data", dataDict));
            Buffer = new ElasticBuffer();
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
        }
    }
}
