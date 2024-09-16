using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using PureFix.Dictionary.Contained;
using PureFix.Dictionary.Definition;
using PureFix.Dictionary.Parser;
using PureFix.Types;


namespace PureFix.Dictionary.Compiler
{
    public partial class MsgCompiler : BaseCompiler, ISetDispatchReceiver
    {
        /***
         * using PureFix.Types.FIX44.QuickFix.Types;
           using System;
           using System.Collections.Generic;
           using System.Linq;
           using System.Text;
           using System.Threading.Tasks;

           namespace PureFix.Types.FIX44.QuickFix
           {
               public class Heartbeat
               {
                   public StandardHeader? StandardHeader { get; set; }
                   public string? TestReqID { get; set; }
                   public StandardTrailer? StandardTrailer { get; set; }
               }
           }
         */

        private const string GetSet = "{ get; set; }";
        private ContainedSimpleField? _LastSimpleField;


        public MsgCompiler(FixDefinitions definitions, Options? options = null) : base(definitions, options)
        {
        }

        private string MakeTypesNamespace()
        {
            return $"{CompilerOptions.BackingTypeNamespace}.Types";
        }

        protected override string GenerateTypes(CompilerType compilerType)
        {
            var isMsg = compilerType.Set.Type == ContainedSetType.Msg;
            var ns = isMsg
                ? $"{CompilerOptions.BackingTypeNamespace}"
                : MakeTypesNamespace();

            _builder.Reset();
            _currentCompilerType = compilerType;

            var usingDeclaration = string.Join
            (
                Environment.NewLine, 
                CompilerOptions.DefaultUsing.Select(s => $"using {s};").Union([$"using {MakeTypesNamespace()};"])
            );

            var inheritsDeclaration = MakeBase(compilerType);
            
            _builder.WriteLine(usingDeclaration);
            _builder.WriteLine();

            using(_builder.BeginBlock($"namespace {ns}"))
            {
                if (compilerType.Set is MessageDefinition messageDefinition)
                {
                    _builder.WriteLine($"[MessageType(\"{messageDefinition.MsgType}\", FixVersion.{Definitions.Version})]");
                }

                using(_builder.BeginBlock($"public sealed partial class {compilerType.QualifiedName}{inheritsDeclaration}"))
                {
                    compilerType.Set.Iterate(this);
                    if (isMsg)
                    {
                        _builder.WriteLine();
                        _builder.WriteLine("IStandardHeader? IFixMessage.StandardHeader => StandardHeader;");
                        _builder.WriteLine();
                        _builder.WriteLine("IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;");
                    }
                }
            }
            return _builder.ToString();
        }

        private string MakeBase(CompilerType compilerType)
        {
            if(compilerType.IsMsg)
            {
                return " : IFixMessage";
            }

            return compilerType.QualifiedName switch
            {
                "StandardHeader"    => " : IStandardHeader, IFixValidator, IFixEncoder",
                "StandardTrailer"   => " : IStandardTrailer, IFixValidator, IFixEncoder",
                _                   => " : IFixValidator, IFixEncoder"
            };
        }

        protected override string GetFileName(CompilerType ct)
        {
            var isMsg = ct.Set.Type == ContainedSetType.Msg;
            var name = isMsg ? ((MessageDefinition)ct.Set).Name : ct.QualifiedName;
            if (isMsg)
            {
                return $"{Path.Join(CompilerOptions.BackingTypeOutputPath, $"{name}")}.cs";
            }

            return MakesTypesFilename(name);
        }

        private string MakesTypesFilename(string typename)
        {
            return $"{Path.Join(CompilerOptions.BackingTypeOutputPath, "Types", $"{typename}")}.cs";
        }

