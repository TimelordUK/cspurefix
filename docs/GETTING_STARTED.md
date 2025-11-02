# Getting Started with CSPureFix Development

## Overview

This guide helps you set up your development environment and understand the workflow for contributing to CSPureFix.

---

## Prerequisites

- **.NET 8.0 SDK** - https://dotnet.microsoft.com/download
- **IDE:** Visual Studio 2022, JetBrains Rider, or VS Code with C# extension
- **Git** - for version control
- **Linux or Windows** - both are supported

---

## Quick Start

### 1. Clone the Repository

```bash
git clone https://github.com/TimelordUK/cspurefix.git
cd cspurefix
```

### 2. Build the Solution

```bash
dotnet restore cspurefix.sln
dotnet build cspurefix.sln
```

### 3. Run Tests

```bash
dotnet test PureFix.Test/PureFix.Test.csproj
```

### 4. Generate Types from Dictionary

```bash
cd PureFix.App

# Generate from FIX 50SP2
dotnet run -- --dict ../path/to/FIX50SP2.xml --output ./generated --generate

# Or trim to specific messages
dotnet run -- --dict ../path/to/FIX50SP2.xml --trim "0,1,2,3,4,5,A,D,8"
```

---

## Project Structure

```
cspurefix/
├── PureFix.Dictionary/       # XML parsing & type generation (CORE)
│   ├── Parser/               # QuickFix & Repo XML parsers
│   ├── Definition/           # Metadata layer
│   ├── Contained/            # Runtime representation
│   └── Compiler/             # Type generator
│
├── PureFix.Types/            # Generated FIX types (DO NOT EDIT)
├── PureFix.Buffer/           # Low-level buffer operations
├── PureFix.Transport/        # Network transport layer
├── PureFix.MessageStore/     # Message persistence
├── PureFix.Data/             # Data models
├── PureFix.Test/             # Unit tests (NUnit)
├── PureFix.App/              # CLI tool
└── docs/                     # Documentation
```

---

## Development Workflow

### Current Status: Pre-Release

We're currently working on **test infrastructure and robustness improvements** before open-source release. The focus is on:

1. **Creating test dictionaries** with problematic real-world cases
2. **Setting up CI/CD** with GitHub Actions
3. **Adding regression tests** to catch breaking changes
4. **Fixing critical bugs** (FieldEnum, duplicate detection, etc.)

### Key Principle: Don't Break Existing Types

**Important:** We keep existing generated types (`PureFix.Types/`) untouched during development. All improvements are tested on **new test dictionaries** first to avoid regression issues.

---

## Understanding the Codebase

### Read These First:

1. **`claude.md`** (project root) - High-level overview
2. **`docs/CODEBASE_EXPLORATION.md`** - Deep technical dive (972 lines)
3. **`docs/CURRENT_ISSUES.md`** - Known issues and priorities
4. **`docs/IMPROVEMENT_PLAN.md`** - 8-week roadmap

### Key Concepts:

#### Three-Layer Architecture

```
XML Dictionary
    ↓
[1. Definition Layer]
  - Metadata about FIX elements
  - SimpleFieldDefinition, ComponentFieldDefinition, etc.
    ↓
[2. Contained Layer]
  - Runtime representation with context
  - ContainedFieldSet, ContainedSimpleField, etc.
    ↓
[3. Generated Layer]
  - C# types implementing IFixMessage, IFixComponent, IFixGroup
```

#### Parsing Pipeline (4 Phases)

```
Phase 1: Initial Construction
  - Parse XML sections (fields, components, messages)
  - Create nodes in dependency graph

Phase 2: Graph Building
  - Build edges between nodes (parent-child)
  - Store in adjacency lists

Phase 3: Deferred Work Queue
  - Breadth-first processing
  - Expand messages/components/groups
  - Resolve references

Phase 4: Post-Processing
  - IndexVisitor flattens hierarchies
  - Create tag lookup indices
```

---

## Common Tasks

### Generate Types from Custom Dictionary

```bash
cd PureFix.App
dotnet run -- \
  --dict /path/to/broker-specific.xml \
  --output ../MyBrokerTypes \
  --generate
```

