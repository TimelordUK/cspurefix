# Test Infrastructure Plan - Foundation First Approach

## Overview

Based on production experience, we need solid test infrastructure BEFORE making any changes to the parser or type generator. This plan focuses on:

1. **CI/CD with GitHub Actions** (Linux + Windows)
2. **Test Dictionary** with real-world problematic cases
3. **Regression Detection** to catch breaking changes immediately
4. **Keep Existing Types Untouched** - work on new test dictionary

**Key Principle:** Use a separate test dictionary to avoid breaking existing code while we improve the system.

---

## Phase 0: Test Infrastructure (Priority 1 - Do This First)

**Duration:** 1-2 weeks
**Risk:** Low
**Impact:** Critical for all future work

### Task 0.1: Create Test Dictionary with Problem Cases (2-3 days)

**Goal:** Create a focused, small dictionary containing all the edge cases we've seen in production.

#### Step 1: Identify Problem Messages from FIX 50SP2

Use existing trim function to extract key session + application messages:

```bash
# Session messages (admin messages)
dotnet run --project PureFix.App -- \
  --dict FIX50SP2.xml \
  --trim "0,1,2,3,4,5,A" \
  > test-dictionaries/TestProblems.xml

# Where:
# 0 = Heartbeat
# 1 = TestRequest
# 2 = ResendRequest
# 3 = Reject
# 4 = SequenceReset
# 5 = Logout
# A = Logon
```

Then add a few application messages:
```bash
# Add key application messages
# D = NewOrderSingle
# 8 = ExecutionReport
# G = OrderCancelReplaceRequest
# F = OrderCancelRequest
# 9 = OrderCancelReject
# j = BusinessMessageReject
# AS = AllocationInstruction
```

**Final trim command:**
```bash
dotnet run --project PureFix.App -- \
  --dict FIX50SP2.xml \
  --trim "0,1,2,3,4,5,A,D,8,G,F,9,j,AS" \
  > test-dictionaries/TestBase.xml
```

#### Step 2: Create Problem Cases Dictionary

Manually edit `TestBase.xml` to add problematic field/enum names we've seen in production:

**Problematic Field Names:**
```xml
<!-- Field with numbers only -->
<field number="9001" name="123Field" type="STRING"/>

<!-- Field with special characters -->
<field number="9002" name="Broker-Specific-Field" type="STRING"/>
<field number="9003" name="Field@Broker" type="STRING"/>
<field number="9004" name="N/A_Field" type="STRING"/>
<field number="9005" name="Field#1" type="STRING"/>
<field number="9006" name="100%_Complete" type="STRING"/>

<!-- Field starting with number -->
<field number="9007" name="1stField" type="STRING"/>

<!-- Field with spaces and special chars -->
<field number="9008" name="Buy/Sell Indicator" type="CHAR"/>
<field number="9009" name="Currency (ISO)" type="CURRENCY"/>
```

**Problematic Enum Values:**
```xml
<field number="9010" name="TestEnum" type="CHAR">
  <value enum="1" description="N/A"/>
  <value enum="2" description="Buy/Sell"/>
  <value enum="3" description="100% Complete"/>
  <value enum="4" description="Broker-Specific"/>
  <value enum="5" description="@Special"/>
  <value enum="6" description="#Hash Value"/>
  <value enum="7" description="Value (Parentheses)"/>
  <value enum="8" description="Value - Dash"/>
  <value enum="9" description="Value + Plus"/>
</field>
```

**Problematic Component/Group Names:**
```xml
<!-- Component with special chars -->
<component name="Broker-Custom-Component">
  <field name="Field1" required="Y"/>
</component>

<!-- Group that might collide -->
<message name="TestMessage" msgtype="ZZ" msgcat="app">
  <group name="NoTestGroup" required="N">
    <field name="TestGroupField" required="Y"/>
  </group>
</message>
```

**Save as:** `test-dictionaries/TestProblems.xml`

#### Step 3: Create Expected Behavior Documentation

Create `test-dictionaries/EXPECTED_BEHAVIOR.md`:

```markdown
# Expected Behavior for Test Dictionary

## Field Name Sanitization

| Input Field Name | Expected C# Property | Notes |
|------------------|---------------------|-------|
| 123Field | _123Field | Prefix with underscore if starts with number |
| Broker-Specific-Field | BrokerSpecificField | Dashes to camelCase |
| Field@Broker | FieldAtBroker | @ symbol converted |
| N/A_Field | NAField | Remove underscores, camelCase |
| Field#1 | FieldHash1 | # becomes "Hash" |
| 100%_Complete | _100PercentComplete | Number start, % to "Percent" |

## Enum Value Sanitization

| Enum Description | Expected C# Constant | Notes |
|------------------|---------------------|-------|
| N/A | NA | Remove slash |
| Buy/Sell | BuySell | Slash removed |
| 100% Complete | _100PercentComplete | Number start, % to "Percent" |
| Broker-Specific | BrokerSpecific | Dash removed |
| @Special | AtSpecial | @ to "At" |
| #Hash Value | HashHashValue | # to "Hash" |
| Value (Parentheses) | ValueParentheses | Remove parens |
| Value - Dash | ValueDash | Dash with spaces |
| Value + Plus | ValuePlus | + to "Plus" |
```

