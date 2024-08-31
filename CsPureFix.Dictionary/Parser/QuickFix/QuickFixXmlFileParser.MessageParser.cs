using PureFix.Buffer.tag;
using PureFix.Dictionary.Definition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PureFix.Dictionary.Parser.QuickFix
{
    public partial class QuickFixXmlFileParser
    {
        private void ParseMessages(XDocument doc)
        {
            var fields = doc.Descendants("messages");
            foreach (var messageElement in fields.Descendants())
            {
                var ad = AsAttributeDict(messageElement);
                var node = MakeNode(ad["name"], messageElement, Node.ElementType.FieldDefinition);
            }
        }

        //  <message name='Logout' msgcat='admin' msgtype='5'>
        private static MessageDefinition GetMessage(XElement fieldElement)
        {
            var name = "";
            var msgCat = "";
            var msgType = "";
            foreach (var a in AsAttributeDict(fieldElement))
            {
                switch (a.Key)
                {
                    case "msgcat":
                        msgCat = a.Value;
                        break;

                    case "name":
                        name = a.Value;
                        break;

                    case "msgType":
                        msgType = a.Value;
                        break;
                }
            }

            var md = new MessageDefinition(name, name, msgType, msgCat, name);
            return md;
        }
    }
}
