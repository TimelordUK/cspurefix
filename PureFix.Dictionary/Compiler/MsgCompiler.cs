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
    public partial class MsgCompiler : ISetDispatchReceiver
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

        public FixDefinitions Definitions { get; }
        public Options CompilerOptions { get; }
        private readonly Queue<CompilerType> _workQueue = [];
        private readonly Dictionary<string, CompilerType> _completed = [];
        private readonly CodeGenerator _builder = new();

        private ContainedSimpleField? _LastSimpleField;
        private CompilerType? _currentCompilerType;


        public MsgCompiler(FixDefinitions definitions, Options? options = null)
        {
            Definitions = definitions;
            options ??= Options.FromVersion(definitions);
            CompilerOptions = options;
        }

        public void Generate(IReadOnlyList<string>? types = null)
        {
            types ??= CompilerOptions.MsgTypes;
            if (types == null) throw new InvalidDataException("no types defined to create");
            CreateTypes(types);
        }

        public void CreateTypes(IReadOnlyList<string> types)
        {
            foreach (var type in types)
            {
                var set = Definitions.GetMsgOrComponent(type);
                if (set == null)
                {
                    throw new InvalidDataException($"no type {type} defined");
                }

                var ct = new CompilerType(Definitions, CompilerOptions, set, set.Name);
                Enqueue(ct);
            }

            Work();
        }

        private void Work()
        {
            while (_workQueue.Count > 0)
            {
                var compilerType = _workQueue.Dequeue();
                var compiledType = GenerateMessages(compilerType);
                var fullName = GetFileName(compilerType);

                WriteFile(fullName, compiledType);
            }
        }

        private void WriteFile(string filename, string content)
        {
            var directory = Path.GetDirectoryName(filename);
            if (directory is not null) Directory.CreateDirectory(directory);

            File.WriteAllText(filename, content);
        }

        private string MakeTypesNamespace()
        {
            return $"{CompilerOptions.BackingTypeNamespace}.Types";
        }

        public string GenerateMessages(CompilerType compilerType)
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

                using(_builder.BeginBlock($"public sealed class {compilerType.QualifiedName}{inheritsDeclaration}"))
                {
                    compilerType.Set.Iterate(this);
                    if (isMsg)
                    {
                        _builder.WriteLine();
                        _builder.WriteLine("IStandardHeader? IFixMessage.StandardHeader => StandardHeader;");
                        _builder.WriteLine();
                        _builder.WriteLine("IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;");

                        /*
                        foreach (var headerOverride in CompilerOptions.HeaderOverrides)
                        {
                            var sf = Definitions.Simple.GetValueOrDefault(headerOverride);
                            if (sf != null)
                            {
                                var getter = $"=> StandardHeader?.{sf.Name}";
                                _builder.WriteLine($"public override {Tags.ToCsType(sf.TagType)}? {sf.Name} {getter};");
                            }
                        }*/
                    }
                }
            }
            return _builder.ToString();
        }

        private string MakeBase(CompilerType compilerType)
        {
            if(compilerType.Set.Type == ContainedSetType.Msg)
            {
                return $" : IFixMessage";
            }

            return compilerType.QualifiedName switch
            {
                "StandardHeader"    => " : IStandardHeader",
                "StandardTrailer"   => " : IStandardTrailer",
                _                   => " : IFixValidator"
            };
        }

        private void Enqueue(CompilerType ct)
        {
            var fullName = GetFileName(ct);
            if (_completed.ContainsKey(fullName))
            {
                return;
            }
            _workQueue.Enqueue(ct);
            _completed[fullName] = ct;
        }

        private string GetFileName(CompilerType ct)
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
            return; // For now

            _builder.WriteLine();
            using (_builder.BeginBlock("bool IFixValidator.IsValid()"))
            {
                List<string> checks = new();

                foreach (var field in containedSet.Fields)
                {
                    if (field.Required)
                    {
                        var name = field.Name;
                        checks.Add($"{name} is not null");

                        if (field.Type == ContainedFieldType.Component)
                        {
                            checks.Add($"((IFixValidator){name}).IsValid()");
                        }
                        else if (field.Type == ContainedFieldType.Group)
                        {
                            checks.Add($"{name}.All(item => ((IFixValidator)item).IsValid())");
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
