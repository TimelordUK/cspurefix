# CSPureFix - C# FIX Protocol Engine

## Project Overview

CSPureFix is a high-performance C# FIX (Financial Information eXchange) protocol engine that generates strongly-typed message definitions from QuickFIX XML dictionaries. Unlike traditional FIX engines that use runtime parsing, CSPureFix generates compile-time types specific to each FIX dictionary, providing type safety and better performance.

**Origin:** Port of the mature TypeScript/Node.js library [jspurefix](https://www.npmjs.com/package/jspurefix) (used in production at hedge funds).

**Current Status:** Pre-release. Working on robustness improvements before general open-source release.

---

## Architecture

### Three-Layer Architecture

```
┌─────────────────────────────────────────────────────────────┐
│                     XML Dictionary                          │
│                  (QuickFix or Repo format)                  │
└────────────────────────┬────────────────────────────────────┘
                         │
                         ▼
              ┌──────────────────────┐
              │   Dictionary Parser   │
              │  (QuickFixXmlParser)  │
              └──────────┬────────────┘
                         │
                         ▼
┌─────────────────────────────────────────────────────────────┐
│              LAYER 1: Definition Layer                       │
│  Metadata about FIX elements (fields, components, groups)   │
│  - SimpleFieldDefinition                                     │
│  - ComponentFieldDefinition                                  │
│  - GroupFieldDefinition                                      │
│  - MessageDefinition                                         │
└────────────────────────┬────────────────────────────────────┘
                         │
                         ▼
┌─────────────────────────────────────────────────────────────┐
│              LAYER 2: Contained Layer                        │
│  Runtime representations with position/context               │
│  - ContainedFieldSet (base)                                  │
│  - ContainedSimpleField                                      │
│  - ContainedComponentField                                   │
│  - ContainedGroupField                                       │
└────────────────────────┬────────────────────────────────────┘
                         │
                         ▼
              ┌──────────────────────┐
              │   Type Generator      │
              │  (MessageGenerator)   │
              └──────────┬────────────┘
                         │
                         ▼
┌─────────────────────────────────────────────────────────────┐
│              LAYER 3: Generated Layer                        │
│  C# types implementing FIX interfaces                        │
│  - IFixMessage (messages)                                    │
│  - IFixComponent (components)                                │
│  - IFixGroup (repeating groups)                              │
└─────────────────────────────────────────────────────────────┘
```

---

## Project Structure

### Core Projects

```
cspurefix/
├── PureFix.Dictionary/         # XML parsing & type generation
│   ├── Parser/
│   │   ├── QuickFix/           # QuickFix XML parser
│   │   ├── Repo/               # FIX Repository parser
│   │   └── FieldEnum.cs        # ⚠️ Character sanitization (has bug)
│   ├── Definition/             # Metadata layer
│   ├── Contained/              # Runtime representation
│   └── Compiler/               # Type generator
│
├── PureFix.Types/              # Generated FIX types (3,797 files)
│   ├── FIX42/QuickFix/
│   ├── FIX43/QuickFix/
│   ├── FIX44/QuickFix/
│   └── FIX50SP2/QuickFix/
│       ├── *.cs                # Message types
│       └── Types/              # Components, groups, enums
│
├── PureFix.Buffer/             # Low-level buffer operations
├── PureFix.Transport/          # Network transport layer
├── PureFix.MessageStore/       # Message persistence
├── PureFix.Data/               # Data models
├── PureFix.Test/               # Unit tests
├── PureFix.ConsoleApp/         # CLI tool
├── PureFix.LogMessageParser/   # Parse FIX logs
└── SeeFixServer/               # FIX server implementation
```

### Key Entry Points

- **Console App:** `PureFix.App/Program.cs` - CLI for generation and parsing
- **Parser Entry:** `PureFix.Dictionary/Parser/QuickFix/QuickFixXmlFileParser.cs`
- **Generator Entry:** `PureFix.Dictionary/Compiler/MessageGenerator.cs`
- **Type Examples:** `PureFix.Types/FIX50SP2/QuickFix/`

---

## How It Works

### 1. Dictionary Parsing (4-Phase Process)

```
Phase 1: Initial Construction
   ↓ Parse XML sections (fields, components, messages)
   ↓ Create nodes in dependency graph

Phase 2: Graph Building
   ↓ Build edges between nodes (parent-child relationships)
   ↓ Store in adjacency lists

Phase 3: Deferred Work Queue
   ↓ Breadth-first processing of nodes
   ↓ Expand messages/components/groups
   ↓ Resolve references

Phase 4: Post-Processing
   ↓ IndexVisitor flattens hierarchies
   ↓ Create tag lookup indices
   ↓ Build fast parsing structures
```

### 2. Type Generation

```csharp
// Input: FIX50SP2.xml
// Output: C# types

// Generated Message:
[MessageType("D", FixVersion.FIX50SP2)]
public sealed partial class NewOrderSingle : IFixMessage
{
    [Component(Offset = 0, Required = true)]
    public StandardHeaderComponent? StandardHeader { get; set; }

    [TagDetails(Tag = 11, Type = TagType.String, Offset = 1, Required = true)]
    public string? ClOrdID { get; set; }

    [TagDetails(Tag = 54, Type = TagType.Char, Offset = 2, Required = true)]
    public char? Side { get; set; }

    // ... more fields

    void IFixEncoder.Encode(IFixWriter writer) { /* generated */ }
    void IFixParser.Parse(IMessageView view) { /* generated */ }
}
```

### 3. Runtime Usage

```csharp
// Parse incoming FIX message
var factory = new FixMessageFactory();
var message = factory.ToFixMessage(messageView);

if (message is NewOrderSingle order)
{
    Console.WriteLine($"Order: {order.ClOrdID} Side: {order.Side}");
}

// Encode outgoing message
var heartbeat = new Heartbeat { TestReqID = "123" };
((IFixEncoder)heartbeat).Encode(writer);
```

---

## Current Issues (Pre-Release)

### 🔴 Critical Issues (Must Fix)

1. **FieldEnum.cs Character Handling** (`PureFix.Dictionary/Parser/FieldEnum.cs:18`)
   - Bug: Line 18 is a no-op `.Replace("-", "-")`
   - Real broker XMLs contain exotic characters not handled
   - Missing systematic character sanitization

2. **Global Groups Create Collisions**
   - Groups generated as global types with parent prefix
   - Same group name in different messages causes issues
   - Should be nested types within parent messages/components
   - 3,797 types in flat namespace

3. **Missing Duplicate Detection**
   - Parser doesn't detect duplicate field names/tags
   - QuickFix C++ does this - we should too
   - Silent failures with malformed dictionaries

4. **Assembly Size Explosion**
   - Production use: 20+ broker dictionaries
   - Each generates 3,000+ types
   - Single assembly becomes massive (hundreds of MB)
   - **Compilation crashes on Linux**

### 🟡 Important Issues (Should Fix)

5. **Type Organization**
   - All 3,797 types in single folder
   - No separation by category (messages/components/enums)
   - IntelliSense overwhelmed

6. **Missing Validation**
   - No validation of field type consistency
   - No check for circular component references
   - No tag number range validation
   - Errors surface too late (during generation)

7. **Test Coverage**
   - No comprehensive tests for:
     - Character sanitization
     - Duplicate detection
     - Type generation correctness
     - Round-trip XML → Parse → Generate → Compile

### 🟢 Minor Issues (Can Address Later)

8. **Two Generator Implementations**
   - `MessageGenerator` (active) vs `MsgCompiler` (disabled)
   - Code duplication, unclear which is better

9. **Static Memoization**
   - `ContainedFieldCollector` uses static cache without invalidation
   - Thread safety not explicit

10. **Documentation Gaps**
    - No README for architecture
    - No contributor guide
    - No examples of extending generator

See `docs/CURRENT_ISSUES.md` for detailed analysis.

---

## Development Roadmap

### Phase 1: Foundation (Weeks 1-2)
- ✅ Deep dive analysis completed
- Fix FieldEnum.cs character handling
- Add comprehensive duplicate detection
- Implement robust XML validation
- Add extensive test coverage

### Phase 2: Type System Redesign (Weeks 3-5)
- Design nested group types architecture
- Implement new recursive compiler
- Preserve backward compatibility
- Extensive testing with real messages

### Phase 3: Scalability (Weeks 6-7)
- Multi-assembly generation for multiple dictionaries
- Improve type organization (folders by category)
- Performance optimization
- Memory usage improvements

### Phase 4: Polish (Week 8)
- Documentation
- Examples
- Clean up dead code
- Prepare for open-source release

See `docs/IMPROVEMENT_PLAN.md` for detailed roadmap.

---

## Testing Strategy

### Real-World Validation
- Code tested carefully against real FIX messages
- Only messages from other FIX engines tested (not live sessions yet)
- Need session recovery with sequence numbers before live testing
- Socket session management improvements planned

### Missing Test Areas
1. Live session with sequence number recovery
2. Session persistence
3. Failover handling
4. Multiple concurrent sessions
5. Broker-specific dictionary edge cases

---

## Technical Highlights

### Strengths
- **Type Safety:** Compile-time checking of FIX messages
- **Performance:** No runtime reflection or parsing overhead
- **Clean Architecture:** Clear separation of concerns
- **Extensibility:** Partial classes allow user extensions
- **Sophisticated Indexing:** Fast tag lookups via post-processing

### Design Patterns
- **Visitor Pattern:** Field traversal (`ISetDispatchReceiver`)
- **Builder Pattern:** `ContainedFieldSet.Builder`
- **Factory Pattern:** `FixMessageFactory` for message instantiation
- **Graph Processing:** Dependency graph for parsing

---

## Key Differences from QuickFIX

| Feature | QuickFIX | CSPureFix |
|---------|----------|-----------|
| **Type System** | Runtime dictionary | Compile-time generated types |
| **Performance** | Runtime parsing | Pre-compiled, fast |
| **Type Safety** | Dictionary-based | Full C# type checking |
| **Memory** | Single dictionary | Type per dictionary (larger) |
| **Flexibility** | Runtime changes | Requires regeneration |
| **Language** | C++ | C# (.NET 8) |

---

## Related Links

- **Original TypeScript Project:** https://www.npmjs.com/package/jspurefix
- **FIX Protocol:** https://www.fixtrading.org/
- **QuickFIX:** http://quickfixengine.org/

---

## Contributing

⚠️ **Not yet ready for contributions** - Working toward open-source release.

Current focus:
1. Fix critical bugs (FieldEnum, duplicates)
2. Redesign group type generation
3. Improve scalability for multiple dictionaries
4. Add comprehensive tests

---

## Documentation

- **`docs/CODEBASE_EXPLORATION.md`** - Comprehensive technical exploration (972 lines)
- **`docs/CURRENT_ISSUES.md`** - Detailed issue analysis
- **`docs/QUICK_REFERENCE.md`** - Quick lookup guide
- **`docs/FILE_STRUCTURE.txt`** - Complete file organization
- **`docs/IMPROVEMENT_PLAN.md`** - Phased roadmap (coming)

---

## License

TBD - Will be open-sourced once robustness improvements are complete.

---

## Contact

For questions or collaboration:
- Original jspurefix: https://www.npmjs.com/package/jspurefix
- This C# port: (contact info TBD)
