# CSPureFix - Quick Reference Guide

## Project Overview
Pure FIX is a comprehensive C# FIX Protocol engine with XML parsing, type generation, and network transport.

**11 Projects:** Dictionary, Types, Buffer, Transport, MessageStore, Data, Test, ConsoleApp, Benchmarks, LogMessageParser, SeeFixServer

---

## CRITICAL ISSUE FOUND

### FieldEnum.cs Bug (Line 18)
**File:** `/PureFix.Dictionary/Parser/FieldEnum.cs`
**Problem:** No-op character replacement
```csharp
.Replace("-", "-")  // LINE 18: Does nothing! Should be removed
.Replace("-", "_")  // LINE 20: Actually replaces dashes
```
**Fix:** Remove line 18 entirely

---

## Architecture Overview

### Three-Layer System

#### 1. Definition Layer
- `SimpleFieldDefinition` - Field metadata (tag, type, enums)
- `ComponentFieldDefinition` - Reusable component definition
- `GroupFieldDefinition` - Repeating group definition
- `MessageDefinition` - Message metadata
- All stored in `FixDefinitions` container

#### 2. Contained Layer (Runtime)
- `ContainedFieldSet` - Abstract base for all sets
- `ContainedSimpleField` - Field with position context
- `ContainedComponentField` - Component with position context
- `ContainedGroupField` - Group with position context
- **Key:** Adds position, required flag, and flattened tag indices

#### 3. Generated Layer (C#)
- Message classes implementing `IFixMessage`
- Component classes implementing `IFixComponent`
- Group classes implementing `IFixGroup`
- Enum constant classes (e.g., `MsgTypeValues`)
- All generated types are `partial class`

---

## Parsing Pipeline

### QuickFix XML Parser (`QuickFixXmlFileParser`)

**4-Phase Process:**

1. **Initial Construction** (Sequential)
   - ParseVersion() → Get FIX version
   - ParseFields() → Create field nodes
   - ParseComponents() → Create component nodes
   - ParseHeader() → Create StandardHeader node
   - ParseTrailer() → Create StandardTrailer node
   - ParseMessages() → Create message nodes

2. **Graph Building**
   - Node = XML element representation with ID, name, type
   - Edge = Directed relationship (Head→Tail)
   - All stored in `_nodes` and `_edges` dictionaries
   - Creates dependency graph

3. **Deferred Work Queue**
   - While queue not empty:
     - Dequeue node
     - Process based on type:
       - SimpleFieldDefinition → `Definitions.AddSimple()`
       - MessageDefinition → Create message, expand fields
       - ComponentDefinition → Create component, expand fields
       - SimpleFieldDeclaration → Place field in parent
       - InlineGroupDefinition → Create group with NoOfField
       - GroupDefinition → Expand group
       - ComponentDeclaration → Add component reference

4. **Post-Processing Indexing** (`IndexVisitor`)
   - For each message:
     - Call `Index()` to flatten nested tags
     - Creates `_containedTag` mapping (tag → parent set)
     - Creates `_tagToField` mapping (tag → direct parent)
     - Collects all tags for segment parser

**Key Point:** Messages must contain StandardHeader + body + StandardTrailer (added automatically)

---

## Parsing Detectors

**QuickFix:** File exists → Use `QuickFixXmlFileParser`
**Repo:** Directory → Use `RepoFixXmlFileParser`
Detection in: `Program.Support.cs`

---

## Type Generation

### Entry Point
```
Program.Generate() 
  → GetDefinitions(dictPath) 
  → MessageGenerator.Process()
  → Generate .cs files
```

### Output Structure
- **Messages:** `{OutputPath}/{MessageName}.cs`
- **Components/Groups:** `{OutputPath}/Types/{Name}.cs`
- **Enums:** `{OutputPath}/Types/{FieldName}Values.cs`

### Namespaces
- **Messages:** `PureFix.Types.{VERSION}.{SOURCE}` (e.g., `FIX50SP2.QuickFix`)
- **Types:** `PureFix.Types.{VERSION}.{SOURCE}.Types`

### Generated Methods
- `IsValid()` - Validates required fields
- `Encode()` - Writes to FIX writer
- `Parse()` - Reads from message view
- `TryGetByTag()` - Lookup by name
- `Reset()` - Clears fields

---

## Key Classes & Their Responsibility

| Class | Responsibility |
|-------|-----------------|
| `QuickFixXmlFileParser` | Parse XML, build graph, deferred processing |
| `FixDefinitions` | Container for all parsed definitions |
| `ContainedFieldSet` | Runtime representation with position & indices |
| `MessageGenerator` | Generate C# code from definitions |
| `CodeGenerator` | Code building utilities (indentation, blocks) |
| `IndexVisitor` | Flatten and index all nested tags post-parsing |
| `ContainedFieldCollector` | Visitor pattern for tag collection (memoized) |

