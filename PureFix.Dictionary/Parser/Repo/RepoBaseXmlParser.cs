using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Dictionary.Definition;
using System.Xml.Linq;

namespace PureFix.Dictionary.Parser.Repo
{
    public abstract class RepoBaseXmlParser
    {
            public IFixDefinitions Definitions { get; private set; }
            protected RepoBaseXmlParser(IFixDefinitions definitions)
            {
                Definitions = definitions;
            }

            public void Parse(string path)
            {
                using var streamReader = File.OpenText(path);
                var txt = streamReader.ReadToEnd();
                ParseText(txt);
            }

            protected abstract void ParseDoc(XDocument doc);

            public void ParseText(string xml)
            {
                var doc = XDocument.Parse(xml);
                ParseDoc(doc);
            }
        }
}
