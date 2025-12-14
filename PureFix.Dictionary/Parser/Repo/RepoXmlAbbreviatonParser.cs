using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using PureFix.Dictionary.Definition;


namespace PureFix.Dictionary.Parser.Repo
{
    /*
	<Abbreviation added="FIX.4.4">
		<Term>Account</Term>
		<AbbrTerm>Acct</AbbrTerm>
	</Abbreviation>
     */
    public partial class RepoXmlAbbreviationParser : RepoBaseXmlParser
    {
        private Dictionary<string, string> _abbreviatons { get; set; } = [];
        public IReadOnlyDictionary<string, string> Abbreviations => _abbreviatons;

        public RepoXmlAbbreviationParser(IFixDefinitions definitions): base(definitions)
        {
        }

        protected override void ParseDoc(XDocument doc)
        {
            var fieldsNode = doc.Descendants("Abbreviations").FirstOrDefault();
            if (fieldsNode == null) return;
            var fields = fieldsNode.Elements("Abbreviation");
            foreach (var fieldElement in fields)
            { 
                var term = fieldElement.AsString("Term");
                var abbrev = fieldElement.AsString("AbbrTerm");

                _abbreviatons[term] = abbrev;     
            }
        }
    }
}
