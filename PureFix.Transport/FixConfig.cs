using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Buffer.Ascii;
using PureFix.Dictionary.Definition;
using PureFix.Dictionary.Parser.QuickFix;
using PureFix.Transport.Session;
using PureFix.Types;
using PureFix.Types.Config;

namespace PureFix.Transport
{
    public class FixConfig : IFixConfig
    {
        public static IFixConfig MakeConfigFromPaths(ILogFactory logFactory, string definitionsPath, string sessionDescriptionPath)
        {
            var definitions = new FixDefinitions();
            var qfParser = new QuickFixXmlFileParser(definitions);
            qfParser.Parse(definitionsPath);
            var config = new FixConfig
            {
                LogFactory = logFactory,
                Definitions = definitions,
                Description = JsonHelper.FromJson<SessionDescription>(sessionDescriptionPath)
            };

            return config;
        }

        public byte? LogDelimiter { get; set; } = AsciiChars.Pipe;
        public byte? Delimiter { get; set; } = AsciiChars.Soh;
        public ILogFactory? LogFactory { get; set; }
        public IFixDefinitions? Definitions { get; set; }
        public ISessionDescription? Description { get; set; }
    }
}
