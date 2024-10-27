using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Dictionary.Definition;
using PureFix.Types;


namespace PureFix.Dictionary.Contained
{
    public partial class ContainedFieldSet
    {
        public void Add(ContainedField field)
        {
            if (_localNameToField.ContainsKey(field.Name)) return;
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
                    AddLocalSimple((ContainedSimpleField)field);
                    break;

                case ContainedFieldType.Component:
                    AddUpdateComponent(field);
                    break;

                case ContainedFieldType.Group:
                    AddUpdateGroup(field);
                    break;
            }
        }

        private void AddUpdateGroup(ContainedField field)
        {
            var gf = field as ContainedGroupField;
            var definition = gf?.Definition;
            if (definition != null && !string.IsNullOrEmpty(definition.Abbreviation) &&
             definition.Abbreviation != field.Name)
            {
                _localNameToField[definition.Abbreviation] = field;
            }
        }

        private void AddUpdateComponent(ContainedField field)
        {
            var cf = field as ContainedComponentField;
            var definition = cf?.Definition;
            if (definition != null && !string.IsNullOrEmpty(definition.Abbreviation) &&
                definition.Abbreviation != field.Name)
            {
                _localNameToField[definition.Abbreviation] = field;
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
                    AddGroupFieldDef((ContainedGroupField)field);
                    break;
                }

                case ContainedFieldType.Component:
                {
                    AddComponentFieldDef((ContainedComponentField)field);
                    break;
                }

                case ContainedFieldType.Simple:
                {
                    AddSimpleFieldDef(parent, (ContainedSimpleField)field);
                    break;
                }
            }
        }

        private void AddGroupFieldDef(ContainedGroupField groupField)
        {
            if (_groups.ContainsKey(groupField.Name)) { return; }

            var definition = groupField.Definition;

            if (definition is not null)
            {
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
        }

        private void AddComponentFieldDef(ContainedComponentField componentField)
        {
            if (_components.ContainsKey(componentField.Name))
            {
                return;
            }

            var definition = componentField.Definition;

            if (definition is not null)
            {
                _components[componentField.Name] = definition;
                AddAllFields(definition);
                MapAllBelow(definition, componentField);
            }
        }

        private void MapAllBelow(ContainedFieldSet set, ContainedField field)
        {
            var tagsBelow = set.Keys();
            for (var i = 0; i < tagsBelow.Count; i++)
            {
                var tag = tagsBelow[i];
                _tagToField[tag] = (set, field);
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
            var fields = containedField.Fields;

            for (int i = 0; i < fields.Count; i++)
            {
                AddContained(containedField, fields[i]);
            }
        }

        public void Index()
        {
            // we need to keep _fields as they are the actual list representing
            // this set - but for all fields below us, need to ensure our tag set
            // is up to date.  This will re-compute the index.

            var level0 = _fields.ToList();
            var tg = new ContainedFieldCollector();
            var fields = tg.Compute(this);
            Reset();

            // add back our saved top level fields.
            
            foreach (var field in level0)
            {
                Add(field);
            }

            // for everything below us, add to our indices
            foreach (var (parent, child) in fields)
            {
                AddContained((ContainedFieldSet)parent, child);
            }
        }
    }
}
