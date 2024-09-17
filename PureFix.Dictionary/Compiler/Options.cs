using PureFix.Dictionary.Definition;
using PureFix.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Dictionary.Compiler
{
    public sealed class Options
    {
            public string? BackingTypeOutputPath { get; set; }
            public string? ParserFormatterOutputPath { get; set; }
            public string? BackingTypeNamespace { get; set; }
            public string? ParserFormatterNamespace { get; set; }
            public string? MsgInheritsFrom { get; set; } = "FixMsg";
            public IReadOnlyList<string>? MsgTypes { get; set; }

            public IReadOnlyList<string> DefaultUsing =
            [
                "System", 
                "System.Collections.Generic", 
                "System.Linq", 
                "System.Text", 
                "System.Threading.Tasks"
            ];

            public HashSet<string> HeaderOverrides = ["MsgType","BodyLength"];

            public static string DefaultRootOutputPath { get; set; } = Path.Join(Directory.GetCurrentDirectory(), "..", "..", "..", "..");
            

            public static Options FromVersion(FixDefinitions definitions)
            {
                switch (definitions.Version)
                {
                    case FixVersion.FIX42:
                        return new Options
                        {
                            MsgTypes = definitions.Message.Select(kv => kv.Value.MsgType).Distinct().ToList(),
                            BackingTypeOutputPath = Path.Join(DefaultRootOutputPath, "PureFix.Types", "FIX4.2", "QuickFix"),
                            ParserFormatterOutputPath = Path.Join(DefaultRootOutputPath, "PureFix.ParserFormat", "FIX4.2", "QuickFix"),
                            BackingTypeNamespace = "PureFix.Types.FIX42.QuickFix",
                            ParserFormatterNamespace = "PureFix.ParserFormat.FIX42.QuickFix"
                        };

                    case FixVersion.FIX43:
                        return new Options
                        {
                            MsgTypes = definitions.Message.Select(kv => kv.Value.MsgType).Distinct().ToList(),
                            BackingTypeOutputPath = Path.Join(DefaultRootOutputPath, "PureFix.Types", "FIX4.3", "QuickFix"),
                            ParserFormatterOutputPath = Path.Join(DefaultRootOutputPath, "PureFix.ParserFormat", "FIX4.3", "QuickFix"),
                            BackingTypeNamespace = "PureFix.Types.FIX43.QuickFix",
                            ParserFormatterNamespace = "PureFix.ParserFormat.FIX43.QuickFix"
                        };

                    case FixVersion.FIX44:
                        return new Options
                        {
                            MsgTypes = definitions.Message.Select(kv => kv.Value.MsgType).Distinct().ToList(),
                            BackingTypeOutputPath = Path.Join(DefaultRootOutputPath, "PureFix.Types", "FIX4.4", "QuickFix"),
                            ParserFormatterOutputPath = Path.Join(DefaultRootOutputPath, "PureFix.ParserFormat", "FIX4.4", "QuickFix"),
                            BackingTypeNamespace = "PureFix.Types.FIX44.QuickFix",
                            ParserFormatterNamespace = "PureFix.ParserFormat.FIX44.QuickFix"
                        };

                    case FixVersion.FIX50SP2:
                        return new Options
                        {
                            MsgTypes = definitions.Message.Select(kv => kv.Value.MsgType).Distinct().ToList(),
                            BackingTypeOutputPath = Path.Join(DefaultRootOutputPath, "PureFix.Types", "FIX5.0.SP2", "QuickFix"),
                            ParserFormatterOutputPath = Path.Join(DefaultRootOutputPath, "PureFix.ParserFormat", "FIX5.0.SP2", "QuickFix"),
                            BackingTypeNamespace = "PureFix.Types.FIX50SP2.QuickFix",
                            ParserFormatterNamespace = "PureFix.ParserFormat.FIX50SP2.QuickFix"
                        };

                    case FixVersion.FIXT11:
                        return new Options
                        {
                            MsgTypes = definitions.Message.Select(kv => kv.Value.MsgType).Distinct().ToList(),
                            BackingTypeOutputPath = Path.Join(DefaultRootOutputPath, "PureFix.Types", "FIT11", "QuickFix"),
                            ParserFormatterOutputPath = Path.Join(DefaultRootOutputPath, "PureFix.ParserFormat", "FIXT11", "QuickFix"),
                            BackingTypeNamespace = "PureFix.Types.FIXT11.QuickFix",
                            ParserFormatterNamespace = "PureFix.ParserFormat.FIXT11.QuickFix"
                        };


                    default:
                        return new Options
                        {
                            MsgTypes = definitions.Message.Select(kv => kv.Value.MsgType).Distinct().ToList(),
                            BackingTypeOutputPath = Path.Join(DefaultRootOutputPath, "PureFix.Types", "FIX", "QuickFix"),
                            ParserFormatterOutputPath = Path.Join(DefaultRootOutputPath, "PureFix.ParserFormat", "FIX", "QuickFix"),
                            ParserFormatterNamespace = "PureFix.ParserFormat.FIX.QuickFix",
                            BackingTypeNamespace = "PureFix.Types.FIX.QuickFix"
                        };
                }
            }
        }
}
