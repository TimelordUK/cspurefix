# Multi-Assembly Generator Design

## Problem Statement

### Current Architecture Issues

**Single Assembly Problems:**
- **3,797 C# files** in PureFix.Types (4 FIX versions)
- **9.9MB compiled DLL** currently
- With **20 broker dictionaries**: ~76,000 files, ~200MB DLL
- **Linux compilation crashes** with large assemblies
- **Long build times** (minutes for incremental builds)
- **IDE performance** degrades (IntelliSense, navigation)
- **All-or-nothing deployment** (can't load just one dictionary)

**Namespace Pollution:**
- 3,797 types in flat namespace
- Groups have parent prefix: `OrderNoAllocs`, `ExecutionReportNoAllocs`
- Hard to navigate, cluttered IntelliSense

**Real-World Impact:**
- Production hedge fund: 20+ broker-specific dictionaries
- Each broker has slight variations
- Cannot compile all in one assembly
- Manual workarounds required

---

## Proposed Solution: Multi-Assembly Generator

### Architecture

```
PureFix.Types.Core.dll                    # Shared base types (once)
  ├─ IFixMessage, IFixComponent, IFixGroup
  ├─ TagDetailsAttribute, ComponentAttribute
  └─ Base interfaces & utilities

PureFix.Types.FIX44.dll                   # Standard FIX 4.4
  ├─ References: Core
  ├─ 500-600 types
  └─ ~2-3MB

PureFix.Types.BrokerA.dll                 # Broker A custom
  ├─ References: Core
  ├─ 800 types (their custom fields)
  └─ ~2.5MB

PureFix.Types.BrokerB.dll                 # Broker B custom
  ├─ References: Core
  ├─ 750 types
  └─ ~2.3MB
```

**Benefits:**
- ✅ Each dictionary = separate DLL (~2-3MB each)
- ✅ Compile one dictionary at a time
- ✅ Lazy load dictionaries at runtime
- ✅ Deploy only needed dictionaries
- ✅ Parallel compilation possible
- ✅ IDE handles smaller projects better

---

## Design: ModularGenerator

### Key Principles

1. **One Assembly Per Dictionary**
   - Each dictionary generates to its own project
   - Project references PureFix.Types.Core
   - Independent compilation

2. **Nested Groups** (solve namespace pollution)
   - Groups nested within parent message/component
   - No more `OrderNoAllocs` - use `Order.NoAllocs`
   - Clean namespaces

3. **Sanitized Names** (solve dash problem)
   - Dictionary filename → valid namespace
   - `FIX44-Core.xml` → `FIX44Core`
   - Remove/replace: `-`, `.`, spaces

4. **Keep Existing Generator Intact**
   - New class: `ModularGenerator`
   - Old `MessageGenerator` unchanged
   - Side-by-side during transition

---

## Implementation Plan

### Phase 1: Core Assembly Extraction

**Create PureFix.Types.Core project:**

```
PureFix.Types.Core/
├── PureFix.Types.Core.csproj
├── Interfaces/
│   ├── IFixMessage.cs
│   ├── IFixComponent.cs
│   ├── IFixGroup.cs
│   ├── IFixValidator.cs
│   ├── IFixEncoder.cs
│   ├── IFixParser.cs
│   ├── IFixLookup.cs
│   └── IFixReset.cs
├── Attributes/
│   ├── MessageTypeAttribute.cs
│   ├── TagDetailsAttribute.cs
│   ├── ComponentAttribute.cs
│   └── GroupAttribute.cs
├── Base/
│   ├── FixVersion.cs
│   ├── TagType.cs
│   └── MsgTag.cs
└── Utilities/
    └── (shared utilities)
```

**Extract from PureFix.Types:**
- Move interfaces to Core
- Move attributes to Core
- Move base enums/types to Core
- Keep PureFix.Types referencing Core (backward compatibility)

---

### Phase 2: ModularGenerator Implementation

**Location:** `PureFix.Dictionary/Compiler/ModularGenerator.cs`

**Class Structure:**

```csharp
namespace PureFix.Dictionary.Compiler
{
    /// <summary>
    /// Generates FIX types into separate assemblies per dictionary
    /// with nested groups and sanitized namespaces
    /// </summary>
    public class ModularGenerator : GeneratorBase
    {
        private readonly ModularGeneratorOptions _modularOptions;

        public ModularGenerator(
            IFixDefinitions definitions,
            ModularGeneratorOptions options)
            : base(options.OutputPath, definitions, options.BaseOptions)
        {
            _modularOptions = options;
        }

        public override void Process()
        {
            // 1. Create assembly project structure
            CreateAssemblyProject();

            // 2. Generate messages with nested groups
            GenerateMessages();

            // 3. Generate global components
            GenerateComponents();

            // 4. Generate enum value classes
            GenerateEnums();

            // 5. Generate factory
            GenerateFactory();

            // 6. Create .csproj file
            GenerateProjectFile();
        }

        private void CreateAssemblyProject()
        {
            // Create folder structure:
            // OutputPath/
            //   AssemblyName/
            //     AssemblyName.csproj
            //     Messages/
            //     Components/
            //     Enums/
        }

        protected override string GenerateType(MessageDefinition message)
        {
            // Generate message with nested group classes
            var generator = new CodeGenerator();

            generator.WriteLine($"namespace {GetSanitizedNamespace()}");
            generator.BeginBlock();

            // Message class
            generator.WriteLine($"[MessageType(\"{message.MsgType}\", FixVersion.{...})]");
            generator.WriteLine($"public sealed partial class {message.Name} : IFixMessage");
            generator.BeginBlock();

            // Properties for fields/components
            GenerateMessageFields(generator, message);

            // Nested group classes
            foreach (var group in GetGroups(message))
            {
                GenerateNestedGroup(generator, group, depth: 1);
            }

            // Methods
            GenerateSupportingFunctions(generator, message);

            generator.EndBlock(); // class
            generator.EndBlock(); // namespace

            return generator.ToString();
        }

        private void GenerateNestedGroup(
            CodeGenerator generator,
            ContainedGroupField group,
            int depth)
        {
            // Nested group class
            generator.WriteLine($"public sealed partial class {group.Name} : IFixGroup");
            generator.BeginBlock();

            // Group fields
            GenerateGroupFields(generator, group);

            // Recursively nested groups (groups within groups)
            foreach (var nestedGroup in GetGroups(group.Definition))
            {
                GenerateNestedGroup(generator, nestedGroup, depth + 1);
            }

            // Methods
            GenerateSupportingFunctions(generator, group.Definition);

            generator.EndBlock(); // class
        }

        private string GetSanitizedNamespace()
        {
            // FIX44-Core.xml → FIX44Core
            // broker.custom.xml → BrokerCustom
            var name = Path.GetFileNameWithoutExtension(_modularOptions.DictionaryPath);
            return SanitizeName(name);
        }

        private string SanitizeName(string name)
        {
            return name
                .Replace("-", "")
                .Replace(".", "")
                .Replace(" ", "")
                .Replace("_", "");
        }
    }
}
```

**Options Class:**

```csharp
public class ModularGeneratorOptions
{
    /// <summary>
    /// Base output path (e.g., "generated-assemblies/")
    /// </summary>
    public string OutputPath { get; set; } = "";

    /// <summary>
    /// Assembly name (e.g., "PureFix.Types.FIX44Core")
    /// </summary>
    public string AssemblyName { get; set; } = "";

    /// <summary>
    /// Path to dictionary XML file (used for namespace generation)
    /// </summary>
    public string DictionaryPath { get; set; } = "";

    /// <summary>
    /// Reference to PureFix.Types.Core.csproj
    /// </summary>
    public string CoreAssemblyPath { get; set; } = "";

    /// <summary>
    /// Base options for GeneratorBase
    /// </summary>
    public Options BaseOptions { get; set; } = new();
}
```

---

### Phase 3: Project File Generation

**Generate .csproj for each dictionary:**

```csharp
private void GenerateProjectFile()
{
    var csproj = $@"<Project Sdk=""Microsoft.NET.Sdk"">
  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
    <GenerateDocumentationFile>false</GenerateDocumentationFile>
  </PropertyGroup>

  <ItemGroup>
    <ProjectReference Include=""../../PureFix.Types.Core/PureFix.Types.Core.csproj"" />
  </ItemGroup>
</Project>";

    var path = Path.Join(
        _modularOptions.OutputPath,
        _modularOptions.AssemblyName,
        $"{_modularOptions.AssemblyName}.csproj");

    File.WriteAllText(path, csproj);
}
```

---

## Output Structure

### Before (Current - Single Assembly):

```
PureFix.Types/
├── FIX42/QuickFix/*.cs (800 files)
├── FIX43/QuickFix/*.cs (850 files)
├── FIX44/QuickFix/*.cs (900 files)
├── FIX50SP2/QuickFix/*.cs (1247 files)
└── PureFix.Types.csproj (3797 files, 9.9MB DLL)
```

### After (New - Multi-Assembly):

```
PureFix.Types.Core/
├── PureFix.Types.Core.csproj
└── (interfaces, attributes, base types)

PureFix.Types.FIX44Core/
├── PureFix.Types.FIX44Core.csproj
├── Messages/
│   ├── Heartbeat.cs
│   ├── Logon.cs
│   ├── ExecutionReport.cs  # Contains nested groups
│   └── ... (10 messages)
├── Components/
│   ├── StandardHeaderComponent.cs
│   ├── InstrumentComponent.cs
│   └── ... (global components)
└── Enums/
    ├── SideValues.cs
    └── ... (enum value classes)

PureFix.Types.BrokerA/
├── PureFix.Types.BrokerA.csproj
├── Messages/ (broker's custom messages)
├── Components/
└── Enums/
```

---

## Example: Nested Groups

### Current (Flat):

```csharp
// PureFix.Types/FIX44/QuickFix/ExecutionReport.cs
public class ExecutionReport : IFixMessage
{
    public ExecutionReportNoContraBrokers[]? ContraBrokers { get; set; }
}

// PureFix.Types/FIX44/QuickFix/Types/ExecutionReportNoContraBrokers.cs
public class ExecutionReportNoContraBrokers : IFixGroup
{
    public string? ContraBroker { get; set; }
}
```

### New (Nested):

```csharp
// PureFix.Types.FIX44Core/Messages/ExecutionReport.cs
public sealed partial class ExecutionReport : IFixMessage
{
    // Nested group class
    public sealed partial class NoContraBrokers : IFixGroup
    {
        public string? ContraBroker { get; set; }
        // Methods...
    }

    // Property uses nested type
    public NoContraBrokers[]? ContraBrokers { get; set; }
}
```

**Benefits:**
- Clear ownership (group belongs to message)
- No name collision (`Order.NoAllocs` vs `ExecutionReport.NoAllocs`)
- Better IntelliSense (grouped by parent)
- Reduced flat namespace pollution

---

## Migration Strategy

### Phase 1: Core Assembly (Week 1)
1. Create PureFix.Types.Core project
2. Extract interfaces and attributes
3. Update PureFix.Types to reference Core
4. Ensure existing code still works

### Phase 2: New Generator (Week 2-3)
1. Implement ModularGenerator
2. Add namespace sanitization
3. Implement nested group generation
4. Test with FIX44-Core.xml

### Phase 3: Validation (Week 3)
1. Generate test dictionary with new generator
2. Compile generated assembly
3. Run round-trip tests
4. Compare with old generator output

### Phase 4: Production Trial (Week 4)
1. Generate one broker dictionary with new generator
2. Test in isolation
3. Compare behavior with old generator
4. Performance benchmarks

### Phase 5: Gradual Migration (Week 5-8)
1. Regenerate dictionaries one at a time
2. Keep old generator available
3. Side-by-side deployment
4. Deprecate old generator gradually

---

## Runtime Loading

### Dictionary Registry

```csharp
public class FixDictionaryRegistry
{
    private readonly Dictionary<string, IFixMessageFactory> _factories = new();

    public void RegisterDictionary(string version, Assembly assembly)
    {
        // Scan assembly for IFixMessageFactory
        var factoryType = assembly.GetTypes()
            .FirstOrDefault(t => typeof(IFixMessageFactory).IsAssignableFrom(t));

        if (factoryType != null)
        {
            var factory = (IFixMessageFactory)Activator.CreateInstance(factoryType)!;
            _factories[version] = factory;
        }
    }

    public void LoadFromDirectory(string path)
    {
        foreach (var dllPath in Directory.GetFiles(path, "PureFix.Types.*.dll"))
        {
            var assembly = Assembly.LoadFrom(dllPath);
            var version = ExtractVersion(Path.GetFileName(dllPath));
            RegisterDictionary(version, assembly);
        }
    }

    public IFixMessageFactory GetFactory(string version)
    {
        return _factories[version];
    }
}
```

### Usage:

```csharp
// Application startup
var registry = new FixDictionaryRegistry();

// Load only dictionaries needed
registry.LoadFromDirectory("./fix-dictionaries");

// Or load specific
registry.RegisterDictionary("FIX44Core",
    Assembly.Load("PureFix.Types.FIX44Core"));

// Use at runtime
var factory = registry.GetFactory("FIX44Core");
var message = factory.ToFixMessage(view);
```

---

## Benefits Summary

### Compilation
- ✅ **10x faster builds** (small assemblies compile in parallel)
- ✅ **No Linux crashes** (2-3MB DLLs vs 200MB)
- ✅ **Incremental compilation** (change one dictionary, rebuild one DLL)

### Development
- ✅ **Better IDE performance** (smaller projects)
- ✅ **Cleaner namespaces** (nested groups)
- ✅ **Easier navigation** (logical grouping)

### Deployment
- ✅ **Selective deployment** (only needed dictionaries)
- ✅ **Lazy loading** (load dictionaries on demand)
- ✅ **Smaller deployments** (3MB vs 200MB)

### Maintenance
- ✅ **Isolated changes** (update one broker without affecting others)
- ✅ **Parallel development** (different dictionaries in parallel)
- ✅ **Easier testing** (test one dictionary at a time)

---

## Implementation Tasks

### Task 1: Extract Core Assembly (2-3 days)
- [ ] Create PureFix.Types.Core project
- [ ] Move interfaces
- [ ] Move attributes
- [ ] Update references
- [ ] Test existing code works

### Task 2: Implement ModularGenerator (3-4 days)
- [ ] Create ModularGenerator class
- [ ] Implement namespace sanitization
- [ ] Implement nested group generation
- [ ] Add project file generation
- [ ] Unit tests

### Task 3: Test with FIX44-Core (1-2 days)
- [ ] Generate FIX44-Core with ModularGenerator
- [ ] Verify compilation
- [ ] Run round-trip tests
- [ ] Compare with MessageGenerator output

### Task 4: Documentation & Examples (1 day)
- [ ] Update usage docs
- [ ] Add examples
- [ ] Migration guide

---

## Success Criteria

- [ ] Core assembly extracted and working
- [ ] ModularGenerator generates compilable code
- [ ] Nested groups work correctly
- [ ] Namespace sanitization works
- [ ] Each dictionary → separate DLL (~2-3MB)
- [ ] All tests pass with new generator
- [ ] Performance equivalent or better
- [ ] Old MessageGenerator still works (unchanged)

---

## Future Enhancements

### Shared Component Assembly
For very common components (Instrument, Parties, etc.):
```
PureFix.Types.CommonComponents.dll
```
Referenced by multiple dictionaries to reduce duplication.

### Code Sharing
Common patterns extracted to reduce generated code size.

### Build Optimization
- Parallel compilation of multiple dictionaries
- Caching of unchanged dictionaries
- Incremental generation

---

_Last Updated: November 2, 2025_
