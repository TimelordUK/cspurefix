using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Dictionary.Contained
{
    public class ContainedField
    {
        public string Name { get; private set; }
        public int Position { get; private set; }
        public ContainedFieldType Type { get; private set; }
        public bool Required { get; private set; }

        public ContainedField(string name, int position, ContainedFieldType type, bool required)
        {
            Name = name;
            Position = position;
            Type = type;
            Required = required;
        }
    }
}
