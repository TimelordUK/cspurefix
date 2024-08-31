using PureFix.Dictionary.Definition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PureFix.Dictionary.Contained
{
    internal class ContainedComponentField : ContainedField
    {
        public ComponentFieldDefinition Definition { get; }
        public ContainedComponentField(ComponentFieldDefinition definition, int position, bool required, string overrideName) :
            base(overrideName ?? definition.Name, position, ContainedFieldType.Component, required)
        {
            Definition = definition;
        }

        public override string ToString()
        {
            return Definition == null ? "" : $"[{Position}:{Required}]= C.{Definition.Fields.Count} ({Name})";
        }
    }
}
