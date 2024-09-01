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
            var messages = doc.Descendants("messages").First().Descendants("message");
            foreach (var messageElement in messages)
            {
                var ad = messageElement.AsAttributeDict();
                MakeNode(ad["msgtype"], messageElement, Node.ElementType.MessageDefinition);
            }
        }

        //  <message name='Logout' msgcat='admin' msgtype='5'>
        private static MessageDefinition GetMessage(XElement fieldElement)
        {
            var name = "";
            var msgCat = "";
            var msgType = "";
            foreach (var a in fieldElement.AsAttributeDict())
            {
                switch (a.Key)
                {
                    case "msgcat":
                        msgCat = a.Value;
                        break;

                    case "name":
                        name = a.Value;
                        break;

                    case "msgtype":
                        msgType = a.Value;
                        break;
                }
            }

            var md = new MessageDefinition(name, name, msgType, msgCat, name);
            return md;
        }
    }
}
