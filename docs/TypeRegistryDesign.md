# Type Registry System Design

## Overview

The Type Registry system enables PureFix to support multiple FIX protocol variants (broker-specific dialects, custom extensions) by allowing applications to register their own generated type assemblies. This is critical for production use where different counterparties may have different FIX dictionary requirements.

## Problem Statement

Currently, PureFix has hardcoded references to generated types (FIX44, FIX50SP2). In production:
- Different brokers use different FIX dialects (JPMFX, BBGFX, etc.)
- Applications need to generate custom types from broker-provided XML dictionaries
- Server applications need to parse FIX files without knowing the dialect in advance
- The core PureFix library should not depend on application-specific types

## Requirements

### Functional Requirements

1. **Dynamic Type Registration**: Applications can register one or more FIX type assemblies at runtime
2. **Dictionary Auto-Detection**: Server mode can identify the correct dictionary by inspecting SenderCompID (tag 49)
3. **Flexible Configuration**: Applications specify which dictionary to use via JSON config
4. **Multiple Dialects**: Support 20+ different broker dialects simultaneously (e.g., server parsing files from multiple sources)
5. **Type Factory Creation**: Registry provides appropriate factories (IFixMessageFactory, ISessionMessageFactory) for each dialect
6. **Parser Creation**: Registry provides parsers configured for specific dialects

### Non-Functional Requirements

1. **Performance**: Type lookup should be O(1) - dictionary-based
2. **Thread Safety**: Registry must be thread-safe for server scenarios
3. **Assembly Loading**: Support loading types from external assemblies
4. **NuGet Friendly**: Core library published to NuGet should not reference application types
5. **Reflection-Based**: Use reflection to discover types in registered assemblies

## Architecture

### Core Components

```
┌─────────────────────────────────────────────────────────────┐
│                     Application Layer                        │
│  ┌────────────┐  ┌────────────┐  ┌────────────┐            │
│  │ Example    │  │ Server     │  │ Initiator  │            │
│  │ Apps       │  │ Mode       │  │ Mode       │            │
│  └────────────┘  └────────────┘  └────────────┘            │
│         │                │                │                  │
│         └────────────────┴────────────────┘                  │
│                          │                                   │
│                          ▼                                   │
│                 ┌─────────────────┐                          │
│                 │ Type Registry   │                          │
│                 │ (Core Library)  │                          │
│                 └─────────────────┘                          │
│                          │                                   │
│         ┌────────────────┼────────────────┐                 │
│         ▼                ▼                ▼                 │
│  ┌────────────┐   ┌────────────┐   ┌────────────┐          │
│  │ JPMFX      │   │ BBGFX      │   │ FIX44      │          │
│  │ Types      │   │ Types      │   │ Types      │          │
│  │ (Generated)│   │ (Generated)│   │ (Generated)│          │
│  └────────────┘   └────────────┘   └────────────┘          │
└─────────────────────────────────────────────────────────────┘
```

### Registry Metadata Structure

**File**: `TypeRegistry.json` (provided by application)

```json
{
  "version": "1.0",
  "registrations": [
    {
      "name": "jpmfx",
      "displayName": "JPMorgan FIX 4.4",
      "dictionaryPath": "Dictionaries/JPMFX44.xml",
      "assemblyPath": "GeneratedTypes/PureFix.Types.JPMFX.dll",
      "senderCompIds": ["JPMORGAN", "JPM-FX", "JPMFX"],
      "targetCompIds": ["CLIENT"],
      "version": "FIX.4.4",
      "rootNamespace": "PureFix.Types.JPMFX",
      "enabled": true
    },
    {
      "name": "bbgfx",
      "displayName": "Bloomberg FIX 4.4",
      "dictionaryPath": "Dictionaries/BBG44.xml",
      "assemblyPath": "GeneratedTypes/PureFix.Types.BBG.dll",
      "senderCompIds": ["BLOOMBERG", "BBG-FX"],
      "targetCompIds": ["CLIENT"],
      "version": "FIX.4.4",
      "rootNamespace": "PureFix.Types.BBG",
      "enabled": true
    },
    {
      "name": "fix44",
      "displayName": "Standard FIX 4.4",
      "dictionaryPath": "Data/FIX44.xml",
      "assemblyPath": "generated-types/PureFix.Types.FIX44/bin/Release/net8.0/PureFix.Types.FIX44.dll",
      "senderCompIds": ["*"],
      "targetCompIds": ["*"],
      "version": "FIX.4.4",
      "rootNamespace": "PureFix.Types.FIX44",
      "enabled": true,
      "isDefault": true
    },
    {
      "name": "fix50sp2",
      "displayName": "Standard FIX 5.0 SP2",
      "dictionaryPath": "Data/FIX50SP2.xml",
      "assemblyPath": "generated-types/PureFix.Types.FIX50SP2/bin/Release/net8.0/PureFix.Types.FIX50SP2.dll",
      "senderCompIds": ["*"],
      "targetCompIds": ["*"],
      "version": "FIX.5.0SP2",
      "rootNamespace": "PureFix.Types.FIX50SP2",
      "enabled": true
    }
  ]
}
```