### Trim Dictionary to Specific Messages

```bash
# Extract just session messages
dotnet run -- \
  --dict FIX50SP2.xml \
  --trim "0,1,2,3,4,5,A" \
  > session-only.xml

# Where message types are:
# 0 = Heartbeat
# 1 = TestRequest
# 2 = ResendRequest
# 3 = Reject
# 4 = SequenceReset
# 5 = Logout
# A = Logon
```

### Parse FIX Log File

```bash
dotnet run -- \
  --fix /path/to/fix-messages.log \
  --dict FIX50SP2.xml \
  --output tags
```

### Run Specific Tests

```bash
# All tests
dotnet test PureFix.Test/PureFix.Test.csproj

# Specific test class
dotnet test --filter "FullyQualifiedName~Fix44TypeCompiler"

# Specific test method
dotnet test --filter "FullyQualifiedName~Check_Builder"
```

---

## Working with Test Dictionaries

### Existing Test Infrastructure

Located in `PureFix.Test/`:
- `Env/TestEntity.cs` - Test setup helper
- `Ascii/Compiler/Fix44TypeCompiler.cs` - Type generation tests
- `Data/` - Test data files

### Creating a Test Dictionary

Use the existing `Check_Builder` test pattern:

```csharp
private IFixDefinitions GetTrimDefinitions(string[] types)
{
    var builder = new QuickFixXmlFileBuilder(Definitions);
    var encoded = builder.Write(types);
    var defs = new FixDefinitions();
    var parser = new QuickFixXmlFileParser(defs);
    parser.ParseText(encoded);
    return defs;
}

[Test]
public void Test_MyCustomDictionary()
{
    string[] msgTypes = ["0", "1", "A", "D", "8"];
    var definitions = GetTrimDefinitions(msgTypes);
    // Test generation, parsing, etc.
}
```

---

## Debugging Tips

### Enable Verbose Logging

The code uses console output for debugging. Add:

```csharp
Console.WriteLine($"Parsing field: {fieldName}");
```

### Inspect Generated Code

After generation, check the output:

```bash
cat ./generated/NewOrderSingle.cs
```

### Trace Parser Flow

Set breakpoints in:
- `QuickFixXmlFileParser.ParseText()` - Entry point
- `QuickFixXmlFileParser.Work()` - Node processing
- `QuickFixXmlFileBuilder.Write()` - XML reconstruction

---

## Common Issues & Solutions

### Issue: Tests Fail on Windows/Linux

**Cause:** Path separators differ (`\` vs `/`)

**Solution:** Use `Path.Join()` or `Path.Combine()`

### Issue: Generated Types Don't Compile

**Cause:** Special characters in field/enum names not sanitized

**Solution:** Check `FieldEnum.cs` character replacement logic

### Issue: Parser Doesn't Find Component

**Cause:** Component reference before definition

**Solution:** Parser uses deferred work queue to resolve later

---

## Next Steps

### For New Contributors:

1. Read `docs/CODEBASE_EXPLORATION.md`
2. Run the existing tests: `dotnet test`
3. Look at `docs/CURRENT_ISSUES.md` for areas to contribute
4. Start with **Phase 0: Test Infrastructure** tasks

### For Experienced Developers:

1. Review `docs/IMPROVEMENT_PLAN.md`
2. Focus on **Task 0.1: Create Test Dictionary** (see TEST_INFRASTRUCTURE_PLAN.md)
3. Help set up **GitHub Actions CI/CD**
4. Add regression tests

---

## Getting Help

- **Documentation:** `docs/` folder
- **Issues:** (GitHub issues will be enabled after open-source release)
- **Questions:** (Contact info TBD)

---

## Contributing Guidelines (Draft)

Currently in pre-release phase. Contribution guidelines will be finalized before open-source release.

**Key principles for now:**
- ✅ All changes must have tests
- ✅ Don't break existing generated types
- ✅ Follow existing code style
- ✅ Document your changes
- ✅ Test on both Linux and Windows if possible

---

_Last Updated: November 2, 2025_
