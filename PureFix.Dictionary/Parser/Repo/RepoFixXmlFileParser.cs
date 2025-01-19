using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Dictionary.Definition;

namespace PureFix.Dictionary.Parser.Repo
{
    public class RepoFixXmlFileParser
    {
        public IFixDefinitions Definitions { get; private set; }
        public RepoFixXmlFileParser(IFixDefinitions definitions) 
        {
            Definitions = definitions;
        }

        public void Parse(string basePath)
        {
            var e = new RepoXmlEnumParser(Definitions);
            e.Parse(Path.Join(basePath, "Enums.xml"));

            var f = new RepoXmlFieldParser(Definitions) { Enums = e.Enums };
            f.Parse(Path.Join(basePath, "Fields.xml"));

            var c = new RepoXmlComponentParser(Definitions);
            c.Parse(Path.Join(basePath, "Components.xml"));

            var a = new RepoXmlAbbreviationParser(Definitions);
            a.Parse(Path.Join(basePath, "Abbreviations.xml"));
        }
    }
}
