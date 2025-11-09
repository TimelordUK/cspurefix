using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Dictionary.Parser
{
    public static class StringExtensions
    {
        public static string UnderscoreToCamelCase(this string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return "";
            }

            if (name.All(char.IsUpper))
            {
                name = name.ToLower().FirstCharToUpper();
                return name;
            }

            if (!name.Contains('_'))
            {
                return name.FirstCharToUpper();
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

            return newname.FirstCharToUpper();
        }

        public static string FirstCharToUpper(this string input) =>
            input switch
            {
                null => throw new ArgumentNullException(nameof(input)),
                "" => throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input)),
                _ => string.Concat(input[0].ToString().ToUpper(), input.AsSpan(1))
            };
    }
}