**Deliverables:**
- `test-dictionaries/TestBase.xml` - Clean base from FIX 50SP2
- `test-dictionaries/TestProblems.xml` - With problematic field/enum names
- `test-dictionaries/EXPECTED_BEHAVIOR.md` - Expected outputs
- `test-dictionaries/README.md` - Documentation

---

### Task 0.2: Set Up GitHub Actions CI/CD (2 days)

**Goal:** Automated builds on Linux and Windows to catch regressions immediately.

#### Step 1: Create Build Workflow

Create `.github/workflows/build.yml`:

```yaml
name: Build and Test

on:
  push:
    branches: [ master, develop ]
  pull_request:
    branches: [ master, develop ]

jobs:
  build:
    strategy:
      matrix:
        os: [ubuntu-latest, windows-latest]
        dotnet-version: ['8.0.x']

    runs-on: ${{ matrix.os }}

    steps:
    - uses: actions/checkout@v4

    - name: Setup .NET
      uses: actions/setup-dotnet@v4
      with:
        dotnet-version: ${{ matrix.dotnet-version }}

    - name: Restore dependencies
      run: dotnet restore cspurefix.sln

    - name: Build
      run: dotnet build cspurefix.sln --configuration Release --no-restore

    - name: Test
      run: dotnet test cspurefix.sln --configuration Release --no-build --verbosity normal

    - name: Upload test results
      if: always()
      uses: actions/upload-artifact@v3
      with:
        name: test-results-${{ matrix.os }}
        path: '**/TestResults/*.trx'
```

#### Step 2: Create Type Generation Test Workflow

Create `.github/workflows/test-generation.yml`:

```yaml
name: Test Dictionary Generation

on:
  push:
    branches: [ master, develop ]
  pull_request:
    branches: [ master, develop ]

jobs:
  generate-test-types:
    strategy:
      matrix:
        os: [ubuntu-latest, windows-latest]

    runs-on: ${{ matrix.os }}

    steps:
    - uses: actions/checkout@v4

    - name: Setup .NET
      uses: actions/setup-dotnet@v4
      with:
        dotnet-version: '8.0.x'

    - name: Build Console App
      run: dotnet build PureFix.App/PureFix.App.csproj --configuration Release

    - name: Generate Types from Test Dictionary
      run: |
        dotnet run --project PureFix.App -- \
          --dict test-dictionaries/TestProblems.xml \
          --output test-generated \
          --generate

    - name: Build Generated Types
      run: |
        # Create temporary test project to compile generated types
        dotnet new classlib -n TestGenerated -o test-compiled
        cp -r test-generated/* test-compiled/
        dotnet add test-compiled/TestGenerated.csproj reference PureFix.Types/PureFix.Types.csproj
        dotnet build test-compiled/TestGenerated.csproj --configuration Release

    - name: Upload generated types
      if: failure()
      uses: actions/upload-artifact@v3
      with:
        name: failed-generation-${{ matrix.os }}
        path: test-generated/
```

#### Step 3: Add Status Badges to README

Update main README.md with status badges:

```markdown
# CSPureFix

[![Build and Test](https://github.com/TimelordUK/cspurefix/actions/workflows/build.yml/badge.svg)](https://github.com/TimelordUK/cspurefix/actions/workflows/build.yml)
[![Test Dictionary Generation](https://github.com/TimelordUK/cspurefix/actions/workflows/test-generation.yml/badge.svg)](https://github.com/TimelordUK/cspurefix/actions/workflows/test-generation.yml)
```

**Deliverables:**
- `.github/workflows/build.yml` - Build + test on Linux and Windows
- `.github/workflows/test-generation.yml` - Test type generation
- Status badges in README

---

### Task 0.3: Create Regression Test Suite (3 days)

**Goal:** Comprehensive tests that catch any breaking changes.

#### Step 1: Create Test Project Structure

