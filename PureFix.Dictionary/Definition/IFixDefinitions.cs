using PureFix.Dictionary.Contained;
using PureFix.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Dictionary.Definition
{
    public interface IFixDefinitions
    {
        IReadOnlyDictionary<string, MessageDefinition> Message { get; }
        void SetVersion(FixVersion version, FixDefinitionSource source = FixDefinitionSource.QuickFix);
        FixVersion Version { get; }
        void AddSimple(SimpleFieldDefinition simpleField);
        void AddMessage(MessageDefinition msg);
        void AddComponent(ComponentFieldDefinition component);
        IContainedSet? GetMsgOrComponent(string type);
        IContainedSet? this[string name] { get; }
        IContainedSet? GetSet(string path);
        SimpleFieldDefinition? this[int tag] { get; }
        IReadOnlyDictionary<string, SimpleFieldDefinition> Simple { get; }
        IReadOnlyDictionary<string, ComponentFieldDefinition> Component { get; }
        IReadOnlyDictionary<int, SimpleFieldDefinition> TagToSimple { get; }
    }
}
