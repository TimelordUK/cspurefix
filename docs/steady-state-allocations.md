# Steady-State Allocation Analysis

Analysis of ongoing allocations in PureFix during steady-state operation (after startup).
These are allocations that occur continuously while the session is running, as opposed to
per-message parsing allocations (see `optimization-opportunities.md`).

## Current Performance

**Baseline (skeleton mode - heartbeats only):** ~1-5 KB/s allocation rate, zero GC collections

This is excellent for most use cases. The library is well-suited for:
- Trade capture
- Drop copy
- Post-trade booking
- Any scenario where microsecond latency is not critical

### Recent Improvement (v0.1.10-alpha)

Removed redundant `AsyncWorkQueue` layer - the `Channel<SessionEvent>` in `EventDispatcher`
already provides serialization for timer and RX events. This reduced steady-state allocations
from ~11 KB/s to ~1-5 KB/s (a **2-10x improvement**).

## Identified Allocation Sources

### 1. String Interpolation in Logging (Medium Impact)

**Location:** `AsciiSession.cs` lines 64, 76, 147, 265 and throughout Transport code

**Current pattern:**
```csharp
logger?.Info($"peerLogon Username = {userName}, heartBtInt = {heartBtInt}");
```

**Problem:** String interpolation allocates even when the log level is disabled.

**Fix (when needed):**
```csharp
// Option A: LoggerMessage.Define (zero-alloc, best for hot paths)
private static readonly Action<ILogger, string, int, Exception?> LogPeerLogon =
    LoggerMessage.Define<string, int>(LogLevel.Information, 0,
        "peerLogon Username = {UserName}, heartBtInt = {HeartBtInt}");

LogPeerLogon(logger, userName, heartBtInt, null);

// Option B: Conditional check (simpler, good for less critical paths)
if (logger?.IsEnabled(LogLevel.Information) == true)
    logger.Info($"peerLogon Username = {userName}");
```

**Estimated savings:** ~3-5 KB/s

---

### 2. TestReqID/Heartbeat String Generation (Low Impact)

**Location:** `AsciiSession.cs` lines 64, 76

**Current pattern:**
```csharp
var text = $"test-req-{now}";
text ??= $"heartbeat-{now}";
```

**Problem:** Allocates a new string for each test request/heartbeat.

**Fix (when needed):**
```csharp
// Pre-allocate buffer and format in-place
private readonly char[] _testReqBuffer = new char[32];
private int FormatTestReqId(long timestamp)
{
    // Format directly into buffer, return length
}
```

**Estimated savings:** ~0.5 KB/s

---

### 3. Task.Delay in Reconnection Loop (Low Impact)

**Location:** `TcpInitiatorConnector.cs`

**Current pattern:**
```csharp
await Task.Delay(TimeSpan.FromSeconds(reconnectSeconds), cancellationToken);
```

**Note:** `PeriodicTimer` is already used elsewhere which is better. This is only in the reconnect loop which runs infrequently.

**Estimated savings:** Minimal (only during reconnection)

---

### 4. CancellationTokenSource.CreateLinkedTokenSource (Low Impact)

**Location:** `FixSession.cs`

**Current pattern:**
```csharp
m_MySource = CancellationTokenSource.CreateLinkedTokenSource(m_parentToken.Value);
```

**Note:** This is necessary for proper cancellation propagation. The allocation happens once per session, not per message.

**Estimated savings:** Not worth changing

---

### 5. Async State Machine Boxing (Very Low Impact)

**Location:** 66 async methods in Transport code

**Problem:** When `await` actually suspends (doesn't complete synchronously), the compiler-generated state machine struct gets boxed to the heap.

**Note:** Most socket operations complete synchronously when data is available, so boxing is infrequent. Eliminating async would require significant architectural changes.

**Estimated savings:** Minimal in practice

---

### 6. PeriodicTimer Ticks (Very Low Impact)

**Location:** `TcpInitiatorConnector.cs`, timer infrastructure

**Note:** This is .NET runtime overhead. Each timer tick has minimal allocation from the timer infrastructure.

**Estimated savings:** Not addressable without eliminating timers

---

## Priority Matrix

| Optimization | Effort | Savings | Priority |
|--------------|--------|---------|----------|
| LoggerMessage.Define for hot paths | Low | ~1-2 KB/s | **Low** |
| Conditional logging checks | Low | ~0.5 KB/s | **Low** |
| Pre-allocate test req strings | Low | ~0.2 KB/s | **Very Low** |
| **Total achievable** | **Low** | **~2-3 KB/s** | |

This could potentially reduce steady-state from ~1-5 KB/s to sub-1 KB/s, approaching zero-allocation.

---

## What Cannot Be Easily Changed

The remaining ~1-5 KB/s comes from:

1. **.NET runtime internals** - Timer infrastructure, thread pool work items
2. **Async state machine overhead** - When awaits actually suspend
3. **Channel operations** - Internal buffering in `Channel<T>`
4. **Logging overhead** - String formatting for log messages

To go lower would require:
- Eliminating async/await (synchronous + spin-wait)
- Custom timer implementation (wheel timers)
- Zero-allocation logging (LoggerMessage.Define everywhere)

**These changes would significantly increase code complexity and are not recommended** unless targeting ultra-low-latency scenarios (sub-microsecond).

---

## Recommendation

**Current performance is excellent - no further optimization needed.**

Current performance (~1-5 KB/s, zero steady-state GC) is approaching zero-allocation and is
excellent for all target use cases:
- Trade capture
- Drop copy
- Post-trade booking
- Market data distribution

The v0.1.10-alpha release achieved the major win by removing the redundant `AsyncWorkQueue`.
Further optimizations would provide diminishing returns.

If users report GC pause issues in production, consider:
1. `LoggerMessage.Define` for frequently-hit log statements
2. Conditional logging guards

The library prioritizes **readability and maintainability** over the last few KB/s of allocation reduction.

---

## Profiling Commands

To measure current allocation rate:
```bash
# Run skeleton mode and observe GC stats
dotnet run -- --skeleton --timeout 60 2>&1 | grep '\[GC\]'

# Capture allocation trace (requires dotnet-trace)
dotnet-trace collect -p <PID> --providers Microsoft-Windows-DotNETRuntime:0x80000:5
```

---

## References

- `optimization-opportunities.md` - Per-message parsing allocations
- `memory-optimization.md` - Lazy structure parsing design
- [LoggerMessage.Define docs](https://learn.microsoft.com/en-us/dotnet/core/extensions/high-performance-logging)
