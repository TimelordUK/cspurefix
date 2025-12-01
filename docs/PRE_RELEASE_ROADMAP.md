# Pre-Release Roadmap

This document outlines the key areas to focus on before the initial NuGet release of PureFix.

## Current Status

- **Parser**: Production-tested with 20+ brokers via React GUI integration
- **Message Store**: QuickFix-compatible file format with comprehensive tests
- **Type Generation**: Working for FIX 4.4 and FIX 5.0 SP2
- **Session Management**: Implemented but requires real-world socket testing

## Priority 1: Real Broker Connection Testing

The FIX engine has not been tested with actual broker connections over sockets. This is the critical next step.

### Tasks
- [ ] Connect to a test/sandbox broker environment
- [ ] Verify logon/logout handshake
- [ ] Test message flow (orders, executions, market data)
- [ ] Validate sequence number handling and recovery
- [ ] Test session reset scenarios
- [ ] Verify heartbeat/test request handling under real network conditions

## Priority 2: Socket Code Review

Careful review of the transport layer for robustness.

### Areas to Review
- [ ] Async socket read/write operations
- [ ] Buffer management during partial reads
- [ ] Connection error handling and recovery
- [ ] Reconnection logic and backoff strategies
- [ ] TLS/SSL handling
- [ ] Network timeout handling

### Key Files
- `PureFix.Transport/` - Transport layer implementation
- Socket read/write loops
- Connection state management

## Priority 3: Memory Safety Audit

Several objects are cached for performance. Need to ensure thread safety and proper lifecycle management.

### Areas to Review
- [ ] Cached view objects (`AsciiView`, `MsgView`)
- [ ] Buffer pooling and reuse (`ElasticBuffer`)
- [ ] Parser state between messages
- [ ] Session state objects
- [ ] Message store index caching

### Concerns
- Thread safety when objects are reused
- Proper disposal of resources
- No dangling references to pooled objects
- Memory leaks under sustained load

## Priority 4: Performance Optimization

Look for easy wins in hot paths before release.

### Auto-Generated Types
- [ ] Review generated component classes (e.g., `InstrmtGrp.cs`)
- [ ] Check for unnecessary allocations in property accessors
- [ ] Consider source generators for compile-time optimization

### Parser Performance
- [ ] Profile parsing hot paths
- [ ] Minimize allocations in tag/value extraction
- [ ] Use `Span<byte>` where possible instead of string allocations
- [ ] Review dictionary lookups for field definitions

### Buffer Management
- [ ] Implement buffer pooling (ArrayPool)
- [ ] Reduce copying during message assembly
- [ ] Optimize checksum calculation

### String Handling
- [ ] Avoid unnecessary string allocations in hot paths
- [ ] Use `ReadOnlySpan<char>` for comparisons
- [ ] Cache frequently used strings

## Priority 5: Pre-Release Checklist

### Testing
- [ ] All unit tests passing on Windows and Linux
- [ ] Integration tests with mock sessions
- [ ] Real broker connection tests
- [ ] Stress test: sustained message throughput
- [ ] Memory profiling under load (no leaks)
- [ ] Long-running stability test (hours/days)

### Documentation
- [ ] API documentation for public types
- [ ] Usage examples
- [ ] Configuration guide
- [ ] Troubleshooting guide

### Package Preparation
- [ ] Review public API surface
- [ ] Mark internal types appropriately
- [ ] Version numbering strategy
- [ ] License file
- [ ] NuGet package metadata
- [ ] README for NuGet page

### CI/CD
- [ ] GitHub Actions passing on all platforms
- [ ] Automated NuGet publishing workflow
- [ ] Release tagging strategy

## Timeline

These tasks should be completed in order of priority. The project is very close to release-ready after completing the broker connection testing and memory safety audit.

## Notes

- The parser is already battle-tested in production with React GUI
- The message store now has comprehensive format verification tests
- Stream abstraction (`ISessionStreamProvider`) enables easy testing without file I/O
- QuickFix-compatible file formats ensure interoperability
