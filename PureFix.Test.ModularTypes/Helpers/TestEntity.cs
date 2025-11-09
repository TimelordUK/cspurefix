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
    }
}