        // public string? TestReqID { get; set; }
        public void OnSimple(ContainedSimpleField sf, int index, object? peek)
        {
            var type = TagManager.ToCsType(sf.Definition.TagType);

            if(sf.Definition.TagType == TagType.Length && peek is ContainedSimpleField peekField && peekField.Definition.TagType == TagType.RawData)
            {
                _builder.WriteLine($"[TagDetails(Tag = {sf.Definition.Tag}, Type = TagType.{TagManager.ToType(sf.Definition.Type)}, Offset = {index}, Required = {MapRequired(sf.Required)}, LinksToTag = {peekField.Definition.Tag})]");
            }
            else if (sf.Definition.TagType == TagType.RawData && _LastSimpleField is not null)
            {
                _builder.WriteLine($"[TagDetails(Tag = {sf.Definition.Tag}, Type = TagType.{TagManager.ToType(sf.Definition.Type)}, Offset = {index}, Required = {MapRequired(sf.Required)}, LinksToTag = {_LastSimpleField.Definition.Tag})]");
            }
            else
            {
                _builder.WriteLine($"[TagDetails(Tag = {sf.Definition.Tag}, Type = TagType.{TagManager.ToType(sf.Definition.Type)}, Offset = {index}, Required = {MapRequired(sf.Required)})]");
            }

            _LastSimpleField = sf;
            var name = sf.Definition.Name;
            _builder.WriteLine($"public {type}? {name} {GetSet}");
            _builder.WriteLine();

            if (sf.Definition.Enums is not null)
            {
                GenerateEnumValues(type, sf.Definition.TagType, name, sf.Definition.Enums);
            }
        }

        public void OnComponent(ContainedComponentField cf,  int index)
        {
            if (cf.Definition == null) return;
            var extended = _currentCompilerType?.GetExtended(cf) ?? cf.Name;
            
            
            _builder.WriteLine($"[Component(Offset = {index}, Required = {MapRequired(cf.Required)})]");
            _builder.WriteLine($"public {cf.Name}? {cf.Name} {GetSet}");

            if(cf.Name != "StandardTrailer")
            {
                _builder.WriteLine();
            }

            // any dependent component also needs to be constructed StandardHeader etc.
            Enqueue(new CompilerType(Definitions, CompilerOptions, cf.Definition, extended));
        }

        public void OnGroup(ContainedGroupField gf, int index)
        {
            if (gf.Definition == null) return;

            var countField = gf.Definition.NoOfField!.Tag;
            var extended = _currentCompilerType?.GetExtended(gf) ?? gf.Name;
            var name = _currentCompilerType?.GetFieldGroupName(gf) ?? gf.Name;

            _builder.WriteLine($"[Group(NoOfTag = {countField}, Offset = {index}, Required = {MapRequired(gf.Required)})]");
            _builder.WriteLine($"public {extended}[]? {name} {GetSet}");
            _builder.WriteLine();
            // any dependent group also needs to be constructed StandardHeader etc.
            Enqueue(new CompilerType(Definitions, CompilerOptions, gf.Definition, extended));
        }

        public void PostIterate(IContainedSet containedSet)
        {
            GenerateIsValid(containedSet);
            GenerateEncode(containedSet);
        }

        public void GenerateEncode(IContainedSet containedSet)
        {
            _builder.WriteLine();

            using (_builder.BeginBlock("void IFixEncoder.Encode(IFixWriter writer)"))
            {
                for(var i = 0; i < containedSet.Fields.Count; i++)
                {
                    switch (containedSet.Fields[i])
                    {
                        case ContainedSimpleField sf:
                        {
                            var name = sf.Name;
                            var metaData = TagManager.GetTagMetaData(sf.Definition.TagType);

                            if (metaData.TagType == TagType.Length && containedSet.Fields[i + 1] is ContainedSimpleField rawField && rawField.Definition.TagType == TagType.RawData)
                            {
                                // It's (length, raw-data) pair
                                var rawFieldName = rawField.Name;
                                using(_builder.BeginBlock($"if ({rawFieldName} is not null)"))
                                {
                                    _builder.WriteLine($"writer.WriteWholeNumber({sf.Definition.Tag}, {rawFieldName}.Length);");

                                    var rawMetaData = TagManager.GetTagMetaData(rawField.Definition.TagType);
                                    _builder.WriteLine($"writer.{rawMetaData.Writer}({rawField.Definition.Tag}, {rawFieldName});");
                                }

                                // We want to skip the actual raw field processing as we've just done it
                                i++;
                            }
                            else
                            {
                                var valueTypeExtract = "";

                                if (metaData.Type.IsValueType)
                                {
                                    valueTypeExtract = ".Value";
                                }
                                
                                _builder.WriteLine($"if ({name} is not null) writer.{metaData.Writer}({sf.Definition.Tag}, {name}{valueTypeExtract});");
                            }
                        }
                        break;

                        case ContainedGroupField gf:
                        {
                            //var metaData = TagManager.GetTagMetaData(gf.Definition.TagType);
                            var countField = gf.Definition!.NoOfField!.Tag;
                            var name = _currentCompilerType?.GetFieldGroupName(gf) ?? gf.Name;
                            
                            using(_builder.BeginBlock($"if ({name} is not null && {name}.Length != 0)"))
                            {
                                _builder.WriteLine($"writer.WriteWholeNumber({countField}, {name}.Length);");
                                using(_builder.BeginBlock($"for (int i = 0; i < {name}.Length; i++)"))
                                {
                                    _builder.WriteLine($"((IFixEncoder){name}[i]).Encode(writer);");
                                }
                            }
                        }
                        break;

                        case ContainedComponentField cf:
                        {
                            var name = cf.Definition!.Name;
                            _builder.WriteLine($"if ({name} is not null) ((IFixEncoder){name}).Encode(writer);");
                        }
                        break;
                    }
                }
            }
        }

