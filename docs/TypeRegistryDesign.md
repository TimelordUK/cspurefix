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
      "messageStorePath": "MessageStore/JPMFX",
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
      "messageStorePath": "MessageStore/BBG",
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
      "messageStorePath": "MessageStore/FIX44",
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
      "messageStorePath": "MessageStore/FIX50SP2",
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

## Server Parsing Scenarios

### Scenario 1: Bulk File Parsing with Tag-Level Access

**Use Case**: Upload multi-day FIX log files (up to 50MB), parse at tag level, filter by MsgType, return to React GUI

**Key Requirements**:
- **Performance**: Tag-level parsing without creating full message objects
- **Filtering**: Regex filter on tag 35 (MsgType) - e.g., only `35=8` (ExecutionReport)
- **Memory Efficiency**: Stream through file, don't load all messages
- **Lazy Parsing**: On message click, parse specific message to full JSON

**API Design**:

```csharp
/// <summary>
/// Parse FIX file at tag level - returns tag positions, not objects
/// </summary>
public interface IFixFileParser
{
    /// <summary>
    /// Parse file and return tag-level representation for each message
    /// </summary>
    /// <param name="fileContents">FIX file contents (multiple days concatenated)</param>
    /// <param name="filter">Optional regex filter on tag 35 (e.g., "^8$" for ExecutionReport only)</param>
    /// <param name="registrationName">Dictionary to use (or null for auto-detect)</param>
    /// <returns>Enumerable of tag-level messages</returns>
    IEnumerable<TagLevelMessage> ParseFile(
        ReadOnlySpan<byte> fileContents,
        string? msgTypeFilter = null,
        string? registrationName = null);

    /// <summary>
    /// Parse single message at specific offset to full JSON
    /// </summary>
    string ParseMessageToJson(ReadOnlySpan<byte> fileContents, int offset, string? registrationName = null);
}

/// <summary>
/// Tag-level message representation for server mode
/// </summary>
public class TagLevelMessage
{
    /// <summary>Offset of message in file</summary>
    public int Offset { get; set; }

    /// <summary>Length of message</summary>
    public int Length { get; set; }

    /// <summary>MsgType (tag 35)</summary>
    public string? MsgType { get; set; }

    /// <summary>MsgSeqNum (tag 34)</summary>
    public int? MsgSeqNum { get; set; }

    /// <summary>SendingTime (tag 52)</summary>
    public DateTime? SendingTime { get; set; }

    /// <summary>SenderCompID (tag 49) - used for auto-detection</summary>
    public string? SenderCompId { get; set; }

    /// <summary>All tags as key-value pairs (tag number -> string value)</summary>
    public Dictionary<int, string> Tags { get; set; }

    /// <summary>Structure information from AsciiParser</summary>
    public Structure? Structure { get; set; }
}
```

**Usage Example**:

```csharp
// Server endpoint: /api/fix/parse-file
[HttpPost]
public async Task<IActionResult> ParseFile(IFormFile file, string? msgTypeFilter)
{
    var fileContents = await ReadFileAsync(file);
    
    // Auto-detect dictionary from first message's SenderCompID
    var parser = _registry.CreateFixFileParser();
    var messages = parser.ParseFile(fileContents, msgTypeFilter);
    
    // Return tag-level data to React GUI
    return Json(messages.Select(m => new {
        offset = m.Offset,
        msgType = m.MsgType,
        seqNum = m.MsgSeqNum,
        sendingTime = m.SendingTime,
        tags = m.Tags,
        structures = m.Structure?.ToString()
    }));
}

// GUI clicks on message at offset 12345
[HttpPost]
public async Task<IActionResult> ParseMessage(IFormFile file, int offset)
{
    var fileContents = await ReadFileAsync(file);
    var parser = _registry.CreateFixFileParser();
    
    // Full parse just for this message
    var json = parser.ParseMessageToJson(fileContents, offset);
    
    return Content(json, "application/json");
}
```

