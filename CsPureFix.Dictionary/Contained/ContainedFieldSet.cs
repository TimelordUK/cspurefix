using PureFix.Dictionary.Parser;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;


namespace PureFix.Dictionary.Contained
{
    public abstract class ContainedFieldSet : IContainedSet
    {
        protected readonly Dictionary<string, IContainedSet> _groups = new();
        protected readonly Dictionary<string, IContainedSet> _components = new();
        protected readonly Dictionary<string, ContainedSimpleField> _simple = new();
        protected readonly List<ContainedField> _fields = new();
        protected readonly Dictionary<int, bool> _containedTag = new();
        protected readonly List<int> _flattendTag = new();
        protected readonly Dictionary<int, bool> _containedLength = new();
        protected readonly Dictionary<int, ContainedSimpleField> _localTag = new();
        protected readonly Dictionary<int, ContainedSimpleField> _localRequired = new();
        protected readonly Dictionary<int, ContainedSimpleField> _tagToSimple = new();
        protected readonly Dictionary<int, ContainedField> _tagToField = new();
        protected readonly Dictionary<string, ContainedField> _localNameToField = new();
        protected readonly Dictionary<string, ContainedSimpleField> _nameToLocalField = new();
        protected readonly List<ContainedSimpleField> _localAttribute = new();

        /**
         * index of name to any group that may be present within the field list
         */
        public IReadOnlyDictionary<string, IContainedSet> Groups => _groups;

        /**
        * index of name to any component that may be present within the field list
        */

        public IReadOnlyDictionary<string, IContainedSet> Components => _components;

        /**
         * index of name to any simple field that may be present within the field list
         */
        public IReadOnlyDictionary<string, ContainedSimpleField> Simple => _simple;

        /**
         *  sequence of fields representing this type - can be simple, group or component
         */
        public IReadOnlyList<ContainedField> Fields => _fields;

        /**
         * any tag at any level i.e. does this set contain a tag
         */
        public IReadOnlyDictionary<int, bool> ContainedTag => _containedTag;

        /**
         * any tag at any level ordered i.e. all tags flattened to list
         */
        public IReadOnlyList<int> FlattenedTag => _flattendTag;

        /**
         * any data tags contained length within this set.
         */
        public IReadOnlyDictionary<int, bool> ContainedLength => _containedLength;

        /**
         * tags only in repository at this level, not from any at deeper levels
         */
        public IReadOnlyDictionary<int, ContainedSimpleField> LocalTag => _localTag;

        /**
         * tags marked required at this level only
         */
        public IReadOnlyDictionary<int, ContainedSimpleField> LocalRequired => _localRequired;

        /**
         * all tags contained within this field set flattened from all levels
        */
        public IReadOnlyDictionary<int, ContainedSimpleField> TagToSimple => _tagToSimple;

        /**
         * direct any tag contained within this set to field one level down where it belongs.
        */
        public IReadOnlyDictionary<int, ContainedField> TagToField => _tagToField;

        /**
         * only repository directly in this set indexed by name
         */
        public IReadOnlyDictionary<string, ContainedField> LocalNameToField => _localNameToField;

        /**
         * for FixMl notation this set of fields appear as attributes i.e. <Pty ID="323" R="38">
         */
        public IReadOnlyDictionary<string, ContainedSimpleField> NameToLocalAttribute => _nameToLocalField;

        /**
         * all attributes in order of being declared
         */
        public IReadOnlyList<ContainedSimpleField> LocalAttribute => _localAttribute;

        /**
         * at any level on this set, first declared simple field
         */
        public ContainedSimpleField FirstSimple { get; private set; }

        public string Name { get; init; }
        public string Category { get; init; }
        public string Abbreviation { get; init; }
        public string Description { get; init; }
        public ContainedSetType Type { get; init; }

        /**
         * parser needs to know about raw fields, any present in this set?
         */
        public bool ContainsRaw { get; private set; }

        protected ContainedFieldSet(ContainedSetType type,
            string name,
            string category,
            string abbreviation,
            string description)
        {
            Name = name;
            Category = category;
            Abbreviation = abbreviation;
            Description = description;
            Type = type;
        }

        /**
         * not for generral usage, this partially resets the set such it can be used on a second
         * pass of the xml parser
         */
        public void Reset()
        {
            _groups.Clear();
            _components.Clear();
            _simple.Clear();
            _fields.Clear();
            _flattendTag.Clear();
            _localAttribute.Clear();
            _localNameToField.Clear();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append($"Set: {Name} (${GetPrefix()}) fields[${Fields.Count}]: ");
            sb.Append($"{string.Join(", ", Fields.Select(s => s.ToString()))}");
            return sb.ToString();
        }

        public abstract string GetPrefix();

        public IReadOnlyList<int> Keys()
        {
            return _containedTag.Keys.ToList();
        }

        public string GetFieldName(int tag)
        {
            if (TagToSimple.TryGetValue(tag, out var sf))
            {
                return sf.Name;
            }

            if (TagToField.TryGetValue(tag, out var f))
            {
                return f.Name;
            }

            return $"{tag}";
        }

        /**
         * recurses down a path to return nested set definitiom of a group or component
         * given in dot notation 'SecListGrp.NoRelatedSym.SecurityTradingRules.BaseTradingRules'
         * @param path in dot notation
         */
        public IContainedSet GetSet(string path)
        {
            return path?.Split('.').Aggregate(this,
                (IContainedSet set, string next) => set.Groups.TryGetValue(next, out var g) 
                    ? g 
                    : set.Components.GetValueOrDefault(next));
        }
    }
}