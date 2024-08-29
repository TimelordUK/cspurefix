using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using PureFix.Buffer.tag;
using PureFix.Dictionary.Parser;

namespace PureFix.Dictionary.Definition
{
    public class SimpleFieldDefinition
    {
        public int Tag { get; }
        public string Name { get; }
        public TagType TagType { get; }
        private readonly Dictionary<string, FieldEnum> _enums;
        public bool IsEnum => _enums != null && _enums.Count > 0;
        public SimpleFieldDefinition(string name, int tag, TagType tagType, List<FieldEnum> enums)
        {
            Name = name;
            Tag = tag;
            TagType = tagType;
            if (enums != null && enums.Count > 0)
            {
                _enums = enums.ToDictionary(fe => fe.Key, fe => fe);

            }
        }

        public string ResolveEnum(string key)
        {
            return _enums.TryGetValue(key, out var fe) ? fe.Val : key;
        }

        public override string ToString()
        {
            var enums = _enums != null ? string.Join(", ", _enums.Values) : "";
            return $"Name = {Name}, Tag = {Tag}, TagType = {TagType}, IsEnum = {IsEnum}, Enums = {_enums?.Count} ?? 0, {enums}";
        }
    }
}