```
PureFix.Test/
├── Dictionary/
│   ├── ParserTests.cs                    # Parser tests
│   ├── DuplicateDetectionTests.cs        # Test duplicate detection
│   ├── FieldEnumTests.cs                 # Character sanitization tests
│   └── ValidationTests.cs                # XML validation tests
├── Generation/
│   ├── TypeGenerationTests.cs            # Type generator tests
│   ├── RoundTripTests.cs                 # Parse → Generate → Compile → Parse
│   └── RegressionTests.cs                # Compare against baseline
├── TestData/
│   ├── Valid/
│   │   ├── TestBase.xml
│   │   └── TestProblems.xml
│   ├── Invalid/
│   │   ├── DuplicateTags.xml
│   │   ├── DuplicateNames.xml
│   │   └── InvalidCharacters.xml
│   └── Messages/
│       ├── Logon.fix
│       ├── NewOrderSingle.fix
│       └── ExecutionReport.fix
└── Baselines/
    └── TestProblems/                      # Baseline generated code
        ├── Logon.cs
        └── ... (other expected outputs)
```

#### Step 2: Implement FieldEnum Character Tests

Create `PureFix.Test/Dictionary/FieldEnumTests.cs`:

```csharp
[TestFixture]
public class FieldEnumTests
{
    [TestCase("N/A", "NA")]
    [TestCase("Buy/Sell", "BuySell")]
    [TestCase("100% Complete", "_100PercentComplete")]
    [TestCase("Broker-Specific", "BrokerSpecific")]
    [TestCase("@Special", "AtSpecial")]
    [TestCase("#Hash Value", "HashHashValue")]
    [TestCase("Value (Parentheses)", "ValueParentheses")]
    [TestCase("Value - Dash", "ValueDash")]
    [TestCase("Value + Plus", "ValuePlus")]
    public void FieldEnum_SanitizesCharacters_Correctly(string input, string expected)
    {
        var fieldEnum = new FieldEnum("1", input);
        Assert.AreEqual(expected, fieldEnum.Val);
    }

    [Test]
    public void FieldEnum_NoDuplicateReplacements()
    {
        // Ensure the .Replace("-", "-") bug is fixed
        var fieldEnum = new FieldEnum("1", "Test-Value");
        Assert.AreEqual("TestValue", fieldEnum.Val);
    }

    [TestCase("class")]
    [TestCase("string")]
    [TestCase("int")]
    [TestCase("public")]
    [TestCase("private")]
    public void FieldEnum_EscapesCSharpKeywords(string keyword)
    {
        var fieldEnum = new FieldEnum("1", keyword);
        Assert.That(fieldEnum.Val, Does.Not.EqualTo(keyword));
        // Should be prefixed or suffixed to avoid keyword collision
    }
}
```

#### Step 3: Implement Round-Trip Tests

Create `PureFix.Test/Generation/RoundTripTests.cs`:

```csharp
[TestFixture]
public class RoundTripTests
{
    [Test]
    public void RoundTrip_TestProblems_Success()
    {
        var dictPath = "TestData/Valid/TestProblems.xml";
        var outputPath = "TestOutput/TestProblems";

        // 1. Parse XML
        var definitions = new FixDefinitions();
        var parser = new QuickFixXmlFileParser(definitions);
        parser.Parse(dictPath);

        Assert.IsNotNull(definitions);
        Assert.That(definitions.Message.Count, Is.GreaterThan(0));

        // 2. Generate types
        var options = Options.FromVersion(definitions);
        options.BackingTypeOutputPath = outputPath;
        var generator = new MessageGenerator(null, definitions, options);
        generator.Process();

        // 3. Verify files created
        Assert.That(Directory.Exists(outputPath), Is.True);
        var files = Directory.GetFiles(outputPath, "*.cs", SearchOption.AllDirectories);
        Assert.That(files.Length, Is.GreaterThan(0));

        // 4. Verify all files compile (using Roslyn)
        var compilation = CompileGenerated(files);
        var diagnostics = compilation.GetDiagnostics()
            .Where(d => d.Severity == DiagnosticSeverity.Error);

        Assert.That(diagnostics, Is.Empty,
            $"Compilation errors:\n{string.Join("\n", diagnostics)}");
    }

    private CSharpCompilation CompileGenerated(string[] files)
    {
        var syntaxTrees = files.Select(f =>
            CSharpSyntaxTree.ParseText(File.ReadAllText(f)));

        var references = new[] {
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(IFixMessage).Assembly.Location),
            // Add other required references
        };

        return CSharpCompilation.Create(
            "TestGenerated",
            syntaxTrees,
            references,
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
    }
}
```

#### Step 4: Implement Regression Tests

Create `PureFix.Test/Generation/RegressionTests.cs`:

