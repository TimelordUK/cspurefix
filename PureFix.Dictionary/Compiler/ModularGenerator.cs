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

        protected override void PostProcess()
        {
            // Generate message factory
            GenerateFactory();

            // Generate session message factory
            GenerateSessionMessageFactory();

            // Generate TypeSystemProvider for registry integration
            GenerateTypeSystemProvider();

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

        private void GenerateSessionMessageFactory()
        {
            var generator = new CodeGenerator();
            WriteCoreUsings(generator);
            generator.WriteLine("using PureFix.Types.Config;");
            generator.WriteLine();

            using (generator.BeginBlock($"namespace {GetNamespace()}"))
            {
                using (generator.BeginBlock("public class SessionMessageFactory : ISessionMessageFactory"))
                {
                    // Add private field for session description
                    generator.WriteLine("private readonly ISessionDescription m_SessionDescription;");
                    generator.WriteLine();

                    // Add constructor
                    using (generator.BeginBlock("public SessionMessageFactory(ISessionDescription sessionDescription)"))
                    {
                        generator.WriteLine("m_SessionDescription = sessionDescription;");
                    }
                    generator.WriteLine();

                    // TestRequest method
                    using (generator.BeginBlock("IFixMessage? ISessionMessageFactory.TestRequest(string testReqId)"))
                    {
                        generator.WriteLine("return new TestRequest { StandardHeader = new Components.StandardHeader(), TestReqID = testReqId };");
                    }
                    generator.WriteLine();

                    // Heartbeat method
                    using (generator.BeginBlock("IFixMessage? ISessionMessageFactory.Heartbeat(string testReqId)"))
                    {
                        generator.WriteLine("return new Heartbeat { StandardHeader = new Components.StandardHeader(), TestReqID = testReqId };");
                    }
                    generator.WriteLine();

                    // ResendRequest method
                    using (generator.BeginBlock("IFixMessage? ISessionMessageFactory.ResendRequest(int beginSeqNo, int endSeqNo)"))
                    {
                        generator.WriteLine("return new ResendRequest { StandardHeader = new Components.StandardHeader(), BeginSeqNo = beginSeqNo, EndSeqNo = endSeqNo };");
                    }
                    generator.WriteLine();

                    // SequenceReset method
                    using (generator.BeginBlock("IFixMessage? ISessionMessageFactory.SequenceReset(int newSeqNo, bool? gapFill)"))
                    {
                        generator.WriteLine("return new SequenceReset { StandardHeader = new Components.StandardHeader(), GapFillFlag = gapFill, NewSeqNo = newSeqNo };");
                    }
                    generator.WriteLine();

                    // Trailer method
                    using (generator.BeginBlock("IStandardTrailer? ISessionMessageFactory.Trailer(int checksum)"))
                    {
                        generator.WriteLine("return new Components.StandardTrailer() { CheckSum = checksum.ToString(\"D3\") };");
                    }
                    generator.WriteLine();

                    // Logon method
                    using (generator.BeginBlock("IFixMessage? ISessionMessageFactory.Logon(string? userRequestId, bool? isResponse)"))
                    {
                        using (generator.BeginBlock("return new Logon"))
                        {
                            generator.WriteLine("Username = !string.IsNullOrEmpty(m_SessionDescription.Username) ? m_SessionDescription.Username : null,");
                            generator.WriteLine("Password = !string.IsNullOrEmpty(m_SessionDescription.Password) ? m_SessionDescription.Password : null,");
                            generator.WriteLine("HeartBtInt = m_SessionDescription.HeartBtInt,");
                            generator.WriteLine("ResetSeqNumFlag = m_SessionDescription.ResetSeqNumFlag,");
                            generator.WriteLine("EncryptMethod = 0");
                        }
                        generator.WriteLine(";");
                    }
                    generator.WriteLine();

                    // Reject method
                    using (generator.BeginBlock("IFixMessage? ISessionMessageFactory.Reject(string msgType, int seqNo, string msg, int reason)"))
                    {
                        using (generator.BeginBlock("return new Reject"))
                        {
                            generator.WriteLine("RefMsgType = msgType,");
                            generator.WriteLine("SessionRejectReason = reason,");
                            generator.WriteLine("RefSeqNum = seqNo,");
                            generator.WriteLine("Text = msg");
                        }
                        generator.WriteLine(";");
                    }
                    generator.WriteLine();

                    // Logout method
                    using (generator.BeginBlock("IFixMessage? ISessionMessageFactory.Logout(string text)"))
                    {
                        using (generator.BeginBlock("return new Logout"))
                        {
                            generator.WriteLine("Text = text");
                        }
                        generator.WriteLine(";");
                    }
                    generator.WriteLine();

                    // Header method
                    using (generator.BeginBlock("IStandardHeader? ISessionMessageFactory.Header(string msgType, int seqNum, DateTime time, IStandardHeader? overrides)"))
                    {
                        generator.WriteLine("var bodyLength = Math.Max(4, m_SessionDescription.BodyLengthChars ?? 7);");
                        generator.WriteLine("var placeholder = (int)Math.Pow(10, bodyLength - 1) + 1;");
                        using (generator.BeginBlock("return new Components.StandardHeader"))
                        {
                            generator.WriteLine("BeginString = m_SessionDescription.BeginString,");
                            generator.WriteLine("BodyLength = placeholder,");
                            generator.WriteLine("MsgType = msgType,");
                            generator.WriteLine("SenderCompID = m_SessionDescription.SenderCompID,");
                            generator.WriteLine("MsgSeqNum = seqNum,");
                            generator.WriteLine("SendingTime = time,");
                            generator.WriteLine("TargetCompID = m_SessionDescription.TargetCompID,");
                            generator.WriteLine("TargetSubID = !string.IsNullOrEmpty(m_SessionDescription.TargetSubID) ? m_SessionDescription.TargetSubID : m_SessionDescription.TargetSubID,");
                            generator.WriteLine("SenderSubID = !string.IsNullOrEmpty(m_SessionDescription.SenderSubID) ? m_SessionDescription.SenderSubID : m_SessionDescription.SenderSubID,");
                        }
                        generator.WriteLine(";");
                    }
                }
            }

            var contents = generator.ToString();
            var path = Path.Join(Options.BackingTypeOutputPath, "SessionMessageFactory.cs");
            WriteFile(path, contents);
        }

        private void GenerateTypeSystemProvider()
        {
            var generator = new CodeGenerator();
            WriteCoreUsings(generator);
            generator.WriteLine("using PureFix.Types.Config;");
            generator.WriteLine("using PureFix.Types.Registry;");
            generator.WriteLine();

            using (generator.BeginBlock($"namespace {GetNamespace()}"))
            {
                generator.WriteLine("/// <summary>");
                generator.WriteLine("/// Type system provider for registry integration.");
                generator.WriteLine("/// Enables dynamic loading and factory creation via ITypeRegistry.");
                generator.WriteLine("/// </summary>");
                using (generator.BeginBlock("public class TypeSystemProvider : ITypeSystemProvider"))
                {
                    // Convert enum to proper FIX version string (e.g., FIX44 -> "FIX.4.4")
                    var version = FormatFixVersion(FixDefinitions.Version);

                    // GetVersion
                    using (generator.BeginBlock("public string GetVersion()"))
                    {
                        generator.WriteLine($"return \"{version}\";");
                    }
                    generator.WriteLine();

                    // GetRootNamespace
                    using (generator.BeginBlock("public string GetRootNamespace()"))
                    {
                        generator.WriteLine($"return \"{GetNamespace()}\";");
                    }
                    generator.WriteLine();

                    // CreateMessageFactory
                    using (generator.BeginBlock("public IFixMessageFactory CreateMessageFactory()"))
                    {
                        generator.WriteLine("return new FixMessageFactory();");
                    }
                    generator.WriteLine();

                    // CreateSessionMessageFactory
                    using (generator.BeginBlock("public ISessionMessageFactory CreateSessionMessageFactory(ISessionDescription sessionDescription)"))
                    {
                        generator.WriteLine("return new SessionMessageFactory(sessionDescription);");
                    }
                    generator.WriteLine();

                    // GetMessageTypes - return all message types
                    using (generator.BeginBlock("public IEnumerable<Type> GetMessageTypes()"))
                    {
                        using (generator.BeginBlock("return new Type[]"))
                        {
                            var messages = FixDefinitions.Message.Select(kv => kv.Value.Name).Distinct().ToList();
                            for (int i = 0; i < messages.Count; i++)
                            {
                                var comma = i < messages.Count - 1 ? "," : "";
                                generator.WriteLine($"typeof({messages[i]}){comma}");
                            }
                        }
                        generator.WriteLine(";");
                    }
                    generator.WriteLine();

                    // GetMessageTypeByMsgType
                    using (generator.BeginBlock("public Type? GetMessageTypeByMsgType(string msgType)"))
                    {
                        using (generator.BeginBlock("return msgType switch"))
                        {
                            // Use distinct by MsgType to avoid duplicate switch cases
                            var seenMsgTypes = new HashSet<string>();
                            foreach (var kv in FixDefinitions.Message)
                            {
                                var m = kv.Value;
                                if (seenMsgTypes.Add(m.MsgType))
                                {
                                    generator.WriteLine($"\"{m.MsgType}\" => typeof({m.Name}),");
                                }
                            }
                            generator.WriteLine("_ => null");
                        }
                        generator.WriteLine(";");
                    }
                }
            }

            var contents = generator.ToString();
            var path = Path.Join(Options.BackingTypeOutputPath, "TypeSystemProvider.cs");
            WriteFile(path, contents);
        }

        private void GenerateProjectFile()
        {
            var assemblyName = _modularOptions.AssemblyName ?? "PureFix.Types.Generated";

            // Generate either PackageReference or ProjectReference based on options
            var itemGroup = _modularOptions.UsePackageReferences
                ? $@"  <ItemGroup>
    <PackageReference Include=""PureFix.Types.Core"" Version=""{_modularOptions.PackageVersion}"" />
    <PackageReference Include=""PureFix.Types"" Version=""{_modularOptions.PackageVersion}"" />
  </ItemGroup>"
                : $@"  <ItemGroup>
    <ProjectReference Include=""{_modularOptions.CoreProjectPath}"" />
    <ProjectReference Include=""{_modularOptions.TypesProjectPath}"" />
  </ItemGroup>";

            var csproj = $@"<Project Sdk=""Microsoft.NET.Sdk"">

  <PropertyGroup>
    <TargetFramework>net9.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
    <GenerateDocumentationFile>false</GenerateDocumentationFile>
    <RootNamespace>{GetNamespace()}</RootNamespace>
  </PropertyGroup>

{itemGroup}

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

                switch (field)
                {
                    case ContainedSimpleField simpleField:
                        HandleFieldProperty(generator, parentPath, i, simpleField, last, next, parentTypeName);
                        break;
                    case ContainedComponentField componentField:
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

                        break;
                    }
                    case ContainedGroupField groupField:
                        HandleGroupProperty(generator, parentPath, i, groupField, parentTypeName);
                        // DON'T call Process() for groups - they're nested inline
                        break;
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
            HandleFieldProperty(generator, parentPath, index, field, last, next, null);
        }

        private void HandleFieldProperty(
            CodeGenerator generator,
            string parentPath,
            int index,
            ContainedSimpleField field,
            ContainedField? last,
            ContainedField? next,
            string? parentTypeName)
        {
            var propName = GetFieldPropertyName(field.Name, parentTypeName);
            var typeName = GetCSharpType(field);

            generator.WriteLine($"[TagDetails(Tag = {field.Definition.Tag}, Type = TagType.{field.Definition.TagType}, Offset = {index}, Required = {(field.Required ? "true" : "false")})]");
            generator.WriteLine($"public {typeName}? {propName} {{get; set;}}");
            generator.WriteLine();

            // Generate enum values if this field has enums
            if (field.Definition.Enums is not null && field.Definition.Enums.Count > 0)
            {
                GenerateEnumValues(typeName, field.Definition.TagType, field.Name, field.Definition.Enums);
            }
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

        private static string GetGroupPropertyName(ContainedGroupField field, string? parentTypeName)
        {
            // Convert "NoOrders" -> "Orders"
            var name = field.Name;
            string propertyName;

            if (name.StartsWith("No") && name.Length > 2)
            {
                propertyName = name.Substring(2);
            }
            else
            {
                // Group name doesn't start with "No" - property would collide with nested class name
                // e.g., LinesOfText group -> LinesOfText class, need LinesOfTextItems property
                propertyName = name + "Items";
            }

            // Check if property name would conflict with parent type name
            if (!string.IsNullOrEmpty(parentTypeName) && propertyName == parentTypeName)
            {
                return propertyName + "Items";
            }
            return propertyName;
        }

        private static string GetFieldPropertyName(string fieldName, string? parentTypeName)
        {
            // Check for naming collision with parent type
            if (!string.IsNullOrEmpty(parentTypeName) && fieldName == parentTypeName)
            {
                // Append "Value" suffix to avoid collision
                return fieldName + "Value";
            }
            return fieldName;
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

        private static void GenerateIsValid(CodeGenerator generator, IContainedSet set)
        {
            using (generator.BeginBlock("bool IFixValidator.IsValid(in FixValidatorConfig config)"))
            {
                var conditions = new List<string>();

                foreach (var field in set.Fields)
                {
                    if (field is ContainedComponentField component)
                    {
                        switch (component.Name)
                        {
                            case "StandardHeader":
                                conditions.Add($"(!config.CheckStandardHeader || ({component.Name} is not null && ((IFixValidator){component.Name}).IsValid(in config)))");
                                break;
                            case "StandardTrailer":
                                conditions.Add($"(!config.CheckStandardTrailer || ({component.Name} is not null && ((IFixValidator){component.Name}).IsValid(in config)))");
                                break;
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
                    switch (field)
                    {
                        case ContainedSimpleField simple:
                        {
                            var propName = GetFieldPropertyName(simple.Name, parentTypeName);
                            var writeMethod = GetWriteMethod(simple.Definition.TagType);
                            generator.WriteLine($"if ({propName} is not null) writer.{writeMethod}({simple.Definition.Tag}, {propName}{(NeedsValueAccess(simple.Definition.TagType) ? ".Value" : "")});");
                            break;
                        }
                        case ContainedComponentField component:
                            generator.WriteLine($"if ({component.Name} is not null) ((IFixEncoder){component.Name}).Encode(writer);");
                            break;
                        case ContainedGroupField group:
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

                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Determines if a message/component is "simple" - no groups and only standard header/trailer components.
        /// Simple messages can skip expensive structure parsing in the generated Parse method.
        /// </summary>
        private static bool IsSimpleMessage(IContainedSet set)
        {
            foreach (var field in set.Fields)
            {
                switch (field)
                {
                    case ContainedGroupField:
                        return false; // Has groups = needs full structure parsing
                    case ContainedComponentField component:
                        // Only StandardHeader/StandardTrailer allowed for simple messages
                        if (component.Name != "StandardHeader" && component.Name != "StandardTrailer")
                            return false;
                        break;
                }
            }
            return true;
        }

        private void GenerateParse(CodeGenerator generator, IContainedSet set, string? parentTypeName)
        {
            bool isSimple = IsSimpleMessage(set);

            using (generator.BeginBlock("void IFixParser.Parse(IMessageView? view)"))
            {
                generator.WriteLine("if (view is null) return;");
                generator.WriteLine();

                foreach (var field in set.Fields)
                {
                    switch (field)
                    {
                        case ContainedSimpleField simple:
                        {
                            var propName = GetFieldPropertyName(simple.Name, parentTypeName);
                            var getMethod = GetGetMethod(simple.Definition.TagType);
                            generator.WriteLine($"{propName} = view.{getMethod}({simple.Definition.Tag});");
                            break;
                        }
                        case ContainedComponentField component:
                        {
                            // For simple messages, skip header/trailer to avoid triggering structure parsing
                            if (isSimple && (component.Name == "StandardHeader" || component.Name == "StandardTrailer"))
                            {
                                // Don't call GetView - header/trailer stay null
                                // This avoids expensive EnsureStructureParsed() for simple admin messages
                                break;
                            }

                            using (generator.BeginBlock($"if (view.GetView(\"{component.Name}\") is IMessageView view{component.Name})"))
                            {
                                generator.WriteLine($"{component.Name} = new();");
                                generator.WriteLine($"((IFixParser){component.Name}).Parse(view{component.Name});");
                            }

                            break;
                        }
                        case ContainedGroupField group:
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

                            break;
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
                            : field is ContainedSimpleField simpleField
                                ? GetFieldPropertyName(simpleField.Name, parentTypeName)
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
                    switch (field)
                    {
                        case ContainedComponentField component:
                            generator.WriteLine($"((IFixReset?){component.Name})?.Reset();");
                            break;
                        case ContainedGroupField groupField:
                        {
                            var propertyName = GetGroupPropertyName(groupField, parentTypeName);
                            generator.WriteLine($"{propertyName} = null;");
                            break;
                        }
                        case ContainedSimpleField simpleField:
                        {
                            var propertyName = GetFieldPropertyName(simpleField.Name, parentTypeName);
                            generator.WriteLine($"{propertyName} = null;");
                            break;
                        }
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

        private static string GetGetMethod(TagType type)
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

        private static bool NeedsValueAccess(TagType type)
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

        private void GenerateEnumValues(string csharpBaseType, TagType tagType, string fieldName, IReadOnlyDictionary<string, FieldEnum> enums)
        {
            var builder = new CodeGenerator();

            var enumName = $"{fieldName}Values";

            // Write core usings
            builder.WriteLine("using System;");
            builder.WriteLine();

            using (builder.BeginBlock($"namespace {Options.BackingTypeNamespace}"))
            using (builder.BeginBlock($"public static class {enumName}"))
            {
                var usedNames = new HashSet<string>();

                foreach (var field in enums.Values)
                {
                    var value = tagType switch
                    {
                        TagType.String => $"\"{field.Key}\"",
                        TagType.Boolean => (field.Key == "Y" ? "true" : "false"),
                        _ => field.Key.ToString()
                    };

                    var constantName = field.Description.UnderscoreToCamelCase();

                    // Some descriptions start with a number, which won't map to a valid C# symbol
                    if (char.IsDigit(constantName[0])) constantName = "_" + constantName;

                    // Handle duplicate constant names by appending the FIX value
                    var originalName = constantName;
                    var counter = 1;
                    while (usedNames.Contains(constantName))
                    {
                        // Append sanitized version of the actual FIX value to make it unique
                        var suffix = field.Key.Replace("-", "").Replace(".", "").Replace(" ", "");
                        constantName = $"{originalName}{suffix}";

                        // If still duplicate, add a counter
                        if (usedNames.Contains(constantName))
                        {
                            constantName = $"{originalName}{counter++}";
                        }
                    }

                    usedNames.Add(constantName);

                    builder.WriteLine($"public const {csharpBaseType} {constantName} = {value};");
                }
            }

            var code = builder.ToString();
            var enumsDir = Path.Join(Options.BackingTypeOutputPath, "Enums");
            Directory.CreateDirectory(enumsDir);
            var filename = Path.Join(enumsDir, $"{enumName}.cs");
            WriteFile(filename, code);
        }

        private static string FormatFixVersion(FixVersion version)
        {
            return version switch
            {
                FixVersion.FIX40 => "FIX.4.0",
                FixVersion.FIX41 => "FIX.4.1",
                FixVersion.FIX42 => "FIX.4.2",
                FixVersion.FIX43 => "FIX.4.3",
                FixVersion.FIX44 => "FIX.4.4",
                FixVersion.FIX50 => "FIX.5.0",
                FixVersion.FIX50SP1 => "FIX.5.0SP1",
                FixVersion.FIX50SP2 => "FIX.5.0SP2",
                FixVersion.FIXML50SP2 => "FIXML.5.0SP2",
                FixVersion.FIXT11 => "FIXT.1.1",
                _ => version.ToString()
            };
        }
    }
}
