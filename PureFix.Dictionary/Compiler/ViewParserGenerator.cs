using PureFix.Dictionary.Contained;
using PureFix.Dictionary.Definition;
using PureFix.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PureFix.Dictionary.Compiler.MsgCompiler;

namespace PureFix.Dictionary.Compiler
{
    public class ViewParserGenerator : GeneratorBase
    {
        private const string GetSet = "{get; set;}";

        private readonly HashSet<string> m_FilesGenerated = new(StringComparer.OrdinalIgnoreCase);

        public ViewParserGenerator(string? root, FixDefinitions fixDefinitions, MsgCompiler.Options options) : base(SelectRoot(root, options.ParserFormatterOutputPath!), fixDefinitions, options)
        {
        }
  
        protected override string GenerateType(MessageDefinition message)
        {
            var generator = new CodeGenerator();
            WriteMessageUsings(generator);
            generator.WriteLine("using PureFix.Buffer.Ascii;");
            generator.WriteLine();

            using(generator.BeginBlock($"namespace {Options.BackingTypeNamespace}"))
            {
                generator.WriteLine($"[MessageType(\"{message.MsgType}\", FixVersion.{FixDefinitions.Version})]");

                var typename = message.Name;
                using(generator.BeginBlock($"public static class {typename}Extensions"))
                {
                    using(generator.BeginBlock($"public static void Parse(this {typename} instance, MsgView? view)"))
                    {
                        generator.WriteLine("if (view is null) return;");
                        generator.WriteLine();
                        ApplyFields(generator, message.Fields);
                    }                    
                }
            }

            var result = generator.ToString();
            return result;
        }

        protected override string GenerateType(ContainedComponentField component)
        {
            var generator = new CodeGenerator();
            WriteUsings(generator);
            generator.WriteLine("using PureFix.Buffer.Ascii;");
            generator.WriteLine();

            using(generator.BeginBlock($"namespace {Options.BackingTypeNamespace}.Types"))
            {
                var typename = component.Name;
                using(generator.BeginBlock($"public static class {typename}ComponentExtensions"))
                {
                    using(generator.BeginBlock($"public static void Parse(this {typename}Component instance, MsgView? view)"))
                    {
                        generator.WriteLine("if (view is null) return;");
                        generator.WriteLine();
                        
                        var fields = component.Definition?.Fields;
                        if(fields is not null) ApplyFields(generator, fields);
                    }                    
                }
            }

            return generator.ToString();
        }
        

        protected override string GenerateType(ContainedGroupField group)
        {
            var generator = new CodeGenerator();
            WriteUsings(generator);
            generator.WriteLine("using PureFix.Buffer.Ascii;");
            generator.WriteLine();

            using(generator.BeginBlock($"namespace {Options.BackingTypeNamespace}.Types"))
            {
                var typename = group.Name;
                using(generator.BeginBlock($"public static class {typename}Extensions"))
                {
                    using(generator.BeginBlock($"public static void Parse(this {typename} instance, MsgView? view)"))
                    {
                        generator.WriteLine("if (view is null) return;");
                        generator.WriteLine();
                        
                        var fields = group.Definition?.Fields;
                        if(fields is not null) ApplyFields(generator, fields);
                    }                    
                }
            }

            return generator.ToString();
        }

        protected override void HandleComponentProperty(CodeGenerator generator, int index, ContainedComponentField component)
        {
            if (component.Definition == null) return;

            var variableName = $"groupView{component.Name}";
            using (generator.BeginBlock($"if (view.GetView(\"{component.Name}\") is MsgView {variableName})"))
            {
                generator.WriteLine($"instance.{component.Name} = new();");
                generator.WriteLine($"instance.{component.Name}!.Parse({variableName});");
            }
        }

        protected override void HandleFieldProperty(CodeGenerator generator, int index, ContainedSimpleField field, ContainedField? last, ContainedField? next)
        {
            var metaData = TagManager.GetTagMetaData(field.Definition.TagType);
            generator.WriteLine($"instance.{field.Name} = view.{metaData.Getter}({field.Definition.Tag});");
        }

        protected override void HandleGroupProperty(CodeGenerator generator, int index, ContainedGroupField group)
        {
            var viewVariable = $"groupView{group.Name}";
            using (generator.BeginBlock($"if (view.GetView(\"{group.Name}\") is MsgView {viewVariable})"))
            {
                generator.WriteLine($"var count = {viewVariable}.GroupCount();");
                
                var typename = MakeTypeName(group);
                generator.WriteLine($"instance.{group.Name} = new {typename}[count];");

                using(generator.BeginBlock("for (var i = 0; i < count; i++)"))
                {
                    generator.WriteLine($"instance.{group.Name}[i] = new();");
                    generator.WriteLine($"instance.{group.Name}[i].Parse({viewVariable}[i]);");
                }
            }
        }
    }
}
