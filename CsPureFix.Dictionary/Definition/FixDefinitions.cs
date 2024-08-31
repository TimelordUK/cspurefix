using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Dictionary.Definition
{
    public class FixDefinitions
    {
        /**
         * all simple fields defined from source definition indexed via name
         * e.g. 'BeginString'
        */
        private readonly Dictionary<string, SimpleFieldDefinition> _nameToSimple = new();
        private readonly Dictionary<string, MessageDefinition> _message = new();
        private readonly Dictionary<int, SimpleFieldDefinition> _tagToSimple = new();

        public IReadOnlyDictionary<string, SimpleFieldDefinition> Simple => _nameToSimple;
        public IReadOnlyDictionary<int, SimpleFieldDefinition> TagToSimple => _tagToSimple;
        /**
         * all messages defined from source definition indexed via name
         * e.g. 'Logon'
        */
        public IReadOnlyDictionary<string, MessageDefinition> Message => _message;

        public void AddSimple(SimpleFieldDefinition simpleField)
        {
            _nameToSimple[simpleField.Name] = simpleField;
            _tagToSimple[simpleField.Tag] = simpleField;
        }

        public void AddMessaqe(MessageDefinition msg)
        {
            _message[msg.MsgType] = msg;
        }
    }
}