### Scenario 2: SQL-CLI Field Extraction

**Use Case**: Upload FIX file, select specific tags to extract, return flat JSON array for SQL querying with window functions

**Key Requirements**:
- **Field Selection**: User specifies which tags to extract
- **Repeated Groups**: Join repeated tags (e.g., parties) with "|" separator
- **Flat Format**: CSV-like structure for SQL engine consumption
- **Aggregation Ready**: Suitable for lag/lead/window functions without database load

**API Design**:

```csharp
/// <summary>
/// Extract selected fields from FIX file for SQL-CLI consumption
/// </summary>
public interface IFixFieldExtractor
{
    /// <summary>
    /// Extract specified tags from all messages in file
    /// </summary>
    /// <param name="fileContents">FIX file contents</param>
    /// <param name="tagSelectors">Tags to extract (e.g., [35, 55, 54, 38, 44])</param>
    /// <param name="msgTypeFilter">Optional filter on MsgType</param>
    /// <param name="registrationName">Dictionary to use</param>
    /// <returns>Flat array of field values suitable for SQL queries</returns>
    List<Dictionary<string, string>> ExtractFields(
        ReadOnlySpan<byte> fileContents,
        int[] tagSelectors,
        string? msgTypeFilter = null,
        string? registrationName = null);
}
```

**Usage Example**:

```csharp
// Server endpoint: /api/fix/extract-fields
[HttpPost]
public async Task<IActionResult> ExtractFields(
    IFormFile file,
    [FromForm] int[] tags,  // e.g., [35,55,54,38,44,447] = MsgType, Symbol, Side, Qty, Price, PartyRole
    [FromForm] string? msgTypeFilter)
{
    var fileContents = await ReadFileAsync(file);
    var extractor = _registry.CreateFieldExtractor();
    
    // Extract selected tags, joining repeated tags with "|"
    var rows = extractor.ExtractFields(fileContents, tags, msgTypeFilter);
    
    // Return flat JSON array
    // Example row: { "35": "8", "55": "AAPL", "54": "1", "38": "1000", "44": "150.50", "447": "D|1|4" }
    return Json(rows);
}

// SQL-CLI can then query:
// $ sql-cli query http://localhost:5000/api/fix/extract-fields \
//   "SELECT Symbol, Side, Qty, Price, 
//           LAG(Price) OVER (PARTITION BY Symbol ORDER BY SendingTime) as PrevPrice
//    FROM fix_data 
//    WHERE MsgType = '8' 
//    GROUP BY Symbol"
```

## Persistent Message Store

### Overview

The message store serves three purposes:
1. **Sequence Number Persistence**: Track last sent/received sequence numbers for session recovery
2. **Message Persistence**: Store sent messages for resend request servicing (tag 2=ResendRequest)
3. **Session Recovery**: Recover state after application restart without seq reset

### Message Store Requirements

**Current State**: `FixMsgMemoryStore` - in-memory only, lost on restart

**Required**: `FixMsgFileStore` - file-based persistence like QuickFix

**Key Features**:
- Persist per session (SenderCompID/TargetCompID pair)
- Store location configured in TypeRegistry.json
- Support manual editing (flat files) for out-of-sync recovery
- Fast lookup by sequence number for resend requests
- Atomic updates (sequence numbers and messages)
- Session state: `(OurSeqNum, TheirSeqNum, LastReceivedTime)`

### Message Store Interface Extensions

