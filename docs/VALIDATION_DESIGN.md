# PureFix Validation Design

## Overview

This document outlines the validation strategy for PureFix, informed by QuickFix's approach but addressing its limitations - particularly around unknown fields and structural parsing.

## Current PureFix Architecture

PureFix has two parsing modes:

1. **View-based (IMessageView)** - Linear iteration over fields, works regardless of field order or unknown fields
2. **Segment-based (typed messages)** - Structured parsing into components/groups, requires fields in expected order and known to dictionary

**Key insight:** View access is resilient; segment parsing is fragile to unknowns and ordering.

---

## Real-World Validation Scenarios

| Scenario | Frequency | QuickFix Behavior | Desired PureFix Behavior |
|----------|-----------|-------------------|--------------------------|
| Enum out of range | Common | Reject (`IncorrectTagValue`) | Configurable: reject/warn/accept |
| Unknown field (exchange adds field) | Common | Flat parse, throws on structured access | Parse with warning, preserve structure where possible |
| Checksum mismatch | Rare | Reject | Validate by default, cheap |
| Body length mismatch | Rare | Reject | Validate by default |
| Int field sent as float | Occasional | Reject | Configurable: truncate/reject/warn |
| Date format mismatch | Occasional | Reject | Configurable with format hints |
| Multi-line string (FPML) | Rare | Problematic | Handle via Data/XmlData type |
| Fields out of order | Very rare | Configurable reject | Segment parse fails, view works |
| Required field missing | Common | Reject | Configurable per-field |

---

## Proposed Validation Levels

```csharp
public enum ValidationMode
{
    /// <summary>
    /// No validation - parse whatever we can, no errors thrown.
    /// Suitable for logging/replay scenarios.
    /// </summary>
    None,

    /// <summary>
    /// Lenient - warn on issues but continue parsing.
    /// Unknown fields preserved, type mismatches coerced where safe.
    /// </summary>
    Lenient,

    /// <summary>
    /// Strict - QuickFix-compatible validation.
    /// Reject malformed messages, unknown fields, type mismatches.
    /// </summary>
    Strict
}
```

---

## Validation Categories

### 1. Protocol-Level (Always On)

These are fundamental FIX protocol requirements:

