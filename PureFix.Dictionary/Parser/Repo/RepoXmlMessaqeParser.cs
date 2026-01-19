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
  <Message added="FIX.2.7">
		<ComponentID>1</ComponentID>
		<MsgType>0</MsgType>
		<Name>Heartbeat</Name>
		<CategoryID>Session</CategoryID>
		<SectionID>Session</SectionID>
		<AbbrName>Heartbeat</AbbrName>
		<NotReqXML>1</NotReqXML>
		<Description>The Heartbeat monitors the status of the communication link and identifies when the last of a string of messages was not received.</Description>
	</Message>
     */
    public partial class RepoXmlMessageParser(IFixDefinitions definitions) : RepoBaseXmlParser(definitions)
    {
        private Dictionary<int, RepoMessageDefinition> _messages { get; set; } = [];
        public IReadOnlyDictionary<int, RepoMessageDefinition> Messages => _messages;

        protected override void ParseDoc(XDocument doc)
        {
            var fielsNode = doc.Descendants("Messages").FirstOrDefault();
            if (fielsNode == null) return;
            var fields = fielsNode.Elements("Message");
            foreach (var fieldElement in fields)
            {
                var at = fieldElement.AsAttributeDict();
                int id = fieldElement.AsInt("ComponentID");              
                var msgType = fieldElement.AsString("Name");             
                var name = fieldElement.AsString("Name");
                var cat = fieldElement.AsString("CategoryID");
                var section = fieldElement.AsString("SectionID");
                var abbr = fieldElement.AsString("AbbrName");
                var description = fieldElement.AsString("Description");
                var notXmlReq = fieldElement.AsInt("NotReqXML");
                var comp = new RepoMessageDefinition(id, msgType, name, cat, section, abbr, notXmlReq, description);
                _messages[id] = comp;     
            }
        }
    }
}
