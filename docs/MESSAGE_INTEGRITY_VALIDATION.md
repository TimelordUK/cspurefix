# Message Integrity & Validation

**Status:** Design sketch - not yet implemented
**Priority:** After session management stabilization
**Goal:** Optional validation with QuickFix feature parity

## Overview

PureFix currently performs minimal validation (checksum). This document outlines a validation layer that:
- Is **optional** and configurable per session
- Works on the **view** (not full message parse) for efficiency
- Provides **QuickFix parity** for rejection scenarios
- Handles **dictionary evolution** gracefully (new fields from exchanges)

## Design Principles

### 1. Validation Should Be Optional

```csharp
public interface ISessionDescription
{
    // Existing...

    /// <summary>
    /// Message validation mode. Default: None for initiators, Basic for acceptors.
    /// </summary>
    ValidationMode? ValidationMode { get; }
}

public enum ValidationMode
{
    /// <summary>No validation beyond checksum (fastest)</summary>
    None,

    /// <summary>Checksum + required fields + basic structure</summary>
    Basic,

    /// <summary>Full validation: enums, tag definitions, structure</summary>
    Strict
}
```

### 2. Initiator vs Acceptor Defaults

| Role | Default | Rationale |
|------|---------|-----------|
| **Initiator** | `None` | Connecting to exchanges/brokers - they won't send illegal messages. CPU spent on validation is wasted. |
| **Acceptor** | `Basic` | Accepting client connections - may receive malformed messages. Need protection. |

### 3. View-Based Validation (No Full Parse)

Validation operates on `IMessageView` only - never forces a full `ToFixMessage()` parse.

```csharp
public interface IMessageValidator
{
    /// <summary>
    /// Validates a message view. Returns validation result without allocating a full message.
    /// </summary>
    ValidationResult Validate(IMessageView view, IFixDefinitions definitions);
}

public record ValidationResult(
    bool IsValid,
    SessionRejectReason? RejectReason,
    int? RefTagId,
    string? Text
);
```

## Validation Checks

### Level 1: None (Checksum Only)
- [x] Checksum verification (already implemented)

### Level 2: Basic
- [ ] Required header fields present (BeginString, BodyLength, MsgType, SenderCompID, TargetCompID, MsgSeqNum, SendingTime)
- [ ] Required trailer fields present (CheckSum)
- [ ] MsgType is defined in dictionary
- [ ] BodyLength matches actual body

### Level 3: Strict
All of Basic, plus:
- [ ] Enum values are valid for their field
- [ ] All tags are defined in dictionary (or allowed as custom)
- [ ] Required message fields present per message definition
- [ ] Conditional required fields (based on other field values)
- [ ] Repeating group count matches actual entries

## QuickFix Rejection Parity

QuickFix rejects messages for these reasons (FIX SessionRejectReason):

| Code | Reason | PureFix Support |
|------|--------|-----------------|
| 0 | Invalid tag number | Strict mode |
| 1 | Required tag missing | Basic mode |
| 2 | Tag not defined for this message type | Strict mode |
| 3 | Undefined tag | Strict mode |
| 4 | Tag specified without a value | Basic mode |
| 5 | Value is incorrect (out of range) for this tag | Strict mode (enums) |
| 6 | Incorrect data format for value | Strict mode |
| 7 | Decryption problem | N/A |
| 8 | Signature problem | N/A |
| 9 | CompID problem | Basic mode |
| 10 | SendingTime accuracy problem | Optional |
| 11 | Invalid MsgType | Basic mode |
| 13 | Tag appears more than once | Strict mode |
| 14 | Tag specified out of required order | Optional (many ignore) |
| 15 | Repeating group fields out of order | Strict mode |
| 16 | Incorrect NumInGroup count | Strict mode |
| 17 | Non "Data" value includes field delimiter | Basic mode |
| 99 | Other | Catch-all |

## The Dictionary Evolution Problem

### The Challenge

