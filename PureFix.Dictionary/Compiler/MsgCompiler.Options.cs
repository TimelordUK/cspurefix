using PureFix.Dictionary.Definition;
using PureFix.Dictionary.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                            BackingTypeOutputPath = Path.Join(DefaultRootOutputPath, "FIX4.4", "quickfix"),
                            BackingTypeNamespace = "PureFix.Types.FIX4._4.quickfix"
                        };

                    case FixVersion.FIX50SP2:
                        return new Options
                        {
                            MsgTypes = definitions.Message.Select(kv => kv.Value.MsgType).Distinct().ToList(),
                            BackingTypeOutputPath = Path.Join(DefaultRootOutputPath, "FIX.50SP2", "quickfix"),
                            BackingTypeNamespace = "PureFix.Types.FIX4._50SP2.quickfix"
                        };

                    default:
                        return new Options
                        {
                            MsgTypes = definitions.Message.Select(kv => kv.Value.MsgType).Distinct().ToList(),
                            BackingTypeOutputPath = Path.Join(DefaultRootOutputPath, "FIX", "quickfix"),
                            BackingTypeNamespace = "PureFix.Types.FIX.quickfix"
                        };
                }
            }
        }
    }
}
