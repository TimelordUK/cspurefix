using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Dictionary.Contained
{
    public abstract class ContainedField
    {
        public string Name { get; }
        public int Position { get; }
        public ContainedFieldType Type { get; }
        public bool Required { get; }

        protected ContainedField(string name, int position, ContainedFieldType type, bool required)
        {
            Name = name;
            Position = position;
            Type = type;
            Required = required;
        }
    }
}
