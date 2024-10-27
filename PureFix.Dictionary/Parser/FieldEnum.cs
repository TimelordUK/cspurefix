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
            var res = Description.Replace(" ", "_").UnderscoreToCamelCase();
            return res;
        }
    }
}