### Type Registration Class

```csharp
namespace PureFix.Types.Registry;

/// <summary>
/// Metadata describing a registered FIX type system
/// </summary>
public class TypeRegistration
{
    /// <summary>Unique identifier for this registration (e.g., "jpmfx")</summary>
    public string Name { get; set; }

    /// <summary>Human-readable name (e.g., "JPMorgan FIX 4.4")</summary>
    public string DisplayName { get; set; }

    /// <summary>Path to QuickFix XML dictionary file</summary>
    public string DictionaryPath { get; set; }

    /// <summary>Path to assembly containing generated types</summary>
    public string AssemblyPath { get; set; }

    /// <summary>SenderCompIDs that should use this dictionary (tag 49)</summary>
    public List<string> SenderCompIds { get; set; }

    /// <summary>TargetCompIDs that should use this dictionary (tag 56)</summary>
    public List<string> TargetCompIds { get; set; }

    /// <summary>FIX version string (e.g., "FIX.4.4")</summary>
    public string Version { get; set; }

    /// <summary>Root namespace in the assembly (e.g., "PureFix.Types.JPMFX")</summary>
    public string RootNamespace { get; set; }

    /// <summary>Whether this registration is active</summary>
    public bool Enabled { get; set; }

    /// <summary>Whether this is the default/fallback dictionary</summary>
    public bool IsDefault { get; set; }
}
```

### Type Registry Service

```csharp
namespace PureFix.Types.Registry;

/// <summary>
/// Central registry for managing multiple FIX type systems
/// </summary>
public interface ITypeRegistry
{
    /// <summary>Load registrations from JSON file</summary>
    void LoadFromFile(string path);

    /// <summary>Register a type system programmatically</summary>
    void Register(TypeRegistration registration);

    /// <summary>Get registration by name</summary>
    TypeRegistration? GetByName(string name);

    /// <summary>Find registration by SenderCompID (tag 49)</summary>
    TypeRegistration? FindBySenderCompId(string senderCompId);

    /// <summary>Find registration by SenderCompID and TargetCompID</summary>
    TypeRegistration? FindByCompIds(string senderCompId, string targetCompId);

    /// <summary>Get the default registration</summary>
    TypeRegistration? GetDefault();

    /// <summary>Get all enabled registrations</summary>
    IEnumerable<TypeRegistration> GetAllEnabled();

    /// <summary>Create a message factory for a registration</summary>
    IFixMessageFactory CreateMessageFactory(string registrationName);

    /// <summary>Create a session message factory for a registration</summary>
    ISessionMessageFactory CreateSessionMessageFactory(string registrationName, ISessionDescription sessionDescription);

    /// <summary>Create a parser for a registration</summary>
    IMessageParser CreateParser(string registrationName, IFixDefinitions definitions);

    /// <summary>Load definitions for a registration</summary>
    IFixDefinitions LoadDefinitions(string registrationName);
}
```

### Type Factory Provider

Each registered type assembly must provide a factory class that implements:

```csharp
namespace PureFix.Types;

/// <summary>
/// Interface that all generated type systems must implement
/// Allows the registry to create factories dynamically
/// </summary>
public interface ITypeSystemProvider
{
    /// <summary>Get the FIX version this type system supports</summary>
    string GetVersion();

    /// <summary>Create a message factory instance</summary>
    IFixMessageFactory CreateMessageFactory();

    /// <summary>Create a session message factory instance</summary>
    ISessionMessageFactory CreateSessionMessageFactory(ISessionDescription sessionDescription);

    /// <summary>Get all message types in this system</summary>
    IEnumerable<Type> GetMessageTypes();

    /// <summary>Get message type by MsgType tag value</summary>
    Type? GetMessageTypeByMsgType(string msgType);
}
```

Generated assemblies will include:

