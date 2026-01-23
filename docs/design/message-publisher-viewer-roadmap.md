# Message Publisher & FIX Viewer Roadmap

## Overview

This document outlines the design for two complementary features:

1. **Message Publisher (Spooler)** - An extensible sink for FIX messages within cspurefix
2. **React FIX Viewer** - A standalone web application for viewing FIX messages (live and offline)

## Architecture

```
┌─────────────────────┐     ┌──────────────────────┐     ┌─────────────────────┐
│  PureFix Session    │────▶│  IMessagePublisher   │────▶│  Persistent Store   │
│  (any application)  │     │  (opt-in sink)       │     │  (Redis/NATS/DB)    │
└─────────────────────┘     └──────────────────────┘     └─────────────────────┘
         │                                                         │
         │                                                         ▼
         ▼                                               ┌─────────────────────┐
    Log files ◀──────────────────────────────────────────│  SignalR Hub        │
         │                                               │  (live relay)       │
         │                                               └─────────────────────┘
         │                                                         │
         ▼                                                         ▼
┌─────────────────────────────────────────────────────────────────────────────┐
│                           React FIX Viewer                                   │
│  ┌─────────────────┐  ┌─────────────────┐  ┌─────────────────────────────┐  │
│  │  Offline Mode   │  │   Live Mode     │  │  Data Dictionary Aware      │  │
│  │  (file upload)  │  │  (SignalR sub)  │  │  (field names, enums, etc)  │  │
│  └─────────────────┘  └─────────────────┘  └─────────────────────────────┘  │
└─────────────────────────────────────────────────────────────────────────────┘
```

---

## Phase 1: Message Publisher (cspurefix)

### Design Goals

- **Opt-in** - Zero overhead if not configured
- **Runs alongside MessageStore** - Does not replace persistence, adds publishing
- **Filterable** - By session, message type, direction (inbound/outbound)
- **Extensible** - Interface-based, multiple implementations possible

### Proposed Interface

```csharp
namespace PureFix.Types;

/// <summary>
/// Optional sink for publishing FIX messages to external systems.
/// Implementations might write to Redis, NATS, databases, or custom queues.
/// </summary>
public interface IMessagePublisher : IDisposable
{
    /// <summary>
    /// Called when a FIX message is sent or received.
    /// </summary>
    /// <param name="context">Message context including session info, direction, timestamp</param>
    /// <param name="rawMessage">The raw FIX message bytes</param>
    ValueTask PublishAsync(MessagePublishContext context, ReadOnlyMemory<byte> rawMessage);
}

public readonly record struct MessagePublishContext
{
    public required string SessionId { get; init; }        // e.g., "init-comp" or "BROKER-A"
    public required string SenderCompId { get; init; }
    public required string TargetCompId { get; init; }
    public required MessageDirection Direction { get; init; }
    public required DateTimeOffset Timestamp { get; init; }
    public required int SeqNum { get; init; }
    public required string MsgType { get; init; }          // e.g., "D", "8", "AE"
}

public enum MessageDirection
{
    Inbound,
    Outbound
}
```

### Configuration

```csharp
// In session configuration
var config = new SessionConfig
{
    // Existing config...

    // New: optional publisher
    MessagePublisher = new RedisMessagePublisher(redisConnection, new PublisherOptions
    {
        StreamName = "fix-messages",
        FilterSessionIds = ["broker-a", "broker-b"],  // null = all sessions
        FilterMsgTypes = null,                         // null = all message types
        IncludeAdminMessages = false                   // skip heartbeats, etc.
    })
};
```

### Potential Implementations

| Package | Backend | Use Case |
|---------|---------|----------|
| `PureFix.Publishers.Redis` | Redis Streams | High-throughput, multiple consumers |
| `PureFix.Publishers.Nats` | NATS JetStream | Lightweight pub/sub, cloud-native |
| `PureFix.Publishers.Sql` | PostgreSQL/SQL Server | Audit trail, compliance |
| `PureFix.Publishers.File` | JSON Lines file | Simple, portable |

### Integration Point

The publisher would be invoked in the session's message processing pipeline, after successful send/receive but potentially in parallel with the message store write.

---

## Phase 2: React FIX Viewer

### Design Goals

- **Standalone application** - Not embedded in demo app
- **Two modes**: Offline (file upload) and Live (SignalR subscription)
- **Data dictionary aware** - Human-readable field names, enum values
- **High performance** - AG Grid for large message volumes

### Technology Stack

