using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
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

        public FixDefinitions Definitions { get; }
        public Options CompilerOptions { get; }
        private readonly Queue<CompilerType> _workQueue = [];
        private readonly Dictionary<string, CompilerType> _completed = [];
        private readonly CodeGenerator _builder = new();


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

                var ct = new CompilerType(Definitions, CompilerOptions, set, set.Name);
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

                var directory = Path.GetDirectoryName(fullName);
                if (directory is not null) Directory.CreateDirectory(directory);

                await File.WriteAllTextAsync(fullName, compiledType);
            }
        }

        public string GenerateMessages(CompilerType compilerType)
        {
            var isMsg = compilerType.Set.Type == ContainedSetType.Msg;
            var ns = isMsg
                ? $"{CompilerOptions.BackingTypeNamespace}"
                : $"{CompilerOptions.BackingTypeNamespace}.Types";
            _builder.Reset();

            var usingDeclaration = string.Join(Environment.NewLine, CompilerOptions.DefaultUsing.Select(s => $"using {s};").Union([$"using {CompilerOptions.BackingTypeNamespace}.Types;"]));
            var inheritsDeclaration = isMsg ? $" : {CompilerOptions.MsgInheritsFrom}" : "";
            
            _builder.WriteLine(usingDeclaration);
            _builder.WriteLine();

            using(_builder.BeginBlock($"namespace {ns}"))
            {
                if (compilerType.Set is MessageDefinition messageDefinition)
                {
                    _builder.WriteLine($"[MessageType(\"{messageDefinition.MsgType}\", FixVersion.{Definitions.Version})]");
                }

                using(_builder.BeginBlock($"public sealed class {compilerType.Name}{inheritsDeclaration}"))
                {
                    compilerType.Set.Iterate(this, "\t\t");
                }
            }
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
            return $"{Path.Join(CompilerOptions.BackingTypeOutputPath, "Types", $"{name}")}.cs";
        }

        // public string? TestReqID { get; set; }
        public void OnSimple(ContainedSimpleField sf, int index)
        {
            var type = Tags.ToCsType(sf.Definition.TagType);
            const string props = "{ get; set; }";

            _builder.WriteLine($"[TagDetails(Tag = {sf.Definition.Tag}, Type = TagType.{TagTypeUtil.ToType(sf.Definition.Type)}, Offset = {index})]");
            _builder.WriteLine($"public {type}? {sf.Definition.Name} {props}");
            _builder.WriteLine();
        }

        public void OnComponent(ContainedComponentField cf,  int index)
        {
            if (cf.Definition == null) return;
            const string props = "{ get; set; }";
            var declared = cf.Name is "StandardHeader" or "StandardTrailer" ? $"override {cf.Name}" : cf.Name;
            
            _builder.WriteLine($"[Component(Offset = {index})]");
            _builder.WriteLine($"public {declared}? {cf.Name} {props}");

            if(cf.Name != "StandardTrailer")
            {
                _builder.WriteLine();
            }

            // any dependent component also needs to be constructed StandardHeader etc.
            Enqueue(new CompilerType(Definitions, CompilerOptions, cf.Definition, cf.Name));
        }

        public void OnGroup(ContainedGroupField gf, int index)
        {
            if (gf.Definition == null) return;
            const string props = "{ get; set; }";

            var countField = gf.Definition.NoOfField!.Tag;

            _builder.WriteLine($"[Group(NoOfTag = {countField}, Offset = {index})]");
            _builder.WriteLine($"public {gf.Name}[]? {gf.Name} {props}");
            _builder.WriteLine();
            // any dependent group also needs to be constructed StandardHeader etc.
            Enqueue(new CompilerType(Definitions, CompilerOptions, gf.Definition, gf.Name));
        }
    }
}
