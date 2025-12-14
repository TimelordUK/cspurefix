using PureFix.Dictionary.Definition;
using PureFix.Dictionary.Parser;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PureFix.Dictionary.Contained
{
    public abstract partial class ContainedFieldSet(
        ContainedSetType type,
        string name,
        string? category,
        string? abbreviation,
        string description)
        : IContainedSet
    {
        protected readonly Dictionary<string, IContainedSet> _groups = [];
        protected readonly Dictionary<string, IContainedSet> _components = [];
        protected readonly Dictionary<string, ContainedSimpleField> _simple = [];
        protected readonly List<ContainedField> _fields = [];
        protected readonly Dictionary<int, IContainedSet> _containedTag = [];
        protected readonly List<int> _flattenedTag = [];
        protected readonly Dictionary<int, bool> _containedLength = [];
        protected readonly Dictionary<int, ContainedSimpleField> _localTag = [];
        protected readonly Dictionary<int, ContainedSimpleField> _localRequired = [];
        protected readonly Dictionary<int, ContainedSimpleField> _tagToSimple = [];
        protected readonly Dictionary<int, SimpleFieldDefinition> _tagToSimpleDefinition = [];
        protected readonly Dictionary<int, (IContainedSet? parent, ContainedField field)> _tagToField = [];
        protected readonly Dictionary<string, IContainedSet> _nameToSet = [];
        protected readonly Dictionary<string, ContainedField> _localNameToField = [];
        protected readonly Dictionary<string, ContainedSimpleField> _nameToLocalField = [];
        protected readonly Dictionary<string, ContainedSimpleField> _nameToLocalAttribute = [];
        protected readonly List<ContainedSimpleField> _localAttribute = [];

        /// <summary>
        /// Index of name to any group that may be present within the field list
        /// </summary>
        public IReadOnlyDictionary<string, IContainedSet> Groups => _groups;

        /// <summary>
        /// index of name to any component that may be present within the field list
        /// </summary>
        public IReadOnlyDictionary<string, IContainedSet> Components => _components;

        /// <summary>
        /// index of name to any simple field that may be present within the field list
        /// </summary>
        public IReadOnlyDictionary<string, ContainedSimpleField> Simple => _simple;

        /// <summary>
        /// sequence of fields representing this type - can be simple, group or component
        /// </summary>
        public IReadOnlyList<ContainedField> Fields => _fields;

        /// <summary>
        /// any tag at any level i.e. does this set contain a tag
        /// </summary>
        public IReadOnlyDictionary<int, IContainedSet> ContainedTag => _containedTag;

        /// <summary>
        /// any tag at any level ordered i.e. all tags flattened to list
        /// </summary>
        public IReadOnlyList<int> FlattenedTag => _flattenedTag;

        /// <summary>
        /// any data tags contained length within this set.
        /// </summary>
        public IReadOnlyDictionary<int, bool> ContainedLength => _containedLength;

        /// <summary>
        /// tags only in repository at this level, not from any at deeper levels
        /// </summary>
        public IReadOnlyDictionary<int, ContainedSimpleField> LocalTag => _localTag;

        /// <summary>
        /// tags marked required at this level only
        /// </summary>
        public IReadOnlyDictionary<int, ContainedSimpleField> LocalRequired => _localRequired;

        /// <summary>
        /// all tags contained within this field set flattened from all levels
        /// </summary>
        public IReadOnlyDictionary<int, ContainedSimpleField> TagToSimple => _tagToSimple;
        public IReadOnlyDictionary<int, SimpleFieldDefinition> TagToSimpleDefinition => _tagToSimpleDefinition;

        /// <summary>
        /// direct any tag contained within this set to field one level down where it belongs.
        /// </summary>
        public IReadOnlyDictionary<int, (IContainedSet? parent, ContainedField field)> TagToField => _tagToField;

        /// <summary>
        /// only repository directly in this set indexed by name
        /// </summary>
        public IReadOnlyDictionary<string, ContainedField> LocalNameToField => _localNameToField;

        /// <summary>
        /// for FixMl notation this set of fields appear as attributes i.e. <Pty ID="323" R="38">
        /// </summary>
        public IReadOnlyDictionary<string, ContainedSimpleField> NameToLocalAttribute => _nameToLocalField;

        /// <summary>
        /// all attributes in order of being declared
        /// </summary>
        public IReadOnlyList<ContainedSimpleField> LocalAttribute => _localAttribute;

        public IReadOnlyDictionary<string, IContainedSet> NameToSet => _nameToSet;

        /// <summary>
        /// at any level on this set, first declared simple field
        /// </summary>
        public ContainedSimpleField? FirstSimple { get; private set; }

        public string Name { get; init; } = name;
        public string? Category { get; init; } = category;
        public string? Abbreviation { get; init; } = abbreviation;
        public string Description { get; init; } = description;
        public ContainedSetType Type { get; init; } = type;

        /// <summary>
        /// parser needs to know about raw fields, any present in this set?
        /// </summary>
        public bool ContainsRaw { get; private set; }

        /// <summary>
        /// not for generral usage, this partially resets the set such it can be used on a second pass of the xml parser
        /// </summary>
        public void Reset()
        {
            _groups.Clear();
            _components.Clear();
            _simple.Clear();
            _fields.Clear();
            _flattenedTag.Clear();
            _localAttribute.Clear();
            _localNameToField.Clear();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append($"Set: {Name} ({GetPrefix()}) fields[{Fields.Count}]: ");
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
                return f.field.Name;
            }

            return $"{tag}";
        }

        /// <summary>
        /// recurses down a path to return nested set definitiom of a group or component
        /// given in dot notation 'SecListGrp.NoRelatedSym.SecurityTradingRules.BaseTradingRules'
        /// </summary>
        /// <param name="path">path in dot notation</param>
        /// <returns></returns>
        public IContainedSet? GetSet(string path)
        {
            return path?.Split('.').Aggregate(this, (IContainedSet? set, string next) =>
            {
                if (set is null) return null;
                return set.Groups.TryGetValue(next, out var g) ? g : set.Components.GetValueOrDefault(next);
            });
        }
    }
}