```csharp
[TestFixture]
public class RegressionTests
{
    [Test]
    public void Generated_Code_MatchesBaseline()
    {
        var dictPath = "TestData/Valid/TestProblems.xml";
        var outputPath = "TestOutput/TestProblems";
        var baselinePath = "Baselines/TestProblems";

        // Generate types
        GenerateTypes(dictPath, outputPath);

        // Compare against baseline
        var outputFiles = Directory.GetFiles(outputPath, "*.cs", SearchOption.AllDirectories);
        var baselineFiles = Directory.GetFiles(baselinePath, "*.cs", SearchOption.AllDirectories);

        Assert.That(outputFiles.Length, Is.EqualTo(baselineFiles.Length),
            "Number of generated files changed");

        foreach (var outputFile in outputFiles)
        {
            var relativePath = Path.GetRelativePath(outputPath, outputFile);
            var baselineFile = Path.Combine(baselinePath, relativePath);

            Assert.That(File.Exists(baselineFile), Is.True,
                $"Baseline file missing: {baselineFile}");

            var outputContent = NormalizeGeneratedCode(File.ReadAllText(outputFile));
            var baselineContent = NormalizeGeneratedCode(File.ReadAllText(baselineFile));

            Assert.That(outputContent, Is.EqualTo(baselineContent),
                $"Generated code differs from baseline: {relativePath}");
        }
    }

    private string NormalizeGeneratedCode(string code)
    {
        // Normalize whitespace, comments, etc. for comparison
        // Ignore generated timestamps, tool versions
        return code
            .Replace("\r\n", "\n")
            .Trim();
    }
}
```

**Deliverables:**
- Complete test suite with 100+ tests
- Test data with valid and invalid dictionaries
- Round-trip tests (Parse → Generate → Compile)
- Regression tests against baselines
- All tests passing

---

### Task 0.4: Create Test Documentation (1 day)

**Goal:** Document how to run tests, add new tests, and interpret results.

Create `docs/TESTING_GUIDE.md`:

```markdown
# Testing Guide

## Running Tests

### All Tests
```bash
dotnet test cspurefix.sln
```

### Specific Test Project
```bash
dotnet test PureFix.Test/PureFix.Test.csproj
```

### Specific Test Category
```bash
dotnet test --filter Category=Parser
dotnet test --filter Category=Generation
dotnet test --filter Category=Regression
```

## Test Data

### Creating New Test Dictionary

Use the trim function to extract messages:
```bash
dotnet run --project PureFix.App -- \
  --dict FIX50SP2.xml \
  --trim "0,1,A,D,8" \
  > test-dictionaries/MyTest.xml
```

### Adding Problem Cases

Edit the XML to add problematic field/enum names. See `EXPECTED_BEHAVIOR.md` for examples.

## Baseline Management

### Creating Baseline

After verifying generated code is correct:
```bash
./scripts/update-baseline.sh test-dictionaries/TestProblems.xml
```

### Reviewing Baseline Changes

When regression tests fail, review the diff:
```bash
./scripts/diff-baseline.sh test-dictionaries/TestProblems.xml
```

## CI/CD

Tests run automatically on:
- Push to master or develop
- Pull requests
- Both Linux and Windows

View results: https://github.com/TimelordUK/cspurefix/actions
```

**Deliverables:**
- `docs/TESTING_GUIDE.md` - Complete testing documentation
- `scripts/update-baseline.sh` - Helper script
- `scripts/diff-baseline.sh` - Helper script

---

## Success Criteria for Phase 0

✅ Test dictionary created with 20+ problematic cases
✅ GitHub Actions running on Linux + Windows
✅ All existing code still compiles and tests pass
✅ Round-trip tests (Parse → Generate → Compile) passing
✅ Regression tests detect any code generation changes
✅ CI/CD catches compilation failures immediately
✅ Documentation complete for adding new tests

**When this is complete, we can confidently start fixing bugs knowing we'll catch regressions immediately.**

---

## Timeline

| Task | Duration | Dependencies |
|------|----------|--------------|
| 0.1 Test Dictionary | 2-3 days | None |
| 0.2 GitHub Actions | 2 days | None (parallel with 0.1) |
| 0.3 Regression Tests | 3 days | 0.1 complete |
| 0.4 Documentation | 1 day | All complete |
| **Total** | **1-2 weeks** | Sequential + parallel |

---

## Next Steps After Phase 0

Once test infrastructure is solid:

1. **Fix FieldEnum.cs bug** - Tests will validate the fix
2. **Add duplicate detection** - Tests will ensure it works
3. **Improve validation** - Tests will cover edge cases
4. **Refactor type generation** - Regression tests will catch issues

**Key Insight:** With solid tests on a separate test dictionary, we can refactor aggressively without fear of breaking production types.

---

_Last Updated: November 2, 2025_