```csharp
namespace PureFix.Transport.Store;

/// <summary>
/// Extended message store interface with session state persistence
/// </summary>
public interface IFixMsgStore
{
    // Existing methods...
    Task<FixMsgStoreState> Clear();
    Task<FixMsgStoreState> GetState();
    Task<FixMsgStoreState> Put(IFixMsgStoreRecord record);
    Task<IFixMsgStoreRecord?> Get(int seq);
    Task<bool> Exists(int seq);
    Task<IFixMsgStoreRecord?[]> GetSeqNumRange(int from, int? to = null);

    // NEW: Session sequence number tracking
    /// <summary>
    /// Get last outbound sequence number we sent
    /// </summary>
    Task<int> GetLastSentSeqNum();

    /// <summary>
    /// Get last inbound sequence number we received
    /// </summary>
    Task<int> GetLastReceivedSeqNum();

    /// <summary>
    /// Set outbound sequence number (after sending message)
    /// </summary>
    Task SetLastSentSeqNum(int seqNum);

    /// <summary>
    /// Set inbound sequence number (after receiving message)
    /// </summary>
    Task SetLastReceivedSeqNum(int seqNum);

    /// <summary>
    /// Get last time we received a message from peer (for heartbeat monitoring)
    /// </summary>
    Task<DateTime?> GetLastReceivedTime();

    /// <summary>
    /// Set last received time
    /// </summary>
    Task SetLastReceivedTime(DateTime timestamp);

    /// <summary>
    /// Reset all sequence numbers and clear messages (seq reset scenario)
    /// </summary>
    Task ResetSequences();
}

/// <summary>
/// Session state for recovery
/// </summary>
public class FixSessionState
{
    public int LastSentSeqNum { get; set; }
    public int LastReceivedSeqNum { get; set; }
    public DateTime? LastReceivedTime { get; set; }
    public string SenderCompId { get; set; }
    public string TargetCompId { get; set; }
    public DateTime CreatedTime { get; set; }
    public DateTime LastModifiedTime { get; set; }
}
```

### File-Based Message Store Implementation

**File Structure** (per session):

```
MessageStore/
  JPMFX/
    init-comp.accept-comp/
      session.meta          # Session state (sequence numbers)
      messages.dat          # Binary message storage
      messages.idx          # Index for fast seq lookup
```

**session.meta** format:
```json
{
  "senderCompId": "init-comp",
  "targetCompId": "accept-comp",
  "lastSentSeqNum": 1234,
  "lastReceivedSeqNum": 5678,
  "lastReceivedTime": "2025-01-09T16:30:45Z",
  "createdTime": "2025-01-08T09:00:00Z",
  "lastModifiedTime": "2025-01-09T16:30:45Z"
}
```

**messages.dat** format (append-only binary):
```
[SeqNum:4][MsgType:1][Timestamp:8][Length:4][EncodedMessage:Length]
[SeqNum:4][MsgType:1][Timestamp:8][Length:4][EncodedMessage:Length]
...
```

**messages.idx** format (for fast lookup):
```
[SeqNum:4][Offset:8]
[SeqNum:4][Offset:8]
...
```

### FixMsgFileStore Implementation

