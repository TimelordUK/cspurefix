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
    	<Enum added="FIX.2.7">
		<Tag>4</Tag>
		<Value>B</Value>
		<SymbolicName>Buy</SymbolicName>
		<Group/>
		<Sort>1</Sort>
		<Description>Buy</Description>
	</Enum>
     */
    public partial class RepoXmlEnumParser : RepoBaseXmlParser
    {
        private Dictionary<int, List<FieldEnum>> _enums = [];
        public IReadOnlyDictionary<int, List<FieldEnum>> Enums => _enums;
        public RepoXmlEnumParser(IFixDefinitions definitions): base(definitions)
        {
        }

        protected override void ParseDoc(XDocument doc)
        {
            var fielsNode = doc.Descendants("Enums").FirstOrDefault();
            if (fielsNode == null) return;
            var fields = fielsNode.Elements("Enum");
          
            foreach (var fieldElement in fields)
            {
                var at = fieldElement.AsAttributeDict();
                int tag = fieldElement.AsInt("Tag");
                var value = fieldElement.AsString("Value");
                var symbolicNamew = fieldElement.AsString("SymbolicName");
                var group = fieldElement.AsString("Group");
                var description = fieldElement.AsString("Description");    
                if (!_enums.TryGetValue(tag, out var l))
                {
                    _enums[tag] = l = [];
                }
                l.Add(new FieldEnum(value, description));
            }
        }
    }
}
