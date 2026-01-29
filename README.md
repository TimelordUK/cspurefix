# PureFix

A high-performance, pure C# FIX protocol engine for .NET.

[![NuGet](https://img.shields.io/nuget/v/PureFix.Transport.svg)](https://www.nuget.org/packages/PureFix.Transport)
[![Build](https://github.com/TimelordUK/cspurefix/actions/workflows/build.yml/badge.svg)](https://github.com/TimelordUK/cspurefix/actions)
[![codecov](https://codecov.io/gh/TimelordUK/cspurefix/graph/badge.svg)](https://codecov.io/gh/TimelordUK/cspurefix)
[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE)

## Project Status: Beta

| Component | Maturity | Notes |
|-----------|----------|-------|
| **Message Parser** | ✅ Production | Extensively tested against 20+ brokers across asset classes |
| **Type Generation** | ✅ Production | Stable API, battle-tested with custom dictionaries |
| **Session Handling** | 🟡 Beta | Robust for normal operation, reconnection, and sequence recovery |
| **Resend/Gap Fill** | 🟡 Beta | Works in common scenarios; edge cases under continued testing |

### Parser Testing Coverage

The message parser has been validated in production drop-copy scenarios (read-only, no order entry) across:

- **Asset Classes**: FX Spot, FX Options, FX Swaps, NDF, NDS, Forwards, CDS, CDX, Government Bonds, Futures
- **Message Types**: ExecutionReport (8), Allocation (J), TradeCaptureReport (AE), and session messages
- **Protocols**: FIX 4.2, 4.4, 5.0 SP2, including FpML extensions
- **Brokers**: 20+ counterparties with broker-specific dictionary customizations

### Test Coverage

| Metric | Coverage |
|--------|----------|
| Line Coverage | 68.8% |
| Branch Coverage | 54.1% |
| Method Coverage | 71.9% |

*Coverage excludes generated FIX type assemblies. Run `./scripts/coverage.sh` for local report.*

### Session Handling Status

Session management handles standard scenarios reliably:
- ✅ Normal logon/logout with heartbeat
- ✅ Reconnection after socket drop or sleep/wake
- ✅ Sequence number persistence and recovery
- ✅ ResetSeqNumFlag handling (initiator, acceptor, both sides)
- ✅ Logon sequence mismatch retry (client catches up to server)
- 🟡 ResendRequest/GapFill edge cases (race conditions under testing)

> **Recommendation**: For order-sending clients, enable `ResendGapFillOnly: true` until resend handling is fully production-validated.

## Performance

PureFix uses a lazy parsing architecture that minimizes allocations:

### Stage 1: Tokenization (buffer to view)

Tokenizes raw bytes into an indexed view. Structure analysis is deferred until needed:

| Message Type      | Fields | Size   | Time   | Allocated (pooled) |
|-------------------|--------|--------|--------|-------------------|
| Heartbeat         | ~10    | 131 B  | 645 ns | 112 B             |
| Logon             | ~22    | 214 B  | 1.0 μs | 112 B             |
| QuoteRequest      | ~30    | 334 B  | 1.5 μs | 112 B             |
| OrderCancelReject | ~370   | 3.9 KB | 17 μs  | 1.2 KB            |
| ExecutionReport   | ~646   | 6.6 KB | 29 μs  | 3.2 KB            |

*Storage and views are pooled and reused.*

Simple field-by-tag access (e.g., `view.GetString(tag)`) uses O(n) linear scan - ideal for session messages, drop copy handlers, and routing where only a few fields are needed.

Structure parsing is triggered on-demand only when accessing components or repeating groups via `GetView()` or `GetGroupInstance()`.

### Stage 2: Field Extraction (view to typed message)

Extracts field values from the view into typed message objects:

| Message Type      | Extract Time | Allocated (new) | Allocated (pooled) |
|-------------------|--------------|-----------------|-------------------|
| Heartbeat         | 4.7 ns       | 40 B            | 0 B               |
| Logon             | 5.9 ns       | 112 B           | 0 B               |
| QuoteRequest      | 5.7 ns       | 96 B            | 0 B               |
| OrderCancelReject | 7.6 ns       | 216 B           | 0 B               |
| ExecutionReport   | 27 ns        | 1,480 B         | 0 B               |

*Message objects can be pooled and reused via `Reset()` for zero-allocation extraction.*

### Zero-Allocation View Access

For routing, filtering, and validation scenarios, the span-based API eliminates per-field allocation entirely:

| Pattern | String API | Span API | Speed | Allocation |
|---------|-----------|----------|-------|------------|
| Single field check | 11.2 ns / 24 B | 8.7 ns / 0 B | **23% faster** | **0 B** |
| Message routing | 11.3 ns / 24 B | 9.2 ns / 0 B | **18% faster** | **0 B** |
| Prefix check | 22.0 ns / 40 B | 9.3 ns / 0 B | **57% faster** | **0 B** |
| Two-field validation | 26.6 ns / 64 B | 14.5 ns / 0 B | **45% faster** | **0 B** |

```csharp
// Zero-allocation message type check
if (view.IsTagEqual(35, "AE"u8))  // vs view.GetString(35) == "AE"
{
    // handle Trade Capture Report
}

// Zero-allocation routing
switch (view.MatchTag(35, "0"u8, "A"u8, "5"u8))
{
    case 0: HandleHeartbeat(view); break;
    case 1: HandleLogon(view); break;
    case 2: HandleLogout(view); break;
}

// Zero-allocation prefix check
if (view.TagStartsWith(8, "FIXT"u8))
{
    // FIXT 1.1 protocol
}

// Raw span access for custom parsing
ReadOnlySpan<byte> clOrdId = view.GetSpan(11);
```

See [docs/msgview-span-api.md](docs/msgview-span-api.md) for full API reference.

*Benchmarks: .NET 9.0 on AMD Ryzen 9 7950X. See [CI benchmarks](https://github.com/TimelordUK/cspurefix/actions).*

## Why PureFix?

### vs QuickFIX/n

| Aspect | PureFix | QuickFIX/n |
|--------|---------|------------|
| **Implementation** | Pure C# | C++ core with C# wrapper |
| **Debugging** | Full source debugging | Limited - crosses native boundary |
| **Custom Types** | Generate types for *your* dictionary | Generic field accessors only |
| **Field Extraction** | Nanosecond extraction, poolable messages | Allocates per field access |
| **Code Generation** | First-class tool (`purefix-gen`) | Limited/manual |

### Key Features

- **Strongly-typed messages** - Generate C# classes from any FIX dictionary with full IntelliSense
- **Custom dictionary support** - Generate types for broker-specific extensions, not just vanilla FIX
- **Pooled message extraction** - Reuse message objects for zero-allocation field extraction
- **Pure C#** - Debug everything, no native interop boundaries
- **Session management** - Logon, heartbeat, sequence numbers, gap fill handled automatically
- **File & memory stores** - Session persistence with QuickFix-compatible file format
- **TLS support** - Secure connections out of the box

### Session Resilience

PureFix sessions are designed to survive network interruptions, including laptop sleep/wake cycles. When a timeout is detected, the session attempts recovery before terminating:

```
[18:56:12] sending test req. state = ActiveNormalSession
[18:56:12] state transition: ActiveNormalSession -> AwaitingProcessingResponseToTestRequest
[18:56:14] Session timeout (attempt 1/3). Attempting recovery - resetting timeout window.
[18:56:16] state transition: AwaitingProcessingResponseToTestRequest -> ActiveNormalSession
[18:56:16] [355,AE]: TradeCaptureReport received - session continues normally
```

After a 7-minute laptop sleep, the session:
1. Detects the timeout and sends a TestRequest
2. Uses the recovery window (3 attempts) instead of immediate termination
3. Receives the peer's response when the network returns
4. Resumes normal message flow without reconnection

This eliminates unnecessary session drops during brief network interruptions or system sleep events.

### Client Safety: ResendGapFillOnly

For **client/initiator sessions**, we strongly recommend enabling `ResendGapFillOnly` in your session configuration:

```json
{
  "resendGapFillOnly": true
}
```

When the counterparty sends a ResendRequest, this setting ensures PureFix responds with a SequenceReset-GapFill instead of replaying stored messages. This prevents accidental re-execution of old orders if the counterparty requests message replay.

**Why this matters**: If a client replays a stored NewOrderSingle from hours ago, the broker may execute it as a new order - causing duplicate fills and potential losses.

> **Note**: Message replay functionality exists but is still in alpha testing. Until fully validated, `ResendGapFillOnly: true` is the safest option for order-sending clients.

## Getting Started

**Want to see it in action?** Check out the [standalone demo](https://github.com/TimelordUK/purefix-standalone-demo) - a complete working example with Trade Capture that you can run immediately.

## Quick Start

```bash
# Install packages
dotnet add package PureFix.Transport

# Install code generator
dotnet tool install purefix-gen

# Generate types from your dictionary
purefix-gen -D FIX44.xml -g -p Generated --nuget --namespace MyApp.Fix
```

### Send a typed message

```csharp
var order = new NewOrderSingle
{
    ClOrdID = "order-123",
    Instrument = new Instrument { Symbol = "AAPL" },
    Side = SideValues.Buy,
    OrderQty = 100,
    OrdType = OrdTypeValues.Market
};

await session.Send(MsgTypeValues.NewOrderSingle, order);
```

### Receive with type safety

```csharp
protected override Task OnApplicationMsg(string msgType, IMessageView view)
{
    if (msgType == MsgType.ExecutionReport)
    {
        var exec = (ExecutionReport)messageFactory.ToFixMessage(view)!;
        Console.WriteLine($"Fill: {exec.Instrument?.Symbol} {exec.LastQty} @ {exec.LastPx}");
    }
    return Task.CompletedTask;
}
```

## Documentation

| Resource | Description |
|----------|-------------|
| [Standalone Demo](https://github.com/TimelordUK/purefix-standalone-demo) | Full working example with Trade Capture |
| [NuGet Package](https://www.nuget.org/packages/PureFix.Transport) | Package documentation |
| [Type Generation](https://github.com/TimelordUK/purefix-standalone-demo#how-type-generation-works) | How dictionaries become C# types |

## Packages

| Package | Purpose |
|---------|---------|
| [PureFix.Transport](https://www.nuget.org/packages/PureFix.Transport) | Session management, TCP, stores |
| [PureFix.Types](https://www.nuget.org/packages/PureFix.Types) | Standard FIX types and utilities |
| [PureFix.Types.Core](https://www.nuget.org/packages/PureFix.Types.Core) | Base interfaces |

## Supported FIX Versions

- FIX 4.0 - 4.4
- FIX 5.0, 5.0 SP1, 5.0 SP2
- Custom/broker-specific dictionaries

## Related Projects

- [jspurefix](https://www.npmjs.com/package/jspurefix) - TypeScript/JavaScript FIX engine (~3k weekly downloads)

## License

MIT
