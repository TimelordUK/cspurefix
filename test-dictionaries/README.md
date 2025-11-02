# Test Dictionaries

This folder contains trimmed FIX dictionaries for testing purposes.

## Why Test Dictionaries?

During development, we want to test parser and type generation improvements without affecting the existing `PureFix.Types/` generated code. Test dictionaries allow us to:

1. Test with real-world problematic cases (special characters, forward references, etc.)
2. Keep existing types stable during development
3. Quickly iterate and regenerate for testing
4. Catch regressions immediately

## FIX 4.4 Test Dictionary

**File:** `FIX44-Test.xml`

**Purpose:** Core session and execution messages commonly used in finance.

### Messages Included

#### Session Messages (Admin)
- `0` - Heartbeat
- `1` - TestRequest
- `2` - ResendRequest
- `3` - Reject
- `4` - SequenceReset
- `5` - Logout
- `A` - Logon

#### Execution Messages (Application)
- `D` - NewOrderSingle
- `8` - ExecutionReport
- `G` - OrderCancelReplaceRequest
- `F` - OrderCancelRequest
- `9` - OrderCancelReject

#### Business Messages
- `j` - BusinessMessageReject

#### Allocation Messages
- `J` - AllocationInstruction
- `P` - AllocationInstructionAck

#### Trade Capture & Confirmation
- `AE` - TradeCaptureReport
- `AK` - Confirmation
- `AU` - ConfirmationAck

### Generation Command

```bash
# From repository root
./PureFix.App/bin/Release/net8.0/PureFix.ConsoleApp \
  --dict Data/FIX44.xml \
  --trim "0,1,2,3,4,5,A,D,8,G,F,9,j,J,P,AE,AK,AU" \
  > test-dictionaries/FIX44-Test.xml
```

Or using the helper script:

```bash
./generate-test-dict.sh
```

### What Gets Included

The trim function automatically includes:
- **StandardHeader** and **StandardTrailer** components
- All **fields** referenced by the messages
- All **components** referenced by the messages (recursively)
- All **groups** defined within the messages
- All **enum values** for fields

This ensures the dictionary is self-contained and parseable.

## Future Test Dictionaries

### FIX44-Problems.xml (Planned)

Will contain problematic cases found in production broker dictionaries:

- Fields with special characters: `Broker-Specific-Field`, `Field@Broker`
- Fields starting with numbers: `123Field`, `1stField`
- Enum values with special chars: `N/A`, `Buy/Sell`, `100% Complete`
- Forward reference issues
- Circular component references (if found)
- Fields with very long names
- Unicode characters in descriptions

### FIX44-Forward-Refs.xml (Planned)

Test dictionary specifically for forward reference detection:

- Component A references Component B (defined later)
- Message references component not yet defined
- Group references field defined later

## Usage

### 1. Parse Test Dictionary

```csharp
var definitions = new FixDefinitions();
var parser = new QuickFixXmlFileParser(definitions);
parser.Parse("test-dictionaries/FIX44-Test.xml");
```

### 2. Generate Types from Test Dictionary

```bash
dotnet run --project PureFix.App -- \
  --dict test-dictionaries/FIX44-Test.xml \
  --output test-generated \
  --generate
```

### 3. Run Tests

```csharp
[Test]
public void Test_FIX44_TestDictionary()
{
    var dictPath = "test-dictionaries/FIX44-Test.xml";
    var definitions = GetDefinitions(dictPath);

    // Should have session messages
    Assert.That(definitions.Message["0"], Is.Not.Null); // Heartbeat
    Assert.That(definitions.Message["A"], Is.Not.Null); // Logon

    // Should have execution messages
    Assert.That(definitions.Message["D"], Is.Not.Null); // NewOrderSingle
    Assert.That(definitions.Message["8"], Is.Not.Null); // ExecutionReport
}
```

## Validation

Each test dictionary should:

- ✅ Parse without errors
- ✅ Generate types successfully
- ✅ Generated types compile
- ✅ Round-trip: Parse → Generate → Compile → Parse messages

## Notes

- Test dictionaries are **committed to git**
- Keep them focused and small for fast iteration
- Document any special cases or edge cases being tested
- Update this README when adding new test dictionaries

---

_Last Updated: November 2, 2025_
