using PureFix.Dictionary.Contained;
using PureFix.Dictionary.Definition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Dictionary.Parser;
using PureFix.Types;

namespace PureFix.Dictionary.Compiler
{
    public partial class MsgCompiler
    {
        public class CompilerType
        {
            public FixDefinitions Definitions { get; }
            public Options CompilerOptions { get; }
            public IContainedSet Set { get; }
            public string QualifiedName { get; }
            public string Declaration => QualifiedName
                is "StandardHeader" 
                or "StandardTrailer" ? 
                $"override {QualifiedName}" :
                QualifiedName;
            
            public CompilerType(FixDefinitions definitions, Options options, IContainedSet set, string qualifiedName)
            {
                Definitions = definitions;
                Set = set;
                QualifiedName = qualifiedName;
                CompilerOptions = options;
            }

            public override string ToString()
            {
                return $"{QualifiedName} {Set}";
            }

            public string GetExtended(ContainedField field) {
                switch (field.Type) {
                    case ContainedFieldType.Group:
                    {
                        var gf = (ContainedGroupField)field;
                        switch (Definitions.Source) {
                            case FixDefinitionSource.QuickFix:
                            {
                                return QualifiedName + field.Name;
                            }

                            case FixDefinitionSource.FixmlRepo:
                            {
                                return gf?.Definition?.Name ?? string.Empty;
                            }
                            default:
                            {
                                return field.Name;
                            }
                        }
                    }

                    case ContainedFieldType.Component:
                    {
                        var cf = (ContainedComponentField)field;
                        switch (Definitions.Source)
                        {
                            case FixDefinitionSource.FixmlRepo:
                            {
                                return cf.Definition?.Name ?? string.Empty;
                            }
                            default:
                            {
                                return field.Name;
                            }
                        }
                    }

                    default:
                        return field.Name;
                }
            }

            public string GetFieldGroupName(ContainedField field) {
                switch (field.Type) {
                    case ContainedFieldType.Group:
                    {
                        var gf = (ContainedGroupField)field; 
                        switch (Definitions.Source) {
                            case FixDefinitionSource.FixmlRepo:
                            {
                                return gf.Definition?.Name ?? string.Empty;
                            }
                            default:
                            {
                                return field.Name;
                            }
                        }
                    }
                    default:
                        return field.Name;
                }
            }
}
    }
}