- **BeginString present and valid**
- **BodyLength present** (value check configurable)
- **MsgType present**
- **CheckSum present** (value check configurable)
- **SOH delimiters** (can't parse without them)

### 2. Structural Validation (Configurable)

| Check | Strict | Lenient | None |
|-------|--------|---------|------|
| BodyLength matches | Error | Warn | Skip |
| CheckSum matches | Error | Warn | Skip |
| Fields in order | Error | Warn (view fallback) | Skip |
| Required fields present | Error | Warn | Skip |
| Repeating group count matches | Error | Warn | Skip |

### 3. Type Validation (Configurable)

| Check | Strict | Lenient | None |
|-------|--------|---------|------|
| Enum value valid | Error | Warn, accept value | Skip |
| Int format valid | Error | Truncate float→int | Skip |
| Date format valid | Error | Try alternate formats | Skip |
| Field type matches | Error | Coerce if safe | Skip |

### 4. Dictionary Validation (Configurable)

| Check | Strict | Lenient | None |
|-------|--------|---------|------|
| Field known to dictionary | Error | Warn, preserve in view | Skip |
| Field valid for message type | Error | Warn | Skip |
| Field valid for component | Error | Warn | Skip |

---

## Unknown Field Handling (Key Improvement over QuickFix)

### The Problem

QuickFix behavior when encountering unknown field in a component:
```
TradeCaptureReport
  └─ Instrument (component)
      ├─ Symbol (55) = "GOLD"
      ├─ SecurityID (48) = "123"
      ├─ UnknownField (99999) = "foo"  ← NEW FIELD FROM EXCHANGE
      └─ SecurityType (167) = "CMDTY"
```

QuickFix: Structured parse fails silently, falls back to flat. Accessing `Instrument.SecurityType` throws.

### Proposed PureFix Behavior

```csharp
public class ParseResult
{
    public IFixMessage Message { get; }
    public bool HasWarnings { get; }
    public IReadOnlyList<ValidationWarning> Warnings { get; }
}

public class ValidationWarning
{
    public ValidationWarningType Type { get; }
    public int Tag { get; }
    public string? Value { get; }
    public string? Context { get; }  // e.g., "Instrument component"
    public string Message { get; }
}

public enum ValidationWarningType
{
    UnknownField,
    UnknownFieldInComponent,
    EnumOutOfRange,
    TypeCoerced,
    RequiredFieldMissing,
    GroupCountMismatch,
    FieldOutOfOrder
}
```

**Key principle:** Parse what we can, preserve unknown fields in view, report warnings.

### Unknown Field Preservation

```csharp
public interface IFixMessage
{
    // Existing typed access
    IInstrument? Instrument { get; }

    // New: Access unknown fields that appeared within a component's span
    IReadOnlyList<RawField> GetUnknownFields(string componentName);

    // New: Access all unknown fields in message
    IReadOnlyList<RawField> UnknownFields { get; }
}

public record RawField(int Tag, ReadOnlyMemory<byte> RawValue, string? StringValue);
```

---

## Segment Parser Changes

### Current Behavior

Segment parser expects fields in dictionary order. Unknown field = parse failure.

### Proposed Changes

1. **Track field spans** - Each component knows its start/end position in raw message
2. **Unknown field collection** - During segment parse, collect unknown tags into side list
3. **Continue parsing** - Don't abort on unknown, skip and continue
4. **Warning generation** - Record what was skipped and where

```csharp
// Pseudo-code for component parsing
void ParseInstrument(IMessageView view, ref int position)
{
    while (position < view.FieldCount)
    {
        var tag = view.GetTag(position);

        if (IsKnownInstrumentField(tag))
        {
            ParseField(tag, view, position);
            position++;
        }
        else if (IsNextComponentStart(tag))
        {
            break; // End of Instrument
        }
        else
        {
            // Unknown field within Instrument span
            _warnings.Add(new ValidationWarning(
                ValidationWarningType.UnknownFieldInComponent,
                tag,
                view.GetString(tag),
                "Instrument"
            ));
            _unknownFields.Add(new RawField(tag, view.GetRaw(tag)));
            position++;
        }
    }
}
```

---

## Configuration

### Session-Level Settings

```json
{
  "Validation": {
    "Mode": "Lenient",
    "CheckBodyLength": true,
    "CheckChecksum": true,
    "AllowUnknownFields": true,
    "AllowEnumOutOfRange": false,
    "CoerceFloatToInt": true,
    "DateFormats": ["yyyyMMdd-HH:mm:ss", "yyyyMMdd-HH:mm:ss.fff"]
  }
}
```

### Programmatic Override

```csharp
var config = new ValidationConfig
{
    Mode = ValidationMode.Lenient,
    OnWarning = warning =>
    {
        _logger.Warn("Validation: {Type} - Tag {Tag}: {Message}",
            warning.Type, warning.Tag, warning.Message);
    },
    // Per-field overrides
    FieldOverrides =
    {
        [Tags.OrdStatus] = new FieldValidation { AllowUnknownEnum = true },
        [Tags.TransactTime] = new FieldValidation { DateFormats = new[] { "yyyyMMdd" } }
    }
};
```

---

## Implementation Phases

### Phase 1: Warning Infrastructure
- [ ] Add `ValidationWarning` type
- [ ] Add `ParseResult` wrapper with warnings collection
- [ ] Add `ValidationConfig` to `IFixConfig`
- [ ] Checksum/BodyLength validation (configurable)

### Phase 2: Type Validation
- [ ] Enum validation with unknown value handling
- [ ] Int/Float coercion option
- [ ] Date format flexibility

### Phase 3: Unknown Field Handling
- [ ] Unknown field collection during parse
- [ ] Unknown field preservation in view
- [ ] Component-aware unknown field tracking

### Phase 4: Structural Validation
- [ ] Required field checking
- [ ] Field ordering (warn vs reject)
- [ ] Group count validation

---

## Comparison with QuickFix

| Feature | QuickFix | PureFix (Proposed) |
|---------|----------|-------------------|
| Validation modes | Binary (on/off per check) | Three levels + per-field |
| Unknown fields | Silent failure, throws later | Warn, preserve, continue |
| Enum validation | Reject only | Reject/Warn/Accept |
| Type coercion | None | Optional float→int |
| Warning callback | None | Yes |
| Unknown field access | Lost | Preserved in view |
| Component-aware unknowns | No | Yes |

---

## Design Decisions

### 1. Default Validation Mode

**Decision:** Default based on role
- **Initiator/Client:** `Lenient` - clients must handle broker quirks
- **Acceptor/Server:** `Strict` - servers should enforce protocol

**Rationale:** Clients connecting to exchanges/brokers encounter many real-world deviations (ICE adding fields, enum extensions, etc.). Servers accepting client connections can be stricter as they control the protocol.

### 2. Field Ordering (Deferred)

**Decision:** Defer to later phase - requires careful analysis

**Options to evaluate:**
- Reorder pass before segment parse
- Build order-independent segment parser
- Accept current strict ordering with view fallback

**Risks:** Reordering could cause subtle bugs. Need comprehensive test suite with out-of-order scenarios first.

### 3. Nested Component Unknown Fields

**Current behavior:** Cascade down the component stack when tag can't be placed, which causes problems when multiple unknown tags appear.

**Challenge:** This is analogous to error recovery in compilers - finding a "sync point" to resume parsing.

**Potential approaches:**
- **Lookahead:** Peek at next N tags to find a known tag at current level
- **Anchor tags:** Identify "anchor" tags that definitively mark component boundaries (e.g., NoLegs for LegGroup)
- **Two-pass:** First pass identifies component spans, second pass parses with known boundaries
- **Heuristic scoring:** Score which level an unknown tag most likely belongs to

**Decision:** Start with simple approach (collect unknowns, preserve in view), defer smart recovery to later phase.

---

## Open Questions

1. **Performance impact** - Warning collection has overhead. Should we:
   - Always collect (simple)
   - Only collect in Lenient mode (optimized)
   - Make collection configurable (flexible)

2. **Lookahead depth** - For sync point recovery, how many tags to peek ahead?
   - Fixed (e.g., 3 tags)
   - Until known tag found
   - Configurable