Exchanges frequently add new fields to components (e.g., new field on `Instrument`). If your dictionary is outdated:

```
Exchange sends: 55=AAPL|48=US0378331005|NEW_TAG=value|...
Your dictionary: Instrument = {Symbol(55), SecurityID(48)}
```

The segment parser sees `NEW_TAG`, doesn't recognize it as part of `Instrument`, and unwinds - thinking the tag belongs to a parent component. This corrupts the parse.

### Potential Solutions

#### Option A: Unknown Tag Tolerance (Recommended)

```csharp
public enum UnknownTagHandling
{
    /// <summary>Reject message with "Undefined tag"</summary>
    Reject,

    /// <summary>Skip unknown tags, continue parsing</summary>
    Skip,

    /// <summary>Collect unknown tags in a separate collection</summary>
    Collect
}
```

With `Skip` or `Collect`, the parser continues even with unknown tags.

#### Option B: Peek-Ahead Heuristic

When encountering an unknown tag, peek at the next tag:
- If next tag is still valid for current component → assume unknown tag is a new field, skip it
- If next tag belongs to parent → unwind normally

**Problem:** Ambiguous cases where the unknown tag could legitimately end the component.

#### Option C: Loose Component Boundaries

Instead of strict component parsing, use a "greedy" approach:
- Parse all fields until hitting a tag that definitely belongs to a different component
- Requires careful definition of "component boundary" tags

### Recommendation

Start with **Option A (Skip)** as default for initiators:
- Exchanges add fields regularly
- Rejecting messages for unknown tags breaks production
- Can still validate known fields strictly

## Implementation Sketch

### IMessageView Extensions

```csharp
public static class MessageViewValidationExtensions
{
    /// <summary>
    /// Quick enum validation without full parse.
    /// </summary>
    public static bool IsValidEnum(this IMessageView view, int tag, IFieldDefinition fieldDef)
    {
        var value = view.GetString(tag);
        if (value == null) return true; // Not present = valid (required check is separate)

        return fieldDef.Enums?.ContainsKey(value) ?? true; // No enums defined = valid
    }

    /// <summary>
    /// Check all enum fields in message are valid.
    /// </summary>
    public static ValidationResult ValidateEnums(this IMessageView view, IMessageDefinition msgDef)
    {
        foreach (var field in msgDef.Fields.Where(f => f.Definition.Enums?.Any() == true))
        {
            if (!view.IsValidEnum(field.Tag, field.Definition))
            {
                return new ValidationResult(
                    false,
                    SessionRejectReason.ValueIncorrect,
                    field.Tag,
                    $"Invalid value for {field.Name}"
                );
            }
        }
        return ValidationResult.Valid;
    }
}
```

### Configuration

```json
{
  "Application": {
    "Validation": {
      "Mode": "Basic",
      "UnknownTags": "Skip",
      "RejectOnInvalidEnum": false,
      "SendingTimeToleranceSeconds": 120
    }
  }
}
```

## Performance Considerations

| Check | Cost | Notes |
|-------|------|-------|
| Checksum | O(n) | Already done during parse |
| Required fields | O(1) per field | View lookup is hash-based |
| Enum validation | O(k) | k = number of enum fields |
| Full structure | O(n) | Must scan all fields |

**Recommendation:** Keep validation lazy where possible. Only validate fields when accessed, not upfront.

## Open Questions

1. **Nested repeating groups** - How deep to validate structure?
2. **Custom tags (5000+)** - Allow always, or require declaration?
3. **Validation caching** - Cache validation result per MsgType?
4. **Async validation** - Validate in parallel while processing?

## Next Steps

1. Stabilize session management (current focus)
2. Implement `ValidationMode.Basic`
3. Add enum validation for `Strict` mode
4. Address dictionary evolution with `UnknownTagHandling`
5. Add configuration to session description
6. Document rejection behavior

---

*This is a design sketch. Implementation details may change based on testing and real-world usage patterns.*
