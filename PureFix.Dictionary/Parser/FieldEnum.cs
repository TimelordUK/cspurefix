using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Dictionary.Parser
{
    public record FieldEnum(string Key, string Description)
    {
        public string Val => _val ??= FromDescription(Description);
        private string? _val;

        private string FromDescription(string description)
        {
            if (string.IsNullOrEmpty(description)) return "Unknown";
            var res = Description
                .Replace("-", "-")
                .Replace("+", "Plus")
                .Replace("-", "_")
                .Replace("&", "And")
                .Replace("#", "Hash")
                .Replace("(", "_")
                .Replace(")", "_")
                .Replace(" ", "_")
                .Replace("\\", "_")
                .Replace("/", "_")
                .Replace("@", "_")
                .Replace("!", "_")
                .UnderscoreToCamelCase();
            return res;
        }
    }
}
