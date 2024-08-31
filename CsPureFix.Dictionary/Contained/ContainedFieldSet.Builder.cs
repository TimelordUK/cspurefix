using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Buffer.tag;
using PureFix.Dictionary.Definition;

namespace PureFix.Dictionary.Contained
{
    public partial class ContainedFieldSet
    {
        public void Add(ContainedField field)
        {
            _fields.Add(field);
            _localNameToField[field.Name] = field;
            AddUpdate(field);
            AddContained(this, field);
        }

        private void AddUpdate(ContainedField field)
        {
            switch (field.Type)
            {
                case ContainedFieldType.Simple:
                    AddLocalSimple(field as ContainedSimpleField);
                    break;

                case ContainedFieldType.Component:
                {
                    var cf = field as ContainedComponentField;
                    var definition = cf?.Definition;
                    if (definition != null && !string.IsNullOrEmpty(definition.Abbreviation) &&
                        definition.Abbreviation != field.Name)
                    {
                        _localNameToField[definition.Abbreviation] = field;
                    }
                    break;
                }

                case ContainedFieldType.Group:
                {
                    var gf = field as ContainedGroupField;
                    var definition = gf?.Definition;
                    if (definition != null && !string.IsNullOrEmpty(definition.Abbreviation) &&
                     definition.Abbreviation != field.Name)
                    {
                        _localNameToField[definition.Abbreviation] = field;
                    }
                    break;
                }
            }
        }

        private void AddLocalSimple(ContainedSimpleField field)
        {
            var definition = field.Definition;
            if (definition == null) throw new ArgumentException($"field {field} has no definition");
            
            if (definition.Abbreviation != null && definition.Abbreviation != definition.Name)
            {
                _localNameToField[definition.Abbreviation] = field;
            }

            if (definition.BaseCategoryAbbreviation != null && definition.BaseCategory != Category) 
            {
                _localNameToField[definition.BaseCategoryAbbreviation] = field;
            }

            if (definition.Abbreviation != null && field.IsAttribute)
            {
                _nameToLocalAttribute[definition.Abbreviation] = field;
                _localAttribute.Add(field);
                _fields.RemoveAt(_fields.Count - 1);
            }

            var tag = definition.Tag;
            _localTag[tag] = field;
            if (field.Required)
            {
                _localRequired[tag] = field;
            }
        }

        private void AddContained(ContainedFieldSet parent, ContainedField field)
        {
            switch (field.Type)
            {
                case ContainedFieldType.Group:
                {
                    AddGroupFieldDef(parent, field as ContainedGroupField);
                    break;
                }

                case ContainedFieldType.Component:
                {
                    AddComponentFieldDef(parent, field as ContainedComponentField);
                    break;
                }

                case ContainedFieldType.Simple:
                {
                    AddSimpleFieldDef(parent, field as ContainedSimpleField);
                    break;
                }
            }
        }

        private void AddGroupFieldDef(ContainedFieldSet containedField, ContainedGroupField groupField)
        {
            if (_groups.ContainsKey(groupField.Name)) { return; }

            var definition = groupField.Definition;
            _groups[groupField.Name] = definition;
            var nof = definition.NoOfField;
            if (nof != null)
            {
                var tag = nof.Tag;
                _containedTag[tag] = true;
                _flattendTag.Add(tag);
            }

            AddAllFields(definition);
            MapAllBelow(definition, groupField);
        }

        private void AddComponentFieldDef(ContainedFieldSet containedField, ContainedComponentField componentField)
        {
            if (_components.ContainsKey(componentField.Name))
            {
                return;
            }

            var definition = componentField.Definition;
            _components[componentField.Name] = definition;
            AddAllFields(definition);
            MapAllBelow(definition, componentField);
        }

        private void MapAllBelow(ContainedFieldSet set, ContainedField field)
        {
            var tagsBelow = set.Keys();
            foreach (var tag in tagsBelow)
            {
                _tagToField[tag] = field;
            }
        }

        private void AddSimpleFieldDef(ContainedFieldSet parent, ContainedSimpleField field)
        {
            if (_simple.ContainsKey(field.Name))
            {
                return;
            }

            FirstSimple ??= field;
            switch (field.Definition.TagType)
            {
                case TagType.RawData:
                {
                    if (parent.Fields[field.Position - 1] is ContainedSimpleField dataLengthField && dataLengthField.Definition.TagType == TagType.Length)
                    {
                        _containedLength[dataLengthField.Definition.Tag] = true;
                        ContainsRaw = true;
                    }
                    break;
                }
            }

            var tag = field.Definition.Tag;
            _simple[field.Name] = field;
            _containedTag[tag] = true;
            _flattendTag.Add(tag);
            _tagToSimple[tag] = field;
        }

        private void AddAllFields(ContainedFieldSet containedField)
        {
            foreach (var field in containedField.Fields)
            {
                AddContained(containedField, field);
            }
        }
    }
}
