using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using PureFix.Dictionary.Contained;
using PureFix.Dictionary.Definition;
using PureFix.Dictionary.Parser;
using PureFix.Tag;

namespace PureFix.Dictionary.Compiler
{
    public class MsgCompiler : ISetDispatchReceiver
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

        /***
         *using PureFix.Types.FIX4._4.quickfix.set;
           using System;
           using System.Collections.Generic;
           using System.Linq;
           using System.Text;
           using System.Threading.Tasks;

           namespace PureFix.Types.FIX4._4.quickfix
           {
               public class Heartbeat
               {
                   public StandardHeader? StandardHeader { get; set; }
                   public string? TestReqID { get; set; }
                   public StandardTrailer? StandardTrailer { get; set; }
               }
           }
         */

        public class CompilerType
        {
            public FixDefinitions Definitions { get; }
            public IContainedSet Set { get; }
            public string Name { get; }

            public CompilerType(FixDefinitions definitions, IContainedSet set, string name)
            {
                Definitions = definitions;
                Set = set;
                Name = name;
            }
        }

        public FixDefinitions Definitions { get; }
        public Options CompilerOptions { get; }
        private readonly Queue<CompilerType> _workQueue = [];
        private readonly Dictionary<string, CompilerType> _completed = [];
        private readonly StringBuilder _builder = new();


        public MsgCompiler(FixDefinitions definitions, Options? options = null)
        {
            Definitions = definitions;
            options ??= Options.FromVersion(definitions);
            CompilerOptions = options;
        }

        public async Task Generate(IReadOnlyList<string>? types = null)
        {
            types ??= CompilerOptions.MsgTypes;
            if (types == null) throw new InvalidDataException("no types defined to create");
            await CreateTypes(types);
        }

        public async Task CreateTypes(IReadOnlyList<string> types)
        {
            foreach (var type in types)
            {
                var set = Definitions.GetMsgOrComponent(type);
                if (set == null)
                {
                    throw new InvalidDataException($"no type {type} defined");
                }

                var ct = new CompilerType(Definitions, set, set.Name);
                Enqueue(ct);
            }

            await Work();
        }

        private async Task Work()
        {
            while (_workQueue.Count > 0)
            {
                var compilerType = _workQueue.Dequeue();
                var compiledType = GenerateMessages(compilerType);
                var fullName = GetFileName(compilerType);
                using var streamReader = File.WriteAllTextAsync(fullName, compiledType);
                await streamReader;
            }
        }

        public string GenerateMessages(CompilerType compilerType)
        {
            var isMsg = compilerType.Set.Type == ContainedSetType.Msg;
            var ns = isMsg
                ? $"{CompilerOptions.BackingTypeNamespace}"
                : $"{CompilerOptions.BackingTypeNamespace}.set";
            _builder.Clear();
            var usingDeclaration = string.Join(Environment.NewLine, CompilerOptions.DefaultUsing.Select(s => $"using {s};").Union([$"using {CompilerOptions.BackingTypeNamespace}.set;"]));
            var inheritsDeclaration = isMsg ? $" : {CompilerOptions.MsgInheritsFrom}" : "";
            _builder.AppendFormat(usingDeclaration);
            _builder.AppendLine();
            _builder.AppendLine($"namespace {ns}");
            _builder.AppendLine("{");
            _builder.AppendLine($"\tpublic class {compilerType.Name}{inheritsDeclaration}");
            _builder.AppendLine("\t{");
            compilerType.Set.Iterate(this, "\t\t");
            _builder.AppendLine("\t}");
            _builder.AppendLine("}");
            return _builder.ToString();
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
            var name = isMsg ? ((MessageDefinition)ct.Set).Name : ct.Set.Name;
            if (isMsg)
            {
                return $"{Path.Join(CompilerOptions.BackingTypeOutputPath, $"{name}")}.cs";
            }
            return $"{Path.Join(CompilerOptions.BackingTypeOutputPath, "set", $"{name}")}.cs";
        }

        // public string? TestReqID { get; set; }
        public void OnSimple(ContainedSimpleField sf, object? state = null)
        {
            var type = Tags.ToCsType(sf.Definition.TagType);
            var indent = state?.ToString() ?? "\t";
            const string props = "{ get; set; }";
            _builder.AppendLine($"{indent}public {type}? {sf.Definition.Name} {props}");
        }

        public void OnComponent(ContainedComponentField cf, object? state = null)
        {
            if (cf.Definition == null) return;
            var indent = state?.ToString() ?? "\t";
            const string props = "{ get; set; }";
            var declared = cf.Name is "StandardHeader" or "StandardTrailer" ? $"override {cf.Name}" : cf.Name;
            _builder.AppendLine($"{indent}public {declared}? {cf.Name} {props}");
            // any dependent component also needs to be constructed StandardHeader etc.
            Enqueue(new CompilerType(Definitions, cf.Definition, cf.Name));
        }

        public void OnGroup(ContainedGroupField cf, object? state = null)
        {
        }
    }
}
