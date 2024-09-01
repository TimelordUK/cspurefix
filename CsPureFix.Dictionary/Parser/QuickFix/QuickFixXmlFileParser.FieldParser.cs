using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;
using System.Xml.Linq;
using PureFix.Buffer.tag;
using PureFix.Dictionary.Definition;

namespace PureFix.Dictionary.Parser.QuickFix
{
    public partial class QuickFixXmlFileParser
    {
        private void ParseFields(XDocument doc)
        {
            var fields = doc.Descendants("fields").First().Descendants("field");
            foreach (var fieldElement in fields)
            {
                var at = fieldElement.AsAttributeDict();
                MakeNode(at["name"], fieldElement, Node.ElementType.SimpleFieldDefinition);
            }
        }

        private void ParseComponents(XDocument doc)
        {
            var fields = doc.Descendants("components").First().Descendants("component");
            foreach (var componentElement in fields)
            {
                var at = componentElement.AsAttributeDict();
                MakeNode(at["name"], componentElement, Node.ElementType.ComponentDefinition);
            }
        }

        private static SimpleFieldDefinition GetField(XElement fieldElement)
        {
            var tag = 0;
            var name = "";
            var type = "";
            foreach (var a in fieldElement.AsAttributeDict())
            {
                switch (a.Key)
                {
                    case "number":
                        tag = int.Parse(a.Value);
                        break;

                    case "name":
                        name = a.Value;
                        break;

                    case "type":
                        type = a.Value;
                        break;
                }
            }

            var values = GetFieldValues(fieldElement);
            var sd = new SimpleFieldDefinition(name, null, type, tag, values);
            return sd;
        }

        private static List<FieldEnum> GetFieldValues(XElement fieldElement)
        {
            var values = fieldElement.Descendants("value");
            List<FieldEnum> result = null;

            foreach (var value in values)
            {
                var enumName = "";
                var description = "";
                foreach (var a in value.AsAttributeDict())
                {
                    switch (a.Key)
                    {
                        case "enum":
                            enumName = a.Value;
                            break;

                        case "description":
                            description = a.Value;
                            break;
                    }
                }

                result ??= new List<FieldEnum>();
                result.Add(new FieldEnum(enumName, description));
            }
            return result;
        }

   
    }
}
