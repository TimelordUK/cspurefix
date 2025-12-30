# Sequence Number Mismatch Fix - Implementation Summary

## Problem Statement
When client starts from file store with seq num 10, but server expects seq num 20:
- Server rejects the logon
- Client was stuck in `InitiationLogonSent` state
- Eventually times out, reconnects, but uses same seq num 10 - infinite loop

## Solution Implemented

### Changes Made to `PureFix.Transport/Ascii/AsciiSession.cs`:

1. **Added retry counter and limit** (lines 23-24):
   ```csharp
   private int m_logonRetryCount;
   private const int MaxLogonRetries = 100;
   ```

2. **Added `PrepareForReconnect` override** (lines 27-36):
   - Resets `m_logonRetryCount = 0` on reconnection
   - Calls `base.PrepareForReconnect()`

3. **Enhanced Reject case handling** (lines 487-509):
   - Extracts `RefSeqNum` (tag 45), `RefMsgType` (tag 372), `Text` (tag 58), `SessionRejectReason` (tag 373)
   - Detects if rejected message was a Logon while in `InitiationLogonSent` state
   - Calls `HandleLogonRejected` if so

4. **Added `HandleLogonRejected` method** (lines 306-344):
   - Increments retry counter
   - If exceeds MaxLogonRetries: logs warning, sets state to `PeerLogonRejected`, stops
   - Otherwise: increments sequence number in encoder and session store, retries logon

5. **Reset retry counter on successful logon** (lines 204-205):
   - Added `m_logonRetryCount = 0` before `OnReady` is called

### Changes Made to `PureFix.Transport/Session/FixSession.cs`:

1. **Made `PrepareForReconnect` virtual** (line 381):
   - Changed `public void PrepareForReconnect()` to `public virtual void PrepareForReconnect()`

## Build Status
- BUILD SUCCEEDED with 0 errors, 0 warnings

## Testing Status
- **ALL 407 TESTS PASS** (including 1 new test)
- 11 tests skipped (pre-existing)
- Run: `dotnet test PureFix.Test.ModularTypes/PureFix.Test.ModularTypes.csproj`

## Bug Fix Applied
During test development, discovered the `HandleLogonRejected` method was double-incrementing the sequence number.
The encoder already increments MsgSeqNum after each message is sent, so the manual increment was causing
sequence numbers to jump by 2 instead of 1 (e.g., 1→3→5 instead of 1→2→3→4→5).

**Fixed** by removing the manual increment in `HandleLogonRejected` - just retry the logon and the encoder
will naturally use the next sequence number.

## New Test Added
**`Initiator_Sequence_Mismatch_Retry_Test`** in `SessionTest.cs`:
- Uses mock server via TestMessageTransport
- Server rejects Logon until sequence reaches 5
- Verifies client retries with incrementing sequence numbers (1, 2, 3, 4, 5)
- Verifies session establishes successfully when sequence catches up

## Completed Items
- ✅ All existing tests pass
- ✅ New unit test verifies sequence mismatch recovery
- ✅ Bug fixed: sequence numbers now increment by 1 (not 2)

## Optional Future Work
### Add demo app truncation feature
Command-line switch `--truncate-seq N` to truncate client's file store for manual testing.

## Key Files
- `PureFix.Transport/Ascii/AsciiSession.cs` - Main changes
- `PureFix.Transport/Session/FixSession.cs` - Made PrepareForReconnect virtual
- `PureFix.Test.ModularTypes/SessionTest.cs` - Existing tests
- `PureFix.Test.ModularTypes/Helpers/TestMessageTransport.cs` - Mock transport

## Flow Summary
```
Client (seq 10) -> Logon -> Server (expects 20)
  [encoder increments to 11 after send]
Server -> Reject (RefMsgType=A, RefSeqNum=10)
Client -> HandleLogonRejected()
         - Encoder already at 11 (incremented after previous send)
         - Retry Logon (seq 11)
  [encoder increments to 12]
Server -> Reject (RefSeqNum=11)
... repeat ...
Client -> Logon (seq 20)
Server -> Logon response
Client -> OnReady(), reset retry counter
```
