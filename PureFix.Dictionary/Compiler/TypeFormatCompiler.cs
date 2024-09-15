using PureFix.Dictionary.Contained;
using PureFix.Dictionary.Definition;
using PureFix.Dictionary.Parser;
using PureFix.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static PureFix.Dictionary.Compiler.MsgCompiler;

namespace PureFix.Dictionary.Compiler
{
    public class TypeFormatCompiler : BaseParserCompiler, ISetDispatchReceiver
    {
        public TypeFormatCompiler(FixDefinitions definitions, Options? options = null) : base(definitions, options)
        {
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
                CompilerOptions.DefaultUsing.Select(s => $"using {s};")
                    .Union([$"using {MakeTypesNamespace()};"])
            );

            _builder.WriteLine(usingDeclaration);
            _builder.WriteLine();

            using (_builder.BeginBlock($"namespace {ns}"))
            {
                using (_builder.BeginBlock($"public sealed partial class {compilerType.QualifiedName}"))
                using (_builder.BeginBlock(
                           "void IFixEncoder.Encode(ElasticBuffer storage, Tags tags, byte delimiter)"))
                {
                    _builder.WriteLine();
                    compilerType.Set.Iterate(this);
                }
            }

            return _builder.ToString();
        }

        private string MakeTypesNamespace()
        {
            return $"{CompilerOptions.BackingTypeNamespace}.Types";
        }

        protected override string GetFileName(CompilerType ct)
        {
            var isMsg = ct.Set.Type == ContainedSetType.Msg;
            var name = isMsg ? ((MessageDefinition)ct.Set).Name : ct.QualifiedName;
            if (isMsg)
            {
                return $"{Path.Join(CompilerOptions.BackingTypeOutputPath, $"{name}")}.Format.cs";
            }

            return MakesTypesFilename(name);
        }

        private string MakesTypesFilename(string typename)
        {
            return $"{Path.Join(CompilerOptions.BackingTypeOutputPath, "Types", $"{typename}")}.Format.cs";
        }

        /*
         * if (ClOrdID != null)
           {
               var at = storage.Pos;
               storage.WriteWholeNumber(11);
               storage.WriteChar((byte)'=');
               storage.WriteString(ClOrdID);
               storage.WriteChar(delimiter);
               tags.Store(at, storage.Pos - at, 11);
           }
         */

        public void OnSimple(ContainedSimpleField sf, int index, object? peek)
        {
            var metaData = TagManager.GetTagMetaData(sf.Definition.TagType);
            _builder.WriteLine($"if ({sf.Definition.Name} != null)");
            using (_builder.BeginBlock())
            {
                _builder.WriteLine("var at = storage.Pos;");
                _builder.WriteLine($"storage.WriteWholeNumber({sf.Definition.Tag});");
                _builder.WriteLine("storage.WriteChar((byte)'=');");
                _builder.WriteLine($"storage.{metaData.Writer}(({metaData.TypeName}){sf.Definition.Name});");
                _builder.WriteLine("storage.WriteChar(delimiter);");
                _builder.WriteLine($"tags.Store(at, storage.Pos - at, {sf.Definition.Tag});");
            }
        }

        // assume that the component has its own parser so just give it the view contained within parent view.
        public void OnComponent(ContainedComponentField cf, int index)
        {
            if (cf.Definition == null) return;
            var extended = _currentCompilerType?.GetExtended(cf) ?? cf.Name;

            Enqueue(new CompilerType(Definitions, CompilerOptions, cf.Definition, extended));
        }

        public void OnGroup(ContainedGroupField gf, int index)
        {
            if (gf.Definition == null) return;
            var extended = _currentCompilerType?.GetExtended(gf) ?? gf.Name;
            Enqueue(new CompilerType(Definitions, CompilerOptions, gf.Definition, extended));
        }
    }
}