```csharp
namespace PureFix.Transport.Store;

/// <summary>
/// File-based message store for session recovery
/// </summary>
public class FixMsgFileStore : IFixMsgStore
{
    private readonly string _storePath;
    private readonly string _sessionId;
    private readonly ReaderWriterLockSlim _lock = new();
    private FixSessionState _sessionState;
    private readonly Dictionary<int, long> _messageIndex = new();

    public FixMsgFileStore(string storePath, string senderCompId, string targetCompId)
    {
        _storePath = storePath;
        _sessionId = $"{senderCompId}.{targetCompId}";
        Directory.CreateDirectory(Path.Combine(_storePath, _sessionId));
        LoadSessionState();
        LoadMessageIndex();
    }

    private string SessionMetaPath => Path.Combine(_storePath, _sessionId, "session.meta");
    private string MessagesDataPath => Path.Combine(_storePath, _sessionId, "messages.dat");
    private string MessagesIndexPath => Path.Combine(_storePath, _sessionId, "messages.idx");

    public async Task<int> GetLastSentSeqNum()
    {
        _lock.EnterReadLock();
        try
        {
            return _sessionState.LastSentSeqNum;
        }
        finally
        {
            _lock.ExitReadLock();
        }
    }

    public async Task<int> GetLastReceivedSeqNum()
    {
        _lock.EnterReadLock();
        try
        {
            return _sessionState.LastReceivedSeqNum;
        }
        finally
        {
            _lock.ExitReadLock();
        }
    }

    public async Task SetLastSentSeqNum(int seqNum)
    {
        _lock.EnterWriteLock();
        try
        {
            _sessionState.LastSentSeqNum = seqNum;
            _sessionState.LastModifiedTime = DateTime.UtcNow;
            await PersistSessionState();
        }
        finally
        {
            _lock.ExitWriteLock();
        }
    }

    public async Task SetLastReceivedSeqNum(int seqNum)
    {
        _lock.EnterWriteLock();
        try
        {
            _sessionState.LastReceivedSeqNum = seqNum;
            _sessionState.LastReceivedTime = DateTime.UtcNow;
            _sessionState.LastModifiedTime = DateTime.UtcNow;
            await PersistSessionState();
        }
        finally
        {
            _lock.ExitWriteLock();
        }
    }

    public async Task<FixMsgStoreState> Put(IFixMsgStoreRecord record)
    {
        _lock.EnterWriteLock();
        try
        {
            // Append to messages.dat
            using var dataStream = File.Open(MessagesDataPath, FileMode.Append, FileAccess.Write);
            var offset = dataStream.Position;
            
            // Write: [SeqNum:4][MsgType:1][Timestamp:8][Length:4][EncodedMessage:Length]
            await WriteIntAsync(dataStream, record.SeqNum);
            await dataStream.WriteAsync(Encoding.ASCII.GetBytes(record.MsgType)[0..1]);
            await WriteLongAsync(dataStream, record.Timestamp.Ticks);
            var encoded = Encoding.ASCII.GetBytes(record.Encoded ?? "");
            await WriteIntAsync(dataStream, encoded.Length);
            await dataStream.WriteAsync(encoded);
            
            // Update index
            _messageIndex[record.SeqNum] = offset;
            
            // Append to messages.idx
            using var indexStream = File.Open(MessagesIndexPath, FileMode.Append, FileAccess.Write);
            await WriteIntAsync(indexStream, record.SeqNum);
            await WriteLongAsync(indexStream, offset);
            
            return await GetState();
        }
        finally
        {
            _lock.ExitWriteLock();
        }
    }

    public async Task<IFixMsgStoreRecord?> Get(int seq)
    {
        _lock.EnterReadLock();
        try
        {
            if (!_messageIndex.TryGetValue(seq, out var offset))
                return null;

            using var dataStream = File.Open(MessagesDataPath, FileMode.Open, FileAccess.Read);
            dataStream.Seek(offset, SeekOrigin.Begin);
            
            var seqNum = await ReadIntAsync(dataStream);
            var msgType = Encoding.ASCII.GetString(new byte[] { (byte)dataStream.ReadByte() });
            var ticks = await ReadLongAsync(dataStream);
            var length = await ReadIntAsync(dataStream);
            var buffer = new byte[length];
            await dataStream.ReadAsync(buffer);
            var encoded = Encoding.ASCII.GetString(buffer);
            
            return new FixMsgStoreRecord(msgType, new DateTime(ticks), seqNum, encoded);
        }
        finally
        {
            _lock.ExitReadLock();
        }
    }

    public async Task ResetSequences()
    {
        _lock.EnterWriteLock();
        try
        {
            _sessionState.LastSentSeqNum = 0;
            _sessionState.LastReceivedSeqNum = 0;
            _messageIndex.Clear();
            
            // Delete message files
            File.Delete(MessagesDataPath);
            File.Delete(MessagesIndexPath);
            
            await PersistSessionState();
        }
        finally
        {
            _lock.ExitWriteLock();
        }
    }

    private void LoadSessionState()
    {
        if (File.Exists(SessionMetaPath))
        {
            var json = File.ReadAllText(SessionMetaPath);
            _sessionState = JsonHelper.FromJson<FixSessionState>(json) ?? new FixSessionState();
        }
        else
        {
            _sessionState = new FixSessionState
            {
                SenderCompId = _sessionId.Split('.')[0],
                TargetCompId = _sessionId.Split('.')[1],
                CreatedTime = DateTime.UtcNow,
                LastModifiedTime = DateTime.UtcNow
            };
        }
    }

    private async Task PersistSessionState()
    {
        var json = JsonHelper.ToJson(_sessionState);
        await File.WriteAllTextAsync(SessionMetaPath, json);
    }

    private void LoadMessageIndex()
    {
        if (!File.Exists(MessagesIndexPath))
            return;

        using var indexStream = File.Open(MessagesIndexPath, FileMode.Open, FileAccess.Read);
        while (indexStream.Position < indexStream.Length)
        {
            var seqNum = ReadIntAsync(indexStream).Result;
            var offset = ReadLongAsync(indexStream).Result;
            _messageIndex[seqNum] = offset;
        }
    }

    private static async Task WriteIntAsync(Stream stream, int value)
    {
        await stream.WriteAsync(BitConverter.GetBytes(value));
    }

    private static async Task WriteLongAsync(Stream stream, long value)
    {
        await stream.WriteAsync(BitConverter.GetBytes(value));
    }

    private static async Task<int> ReadIntAsync(Stream stream)
    {
        var buffer = new byte[4];
        await stream.ReadAsync(buffer);
        return BitConverter.ToInt32(buffer);
    }

    private static async Task<long> ReadLongAsync(Stream stream)
    {
        var buffer = new byte[8];
        await stream.ReadAsync(buffer);
        return BitConverter.ToInt64(buffer);
    }
}
```

