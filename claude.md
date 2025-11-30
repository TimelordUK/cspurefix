# CSPureFix - C# FIX Protocol Engine

## Project Overview

CSPureFix is a high-performance C# FIX (Financial Information eXchange) protocol engine that generates strongly-typed message definitions from QuickFIX XML dictionaries. Unlike traditional FIX engines that use runtime parsing, CSPureFix generates compile-time types specific to each FIX dictionary, providing type safety and better performance.

**Origin:** Port of the mature TypeScript/Node.js library [jspurefix](https://www.npmjs.com/package/jspurefix) (used in production at hedge funds).

**Current Status:** Pre-release, .NET 9. Modular type system complete. Working on XML parser robustness and session persistence.

**Design Goals:**
- Good memory usage and performance (not ultra-low-latency, but at least as good as QuickFIX)
- Type safety via compile-time generated types
- Support for 20+ broker-specific dictionaries simultaneously
- Production-ready session management with sequence number persistence

---

## Key Classes Reference

### Runtime Parsing Pipeline

#### `AsciiParser` (`PureFix.Buffer/Ascii/AsciiParser.cs`)
Takes a stream of bytes from a transport (socket, memory buffer) and parses FIX messages. Continues parsing until it reaches end of message (checksum tag 10). Creates a set of `TagPos` representing the position of each tag=value pair in the buffer.

```
Input: Raw bytes from socket/buffer
Output: TagPos[] - positions of all tags in the message
```

#### `AsciiSegmentParser` (`PureFix.Buffer/Ascii/AsciiSegmentParser.cs`)
Creates structure from parsed tags. Iterates over the set of `TagPos` and works out where all segments (components, groups) are located.

**Important Design Note:** Root-level components may not have contiguous tags. For example, an Instrument component at root level may have its tags scattered throughout the message (not in one direct span). The segment parser handles this via fragment detection and `TagIndex` lookups.

```
Input: TagPos[] + MessageDefinition
Output: Structure - tree of SegmentDescription objects
```

#### `AsciiView` (`PureFix.Buffer/Ascii/AsciiView.cs`)
Represents a single parsed message. Provides low-level access to extract field values and types. Recursive in nature - you can request a view from another view. For example, requesting an Instrument component returns just that slice (handling non-contiguous segments).

```csharp
var view = parser.GetView();
var instrument = view.GetComponent("Instrument"); // Returns view of just Instrument
var symbol = instrument.GetString(55); // Tag 55 = Symbol
```

#### `Structure` and `SegmentDescription` (`PureFix.Buffer/Segment/`)
`Structure` holds the parsed tag positions plus a list of `SegmentDescription` objects representing the message hierarchy (header, body components, groups, trailer).

### Session Management

#### `AsciiSession` (`PureFix.Transport/Ascii/AsciiSession.cs`)
Abstract class managing a real FIX session. Handles:
- Login/logout handshake
- Heartbeat/TestRequest exchange
- Sequence number tracking
- Resend request handling
- Session state machine

**Needs Enhancement:** Validation of illegal enums, illegal message types, persistent sequence numbers.

#### `IFixMsgStore` (`PureFix.Transport/Store/IFixMsgStore.cs`)
Interface for storing sent messages (for resend requests). Current implementations:
- `FixMsgMemoryStore` - in-memory only
- File-based store needed for production (QuickFIX-style flat file)

**TODO:** Add sequence number persistence methods:
- `GetLastSentSeqNum()` / `SetLastSentSeqNum()`
- `GetLastReceivedSeqNum()` / `SetLastReceivedSeqNum()`

### XML Dictionary Parsing

#### `QuickFixXmlFileParser` (`PureFix.Dictionary/Parser/QuickFix/QuickFixXmlFileParser.cs`)
Parses QuickFIX-format XML data dictionaries. Uses a graph-based approach to handle forward references (e.g., a component referencing another component not yet parsed).

**Parsing Phases:**
1. Parse fields (with enums) → `SimpleFieldDefinition`
2. Parse components → `ComponentFieldDefinition`
3. Parse header/trailer
4. Parse messages → `MessageDefinition`
5. Work queue processes forward references
6. `IndexVisitor` flattens hierarchies and builds lookup indices

**Needs Enhancement:**
- Duplicate detection (fields, components, messages)
- Character sanitization for weird enum names (numbers, symbols)
- Good error messages with line numbers
- "Did you mean X?" suggestions for undefined references

#### `IFixDefinitions` / `FixDefinitions` (`PureFix.Dictionary/Definition/`)
Represents all types within an XML dictionary. Provides lookups:
- `Message[msgType]` - get message definition by MsgType (e.g., "D" for NewOrderSingle)
- `Component[name]` - get component by name
- `TagToSimple[tag]` - get field definition by tag number
- `Simple[name]` - get field by name

#### `ContainedFieldSet` (`PureFix.Dictionary/Contained/ContainedFieldSet.cs`)
Represents everything needed to perfectly describe a message, component, or group. Recursive structure - fields may themselves be components or groups containing more fields.

Key indices maintained:
- `Fields` - ordered list of all fields
- `Groups` / `Components` - named lookups
- `ContainedTag` - any tag at any depth
- `FlattenedTag` - all tags in order
- `LocalTag` - tags at this level only (not nested)
- `TagToField` - maps tag to its containing field

### Type Generation

#### `ModularGenerator` (`PureFix.Dictionary/Compiler/ModularGenerator.cs`)
Generates C# types from parsed definitions. Creates one assembly per dictionary with:
- Nested group classes (no more namespace pollution)
- Clean folder structure (Messages/, Components/, Enums/)
- References to `PureFix.Types.Core` for interfaces

### Example Applications

#### Skeleton (`Examples/PureFix.Examples.Skeleton/`)
Simple example showing how to build an application. Uses DI to inject dependencies:
- `IFixConfig` - session configuration
- `IMessageParser` / `IMessageEncoder`
- `IFixMsgStore` - message store
- Factory classes for message creation

---

## Project Structure

```
cspurefix/
├── PureFix.Buffer/              # Low-level buffer operations
│   └── Ascii/
│       ├── AsciiParser.cs       # Byte stream → TagPos[]
│       ├── AsciiSegmentParser.cs # TagPos[] → Structure
│       └── AsciiView.cs         # Message view with field access
│
├── PureFix.Dictionary/          # XML parsing & type generation
│   ├── Parser/QuickFix/         # XML parser (graph-based)
│   ├── Definition/              # FixDefinitions, field/message defs
│   ├── Contained/               # ContainedFieldSet and variants
│   └── Compiler/                # ModularGenerator
│
├── PureFix.Transport/           # Session management
│   ├── Ascii/AsciiSession.cs    # FIX session state machine
│   └── Store/IFixMsgStore.cs    # Message store interface
│
├── PureFix.Types.Core/          # Shared interfaces & attributes
│   ├── IFixMessage, IFixComponent, IFixGroup
│   └── TagDetailsAttribute, etc.
│
├── generated-types/             # Per-dictionary assemblies
│   ├── PureFix.Types.FIX44/
│   ├── PureFix.Types.FIX44UnitTest/  # Reduced test dictionary
│   └── PureFix.Types.FIX50SP2/
│
├── PureFix.Test.ModularTypes/   # Unit tests (250 passing)
└── Examples/                    # Example applications
```

---

## Current Development Status

### Completed
- [x] Modular type system (one assembly per dictionary)
- [x] Nested groups (no more `OrderNoAllocs` pollution)
- [x] Core type extraction to `PureFix.Types.Core`
- [x] .NET 9 upgrade
- [x] 250 unit tests passing
- [x] Basic session management (login, heartbeat, resend)

### In Progress
- [ ] **XML Parser Robustness**
  - Duplicate field/component detection
  - Character sanitization for weird enum names (starting with numbers, symbols)
  - Good error messages with line numbers
  - "Did you mean X?" suggestions

### Planned
- [ ] **Persistent Sequence Numbers** (QuickFIX-style)
  - Flat file: `session.meta` with seq numbers
  - Broker resets at agreed time (e.g., 22:30)
  - Reconnect with same seq numbers if no reset

- [ ] **Type Registry** (for server mode)
  - Upload FIX log → auto-detect broker by tag 49 (SenderCompID)
  - Parse with correct type system
  - Docker container with web endpoint

- [ ] **Session Validation**
  - Illegal enum value detection
  - Illegal message type detection
  - Sequence number gap handling

---

## XML Parser Edge Cases (TODO)

Real broker dictionaries contain:
1. **Names starting with numbers** - `"1stLegSymbol"` → invalid C# identifier
2. **Incompatible enum names** - symbols like `N/A`, `+`, `-` in enum values
3. **Duplicate definitions** - same tag or name defined twice
4. **Forward references** - component A references component B not yet parsed
5. **Missing definitions** - message references undefined component

Test dictionary: `test-dictionaries/` should contain edge cases for validation.

---

## Architecture Diagrams

### Message Parsing Flow
```
┌─────────────┐    ┌─────────────┐    ┌──────────────────┐
│ Socket/     │───▶│ AsciiParser │───▶│ AsciiSegmentParser│
│ Buffer      │    │ (TagPos[])  │    │ (Structure)       │
└─────────────┘    └─────────────┘    └──────────────────┘
                                              │
                                              ▼
                   ┌─────────────┐    ┌──────────────────┐
                   │ Generated   │◀───│ AsciiView        │
                   │ IFixMessage │    │ (field access)   │
                   └─────────────┘    └──────────────────┘
```

### Type Generation Flow
```
┌─────────────┐    ┌──────────────────┐    ┌─────────────────┐
│ FIX44.xml   │───▶│ QuickFixXmlParser│───▶│ FixDefinitions  │
└─────────────┘    │ (graph-based)    │    │ (all types)     │
                   └──────────────────┘    └─────────────────┘
                                                   │
                                                   ▼
                   ┌──────────────────┐    ┌─────────────────┐
                   │ C# Source Files  │◀───│ ModularGenerator│
                   │ (.cs)            │    │ (nested groups) │
                   └──────────────────┘    └─────────────────┘
                           │
                           ▼
                   ┌──────────────────┐
                   │ PureFix.Types.   │
                   │ FIX44.dll        │
                   └──────────────────┘
```

---

## Key Differences from QuickFIX

| Feature | QuickFIX | CSPureFix |
|---------|----------|-----------|
| **Type System** | Runtime dictionary | Compile-time generated types |
| **Performance** | Runtime parsing | Pre-compiled, fast |
| **Type Safety** | Dictionary-based | Full C# type checking |
| **Multi-Dialect** | Single dictionary | 20+ dictionaries as separate DLLs |
| **Language** | C++ | C# (.NET 9) |
| **Origin** | Open source | Port of jspurefix (TypeScript) |

---

## Related Documentation

- `docs/TypeRegistryDesign.md` - Server mode, auto-detection, persistent store design
- `docs/IMPROVEMENT_PLAN.md` - Phased 8-week roadmap
- `docs/MULTI_ASSEMBLY_GENERATOR_DESIGN.md` - Per-dictionary assembly architecture
- `docs/CODEBASE_EXPLORATION.md` - Deep technical exploration

---

## Related Links

- **Original TypeScript Project:** https://www.npmjs.com/package/jspurefix
- **FIX Protocol:** https://www.fixtrading.org/
- **QuickFIX:** http://quickfixengine.org/
