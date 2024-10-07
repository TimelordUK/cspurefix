using PureFix.Dictionary.Contained;
using PureFix.Dictionary.Definition;
using PureFix.Dictionary.Parser;
using PureFix.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Dictionary.Compiler
{
    /// <summary>
    /// Generates FIX types
    /// </summary>
    public class MessageGenerator : GeneratorBase
    {
        private const string GetSet = "{get; set;}";

        public MessageGenerator(string? root, IFixDefinitions fixDefinitions, Options options) : base(SelectRoot(root, options.BackingTypeOutputPath!), fixDefinitions, options)
        {
        }

        public override void PostProcess()
        {
            var generator = new CodeGenerator();
            WriteMessageUsings(generator);
            generator.WriteLine();
            using (generator.BeginBlock($"namespace {Options.BackingTypeNamespace}.Types"))
            {
                using (generator.BeginBlock("public class FixMessageFactory : IFixMessageFactory"))
                {
                    using (generator.BeginBlock($"public IFixMessage? ToFixMessage(IMessageView view)"))
                    {
                        generator.WriteLine("var msgType = view.GetString((int)MsgTag.MsgType);");
                        using (generator.BeginBlock("switch (msgType)"))
                        {
                            foreach (var name in FixDefinitions.Message.Select(kv => kv.Value.Name).Distinct())
                            {
                                var m = FixDefinitions.Message[name];
                                if (m == null) continue;
                                using (generator.BeginBlock($"case \"{m.MsgType}\":"))
                                {
                                    generator.WriteLine($"var o = new {m.Name}();");
                                    generator.WriteLine("((IFixParser)o).Parse(view);");
                                    generator.WriteLine("return o;");
                                }
                            }
                        }
                        generator.WriteLine("return null;");
                    }
                }
            }
            var contents = generator.ToString();
            var path = Path.Join(Options.BackingTypeOutputPath, "FixMessageFactory.cs");
            WriteFile(path, contents);
        }


        protected override string GenerateType(MessageDefinition message)
        {
            var generator = new CodeGenerator();
            WriteMessageUsings(generator);
            generator.WriteLine();

            using(generator.BeginBlock($"namespace {Options.BackingTypeNamespace}"))
            {
                generator.WriteLine($"[MessageType(\"{message.MsgType}\", FixVersion.{FixDefinitions.Version})]");

                var typename = message.Name;
                using(generator.BeginBlock($"public sealed partial class {typename} : {nameof(IFixMessage)}"))
                {
                    ApplyFields(generator, message.Fields);

                    generator.WriteLine();
                    generator.WriteLine("IStandardHeader? IFixMessage.StandardHeader => StandardHeader;");
                    generator.WriteLine();
                    generator.WriteLine("IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;");

                    generator.WriteLine();
                    GenerateSupportingFunctions(generator, message);
                }
            }

            var result = generator.ToString();
            return result;
        }

        protected override string GenerateType(ContainedGroupField group)
        {
            var generator = new CodeGenerator();
            WriteMessageUsings(generator);
            generator.WriteLine();

            using(generator.BeginBlock($"namespace {Options.BackingTypeNamespace}.Types"))
            {
                using(generator.BeginBlock($"public sealed partial class {group.Name} : {nameof(IFixGroup)}"))
                {
                    var fields = group.Definition?.Fields;
                    if(fields is not null) ApplyFields(generator, fields);

                    generator.WriteLine();
                    GenerateSupportingFunctions(generator, group.Definition!);
                }
            }

            return generator.ToString();
        }

        protected override string GenerateType(ContainedComponentField component)
        {
            var generator = new CodeGenerator();
            WriteMessageUsings(generator);
            generator.WriteLine();

            string additionImplements = component.Name switch
            {
                "StandardHeader"    => ", IStandardHeader",
                "StandardTrailer"   => ", IStandardTrailer",
                _                   => ""
            };

            using(generator.BeginBlock($"namespace {Options.BackingTypeNamespace}.Types"))
            {
                using(generator.BeginBlock($"public sealed partial class {component.Name}Component : {nameof(IFixComponent)}{additionImplements}"))
                {
                    var fields = component.Definition?.Fields;
                    if(fields is not null) ApplyFields(generator, fields);

                    generator.WriteLine();
                    GenerateSupportingFunctions(generator, component.Definition!);
                }
            }

            return generator.ToString();
        }        
        
        protected override void HandleGroupProperty(CodeGenerator generator, int index, ContainedGroupField field)
        {
            if (field.Definition == null) return;

            var countField = field.Definition.NoOfField!.Tag;
            var typeName = MakeTypeName(field);

            generator.WriteLine($"[Group(NoOfTag = {countField}, Offset = {index}, Required = {MapRequired(field.Required)})]");
            generator.WriteLine($"public {typeName}[]? {field.Name} {GetSet}");
            generator.WriteLine();
        }

        protected override void HandleComponentProperty(CodeGenerator generator, int index, ContainedComponentField field)
        {
            if (field.Definition == null) return;
            var typename = MakeTypeName(field);
            
            generator.WriteLine($"[Component(Offset = {index}, Required = {MapRequired(field.Required)})]");
            generator.WriteLine($"public {typename}? {field.Name} {GetSet}");

            if(field.Name != "StandardTrailer")
            {
                generator.WriteLine();
            }
        }

        protected override void HandleFieldProperty(CodeGenerator generator, int index, ContainedSimpleField field, ContainedField? last, ContainedField? next)
        {
            var type = TagManager.ToCsType(field.Definition.TagType);

            if (field.Definition.TagType == TagType.Length && next is ContainedSimpleField peekField && peekField.Definition.TagType == TagType.RawData)
            {
                generator.WriteLine($"[TagDetails(Tag = {field.Definition.Tag}, Type = TagType.{TagManager.ToType(field.Definition.Type)}, Offset = {index}, Required = {MapRequired(field.Required)}, LinksToTag = {peekField.Definition.Tag})]");
            }
            else if(field.Definition.TagType == TagType.RawData && last is not null)
            {
                generator.WriteLine($"[TagDetails(Tag = {field.Definition.Tag}, Type = TagType.{TagManager.ToType(field.Definition.Type)}, Offset = {index}, Required = {MapRequired(field.Required)}, LinksToTag = {((ContainedSimpleField)last).Definition.Tag})]");
            }
            else
            {
                generator.WriteLine($"[TagDetails(Tag = {field.Definition.Tag}, Type = TagType.{TagManager.ToType(field.Definition.Type)}, Offset = {index}, Required = {MapRequired(field.Required)})]");
            }

            generator.WriteLine($"public {type}? {field.Definition.Name} {GetSet}");
            generator.WriteLine();

            if(field.Definition.Enums is not null)
            {
                GenerateEnums(type, field.Definition.TagType, field.Definition.Name, field.Definition.Enums);
            }
        }

        private void GenerateSupportingFunctions(CodeGenerator generator, IContainedSet set)
        {
            WriteIsValid(generator, set);
            generator.WriteLine();
            WriteEncode(generator, set);
            generator.WriteLine();
            WriteParse(generator, set);
            generator.WriteLine();
            WriteTryGetByTag(generator, set);
            generator.WriteLine();
            WriteReset(generator, set);
        }

        private void WriteReset(CodeGenerator generator, IContainedSet containedSet)
        {
            using (generator.BeginBlock("void IFixReset.Reset()"))
            {
                for (var i = 0; i < containedSet.Fields.Count; i++)
                {
                    switch (containedSet.Fields[i])
                    {
                        case ContainedSimpleField sf:
                            {
                                generator.WriteLine($"{sf.Name} = null;");
                            }
                            break;

                        case ContainedComponentField cf:
                            {
                                generator.WriteLine($"((IFixReset?){cf.Name})?.Reset();");
                            }
                            break;

                        case ContainedGroupField gf:
                            {
                                generator.WriteLine($"{gf.Name} = null;");
                            }
                            break;
                    }
                }
            }
        }

        private void WriteTryGetByTag(CodeGenerator generator, IContainedSet containedSet)
        {
            using (generator.BeginBlock("bool IFixLookup.TryGetByTag(string name, out object? value)"))
            {
                generator.WriteLine("value = null;");

                if (containedSet.Fields.Count == 0)
                {
                    generator.WriteLine("return false;");
                }
                else
                {
                    using(generator.BeginBlock($"switch (name)"))
                    {
                        for(var i = 0; i < containedSet.Fields.Count; i++)
                        {
                            var field = containedSet.Fields[i];
                            var name = field.Name;

                            generator.WriteLine($"case \"{name}\":");
                            using(generator.BeginIndent())
                            {
                                generator.WriteLine($"value = {name};");
                                generator.WriteLine("break;");
                            }
                        }

                        generator.WriteLine("default: return false;");
                    }

                    generator.WriteLine("return true;");
                }
            }
        }
            
        private void WriteParse(CodeGenerator generator, IContainedSet containedSet)
        {
            using (generator.BeginBlock("void IFixParser.Parse(IMessageView? view)"))
            {
                generator.WriteLine("if (view is null) return;");
                generator.WriteLine();

                for(var i = 0; i < containedSet.Fields.Count; i++)
                {
                    switch (containedSet.Fields[i])
                    {
                        case ContainedSimpleField sf:
                        {
                            var name = sf.Name;
                            var metaData = TagManager.GetTagMetaData(sf.Definition.TagType);
                            generator.WriteLine($"{name} = view.{metaData.Getter}({sf.Definition.Tag});");
                        }
                        break;

                        case ContainedGroupField gf:
                        {
                            var name = gf.Name;
                            var tempName = $"view{name}";
                            using(generator.BeginBlock($"if (view.GetView(\"{name}\") is IMessageView {tempName})"))
                            {
                                generator.WriteLine($"var count = {tempName}.GroupCount();");
                                generator.WriteLine($"{name} = new {MakeTypeName(gf)}[count];");
                                using(generator.BeginBlock($"for (int i = 0; i < count; i++)"))
                                {
                                    generator.WriteLine($"{name}[i] = new();");
                                    generator.WriteLine($"((IFixParser){name}[i]).Parse({tempName}.GetGroupInstance(i));");
                                }
                            }
                        }
                        break;

                        case ContainedComponentField cf:
                        {
                            var name = cf.Name;
                            var tempName = $"view{name}";
                            using(generator.BeginBlock($"if (view.GetView(\"{name}\") is IMessageView {tempName})"))
                            {
                                generator.WriteLine($"{name} = new();");
                                generator.WriteLine($"((IFixParser){name}).Parse({tempName});");
                            }
                        }
                        break;
                    }
                }
            }
        }

        private void WriteEncode(CodeGenerator generator, IContainedSet containedSet)
        {            
            using (generator.BeginBlock("void IFixEncoder.Encode(IFixWriter writer)"))
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
                                using(generator.BeginBlock($"if ({rawFieldName} is not null)"))
                                {
                                    generator.WriteLine($"writer.WriteWholeNumber({sf.Definition.Tag}, {rawFieldName}.Length);");

                                    var rawMetaData = TagManager.GetTagMetaData(rawField.Definition.TagType);
                                    generator.WriteLine($"writer.{rawMetaData.Writer}({rawField.Definition.Tag}, {rawFieldName});");
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
                                
                                generator.WriteLine($"if ({name} is not null) writer.{metaData.Writer}({sf.Definition.Tag}, {name}{valueTypeExtract});");
                            }
                        }
                        break;

                        case ContainedGroupField gf:
                        {
                            //var metaData = TagManager.GetTagMetaData(gf.Definition.TagType);
                            var countField = gf.Definition!.NoOfField!.Tag;
                            var name = gf.Name;
                            
                            using(generator.BeginBlock($"if ({name} is not null && {name}.Length != 0)"))
                            {
                                generator.WriteLine($"writer.WriteWholeNumber({countField}, {name}.Length);");
                                using(generator.BeginBlock($"for (int i = 0; i < {name}.Length; i++)"))
                                {
                                    generator.WriteLine($"((IFixEncoder){name}[i]).Encode(writer);");
                                }
                            }
                        }
                        break;

                        case ContainedComponentField cf:
                        {
                            var name = cf.Definition!.Name;
                            generator.WriteLine($"if ({name} is not null) ((IFixEncoder){name}).Encode(writer);");
                        }
                        break;
                    }
                }
            }
        }


        private void WriteIsValid(CodeGenerator generator, IContainedSet containedSet)
        {
            using (generator.BeginBlock("bool IFixValidator.IsValid(in FixValidatorConfig config)"))
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
                    generator.WriteLine("return true;");
                }
                else
                {
                    generator.WriteLine("return");
                    using (generator.BeginIndent())
                    {
                        for (int i = 0; i < checks.Count; i++)
                        {
                            var check = checks[i];

                            var line = (i == 0) ? check : $"&& {check}";

                            if (i == checks.Count - 1)
                            {
                                generator.WriteLine($"{line};");
                            }
                            else
                            {
                                generator.WriteLine(line);
                            }
                        }
                    }
                }
            }
        }

        private void GenerateEnums(string csharpBaseType, TagType tagType, string fieldName, IReadOnlyDictionary<string, FieldEnum> enums)
        {
            var enumName = $"{fieldName}Values";

            var filename = MakeEnumFilename(enumName);
            if(FilesGenerated.Add(filename) == false) return;

            var generator = new CodeGenerator();

            if(enumName == "MsgTypeValues")
            {
                Console.WriteLine();
            }

            using(generator.BeginBlock($"namespace {Options.BackingTypeNamespace}.Types"))
            using(generator.BeginBlock($"public static class {enumName}"))
            {
                var hasDuplicates = HasDuplicates(enums);

                foreach(var field in enums.Values)
                {
                    var value = tagType switch
                    {
                        TagType.String  => $"\"{field.Key}\"",
                        TagType.Boolean => (field.Key == "Y" ? "true" : "false"),
                        _               => field.Key.ToString()
                    };

                    // FIX5 has some duplicate enum values that differ only by case (seriously!)
                    // So we'll need some special handling for the enum values
                    var constantName = field.Description.UnderscoreToCamelCase();
                    if(hasDuplicates && csharpBaseType == "string")
                    {
                        constantName = new string(field.Key.Where(c => Char.IsLetterOrDigit(c)).ToArray());;
                    }

                    // Some descriptions start with a number, which won't map to a valid C# symbol
                    if(char.IsDigit(constantName[0])) constantName = "_" + constantName;

                    generator.WriteLine($"public const {csharpBaseType} {constantName} = {value};");
                }
            }

            var code = generator.ToString();
            WriteFile(filename, code);
        }

        private bool HasDuplicates(IReadOnlyDictionary<string, FieldEnum> enums)
        {
            var uniqueItems = enums.Values.Select(e => e.Description).ToHashSet(StringComparer.OrdinalIgnoreCase);
            return uniqueItems.Count != enums.Count;
        }
    }
}
