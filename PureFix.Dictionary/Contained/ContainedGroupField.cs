using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Dictionary.Definition;

namespace PureFix.Dictionary.Contained
{
    public class ContainedGroupField(
        GroupFieldDefinition? definition,
        int position,
        bool required,
        string? overrideName)
        : ContainedField(overrideName ?? definition?.Name ?? throw new ArgumentException("could not resolve name"),
            position, ContainedFieldType.Group, required)
    {
        public GroupFieldDefinition? Definition { get; } = definition;

        public override string ToString()
        {
            if (Definition == null)
            {
                return "";
            }

            var tag = Definition.NoOfField?.Tag ?? -1;
            return $"[{Position}:{Required}]= G.{Definition.Fields.Count} (0={tag})({Name})";
        }
    }
}
