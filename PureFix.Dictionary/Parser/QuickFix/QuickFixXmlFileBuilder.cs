using PureFix.Dictionary.Contained;
using PureFix.Dictionary.Definition;
using PureFix.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PureFix.Dictionary.Parser.QuickFix
{
    public class QuickFixXmlFileBuilder(IFixDefinitions definitions)
    {
        private readonly Dictionary<int, string> _usedTags = [];
        private readonly Dictionary<string, ContainedComponentField> _seenComponents = [];
        private readonly int _indent = 2;

        public string Write(string[] msgTypes)
        {
            var sb = new StringBuilder();
            if (msgTypes.Length == 0)
            {
                return sb.ToString();
            }
            var m0Def = definitions.Message.GetValueOrDefault(msgTypes[0]);
            if (m0Def == null) return sb.ToString();
            sb.Append(QuickFixXmlFormatter.StartFix(definitions.GetMajor(), definitions.GetMinor(), definitions.GetServicePack()));
            var header = WriteComponent($"{m0Def.Name}.StandardHeader", "header");
            sb.Append(header);
            var trailer = WriteComponent($"{m0Def.Name}.StandardTrailer", "trailer");
            sb.Append(trailer);
            var messages = WriteMessages(msgTypes, _indent);
            sb.Append(messages);
            var components = WriteComponents(_indent);
            sb.Append(components);
            var fields = WriteFieldDefinitions(_indent);
            sb.Append(fields);
            sb.Append(QuickFixXmlFormatter.EndFix());
            return sb.ToString();          
        }

        private string WriteComponent(string name, string destName)
        {
            var sb = new StringBuilder();

            var set = definitions.GetSet(name);
            if (set == null)
            {
                return string.Empty;
            }
            sb.Append(QuickFixXmlFormatter.StartEntity(destName, _indent));
            sb.Append(WriteFields(set.Fields, _indent + _indent));
            sb.Append(QuickFixXmlFormatter.EndEntity(destName, _indent));

            return sb.ToString();
        }

        private string WriteFieldDefinitions(int leadingIndent) {
            var sb = new StringBuilder(2 * 1024);
            var tags = _usedTags.Keys.ToList();
            tags.Sort();
            sb.Append(QuickFixXmlFormatter.StartEntity("fields", leadingIndent));
            foreach (var t in tags) {
                var sf = definitions.TagToSimple[t];
                //   <field number='1' name='Account' type='STRING' />
                sb.Append(QuickFixXmlFormatter.DefineField(sf, leadingIndent + _indent));
                if (!sf.IsEnum) continue;
                var en = WriteEnumDefinition(sf, leadingIndent + _indent + _indent);
                sb.Append(en);
                sb.Append(QuickFixXmlFormatter.EndEntity("field", leadingIndent + _indent));
            }
            sb.Append(QuickFixXmlFormatter.EndEntity("fields", leadingIndent));
            return sb.ToString();
        }

        private static string WriteEnumDefinition(SimpleFieldDefinition sf, int leadingIndent) {
            var sb = new StringBuilder(2 * 1024);
            var keys = sf.Enums?.Keys.ToList() ?? [];
            keys.Sort();
            foreach (var key in keys) {
                var fe = sf?.Enums?.GetValueOrDefault(key);
                if (fe == null) continue;
                sb.Append(QuickFixXmlFormatter.AddEnum(fe, leadingIndent));
            }
            return sb.ToString();
        }
    
        private string WriteComponents(int leadingIndent)
        {
            var sb = new StringBuilder(2 * 1024);
            sb.Append(QuickFixXmlFormatter.StartEntity("components", leadingIndent));
            var components = _seenComponents.Values.ToList();
            foreach (var component in components)
            {
                sb.Append(QuickFixXmlFormatter.StartComponent(component.Name, leadingIndent + _indent));
                if (component.Definition == null) continue;
                var def = WriteFields(component.Definition.Fields, leadingIndent + _indent + _indent);
                sb.Append(def);
                sb.Append(QuickFixXmlFormatter.EndComponent(leadingIndent + _indent));
            }
            sb.Append(QuickFixXmlFormatter.EndEntity("components", leadingIndent));
            return sb.ToString();
        }

        private string WriteMessages(string[] msgTypes, int leadingIndent)
        {
            var sb = new StringBuilder(2 * 1024);
            sb.Append(QuickFixXmlFormatter.StartEntity("messages", leadingIndent));
            foreach (var mt in msgTypes)
            {
                var md = definitions.Message.GetValueOrDefault(mt);
                if (md == null) continue;
                sb.Append(QuickFixXmlFormatter.DefineMessage(md, leadingIndent + _indent));
                var fields = WriteFields(md.Fields.Skip(1).SkipLast(1).ToList(), leadingIndent + _indent + _indent);
                sb.Append(fields);
                sb.Append(QuickFixXmlFormatter.EndEntity("message", leadingIndent + _indent));
            }
            sb.Append(QuickFixXmlFormatter.EndEntity("messages", leadingIndent));
            return sb.ToString();
        }

        private string WriteFields(IReadOnlyList<ContainedField> fields, int leadingIndent)
        {
            var sb = new StringBuilder();
            foreach (var field in fields)
            {
                switch (field.Type)
                {
                    case ContainedFieldType.Simple:
                        WriteSimpleField((ContainedSimpleField)field, sb, leadingIndent);
                        break;

                    case ContainedFieldType.Group:
                        WriteGroupField((ContainedGroupField)field, sb, leadingIndent);
                        break;

                    case ContainedFieldType.Component:
                        WriteComponentField((ContainedComponentField)field, sb, leadingIndent);
                        break;
                }
            }

            return sb.ToString();
        }

        private void WriteGroupField(ContainedGroupField containedGroupField, StringBuilder sb, int leadingIndent)
        { 
            if (containedGroupField?.Definition == null) return;
            _usedTags[containedGroupField.Definition?.NoOfField?.Tag ?? 0] = containedGroupField.Name ?? "";
            sb.Append(QuickFixXmlFormatter.AddGroup(containedGroupField, leadingIndent));
            var groupDef = WriteFields(containedGroupField.Definition?.Fields ?? [], leadingIndent + _indent);
            sb.Append(groupDef);
            sb.Append(QuickFixXmlFormatter.EndGroup(leadingIndent));
        }

        private void WriteComponentField(ContainedComponentField containedComponentField, StringBuilder sb, int leadingIndent)
        {
            sb.Append(QuickFixXmlFormatter.AddComponent(containedComponentField, leadingIndent));

            // Add to seen components if not already there
            if (_seenComponents.TryAdd(containedComponentField.Name, containedComponentField))
            {
                // Recursively process nested components to ensure all dependencies are included
                if (containedComponentField.Definition != null)
                {
                    ProcessComponentFields(containedComponentField.Definition.Fields);
                }
            }
        }

        private void ProcessComponentFields(IReadOnlyList<ContainedField> fields)
        {
            foreach (var field in fields)
            {
                switch (field.Type)
                {
                    case ContainedFieldType.Simple:
                        var simpleField = (ContainedSimpleField)field;
                        _usedTags[simpleField.Definition.Tag] = simpleField.Name;
                        break;

                    case ContainedFieldType.Group:
                        var groupField = (ContainedGroupField)field;
                        if (groupField?.Definition != null)
                        {
                            _usedTags[groupField.Definition?.NoOfField?.Tag ?? 0] = groupField.Name ?? "";
                            ProcessComponentFields(groupField.Definition?.Fields ?? []);
                        }
                        break;

                    case ContainedFieldType.Component:
                        var componentField = (ContainedComponentField)field;
                        if (_seenComponents.TryAdd(componentField.Name, componentField))
                        {
                            if (componentField.Definition != null)
                            {
                                ProcessComponentFields(componentField.Definition.Fields);
                            }
                        }
                        break;
                }
            }
        }

        private void WriteSimpleField(ContainedSimpleField containedSimpleField, StringBuilder sb, int leadingIndent)
        {
            sb.Append(QuickFixXmlFormatter.AddField(containedSimpleField, leadingIndent));
            _usedTags[containedSimpleField.Definition.Tag] = containedSimpleField.Name;
        }
    }
}
