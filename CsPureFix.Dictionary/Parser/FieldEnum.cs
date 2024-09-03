using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Dictionary.Parser
{
    public record FieldEnum(string Key, string Description)
    {
        public string Val => _val ??= Description.UnderscoreToCamelCase();
        private string? _val;
    }
}