- React 18+ with TypeScript
- AG Grid (Community or Enterprise)
- SignalR client for live mode
- TailwindCSS or similar for styling

### UI Layout (Draft)

```
┌────────────────────────────────────────────────────────────────────────────┐
│  Mode: [Offline ▼]  │  Dictionary: [FIX44 ▼]  │  Filter: [________]        │
├────────────────────────────────────────────────────────────────────────────┤
│                                                                            │
│  ┌─ Message Summary Grid ───────────────────────────────────────────────┐  │
│  │ Time       │ Dir │ MsgType    │ Sender   │ Target   │ ClOrdID │ ... │  │
│  │ 10:30:01.5 │ OUT │ NewOrder(D)│ CLIENT-A │ BROKER-X │ ORD-001 │     │  │
│  │ 10:30:01.7 │ IN  │ ExecRpt(8) │ BROKER-X │ CLIENT-A │ ORD-001 │     │  │
│  │ 10:30:02.1 │ OUT │ NewOrder(D)│ CLIENT-A │ BROKER-X │ ORD-002 │     │  │
│  └──────────────────────────────────────────────────────────────────────┘  │
│                                                                            │
│  ┌─ Selected Message Detail ────────────────────────────────────────────┐  │
│  │                                                                      │  │
│  │  ┌─ All Tags Grid ──────────────┐  ┌─ JSON View ──────────────────┐  │  │
│  │  │ Tag │ Name      │ Value      │  │ {                            │  │  │
│  │  │ 8   │ BeginStr  │ FIX.4.4    │  │   "header": {                │  │  │
│  │  │ 35  │ MsgType   │ D (New...) │  │     "BeginString": "FIX.4.4",│  │  │
│  │  │ 49  │ SenderCo  │ CLIENT-A   │  │     "MsgType": "D",          │  │  │
│  │  │ 56  │ TargetCo  │ BROKER-X   │  │     ...                      │  │  │
│  │  │ ... │           │            │  │   },                         │  │  │
│  │  └─────────────────────────────-┘  │   "body": { ... }            │  │  │
│  │                                    │ }                            │  │  │
│  │                                    └──────────────────────────────┘  │  │
│  └──────────────────────────────────────────────────────────────────────┘  │
└────────────────────────────────────────────────────────────────────────────┘
```

### Offline Mode Features

- Drag-and-drop or file picker for FIX log files
- Parse standard FIX log format (pipe-delimited, SOH-delimited)
- Data dictionary upload (QuickFIX XML format) for field resolution
- Export filtered results to CSV/JSON

### Live Mode Features

- Connect to SignalR hub URL
- Subscribe to specific sessions or all
- Real-time grid updates (append or rolling window)
- Pause/resume stream
- Same detail view as offline

### Data Dictionary Integration

The viewer needs access to a FIX data dictionary for:
- Resolving tag numbers to field names (e.g., 35 → MsgType)
- Resolving enum values (e.g., MsgType "D" → "NewOrderSingle")
- Understanding repeating groups structure

Options:
1. **Upload dictionary file** - User provides QuickFIX XML
2. **Fetch from server** - SignalR hub provides dictionary on connect
3. **Embedded common dictionaries** - Bundle FIX42, FIX44, FIX50SP2

---

## Phase 3: SignalR Hub (Bridge)

A small ASP.NET Core service that:
- Subscribes to the message queue (Redis/NATS)
- Relays messages to connected SignalR clients
- Optionally serves data dictionaries
- Handles client filtering preferences

This could be:
- A separate microservice
- Part of an existing .NET backend
- A simple console app for demo purposes

---

## Open Questions

1. **Message format over the wire** - Raw FIX bytes? JSON? Both?
2. **Authentication** - Required for live mode in production?
3. **Message replay** - Should the hub support historical replay from the queue?
4. **Multi-session view** - How to visualize messages from multiple sessions?
5. **Correlation** - Link related messages (order → executions)?

---

## Implementation Order

1. **`IMessagePublisher` interface** in PureFix.Types
2. **`PureFix.Publishers.File`** - JSON Lines output (simplest first)
3. **React viewer - offline mode** - File upload, AG Grid, dictionary support
4. **`PureFix.Publishers.Redis`** or NATS
5. **SignalR hub** - Bridge queue to browser
6. **React viewer - live mode** - SignalR integration

---

## Related Files

- `PureFix.Types/IMessageStore.cs` - Existing persistence interface
- `PureFix.LogMessageParser/` - Existing log parsing utilities
- `purefix-standalone-demo/` - Demo app for testing integration
