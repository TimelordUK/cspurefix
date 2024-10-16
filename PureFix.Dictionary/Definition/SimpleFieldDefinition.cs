using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using PureFix.Dictionary.Parser;
using PureFix.Types;


namespace PureFix.Dictionary.Definition
{
    public class SimpleFieldDefinition
    {
        public int Tag { get; }
        public string Name { get; }
        public string Description { get; }
        public string Type { get; }
        public TagType TagType { get; }
        private readonly Dictionary<string, FieldEnum>? _enums;
        private readonly HashSet<string>? _enumVals;
        public bool IsEnum => _enums != null && _enums.Count > 0;
        // needed for FIXML as "ID" is resolved within the context of its parent.
        public string? Abbreviation { get; }
        public string? BaseCategory { get; }
        public string? BaseCategoryAbbreviation { get; }
        public int Count => _enums?.Count ?? 0;
        public IReadOnlySet<string>? EnumVals => _enumVals;
        public IReadOnlyDictionary<string, FieldEnum>? Enums => _enums;

        // var sd = new SimpleFieldDefinition(name, tag, TagTypeUtil.ToType(type), values);
        public SimpleFieldDefinition(string name, string? cat, string type, int tag, List<FieldEnum>? enums) : this(name, null, cat, null, type, name, tag, enums)
        {
        }

        public SimpleFieldDefinition(string name, string? abbreviation, string? baseCategory, string? baseCategoryAbbreviation, string type, string description, int tag, List<FieldEnum>? enums)
        {
            Abbreviation = abbreviation;
            BaseCategory = baseCategory;
            Description = description;
            BaseCategoryAbbreviation = baseCategoryAbbreviation;
            Name = name;
            Type = type;
            Tag = tag;
            TagType = TagManager.ToType(type);
            
            if (enums != null && enums.Count > 0)
            {
                _enums = enums.ToDictionary(fe => fe.Key, fe => fe);
                _enumVals = [.._enums.Values.Select(kv => kv.Val).Distinct()];
            }
        }

        public string ResolveEnum(string key)
        {
            if (_enums is null) return key;
            return _enums.TryGetValue(key, out var fe) ? fe.Val : key;
        }

        public override string ToString()
        {
            var enums = _enums != null ? string.Join(", ", _enums.Values) : "";
            return $"Name = {Name}, Tag = {Tag}, TagType = {TagType}, IsEnum = {IsEnum}, Enums = {_enums?.Count} ?? 0, {enums}";
        }
    }
}
