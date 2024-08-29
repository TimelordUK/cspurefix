using System;
using System.Collections.Generic;
using System.Linq;
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
            var fields = doc.Descendants("fields");
            foreach (var fieldElement in fields.Descendants())
            {
                (int tag, string name, string type) t = GetField(fieldElement);
                var values = GetFieldValues(fieldElement);
                var sf = new SimpleFieldDefinition(t.name, t.tag, TagTypeUtil.ToType(t.type), values);
                Definitions.AddSimple(sf);
            }
        }

        private static (int tag, string mame, string type) GetField(XElement fieldElement)
        {
            var tag = 0;
            var name = "";
            var type = "";
            foreach (var a in AsAttributeDict(fieldElement))
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
            return (tag, name, type);
        }

        private static List<FieldEnum> GetFieldValues(XElement fieldElement)
        {
            var values = fieldElement.Descendants("value");
            List<FieldEnum> result = null;

            foreach (var value in values)
            {
                var enumName = "";
                var description = "";
                foreach (var a in AsAttributeDict(value))
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

        private static Dictionary<string, string> AsAttributeDict(XElement element)
        {
            return element.Attributes().ToDictionary(a => a.Name.LocalName, a => a.Value);
        }
    }
}
