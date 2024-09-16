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
    public class ViewParserCompiler : BaseCompiler, ISetDispatchReceiver
    {
        public ViewParserCompiler(FixDefinitions definitions, Options? options = null) : base(definitions, options)
        {
        }

        /*
         * public static class HeartbeatExt
           {
               public static void Parse(this Heartbeat instance, MsgView? view)
               {
                   instance.StandardHeader ??= new StandardHeader();
                   instance.StandardHeader.Parse(view?.GetView("StandardHeader"));
                   instance.TestReqID = view?.GetString(112);
                   instance.StandardTrailer ??= new StandardTrailer();
                   instance.StandardTrailer.Parse(view?.GetView("StandardTrailer"));
               }
           }
         */
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
                    .Union([$"using {MakeTypesNamespace()};", "using PureFix.Buffer.Ascii;"])
            );

            _builder.WriteLine(usingDeclaration);
            _builder.WriteLine();

            using (_builder.BeginBlock($"namespace {ns}"))
            {
                if (compilerType.Set is MessageDefinition messageDefinition)
                {
                    _builder.WriteLine(
                        $"[MessageType(\"{messageDefinition.MsgType}\", FixVersion.{Definitions.Version})]");
                }

                using (_builder.BeginBlock($"public static class {compilerType.QualifiedName}Ext"))
                using (_builder.BeginBlock(
                           $"public static void Parse(this {compilerType.QualifiedName} instance, MsgView? view)"))
                {
                    _builder.WriteLine("if (view is null) return;");
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
                return $"{Path.Join(CompilerOptions.ParserFormatterOutputPath, $"{name}")}Ext.cs";
            }

            return MakesTypesFilename(name);
        }

        private string MakesTypesFilename(string typename)
        {
            return $"{Path.Join(CompilerOptions.ParserFormatterOutputPath, "Types", $"{typename}")}Ext.cs";
        }

        // instance.TestReqID = view?.GetString(112);
        public void OnSimple(ContainedSimpleField sf, int index, object? peek)
        {
            var metaData = TagManager.GetTagMetaData(sf.Definition.TagType);
            _builder.WriteLine($"instance.{sf.Name} = view.{metaData.Getter}({sf.Definition.Tag});");
        }

        // assume that the component has its own parser so just give it the view contained within parent view.
        public void OnComponent(ContainedComponentField cf, int index)
        {
            if (cf.Definition == null) return;
            var extended = _currentCompilerType?.GetExtended(cf) ?? cf.Name;

            var variableName = $"groupView{cf.Name}";
            using (_builder.BeginBlock($"if (view.GetView(\"{cf.Name}\") is MsgView {variableName})"))
            {
                _builder.WriteLine($"instance.{cf.Name} = new {extended}();");
                _builder.WriteLine($"instance.{cf.Name}!.Parse({variableName});");
            }

            Enqueue(new CompilerType(Definitions, CompilerOptions, cf.Definition, extended));
        }

        // for a group we need to allocate an array for received component count and parse each instance.
        // var count = view?.GroupCount() ?? 0;
        // instance.NoHops = new HopNoHops[count];
        public void OnGroup(ContainedGroupField gf, int index)
        {
            if (gf.Definition == null) return;

            var groupView = $"groupView{gf.Name}";
            var count = $"count{gf.Name}";
            _builder.WriteLine($"var {groupView} = view.GetView(\"{gf.Name}\");");
            _builder.WriteLine($"if ({groupView} is null) return;");
            _builder.WriteLine();

            var extended = _currentCompilerType?.GetExtended(gf) ?? gf.Name;
            _builder.WriteLine($"var {count} = {groupView}.GroupCount();");
            _builder.WriteLine($"instance.{gf.Name} = new {extended}[{count}];");
            using (_builder.BeginBlock($"for (var i = 0; i < {count}; ++i)"))
            {
                _builder.WriteLine($"instance.{gf.Name}[i] = new();");
                _builder.WriteLine($"instance.{gf.Name}[i].Parse({groupView}[i]);");
            }

            Enqueue(new CompilerType(Definitions, CompilerOptions, gf.Definition, extended));
        }
    }
}
