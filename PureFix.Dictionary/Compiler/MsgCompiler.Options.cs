using PureFix.Dictionary.Definition;
using PureFix.Dictionary.Parser;
using PureFix.Types;

namespace PureFix.Dictionary.Compiler
{
    public partial class MsgCompiler
    {
        public class Options
        {
            public string? BackingTypeOutputPath { get; set; }
            public string? BackingTypeNamespace { get; set; }
            public string? MsgInheritsFrom { get; set; } = "FixMsg";
            public IReadOnlyList<string>? MsgTypes { get; set; }

            public IReadOnlyList<string> DefaultUsing =
                ["System", "System.Collections.Generic", "System.Linq", "System.Text", "System.Threading.Tasks"];

            public static string DefaultRootOutputPath { get; set; } = Path.Join(Directory.GetCurrentDirectory(), "..",
                "..",
                "..", "..", "PureFix.Types");


            public static Options FromVersion(FixDefinitions definitions)
            {
                switch (definitions.Version)
                {
                    case FixVersion.FIX44:
                        return new Options
                        {
                            MsgTypes = definitions.Message.Select(kv => kv.Value.MsgType).Distinct().ToList(),
                            BackingTypeOutputPath = Path.Join(DefaultRootOutputPath, "FIX4.4", "QuickFix"),
                            BackingTypeNamespace = "PureFix.Types.FIX44.QuickFix"
                        };

                    case FixVersion.FIX50SP2:
                        return new Options
                        {
                            MsgTypes = definitions.Message.Select(kv => kv.Value.MsgType).Distinct().ToList(),
                            BackingTypeOutputPath = Path.Join(DefaultRootOutputPath, "FIX.50SP2", "QuickFix"),
                            BackingTypeNamespace = "PureFix.Types.FIX450SP2.QuickFix"
                        };

                    default:
                        return new Options
                        {
                            MsgTypes = definitions.Message.Select(kv => kv.Value.MsgType).Distinct().ToList(),
                            BackingTypeOutputPath = Path.Join(DefaultRootOutputPath, "FIX", "QuickFix"),
                            BackingTypeNamespace = "PureFix.Types.FIX.QuickFix"
                        };
                }
            }
        }
    }
}