```csharp
// In PureFix.Types.FIX44/TypeSystemProvider.cs (auto-generated)
namespace PureFix.Types.FIX44;

public class TypeSystemProvider : ITypeSystemProvider
{
    public string GetVersion() => "FIX.4.4";

    public IFixMessageFactory CreateMessageFactory()
    {
        return new FixMessageFactory();
    }

    public ISessionMessageFactory CreateSessionMessageFactory(ISessionDescription sessionDescription)
    {
        return new SessionMessageFactory(sessionDescription);
    }

    public IEnumerable<Type> GetMessageTypes()
    {
        return new Type[]
        {
            typeof(Heartbeat),
            typeof(Logon),
            typeof(NewOrderSingle),
            // ... all message types
        };
    }

    public Type? GetMessageTypeByMsgType(string msgType)
    {
        return msgType switch
        {
            "0" => typeof(Heartbeat),
            "A" => typeof(Logon),
            "D" => typeof(NewOrderSingle),
            // ... all mappings
            _ => null
        };
    }
}
```

### Session Configuration Integration

Update session JSON config to reference registry:

```json
{
  "Application": {
    "Name": "test_client",
    "Dictionary": "Data/FIX44.xml",
    "DictionaryName": "fix44",  // NEW: Reference to registry
    "FixVersion": "FIX.4.4"
  },
  "SenderCompID": "init-comp",
  "TargetCompID": "accept-comp",
  "HeartBtInt": 30,
  "ResetSeqNumFlag": true
}
```

## Use Cases

### Use Case 1: Application Startup with Registry

```csharp
// Application startup
var registry = new TypeRegistry();
registry.LoadFromFile("TypeRegistry.json");

// Session creation
var config = FixConfig.MakeConfigFromPaths(
    dictRootPath,
    sessionConfigPath,
    registry); // Pass registry

// Registry automatically resolves "fix44" from session config
// and sets appropriate factories on config
```

### Use Case 2: Server Mode - Auto-Detect Dictionary

```csharp
// Server receives FIX file upload
var fileContents = await Request.ReadFileAsync();

// Peek at first message to get SenderCompID
var senderCompId = FixFileAnalyzer.ExtractSenderCompId(fileContents);

// Find matching registration
var registration = registry.FindBySenderCompId(senderCompId);
if (registration == null)
{
    registration = registry.GetDefault(); // Fallback
}

// Create parser for this dictionary
var definitions = registry.LoadDefinitions(registration.Name);
var parser = registry.CreateParser(registration.Name, definitions);

// Parse all messages
var messages = parser.ParseFile(fileContents);
```

### Use Case 3: Multi-Dialect Server

```csharp
// Server handling connections from multiple brokers
public class MultiDialectServer
{
    private readonly ITypeRegistry _registry;

    public async Task HandleConnection(TcpClient client)
    {
        // Read first message to identify counterparty
        var firstMessage = await ReadFirstMessageAsync(client);
        var senderCompId = firstMessage.GetTag(49);

        // Lookup appropriate dialect
        var registration = _registry.FindBySenderCompId(senderCompId);

        // Create session with correct types
        var sessionFactory = _registry.CreateSessionMessageFactory(
            registration.Name,
            sessionDescription);

        var messageFactory = _registry.CreateMessageFactory(registration.Name);

        // Create session
        var session = new FixSession(config, logFactory, parser, encoder, queue, clock);

        // Handle messages with correct types
        await session.ProcessMessagesAsync();
    }
}
```

### Use Case 4: Application with Custom Types

```csharp
// Application using broker-specific types
// 1. Generate types: dotnet run --project PureFix.Dictionary -- -i Dictionaries/JPMFX44.xml -o GeneratedTypes/JPMFX

// 2. Create TypeRegistry.json with JPMFX entry

// 3. Application code
var registry = new TypeRegistry();
registry.LoadFromFile("TypeRegistry.json");

// Session config specifies "jpmfx"
var config = FixConfig.MakeConfigFromPaths(
    dictRootPath,
    "session-jpm.json",  // Has "DictionaryName": "jpmfx"
    registry);

// Everything works with JPM-specific message types
```

## Implementation Plan

### Phase 1: Core Registry Infrastructure (Week 1)
- [ ] Create `PureFix.Types.Registry` namespace
- [ ] Implement `TypeRegistration` class
- [ ] Implement `ITypeRegistry` interface
- [ ] Implement `TypeRegistry` class with JSON loading
- [ ] Add unit tests for registry operations

### Phase 2: Type System Provider Interface (Week 1)
- [ ] Create `ITypeSystemProvider` interface
- [ ] Update ModularGenerator to generate `TypeSystemProvider` class
- [ ] Regenerate FIX44 and FIX50SP2 with provider
- [ ] Test reflection-based type discovery

### Phase 3: Factory Creation (Week 2)
- [ ] Implement `CreateMessageFactory` in registry
- [ ] Implement `CreateSessionMessageFactory` in registry
- [ ] Implement `CreateParser` in registry
- [ ] Update `FixConfig.MakeConfigFromPaths` to accept registry
- [ ] Add registry resolution logic

