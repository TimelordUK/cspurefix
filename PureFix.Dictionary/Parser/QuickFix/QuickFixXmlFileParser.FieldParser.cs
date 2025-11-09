using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Reflection.PortableExecutable;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;
using System.Xml.Linq;
using PureFix.Dictionary.Definition;

namespace PureFix.Dictionary.Parser.QuickFix
{
    public partial class QuickFixXmlFileParser
    {
        private void ParseFields(XDocument doc)
        {
            var fieldNode = doc.Descendants("fields").FirstOrDefault();
            if (fieldNode == null) return;
            var fields = fieldNode.Elements("field");
            foreach (var fieldElement in fields)
            {
                var at = fieldElement.AsAttributeDict();
                MakeNode(NameFrom(at), fieldElement, Node.ElementType.SimpleFieldDefinition);
            }
        }

        private void ParseComponents(XDocument doc)
        {
            var componentsNode = doc.Descendants("components").FirstOrDefault();
            if (componentsNode == null) return;
            var components = componentsNode.Elements("component");
            foreach (var componentElement in components)
            {
                var at = componentElement.AsAttributeDict();
                MakeNode(NameFrom(at), componentElement, Node.ElementType.ComponentDefinition);
            }
        }

        private void ParseHeader(XDocument doc)
        {
            var header = doc.Descendants("header").FirstOrDefault() ?? throw new InvalidDataException("no header declared in fix definitions");
            _header = MakeNode("StandardHeader", header, Node.ElementType.ComponentDefinition);
        }

        private void ParseTrailer(XDocument doc)
        {
            var trailer = doc.Descendants("trailer").FirstOrDefault() ?? throw new InvalidDataException("no trailer declared in fix definitions");
            _trailer = MakeNode("StandardTrailer", trailer, Node.ElementType.ComponentDefinition);
        }

        private static string NameFrom(IReadOnlyDictionary<string, string> atts)
        {
            var name = atts["name"];
            name = name.Replace(" ", string.Empty);
            if (char.IsNumber(name[0]))
            {
                name = $"F{name}";
            }
            return name;
        }

        private static SimpleFieldDefinition GetField(XElement fieldElement)
        {
            var atts = fieldElement.AsAttributeDict();
            var name = NameFrom(atts);
            var tag = int.Parse(atts.GetValueOrDefault("number") ?? "-1");
            if (tag < 0) throw new InvalidDataException($"no tag/number for {name}");           
            var type = atts.GetValueOrDefault("type") ?? "STRING";
            
            var values = GetFieldValues(fieldElement);
            var sd = new SimpleFieldDefinition(name, null, type, tag, values);
            return sd;
        }

        private static List<FieldEnum>? GetFieldValues(XElement fieldElement)
        {
            var values = fieldElement.Elements("value");
            List<FieldEnum>? result = null;

            foreach (var value in values)
            {
                var atts = value.AsAttributeDict();
                var enumName = atts["enum"];
                var description = atts.GetValueOrDefault("description") ?? "";
                result ??= [];
                result.Add(new FieldEnum(enumName, description));
            }
            return result;
        }
    }
}