---

## FIX Element Representation

### Fields
- **Definition:** `SimpleFieldDefinition` (tag, type, enums)
- **Contained:** `ContainedSimpleField` (position, required)
- **Generated:** Property with `[TagDetails]` attribute

### Enums
- **Definition:** `FieldEnum` (key, description, computed Val)
- **Conversion:** `"Buy-Sell"` → special chars replaced → `UnderscoreToCamelCase()` → `"BuySell"`
- **Generated:** Static class with const values

### Components
- **Definition:** `ComponentFieldDefinition` extends `ContainedFieldSet`
- **Contained:** `ContainedComponentField` (reference to definition)
- **Generated:** Sealed partial class implementing `IFixComponent`
- **Shared:** Same definition used in multiple messages with different positions

### Groups
- **Definition:** `GroupFieldDefinition` extends `ContainedFieldSet`
- **NoOfField:** Reference to NUMINGROUP field (e.g., "NoOrders")
- **Generated:** Sealed partial class array property + count field

### Messages
- **Definition:** `MessageDefinition` extends `ContainedFieldSet`
- **MsgType:** Single char/code (A, 0, 8, etc.)
- **Always Include:** StandardHeader + body + StandardTrailer
- **Generated:** Sealed partial class implementing `IFixMessage`

---

## Important File Locations

**Core Parsing:**
- `/PureFix.Dictionary/Parser/QuickFix/QuickFixXmlFileParser.cs` (332 lines)
- `/PureFix.Dictionary/Parser/QuickFix/QuickFixXmlFileParser.DependencyGraph.cs` (Node & Edge)
- `/PureFix.Dictionary/Parser/QuickFix/QuickFixXmlFileParser.PostProcessor.cs` (IndexVisitor)

**Definitions:**
- `/PureFix.Dictionary/Definition/FixDefinitions.cs` (container)
- `/PureFix.Dictionary/Definition/SimpleFieldDefinition.cs`

**Runtime Representation:**
- `/PureFix.Dictionary/Contained/ContainedFieldSet.cs` (1,185 lines - base class)
- `/PureFix.Dictionary/Contained/ContainedFieldSet.Builder.cs` (237 lines - indexing)

**Type Generation:**
- `/PureFix.Dictionary/Compiler/MessageGenerator.cs` (used)
- `/PureFix.Dictionary/Compiler/MsgCompiler.cs` (disabled - removed from build)
- `/PureFix.Dictionary/Compiler/CodeGenerator.cs` (utilities)

**Generated Types:**
- `/PureFix.Types/FIX50SP2/QuickFix/` - Messages
- `/PureFix.Types/FIX50SP2/QuickFix/Types/` - Components, groups, enums

---

## Known Issues

1. **FieldEnum.cs Line 18** - No-op replace statement (CRITICAL FIX)
2. **Dual Generators** - MsgCompiler disabled, MessageGenerator used (code duplication)
3. **Partial Classes** - All generated types are partial (implies hand-code elsewhere not found)
4. **Type Folder Organization** - 500+ files mixed without separation
5. **Static Memoization** - `ContainedFieldCollector._memoised` cache not invalidated

---

## Usage Examples

### Parse Dictionary
```csharp
var definitions = new FixDefinitions();
var parser = new QuickFixXmlFileParser(definitions);
parser.Parse("FIX50SP2.xml");
```

### Access Parsed Data
```csharp
var msg = definitions["Logon"];          // By name
var field = definitions[8];               // By tag
var msgType = definitions["A"];           // By MsgType code
```

### Generate Types
```csharp
var options = Options.FromVersion(definitions);
options.BackingTypeOutputPath = "./output";
options.BackingTypeNamespace = "MyApp.Types";
var generator = new MessageGenerator(null, definitions, options);
generator.Process();
```

### Console Commands
```bash
# Generate types
dotnet run -- --dictpath ./FIX50SP2.xml --outputpath ./output --generate

# Parse log
dotnet run -- --fixlogpath ./messages.log

# Trim messages
dotnet run -- --dictpath ./FIX50SP2.xml --msgtypes 0 1 A
```

---

## Quick Navigation

**Parser Entry:** `QuickFixXmlFileParser.ParseText(xml)`
**Definitions Container:** `FixDefinitions` class
**Runtime Sets:** Classes extending `ContainedFieldSet`
**Generator Entry:** `MessageGenerator.Process()`
**Console Entry:** `/PureFix.App/Program.cs`

---

**Comprehensive Report:** See `CODEBASE_EXPLORATION.md` (972 lines)
