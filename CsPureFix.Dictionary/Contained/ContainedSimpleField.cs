using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Dictionary.Definition;

namespace PureFix.Dictionary.Contained
{
    public class ContainedSimpleField(
        SimpleFieldDefinition definition,
        int position,
        bool required,
        bool attribute,
        string? overrideName)
        : ContainedField(overrideName ?? definition.Name, position, ContainedFieldType.Simple, required)
    {
        public SimpleFieldDefinition Definition { get; } = definition;
        public bool IsAttribute { get; } = attribute;

        public override string ToString()
        {
            return $"[{Position}:{Required}] = S.{Definition.Tag} ({Name})";
        }
    }
}
