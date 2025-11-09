using PureFix.Dictionary.Contained;
using PureFix.Dictionary.Definition;
using PureFix.Dictionary.Parser;
using PureFix.Types;
using System.Text;

namespace PureFix.Dictionary.Compiler
{
    /// <summary>
    /// Generates FIX types into separate assemblies per dictionary with nested groups
    /// </summary>
    public class ModularGenerator : GeneratorBase
    {
        private readonly ModularGeneratorOptions _modularOptions;

        public ModularGenerator(
            IFixDefinitions definitions,
            ModularGeneratorOptions options)
            : base(options.BaseOptions.BackingTypeOutputPath!, definitions, options.BaseOptions)
        {
            _modularOptions = options;
        }

        public override void PostProcess()
        {
            // Generate message factory
            GenerateFactory();

            // Generate .csproj file
            GenerateProjectFile();
        }

        private void GenerateFactory()
        {
            var generator = new CodeGenerator();
            WriteCoreUsings(generator);
            generator.WriteLine();

            using (generator.BeginBlock($"namespace {GetNamespace()}"))
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

        private void GenerateProjectFile()
        {
            var assemblyName = _modularOptions.AssemblyName ?? "PureFix.Types.Generated";

            var csproj = $@"<Project Sdk=""Microsoft.NET.Sdk"">

  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
    <GenerateDocumentationFile>false</GenerateDocumentationFile>
    <RootNamespace>{GetNamespace()}</RootNamespace>
  </PropertyGroup>

  <ItemGroup>
    <ProjectReference Include=""{_modularOptions.CoreProjectPath}"" />
  </ItemGroup>

</Project>";

            var projectPath = Path.Join(_modularOptions.OutputPath, assemblyName, $"{assemblyName}.csproj");

            // Ensure directory exists
            var projectDir = Path.GetDirectoryName(projectPath);
            if (projectDir != null && !Directory.Exists(projectDir))
            {
                Directory.CreateDirectory(projectDir);
            }

            File.WriteAllText(projectPath, csproj);
        }

        protected override string GenerateType(MessageDefinition message)
        {
            var generator = new CodeGenerator();
            WriteCoreUsings(generator);
            generator.WriteLine();

            using (generator.BeginBlock($"namespace {GetNamespace()}"))
            {
                generator.WriteLine($"[MessageType(\"{message.MsgType}\", FixVersion.{FixDefinitions.Version})]");

                using (generator.BeginBlock($"public sealed partial class {message.Name} : IFixMessage"))
                {
                    // Generate properties for fields/components
                    ApplyFields(generator, "", message, message.Name);

                    generator.WriteLine();
                    generator.WriteLine("IStandardHeader? IFixMessage.StandardHeader => StandardHeader;");
                    generator.WriteLine();
                    generator.WriteLine("IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;");

                    generator.WriteLine();
                    GenerateSupportingFunctions(generator, "", message, message.Name);
                }
            }

            return generator.ToString();
        }

        protected override string GenerateType(string parentPath, ContainedComponentField component)
        {
            var generator = new CodeGenerator();
            WriteCoreUsings(generator);
            generator.WriteLine();

            // Components go in a Components subdirectory
            using (generator.BeginBlock($"namespace {GetNamespace()}.Components"))
            {
                var componentName = component.Definition?.Name ?? "UnknownComponent";
                // StandardHeader and StandardTrailer implement their specific interfaces
                var interfaces = componentName switch
                {
                    "StandardHeader" => "IFixComponent, IStandardHeader",
                    "StandardTrailer" => "IFixComponent, IStandardTrailer",
                    _ => "IFixComponent"
                };
                using (generator.BeginBlock($"public sealed partial class {componentName} : {interfaces}"))
                {
                    if (component.Definition != null)
                    {
                        ApplyFields(generator, "", component.Definition, componentName);
                        generator.WriteLine();
                        GenerateSupportingFunctions(generator, "", component.Definition, componentName);
                    }
                }
            }

            return generator.ToString();
        }

        protected override string GenerateType(string parentPath, ContainedGroupField group)
        {
            // Groups are generated as nested classes within their parent message/component
            // This method is called by GeneratorBase.Process() but we don't use it
            // Groups are generated inline by GenerateNestedGroup() called from HandleGroupProperty
            // Return empty string to satisfy the abstract method
            return string.Empty;
        }

        /// <summary>
        /// Override ApplyFields to prevent calling Process() for groups
        /// since we generate them inline
        /// </summary>
        protected new void ApplyFields(CodeGenerator generator, string parentPath, IContainedSet set, string? parentTypeName = null)
        {
            ContainedField? last = null;
            var fields = set.Fields;
            for (int i = 0; i < fields.Count; i++)
            {
                var field = fields[i];
                var next = (i == fields.Count - 1 ? null : fields[i + 1]);

                if (field is ContainedSimpleField simpleField)
                {
                    HandleFieldProperty(generator, parentPath, i, simpleField, last, next);
                }
                else if (field is ContainedComponentField componentField)
                {
                    HandleComponentProperty(generator, parentPath, i, componentField);
                    // Generate component as separate file
                    var componentCode = GenerateType(parentPath, componentField);
                    if (!string.IsNullOrEmpty(componentCode) && componentField.Definition != null)
                    {
                        var componentsDir = Path.Join(Options.BackingTypeOutputPath, "Components");
                        Directory.CreateDirectory(componentsDir);
                        var componentPath = Path.Join(componentsDir, $"{componentField.Definition.Name}.cs");
                        WriteFile(componentPath, componentCode);
                    }
                }
                else if (field is ContainedGroupField groupField)
                {
                    HandleGroupProperty(generator, parentPath, i, groupField, parentTypeName);
                    // DON'T call Process() for groups - they're nested inline
                }

                last = field;
            }
        }

        private void GenerateNestedGroup(
            CodeGenerator generator,
            string parentPath,
            ContainedGroupField group,
            int indentLevel = 1)
        {
            if (group.Definition == null) return;

            var groupName = group.Name;

            using (generator.BeginBlock($"public sealed partial class {groupName} : IFixGroup"))
            {
                // Generate fields for the group
                ApplyFields(generator, parentPath + groupName, group.Definition, groupName);

                generator.WriteLine();
                GenerateSupportingFunctions(generator, parentPath + groupName, group.Definition, groupName);
            }
        }

        protected override void HandleFieldProperty(
            CodeGenerator generator,
            string parentPath,
            int index,
            ContainedSimpleField field,
            ContainedField? last,
            ContainedField? next)
        {
            var propName = field.Name;
            var typeName = GetCSharpType(field);

            generator.WriteLine($"[TagDetails(Tag = {field.Definition.Tag}, Type = TagType.{field.Definition.TagType}, Offset = {index}, Required = {(field.Required ? "true" : "false")})]");
            generator.WriteLine($"public {typeName}? {propName} {{get; set;}}");
            generator.WriteLine();
        }

        protected override void HandleComponentProperty(
            CodeGenerator generator,
            string parentPath,
            int index,
            ContainedComponentField field)
        {
            var componentName = field.Definition?.Name ?? "UnknownComponent";

            generator.WriteLine($"[Component(Offset = {index}, Required = {(field.Required ? "true" : "false")})]");
            generator.WriteLine($"public {componentName}? {field.Name} {{get; set;}}");
            generator.WriteLine();
        }

        protected override void HandleGroupProperty(
            CodeGenerator generator,
            string parentPath,
            int index,
            ContainedGroupField field)
        {
            HandleGroupProperty(generator, parentPath, index, field, null);
        }

        private void HandleGroupProperty(
            CodeGenerator generator,
            string parentPath,
            int index,
            ContainedGroupField field,
            string? parentTypeName)
        {
            var groupName = field.Name;

            // Generate the nested group class first
            GenerateNestedGroup(generator, parentPath, field);

            // Then generate the property
            var noOfTag = field.Definition?.NoOfField?.Tag ?? -1;
            generator.WriteLine($"[Group(NoOfTag = {noOfTag}, Offset = {index}, Required = {(field.Required ? "true" : "false")})]");
            generator.WriteLine($"public {groupName}[]? {GetGroupPropertyName(field, parentTypeName)} {{get; set;}}");
            generator.WriteLine();
        }

        private string GetGroupPropertyName(ContainedGroupField field, string? parentTypeName)
        {
            // Convert "NoOrders" -> "Orders"
            var name = field.Name;
            if (name.StartsWith("No") && name.Length > 2)
            {
                var propertyName = name.Substring(2);
                // Check if property name would conflict with parent type name
                if (!string.IsNullOrEmpty(parentTypeName) && propertyName == parentTypeName)
                {
                    // Append "Items" suffix to avoid collision with parent type name
                    return propertyName + "Items";
                }
                return propertyName;
            }
            return name;
        }

        protected void GenerateSupportingFunctions(CodeGenerator generator, string parentPath, IContainedSet set)
        {
            GenerateSupportingFunctions(generator, parentPath, set, null);
        }

        private void GenerateSupportingFunctions(CodeGenerator generator, string parentPath, IContainedSet set, string? parentTypeName)
        {
            // Generate IsValid
            GenerateIsValid(generator, set);
            generator.WriteLine();

            // Generate Encode
            GenerateEncode(generator, set, parentTypeName);
            generator.WriteLine();

            // Generate Parse
            GenerateParse(generator, set, parentTypeName);
            generator.WriteLine();

            // Generate TryGetByTag
            GenerateTryGetByTag(generator, set, parentTypeName);
            generator.WriteLine();

            // Generate Reset
            GenerateReset(generator, set, parentTypeName);
        }

        private void GenerateIsValid(CodeGenerator generator, IContainedSet set)
        {
            using (generator.BeginBlock("bool IFixValidator.IsValid(in FixValidatorConfig config)"))
            {
                var conditions = new List<string>();

                foreach (var field in set.Fields)
                {
                    if (field is ContainedComponentField component)
                    {
                        if (component.Name == "StandardHeader")
                        {
                            conditions.Add($"(!config.CheckStandardHeader || ({component.Name} is not null && ((IFixValidator){component.Name}).IsValid(in config)))");
                        }
                        else if (component.Name == "StandardTrailer")
                        {
                            conditions.Add($"(!config.CheckStandardTrailer || ({component.Name} is not null && ((IFixValidator){component.Name}).IsValid(in config)))");
                        }
                    }
                }

                if (conditions.Count > 0)
                {
                    var joinedConditions = string.Join(" && ", conditions);
                    generator.WriteLine($"return {joinedConditions};");
                }
                else
                {
                    generator.WriteLine("return true;");
                }
            }
        }

        private void GenerateEncode(CodeGenerator generator, IContainedSet set, string? parentTypeName)
        {
            using (generator.BeginBlock("void IFixEncoder.Encode(IFixWriter writer)"))
            {
                foreach (var field in set.Fields)
                {
                    if (field is ContainedSimpleField simple)
                    {
                        var writeMethod = GetWriteMethod(simple.Definition.TagType);
                        generator.WriteLine($"if ({simple.Name} is not null) writer.{writeMethod}({simple.Definition.Tag}, {simple.Name}{(NeedsValueAccess(simple.Definition.TagType) ? ".Value" : "")});");
                    }
                    else if (field is ContainedComponentField component)
                    {
                        generator.WriteLine($"if ({component.Name} is not null) ((IFixEncoder){component.Name}).Encode(writer);");
                    }
                    else if (field is ContainedGroupField group)
                    {
                        var propName = GetGroupPropertyName(group, parentTypeName);
                        var countField = group.Definition?.NoOfField?.Tag ?? -1;

                        using (generator.BeginBlock($"if ({propName} is not null && {propName}.Length != 0)"))
                        {
                            generator.WriteLine($"writer.WriteWholeNumber({countField}, {propName}.Length);");
                            using (generator.BeginBlock($"for (int i = 0; i < {propName}.Length; i++)"))
                            {
                                generator.WriteLine($"((IFixEncoder){propName}[i]).Encode(writer);");
                            }
                        }
                    }
                }
            }
        }

        private void GenerateParse(CodeGenerator generator, IContainedSet set, string? parentTypeName)
        {
            using (generator.BeginBlock("void IFixParser.Parse(IMessageView? view)"))
            {
                generator.WriteLine("if (view is null) return;");
                generator.WriteLine();

                foreach (var field in set.Fields)
                {
                    if (field is ContainedSimpleField simple)
                    {
                        var getMethod = GetGetMethod(simple.Definition.TagType);
                        generator.WriteLine($"{simple.Name} = view.{getMethod}({simple.Definition.Tag});");
                    }
                    else if (field is ContainedComponentField component)
                    {
                        using (generator.BeginBlock($"if (view.GetView(\"{component.Name}\") is IMessageView view{component.Name})"))
                        {
                            generator.WriteLine($"{component.Name} = new();");
                            generator.WriteLine($"((IFixParser){component.Name}).Parse(view{component.Name});");
                        }
                    }
                    else if (field is ContainedGroupField group)
                    {
                        var propName = GetGroupPropertyName(group, parentTypeName);
                        var tempName = $"view{group.Name}";

                        using (generator.BeginBlock($"if (view.GetView(\"{group.Name}\") is IMessageView {tempName})"))
                        {
                            generator.WriteLine($"var count = {tempName}.GroupCount();");
                            generator.WriteLine($"{propName} = new {group.Name}[count];");
                            using (generator.BeginBlock($"for (int i = 0; i < count; i++)"))
                            {
                                generator.WriteLine($"{propName}[i] = new();");
                                generator.WriteLine($"((IFixParser){propName}[i]).Parse({tempName}.GetGroupInstance(i));");
                            }
                        }
                    }
                }
            }
        }

        private void GenerateTryGetByTag(CodeGenerator generator, IContainedSet set, string? parentTypeName)
        {
            using (generator.BeginBlock("bool IFixLookup.TryGetByTag(string name, out object? value)"))
            {
                generator.WriteLine("value = null;");
                using (generator.BeginBlock("switch (name)"))
                {
                    foreach (var field in set.Fields)
                    {
                        var propertyName = field is ContainedGroupField groupField
                            ? GetGroupPropertyName(groupField, parentTypeName)
                            : field.Name;

                        using (generator.BeginBlock($"case \"{field.Name}\":"))
                        {
                            generator.WriteLine($"value = {propertyName};");
                            generator.WriteLine("break;");
                        }
                    }

                    using (generator.BeginBlock("default:"))
                    {
                        generator.WriteLine("return false;");
                    }
                }
                generator.WriteLine("return true;");
            }
        }

        private void GenerateReset(CodeGenerator generator, IContainedSet set, string? parentTypeName)
        {
            using (generator.BeginBlock("void IFixReset.Reset()"))
            {
                foreach (var field in set.Fields)
                {
                    if (field is ContainedComponentField component)
                    {
                        generator.WriteLine($"((IFixReset?){component.Name})?.Reset();");
                    }
                    else if (field is ContainedGroupField groupField)
                    {
                        var propertyName = GetGroupPropertyName(groupField, parentTypeName);
                        generator.WriteLine($"{propertyName} = null;");
                    }
                    else
                    {
                        generator.WriteLine($"{field.Name} = null;");
                    }
                }
            }
        }

        private void WriteCoreUsings(CodeGenerator generator)
        {
            generator.WriteLine("using System;");
            generator.WriteLine("using System.Collections.Generic;");
            generator.WriteLine("using System.Linq;");
            generator.WriteLine("using System.Text;");
            generator.WriteLine("using System.Threading.Tasks;");
            generator.WriteLine("using PureFix.Types;");
            generator.WriteLine($"using {GetNamespace()}.Components;");
        }

        private string GetNamespace()
        {
            return _modularOptions.AssemblyName ?? "PureFix.Types.Generated";
        }

        private string GetCSharpType(ContainedSimpleField field)
        {
            return field.Definition.TagType switch
            {
                TagType.String => "string",
                TagType.Int => "int",
                TagType.Float => "double",
                TagType.Boolean => "bool",
                TagType.UtcTimestamp => "DateTime",
                TagType.UtcDateOnly => "DateOnly",
                TagType.UtcTimeOnly => "TimeOnly",
                TagType.LocalDate => "DateOnly",
                TagType.RawData => "byte[]",
                TagType.Length => "int",
                TagType.MonthYear => "MonthYear",
                _ => "string"
            };
        }

        private string GetWriteMethod(TagType type)
        {
            return type switch
            {
                TagType.String => "WriteString",
                TagType.Int => "WriteWholeNumber",
                TagType.Float => "WriteNumber",
                TagType.Boolean => "WriteBoolean",
                TagType.UtcTimestamp => "WriteUtcTimeStamp",
                TagType.UtcDateOnly => "WriteUtcDateOnly",
                TagType.UtcTimeOnly => "WriteTimeOnly",
                TagType.LocalDate => "WriteLocalDateOnly",
                TagType.RawData => "WriteBuffer",
                TagType.Length => "WriteWholeNumber",
                TagType.MonthYear => "WriteMonthYear",
                _ => "WriteString"
            };
        }

        private string GetGetMethod(TagType type)
        {
            return type switch
            {
                TagType.String => "GetString",
                TagType.Int => "GetInt32",
                TagType.Float => "GetDouble",
                TagType.Boolean => "GetBool",
                TagType.UtcTimestamp => "GetDateTime",
                TagType.UtcDateOnly => "GetDateOnly",
                TagType.UtcTimeOnly => "GetTimeOnly",
                TagType.LocalDate => "GetDateOnly",
                TagType.RawData => "GetByteArray",
                TagType.Length => "GetInt32",
                TagType.MonthYear => "GetMonthYear",
                _ => "GetString"
            };
        }

        private bool NeedsValueAccess(TagType type)
        {
            return type switch
            {
                TagType.Int => true,
                TagType.Float => true,
                TagType.Boolean => true,
                TagType.UtcTimestamp => true,
                TagType.UtcDateOnly => true,
                TagType.UtcTimeOnly => true,
                TagType.LocalDate => true,
                TagType.Length => true,
                TagType.MonthYear => true,
                _ => false
            };
        }
    }
}
