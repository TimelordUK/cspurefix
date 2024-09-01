using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Dictionary.Contained;
using PureFix.Dictionary.Parser;

namespace PureFix.Dictionary.Definition
{
    public class ComponentFieldDefinition : ContainedFieldSet
    {
        public ComponentFieldDefinition(string name, string? abbreviation, string? category, string description) :
            base(ContainedSetType.Component, name, category, abbreviation, description)
        {
        }

        public override string GetPrefix()
        {
            return "C";
        }
    }
}
