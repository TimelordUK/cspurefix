# PureFix

A high-performance, pure C# FIX protocol engine for .NET.

[![NuGet](https://img.shields.io/nuget/v/PureFix.Transport.svg)](https://www.nuget.org/packages/PureFix.Transport)
[![Build](https://github.com/TimelordUK/cspurefix/actions/workflows/build.yml/badge.svg)](https://github.com/TimelordUK/cspurefix/actions)
[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE)

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
