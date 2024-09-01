using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Dictionary.Contained
{
    public abstract class ContainedField(string name, int position, ContainedFieldType type, bool required)
    {
        public string Name { get; } = name;
        public int Position { get; } = position;
        public ContainedFieldType Type { get; } = type;
        public bool Required { get; } = required;
    }
}
