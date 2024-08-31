using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Dictionary.Definition;

namespace PureFix.Dictionary.Contained
{
    public class ContainedSimpleField : ContainedField
    {
        public SimpleFieldDefinition Definition { get; }
        public ContainedSimpleField(SimpleFieldDefinition definition, int position, bool required, bool attribute,
            string overrideName) : base(overrideName ?? definition.Name, position, ContainedFieldType.Simple, required)
        {
            Definition = definition;
        }
        public override string ToString()
        {
            return $"[{Position}]= S.{Definition.Tag} ({Name})";
        }
    }
}
