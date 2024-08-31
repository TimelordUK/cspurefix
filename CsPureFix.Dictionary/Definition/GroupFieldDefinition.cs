using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Dictionary.Contained;
using PureFix.Dictionary.Parser;

namespace PureFix.Dictionary.Definition
{
    public class GroupFieldDefinition : ContainedFieldSet
    {
        public SimpleFieldDefinition NoOfField { get; }

        public GroupFieldDefinition(string name, string abbreviation, string category, SimpleFieldDefinition noOfField,
            string description) :
            base(ContainedSetType.Group, name, abbreviation, category, description)
        {
            NoOfField = noOfField;
            if (noOfField != null)
            {
                _containedTag[noOfField.Tag] = true;
            }
        }

        public override string GetPrefix()
        {
            return "G";
        }
    }
}
