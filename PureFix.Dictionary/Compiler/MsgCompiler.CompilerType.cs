using PureFix.Dictionary.Contained;
using PureFix.Dictionary.Definition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Dictionary.Parser;

namespace PureFix.Dictionary.Compiler
{
    public partial class MsgCompiler
    {
        public class CompilerType
        {
            public FixDefinitions Definitions { get; }
            public Options CompilerOptions { get; }
            public IContainedSet Set { get; }
            public string Name { get; }
            public string Indent { get; set; } = "\t";
            
            public bool IsMsg => Set.Type == ContainedSetType.Msg;
            public string BackingTypeNamespace => IsMsg
                ? $"{CompilerOptions.BackingTypeNamespace}"
            : $"{CompilerOptions.BackingTypeNamespace}.set";

            public CompilerType(FixDefinitions definitions, Options options, IContainedSet set, string name)
            {
                Definitions = definitions;
                Set = set;
                Name = name;
                CompilerOptions = options;
            }

            public override string ToString()
            {
                return $"{Name} {Set}";
            }
        }
    }
}