        public void GenerateIsValid(IContainedSet containedSet)
        {
            _builder.WriteLine();
            using (_builder.BeginBlock("bool IFixValidator.IsValid(in FixValidatorConfig config)"))
            {
                List<string> checks = new();

                foreach (var field in containedSet.Fields)
                {
                    if (field.Required)
                    {
                        var name = field.Name;

                        if (name == "StandardHeader")
                        {
                            checks.Add($"(!config.CheckStandardHeader || ({name} is not null && ((IFixValidator){name}).IsValid(in config)))");
                        }
                        else if (name == "StandardTrailer")
                        {
                            checks.Add($"(!config.CheckStandardTrailer || ({name} is not null && ((IFixValidator){name}).IsValid(in config)))");
                        }
                        else
                        {
                            if (field.Type == ContainedFieldType.Component)
                            {
                                checks.Add($"{name} is not null && ((IFixValidator){name}).IsValid(in config)");
                            }
                            else if (field.Type == ContainedFieldType.Group)
                            {
                                checks.Add($"{name} is not null && FixValidator.IsValid({name}, in config)");
                            }
                            else
                            {
                                checks.Add($"{name} is not null");
                            }
                        }
                    }
                }

                if (checks.Count == 0)
                {
                    _builder.WriteLine("return true;");
                }
                else
                {
                    _builder.WriteLine("return");
                    using (_builder.BeginIndent())
                    {
                        for (int i = 0; i < checks.Count; i++)
                        {
                            var check = checks[i];

                            var line = (i == 0) ? check : $"&& {check}";

                            if (i == checks.Count - 1)
                            {
                                _builder.WriteLine($"{line};");
                            }
                            else
                            {
                                _builder.WriteLine(line);
                            }
                        }
                    }
                }
            }
        }

        private void GenerateEnumValues(string csharpBaseType, TagType tagType, string fieldName, IReadOnlyDictionary<string, FieldEnum> enums)
        {
            var builder = new CodeGenerator();

            var enumName = $"{fieldName}Values";

            using(builder.BeginBlock($"namespace {MakeTypesNamespace()}"))
            using(builder.BeginBlock($"public static class {enumName}"))
            {
                foreach(var field in enums.Values)
                {
                    var value = tagType switch
                    {
                        TagType.String  => $"\"{field.Key}\"",
                        TagType.Boolean => (field.Key == "Y" ? "true" : "false"),
                        _               => field.Key.ToString()
                    };

                    var constantName = field.Description.UnderscoreToCamelCase();

                    // Some descriptions start with a number, which won't map to a valid C# symbol
                    if(char.IsDigit(constantName[0])) constantName = "_" + constantName;

                    builder.WriteLine($"public const {csharpBaseType} {constantName} = {value};");
                }
            }

            var code = builder.ToString();
            var filename = MakesTypesFilename(enumName);
            WriteFile(filename, code);
        }

        private string MapRequired(bool value)
        {
            return value ? "true" : "false";
        }
    }
}
