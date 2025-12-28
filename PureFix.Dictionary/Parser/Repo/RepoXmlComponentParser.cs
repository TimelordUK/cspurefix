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
     <Component added="FIX.4.3">
		    <ComponentID>1000</ComponentID>
		    <ComponentType>Block</ComponentType>
		    <CategoryID>Common</CategoryID>
		    <Name>CommissionData</Name>
		    <AbbrName>Comm</AbbrName>
		    <NotReqXML>0</NotReqXML>
		    <Description>The CommissionDate component block is used to carry commission information such as the type of commission and the rate.</Description>
	  </Component>
     */
    public partial class RepoXmlComponentParser(IFixDefinitions definitions) : RepoBaseXmlParser(definitions)
    {
        private Dictionary<int, RepoComponentDefinition> _componentsByID { get; } = [];
        private Dictionary<string, RepoComponentDefinition> _componentsByName { get; } = [];

        public IReadOnlyDictionary<int, RepoComponentDefinition> ComponentsByID => _componentsByID;
        public IReadOnlyDictionary<string, RepoComponentDefinition> ComponentsByName => _componentsByName;

        protected override void ParseDoc(XDocument doc)
        {
            var fieldsNode = doc.Descendants("Components").FirstOrDefault();
            if (fieldsNode == null) return;
            var fields = fieldsNode.Elements("Component");
            foreach (var fieldElement in fields)
            {
                var at = fieldElement.AsAttributeDict();
                int id = fieldElement.AsInt("ComponentID");              
                var type = fieldElement.AsString("ComponentType");
                var cat = fieldElement.AsString("CategoryID");
                var name = fieldElement.AsString("Name");
                var abbr = fieldElement.AsString("AbbrName");
                var description = fieldElement.AsString("Description");
                var notXmlReq = fieldElement.AsInt("NotReqXML");
                var comp = new RepoComponentDefinition(id, type, cat, name, abbr, notXmlReq, description);
                _componentsByID[id] = comp;
                _componentsByName[name] = comp;
            }
        }
    }
}