### Phase 4: Auto-Detection (Week 2)
- [ ] Implement `FixFileAnalyzer` for peeking at SenderCompID
- [ ] Implement `FindBySenderCompId` matching logic
- [ ] Support wildcard matching ("*")
- [ ] Add unit tests for auto-detection

### Phase 5: Integration (Week 3)
- [ ] Update example applications to use registry
- [ ] Create sample TypeRegistry.json files
- [ ] Update session JSON schema to include DictionaryName
- [ ] Add integration tests with multiple dictionaries

### Phase 6: Documentation (Week 3)
- [ ] Document registry JSON format
- [ ] Create tutorial for adding custom dictionaries
- [ ] Document type generation workflow
- [ ] Add examples for server mode

### Phase 7: Performance & Threading (Week 4)
- [ ] Add thread-safety to registry
- [ ] Optimize type lookup performance
- [ ] Add caching for loaded assemblies
- [ ] Benchmark registry operations

## Technical Considerations

### Assembly Loading

```csharp
// Use AssemblyLoadContext for isolation
var loadContext = new AssemblyLoadContext("PureFix.JPMFX", isCollectible: false);
var assembly = loadContext.LoadFromAssemblyPath(assemblyPath);

// Find provider type
var providerType = assembly.GetTypes()
    .FirstOrDefault(t => typeof(ITypeSystemProvider).IsAssignableFrom(t));

// Create instance
var provider = (ITypeSystemProvider)Activator.CreateInstance(providerType);
```

### Caching Strategy

```csharp
private readonly ConcurrentDictionary<string, AssemblyLoadContext> _loadedContexts;
private readonly ConcurrentDictionary<string, ITypeSystemProvider> _providers;
private readonly ConcurrentDictionary<string, IFixDefinitions> _definitions;
```

### SenderCompID Matching

1. Exact match: `senderCompId == registration.SenderCompIds[i]`
2. Wildcard match: `registration.SenderCompIds.Contains("*")`
3. Prefix match: `senderCompId.StartsWith(registration.SenderCompIds[i])`
4. Fallback: Use registration with `IsDefault = true`

### Error Handling

- Missing assembly: Log warning, use default types
- Invalid metadata: Throw `TypeRegistryException` with details
- Type mismatch: Validate assembly implements required interfaces
- Circular dependencies: Detect and prevent

## Testing Strategy

### Unit Tests
- Registry loading from JSON
- Registration lookup by name
- SenderCompID matching logic
- Factory creation via reflection
- Definition loading

### Integration Tests
- End-to-end with multiple dictionaries
- Auto-detection from FIX file
- Session creation with custom types
- Message parsing with custom types

### Performance Tests
- Benchmark registry lookup: < 1μs per lookup
- Benchmark factory creation: < 100μs per factory
- Memory usage with 20 registered types: < 50MB

## Migration Path

### Existing Code
```csharp
// Old way (hardcoded)
var factory = new Types.FIX44.FixMessageFactory();
var sessionFactory = new Fix44SessionMessageFactory(sessionDescription);
```

### New Code
```csharp
// New way (registry-based)
var registry = new TypeRegistry();
registry.LoadFromFile("TypeRegistry.json");

var factory = registry.CreateMessageFactory("fix44");
var sessionFactory = registry.CreateSessionMessageFactory("fix44", sessionDescription);
```

### Backward Compatibility
- Keep existing hardcoded approach working
- Make registry optional for simple use cases
- Provide default registry with FIX44/FIX50SP2 if none specified

## NuGet Packaging Strategy

```
PureFix.Core (on NuGet)
├── PureFix.Types (core interfaces)
├── PureFix.Types.Registry (new)
├── PureFix.Dictionary
├── PureFix.Buffer
└── PureFix.Transport

PureFix.StandardTypes (on NuGet)
├── PureFix.Types.FIX44
└── PureFix.Types.FIX50SP2

PureFix.Examples (NOT on NuGet)
├── Custom dictionaries
├── Custom TypeRegistry.json
└── Example applications
```

Applications using PureFix:
1. Reference `PureFix.Core` from NuGet
2. Optionally reference `PureFix.StandardTypes` from NuGet
3. Generate their own types from broker XMLs
4. Create their own TypeRegistry.json
5. Deploy assemblies together

## Open Questions

1. **Assembly versioning**: How to handle multiple versions of same dictionary?
2. **Hot reload**: Should registry support reloading without restart?
3. **Discovery**: Should registry auto-discover assemblies in a folder?
4. **Validation**: Should registry validate generated assemblies at registration?
5. **Serialization**: Should registry cache parsed definitions to disk?

## Next Steps

1. Review and refine this design document
2. Create prototype of core registry implementation
3. Update code generator to emit TypeSystemProvider
4. Test with JPMFX example dictionary
5. Gather feedback from stakeholders
