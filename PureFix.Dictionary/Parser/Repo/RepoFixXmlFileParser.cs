using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Dictionary.Contained;
using PureFix.Dictionary.Definition;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PureFix.Dictionary.Parser.Repo
{
    public class RepoFixXmlFileParser
    {
        public IFixDefinitions Definitions { get; private set; }

        private IReadOnlyDictionary<int, List<RepoMsgContentDefinition>>? _content;
        private IReadOnlyDictionary<int, RepoMessageDefinition>? _messages;
        private IReadOnlyDictionary<int, RepoComponentDefinition>? _componentsById;
        private IReadOnlyDictionary<string, RepoComponentDefinition>? _componentsByName;
        private Dictionary<int, GroupFieldDefinition> _groups = [];
  
        public RepoFixXmlFileParser(IFixDefinitions definitions) 
        {
            Definitions = definitions;
        }

        public void Parse(string basePath)
        {
            var e = new RepoXmlEnumParser(Definitions);
            e.Parse(Path.Join(basePath, "Enums.xml"));

            var f = new RepoXmlFieldParser(Definitions) { Enums = e.Enums };
            f.Parse(Path.Join(basePath, "Fields.xml"));

            var c = new RepoXmlComponentParser(Definitions);
            c.Parse(Path.Join(basePath, "Components.xml"));

            var a = new RepoXmlAbbreviationParser(Definitions);
            a.Parse(Path.Join(basePath, "Abbreviations.xml"));

            var d = new RepoXmlDataTypeParser(Definitions);
            d.Parse(Path.Join(basePath, "Datatypes.xml"));

            var t = new RepoXmlMsgContentParser(Definitions);
            t.Parse(Path.Join(basePath, "MsgContents.xml"));

            var m = new RepoXmlMessageParser(Definitions);
            m.Parse(Path.Join(basePath, "Messages.xml"));

            _content = t.Contents;
            _messages = m.Messages;
            _componentsById = c.ComponentsByID;
            _componentsByName = c.ComponentsByName;

            Complex();

        }

        private void Complex()
        {
            if (_componentsById == null) return;
            if (_messages == null) return;
            if (_content == null) return;

            foreach (var item in _componentsById.Values)
            {
                var def = Resolve(item);
            }

            if (Definitions.Component.TryGetValue("StandardHeader", out var hdr))
            {
                Definitions.AddComponent( hdr, "header");
            }

            if (Definitions.Component.TryGetValue("StandardTrailer", out var tr))
            {
                Definitions.AddComponent(tr, "trailer");
            }
        }

        private IContainedSet? Resolve(RepoComponentDefinition component)
        {
            if (_content == null) return null;
            switch (component.ComponentType)
            {
                case "ImplicitBlockRepeating":
                case "BlockRepeating":
                    {
                        if (_content.TryGetValue(component.ComponentID, out var content)) {
                            if (Definitions.TagToSimple.TryGetValue(component.ComponentID, out var noField))
                            {
                                if (_groups.TryGetValue(component.ComponentID, out var group))
                                {
                                    return group;
                                }
                                group = new GroupFieldDefinition(component.Name, component.AbbrName, component.CategoryID, noField, component.Description);
                                _groups[component.ComponentID] = group;
                                ResolveFieldSet(content[1..], group);
                            }
                        }
                        break;
                    }

                default:
                    {
                        if (_content.TryGetValue(component.ComponentID, out var content))
                        {
                            if (!Definitions.Component.TryGetValue(component.Name, out var compDef))
                            {
                                compDef = new ComponentFieldDefinition(component.Name, component.AbbrName, component.CategoryID, component.Description);
                                Definitions.AddComponent(compDef);
                            }
                            ResolveFieldSet(content, compDef);
                            return compDef;
                        }
                        break;
                    }
            }

            return null;
        }

        private void ResolveFieldSet(List<RepoMsgContentDefinition> parentSet, ContainedFieldSet builder)
        {
            if (_componentsByName == null) return;
            foreach (var current in parentSet)
            {
                var required = current.Required == 1;
                if (int.TryParse(current.TagText, out var tag))
                {
                    if (Definitions.TagToSimple.TryGetValue(tag, out var sf))
                    {
                        builder.Add(new ContainedSimpleField(sf, builder.Fields.Count, required, false, null));
                    }
                } else
                {
                    IContainedSet? childSet = null;
                    if (!Definitions.Component.TryGetValue(current.TagText, out var cs))
                    {
                        if (_componentsByName.TryGetValue(current.TagText, out var cl)) {
                            childSet = Resolve(cl);
                        }
                    } else
                    {
                        childSet = cs;
                    }
                    if (childSet != null)
                    {
                        switch (childSet.Type)
                        {
                            case ContainedSetType.Component:
                                {
                                    builder.Add(new ContainedComponentField(childSet as ComponentFieldDefinition, builder.Fields.Count, required, null));
                                    break;
                                }

                            case ContainedSetType.Group:
                                {
                                    builder.Add(new ContainedGroupField(childSet as GroupFieldDefinition, builder.Fields.Count, required, null));
                                    break;
                                }

                            default:
                                {
                                    throw new ArgumentException("bad set type");
                                }
                        }
                    }
                }
            }
        }
    }
}
