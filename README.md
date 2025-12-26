# PureFix

A high-performance, pure C# FIX protocol engine for .NET.

[![NuGet](https://img.shields.io/nuget/v/PureFix.Transport.svg)](https://www.nuget.org/packages/PureFix.Transport)
[![Build](https://github.com/TimelordUK/cspurefix/actions/workflows/build.yml/badge.svg)](https://github.com/TimelordUK/cspurefix/actions)
[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE)

## Performance

PureFix is designed for low-latency trading applications with minimal allocations:

| Message Type        | Parse Time | Allocated |
|---------------------|------------|-----------|
| Heartbeat           | 6.4 ns     | 40 B      |
| Logon               | 9.2 ns     | 112 B     |
| QuoteRequest        | 8.2 ns     | 96 B      |
| OrderCancelReject   | 12.1 ns    | 216 B     |
| ExecutionReport     | 52.6 ns    | 1,480 B   |

*Benchmarks run on .NET 9.0, see [full benchmark results](https://github.com/TimelordUK/cspurefix/actions)*

## Why PureFix?

### vs QuickFIX/n

| Aspect | PureFix | QuickFIX/n |
|--------|---------|------------|
| **Implementation** | Pure C# | C++ core with C# wrapper |
| **Debugging** | Full source debugging | Limited - crosses native boundary |
| **Custom Types** | Generate types for *your* dictionary | Generic field accessors only |
| **Allocations** | Minimal, designed for low-GC | Allocates heavily during parsing |
| **Code Generation** | First-class tool (`purefix-gen`) | Limited/manual |

### Key Features

- **Strongly-typed messages** - Generate C# classes from any FIX dictionary with full IntelliSense
- **Custom dictionary support** - Generate types for broker-specific extensions, not just vanilla FIX
- **Minimal allocations** - Parse messages without GC pressure
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
