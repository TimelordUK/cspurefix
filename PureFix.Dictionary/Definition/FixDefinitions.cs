using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Dictionary.Contained;
using PureFix.Dictionary.Parser;

namespace PureFix.Dictionary.Definition
{
    public class FixDefinitions : IEnumerable<MessageDefinition>
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
        public FixVersion Version { get; private set; }
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
                name = path[..idx];
            }
            else
            {
                return Message.GetValueOrDefault(name);
            }

            return Message.GetValueOrDefault(name)?.GetSet(path[(idx + 1)..]);
        }

        public void SetVersion(FixVersion version)
        {
            Version = version;
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
            sb.Append($"message.Count = {Message.Count / 2} "); // lookup via "A" or "Logon"
            sb.Append($"simple.Count = {Simple.Count} ");
            sb.Append($"component.Count = {Component.Count} ");
            return sb.ToString();
        }
    }

    public static class FixDefinitionExt
    {
        public static int GetMajor(this FixDefinitions definitions)
        {
            return FixVersionParser.GetMajor(definitions.Version);
        }

        public static int GetMinor(this FixDefinitions definitions)
        {
            return FixVersionParser.GetMinor(definitions.Version);
        }

        public static int GetServicePack(this FixDefinitions definitions)
        {
            return FixVersionParser.GetServicePack(definitions.Version);
        }
    }
}
