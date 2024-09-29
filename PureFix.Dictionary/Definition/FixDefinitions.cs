using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using PureFix.Dictionary.Contained;
using PureFix.Types;

namespace PureFix.Dictionary.Definition
{
    public class FixDefinitions : IEnumerable<MessageDefinition>, IFixDefinitions
    {
        /**
         * all simple fields defined from source definition indexed via name
         * e.g. 'BeginString'
        */
        private readonly Dictionary<string, SimpleFieldDefinition> _nameToSimple = [];

        private readonly Dictionary<string, MessageDefinition> _message = [];
        private readonly Dictionary<int, SimpleFieldDefinition> _tagToSimple = [];
        private readonly Dictionary<string, ComponentFieldDefinition> _component = [];

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
         * e.g. "Logon" or msgType "A"
        */
        public IReadOnlyDictionary<string, MessageDefinition> Message => _message;

        public FixVersion Version { get; private set; }
        public FixDefinitionSource Source { get; private set; } = FixDefinitionSource.QuickFix;

        public void AddSimple(SimpleFieldDefinition simpleField)
        {
            _nameToSimple[simpleField.Name] = simpleField;
            _tagToSimple[simpleField.Tag] = simpleField;
        }

        public void AddMessage(MessageDefinition msg)
        {
            _message[msg.Name] = msg;
            if (msg.MsgType != msg.Name)
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

        /*
         * a top level definition for either a message or globally defined component
         */
        public IContainedSet? GetMsgOrComponent(string type)
        {
            return Message.GetValueOrDefault(type) as IContainedSet ?? Component.GetValueOrDefault(type);
        }

        public IContainedSet? this[string name] => GetMsgOrComponent(name);
        public SimpleFieldDefinition? this[int tag] => TagToSimple.GetValueOrDefault(tag);

        /*
         * this path must begin with the message name and using dot notation locates
         * a nested group or component held within the message.
         * e.g. GetSet("SecurityList.SecListGrp")
         */
        public IContainedSet? GetSet(string path)
        {
            var idx = path.IndexOf('.', StringComparison.Ordinal);
            var name = path;
            if (idx > 0)
            {
                name = path[..idx];
            }
            else
            {
                return Message.GetValueOrDefault(name);
            }

            return Message.GetValueOrDefault(name)?.GetSet(path[(idx + 1)..]);
        }

        public void SetVersion(FixVersion version, FixDefinitionSource source = FixDefinitionSource.QuickFix)
        {
            Version = version;
            Source = source;
        }

        public IEnumerator<MessageDefinition> GetEnumerator()
        {
           return Message.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
           return GetEnumerator();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append($"message.Count = {Message.Count / 2} types = {string.Join(", ", Message.Keys)}"); // lookup via "A" or "Logon"
            sb.Append($"simple.Count = {Simple.Count} ");
            sb.Append($"component.Count = {Component.Count} ");
            return sb.ToString();
        }
    }
}
