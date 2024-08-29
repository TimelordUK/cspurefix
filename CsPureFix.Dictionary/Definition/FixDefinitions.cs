using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Dictionary.Definition
{
    public class FixDefinitions
    {
        private readonly Dictionary<string, SimpleFieldDefinition> _nameToSimple = new();
        private readonly Dictionary<int, SimpleFieldDefinition> _tagToSimple = new();
        public IReadOnlyDictionary<string, SimpleFieldDefinition> Simple => _nameToSimple;
        public IReadOnlyDictionary<int, SimpleFieldDefinition> TagToSimple => _tagToSimple;
        public void AddSimple(SimpleFieldDefinition simpleField)
        {
            _nameToSimple[simpleField.Name] = simpleField;
            _tagToSimple[simpleField.Tag] = simpleField;
        }
    }
}
