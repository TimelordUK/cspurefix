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
 * MsgContents xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" copyright="Copyright (c) FIX Protocol Ltd. All Rights Reserved." edition="2010" version="FIX.5.0SP2" xsi:noNamespaceSchemaLocation="../../schema/MsgContents.xsd" generated="2010-05-20T02:30:02.802-04:00" latestEP="95">
<MsgContent added="FIX.2.7">
    <ComponentID>1</ComponentID>
    <TagText>StandardHeader</TagText>
    <Indent>0</Indent>
    <Position>1</Position>
    <Reqd>1</Reqd>
    <Description>MsgType = 0</Description>
</MsgContent>
 */
    public partial class RepoXmlMsgContentParser : RepoBaseXmlParser
    {
        private Dictionary<int, List<RepoMsgContentDefinition>> _contents { get; set; } = [];
        public IReadOnlyDictionary<int, List<RepoMsgContentDefinition>> Contents => _contents;

        public RepoXmlMsgContentParser(IFixDefinitions definitions): base(definitions)
        {
        }

        protected override void ParseDoc(XDocument doc)
        {
            var fielsNode = doc.Descendants("MsgContents").FirstOrDefault();
            if (fielsNode == null) return;
            var fields = fielsNode.Elements("MsgContent");
            foreach (var fieldElement in fields)
            {
                var at = fieldElement.AsAttributeDict();
                int id = fieldElement.AsInt("ComponentID");              
                var tagText = fieldElement.AsString("TagText");
                int indent = fieldElement.AsInt("Indent");
                string position = fieldElement.AsString("Position");
                int required = fieldElement.AsInt("Reqd");              
                var description = fieldElement.AsString("Description");
              
                var comp = new RepoMsgContentDefinition(id, tagText, indent, position, required, description);
                if (!_contents.TryGetValue(id, out var contents))
                {
                    _contents[id] = contents = [];
                }
                contents.Add(comp);     
            }
        }
    }
}