### Session Recovery Flow

```csharp
// Application startup after restart
public async Task<IFixSession> RecoverSession(string registrationName)
{
    var registration = _registry.GetByName(registrationName);
    
    // Create file-based message store
    var messageStore = new FixMsgFileStore(
        registration.MessageStorePath,
        sessionDescription.SenderCompID,
        sessionDescription.TargetCompID);
    
    // Get persisted sequence numbers
    var lastSent = await messageStore.GetLastSentSeqNum();
    var lastReceived = await messageStore.GetLastReceivedSeqNum();
    
    Log.Info($"Recovering session: LastSent={lastSent}, LastReceived={lastReceived}");
    
    // Create session with recovered state
    var session = new FixSession(config, logFactory, parser, encoder, queue, clock);
    session.SetSequenceNumbers(lastSent + 1, lastReceived + 1);
    
    // Logon without seq reset
    await session.LogonAsync(resetSeqNum: false);
    
    return session;
}

// Handle ResendRequest (tag 2)
public async Task HandleResendRequest(int beginSeqNo, int endSeqNo)
{
    // Retrieve messages from store
    var messages = await _messageStore.GetSeqNumRange(beginSeqNo, endSeqNo);
    
    foreach (var message in messages)
    {
        if (message != null)
        {
            // Resend with PossDupFlag=Y
            await _session.ResendMessage(message.Encoded, possDupFlag: true);
        }
        else
        {
            // Gap - send SequenceReset
            await _session.SendSequenceReset(gapFillFlag: true, newSeqNo: ...);
        }
    }
}
```

### TypeRegistry Integration

Update `TypeRegistration` class:

```csharp
public class TypeRegistration
{
    // ... existing properties ...
    
    /// <summary>Path to message store directory for this dictionary</summary>
    public string MessageStorePath { get; set; }
}
```

Update `ITypeRegistry` interface:

```csharp
public interface ITypeRegistry
{
    // ... existing methods ...
    
    /// <summary>Create a message store for a registration</summary>
    IFixMsgStore CreateMessageStore(string registrationName, string senderCompId, string targetCompId);
    
    /// <summary>Create a file parser for server mode</summary>
    IFixFileParser CreateFixFileParser();
    
    /// <summary>Create a field extractor for SQL-CLI mode</summary>
    IFixFieldExtractor CreateFieldExtractor();
}
```

