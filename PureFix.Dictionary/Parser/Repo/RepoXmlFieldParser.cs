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
     * <Field added="FIX.2.7">
		<Tag>1</Tag>
		<Name>Account</Name>
		<Type>String</Type>
		<AbbrName>Acct</AbbrName>
		<NotReqXML>0</NotReqXML>
		<Description>Account mnemonic as agreed between buy and sell sides, e.g. broker and institution or investor/intermediary and fund manager.</Description>
	</Field>
     */
    public partial class RepoXmlFieldParser : RepoBaseXmlParser
    {
        public required IReadOnlyDictionary<int, List<FieldEnum>> Enums { get; init ; }

        public RepoXmlFieldParser(IFixDefinitions definitions): base(definitions)
        {
        }

        protected override void ParseDoc(XDocument doc)
        {
            var fielsNode = doc.Descendants("Fields").FirstOrDefault();
            if (fielsNode == null) return;
            var fields = fielsNode.Elements("Field");
            foreach (var fieldElement in fields)
            {
                var at = fieldElement.AsAttributeDict();
                int tag = fieldElement.AsInt("Tag");
                var name = fieldElement.AsString("Name");
                var type = fieldElement.AsString("Type");
                var abbr = fieldElement.AsString("AbbrName");
                var description = fieldElement.AsString("Description");
                var fieldName = !string.IsNullOrEmpty(abbr) ? abbr : name;
                var sf = new SimpleFieldDefinition(name, "", type, tag, Enums.GetValueOrDefault(tag));
                Definitions.AddSimple(sf);
            }
        }
    }
}
