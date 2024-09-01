using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Dictionary.Contained;

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
        private readonly Dictionary<string, ComponentFieldDefinition> _component = new();

        public IReadOnlyDictionary<string, SimpleFieldDefinition> Simple => _nameToSimple;
        /**
         * all global scope components - top level.
         */
        public IReadOnlyDictionary<string, ComponentFieldDefinition> Component => _component;
        /**
         * numeric tag lookup to field definition.
         */
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
            _message[msg.Name] = msg;
            if (msg.MsgType != null && msg.MsgType != msg.Name)
            {
                _message[msg.MsgType] = msg;
            }

            if (msg.Abbreviation == null || msg.Abbreviation == msg.Name) return;
            _message[msg.Abbreviation] = msg;
        }

        public void AddComponent(ComponentFieldDefinition component)
        {
            _component[component.Name] = component;
        }

        public IContainedSet? GetSet(string path)
        {
            var idx = path.IndexOf('.', StringComparison.Ordinal);
            var name = path;
            if (idx > 0)
            {
                name = path.Substring(0, idx);
            }
            else
            {
                return Message.GetValueOrDefault(name);
            }

            return Message.GetValueOrDefault(name)?.GetSet(path[(idx + 1)..]);
        }
    }
}
