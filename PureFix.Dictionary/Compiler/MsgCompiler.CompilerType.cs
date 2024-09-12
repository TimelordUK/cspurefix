using PureFix.Dictionary.Contained;
using PureFix.Dictionary.Definition;

namespace PureFix.Dictionary.Compiler
{
    public partial class MsgCompiler
    {
        public class CompilerType
        {
            public FixDefinitions Definitions { get; }
            public IContainedSet Set { get; }
            public string Name { get; }
            public string Declaration => Name 
                is "StandardHeader" 
                or "StandardTrailer" ? 
                $"override {Name}" : 
                Name;

            public CompilerType(FixDefinitions definitions, IContainedSet set, string name)
            {
                Definitions = definitions;
                Set = set;
                Name = name;
            }
        }
    }
}
