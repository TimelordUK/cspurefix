using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Reflection.PortableExecutable;
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
            var fielsNode = doc.Descendants("fields").FirstOrDefault();
            if (fielsNode == null) return;
            var fields = fielsNode.Elements("field");
            foreach (var fieldElement in fields)
            {
                var at = fieldElement.AsAttributeDict();
                MakeNode(at["name"], fieldElement, Node.ElementType.SimpleFieldDefinition);
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
                MakeNode(at["name"], componentElement, Node.ElementType.ComponentDefinition);
            }
        }

        private void ParseHeader(XDocument doc)
        {
            var header = doc.Descendants("header").FirstOrDefault();
            if (header == null) throw new InvalidDataException("no header declared in fix definitions");
            _header = MakeNode("StandardHeader", header, Node.ElementType.ComponentDefinition);
        }

        private void ParseTrailer(XDocument doc)
        {
            var trailer = doc.Descendants("trailer").FirstOrDefault();
            if (trailer == null) throw new InvalidDataException("no trailer declared in fix definitions");
            _trailer = MakeNode("StandardTrailer", trailer, Node.ElementType.ComponentDefinition);
        }

        private static SimpleFieldDefinition GetField(XElement fieldElement)
        {
            var atts = fieldElement.AsAttributeDict();
            var tag = int.Parse(atts["number"]);
            var name = atts["name"];
            var type = atts["type"];
            
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
                result ??= new List<FieldEnum>();
                result.Add(new FieldEnum(enumName, description));
            }
            return result;
        }
    }
}
