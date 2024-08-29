using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Dictionary.Parser
{
    public record FieldEnum(string Key, string Description)
    {
        public string Val => _val??= UnderscoreToCamelCase(Description);
        private string _val;

        private static string FromCamel(string input)
        {
            var result = string.Concat(input.Select((x, i) => i > 0 && char.IsUpper(x) ? $"_{x}" : x.ToString()));
            return result;
        }

        private string UnderscoreToCamelCase(string name)
        {
            if (string.IsNullOrEmpty(name) || !name.Contains("_"))
            {
                return name;
            }
            var array = name.Split('_');
            for (var i = 0; i < array.Length; i++)
            {
                var s = array[i];
                var first = string.Empty;
                var rest = string.Empty;
                if (s.Length > 0)
                {
                    first = char.ToUpperInvariant(s[0]).ToString();
                }
                if (s.Length > 1)
                {
                    rest = s[1..].ToLowerInvariant();
                }
                array[i] = first + rest;
            }
            string newname = string.Join("", array);
            if (newname.Length > 0)
            {
                newname = char.ToLowerInvariant(newname[0]) + newname[1..];
            }
            else
            {
                newname = name;
            }
            return newname;
        }
    }
}
