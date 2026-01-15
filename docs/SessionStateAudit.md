# Session State Audit

## Current State Distribution

### 1. AsciiEncoder
| State | Type | Purpose |
|-------|------|---------|
| `MsgSeqNum` | `int` | Next outgoing sequence number to use |

**Updated by**: `Encode()` (auto-increment), `BaseApp.OnRun()` (init), `AsciiSession` (resets)

### 2. FixSessionState
| State | Type | Purpose |
|-------|------|---------|
| `LastPeerMsgSeqNum` | `int?` | Last received sequence from peer |
| `State` | `SessionState` | Session state machine |
| `LastReceivedAt` | `DateTime?` | Timing for heartbeat/timeout |
| `LastSentAt` | `DateTime?` | Timing for heartbeat |
| `LastTestRequestAt` | `DateTime?` | Timing for timeout detection |
| `LogoutSentAt` | `DateTime?` | Timing for logout confirm |
| `PeerHeartBeatSecs` | `int?` | Peer's heartbeat interval |
| `CompID`, `PeerCompID` | `string?` | Identity |

**Updated by**: `FixSession` (various), `AsciiSession` (resets, message receipt)

### 3. IFixSessionStore
| State | Type | Purpose |
|-------|------|---------|
| `SenderSeqNum` | `int` | Persisted next outgoing sequence |
| `TargetSeqNum` | `int` | Persisted expected incoming sequence |
| Messages | stored | For resend support |

**Updated by**: `AsciiSession` (after encode, on reset), `BaseApp` (init)

### 4. AsciiSession (circuit breaker / retry state)
| State | Type | Purpose |
|-------|------|---------|
| `m_lastResendRequestBeginSeq` | `int?` | Circuit breaker: pending request start |
| `m_pendingResendEndSeq` | `int?` | Circuit breaker: pending request end |
| `m_resendRequestDuplicateCount` | `int` | Circuit breaker: blocked request count |
| `m_logonRetryCount` | `int` | Logon retry for seq mismatch |
| `m_timeoutRecoveryAttempts` | `int` | Timeout recovery counter |

**Updated by**: `AsciiSession` (gap detection, message receipt, timeout)

---

## Reset Scenarios

### Scenario 1: PrepareForReconnect (new socket, same session)
**Trigger**: Transport disconnected, client reconnecting

**Current behavior**:
```
FixSession.PrepareForReconnect():
  - m_sessionState.State = Idle
  - m_sessionState.LastReceivedAt = null
  - m_sessionState.LastSentAt = null
  - m_sessionState.LastTestRequestAt = null
  - m_sessionState.LogoutSentAt = null
  - m_transport = null

AsciiSession.PrepareForReconnect():
  - m_logonRetryCount = 0
  - m_lastResendRequestBeginSeq = null
  - m_pendingResendEndSeq = null
  - m_resendRequestDuplicateCount = 0
  - m_timeoutRecoveryAttempts = 0
```

**NOT reset**: `m_encoder.MsgSeqNum`, `m_sessionStore` seqnums, `m_sessionState.LastPeerMsgSeqNum`

### Scenario 2: Initiator with ResetSeqNumFlag=Y (OnPreLogon)
**Trigger**: Config has `ResetSeqNumFlag=true`, about to send Logon

**Current behavior**:
```
AsciiSession.OnPreLogon():
  - await m_sessionStore.Reset()  // Clears messages, resets seqnums to 1
  - m_encoder.MsgSeqNum = 1
  - m_sessionState.LastPeerMsgSeqNum = 0
  - m_resender = new FixMsgAsciiStoreResend(...)  // Fresh resender
  - await OnSessionReset()  // Notifies derived classes
```

### Scenario 3: Peer sends Logon with ResetSeqNumFlag=Y (PeerLogon)
**Trigger**: Received Logon with `ResetSeqNumFlag=Y`

**Current behavior**:
```
AsciiSession.PeerLogon():
  - weAlsoReset = m_config.ResetSeqNumFlag()
  - savedEncoderSeqNum = weAlsoReset ? m_encoder.MsgSeqNum : null
  - await m_sessionStore.Reset()
  - m_encoder.MsgSeqNum = savedEncoderSeqNum ?? m_sessionStore.SenderSeqNum
  - m_resender = new FixMsgAsciiStoreResend(...)
  - m_sessionState.LastPeerMsgSeqNum = peerSeqNum
  - await m_sessionStore.SetTargetSeqNum(peerSeqNum + 1)
  - await OnSessionReset()
```

### Scenario 4: Acceptor responds with ResetSeqNumFlag=Y (when peer didn't)
**Trigger**: Acceptor config has `ResetSeqNumFlag=true`, peer sent normal Logon

**Current behavior**:
```
AsciiSession.PeerLogon() (acceptor branch):
  - m_sessionState.LastPeerMsgSeqNum = 0
  - await m_sessionStore.SetTargetSeqNum(1)
  - await m_sessionStore.Reset()
  - m_encoder.MsgSeqNum = 1
  - m_resender = new FixMsgAsciiStoreResend(...)
```

---

## Problems with Current Design

1. **Scattered updates**: Same logical state (sequence numbers) updated in multiple places
2. **Inconsistent patterns**: Some resets call `OnSessionReset()`, others don't
3. **Hidden dependencies**: Encoder seqnum must match store seqnum, but nothing enforces this
4. **Testing difficulty**: Can't test sequence logic without full session
5. **Race conditions risk**: Multiple components read/write same conceptual state
6. **Unclear ownership**: Who "owns" the sequence numbers?

---

## Proposed: SessionSequenceCoordinator

Single source of truth for all sequence-related state:

```csharp
public class SessionSequenceCoordinator
{
    // THE source of truth
    private int _nextSenderSeqNum = 1;
    private int _expectedTargetSeqNum = 1;

    // Delegates
    private readonly IFixSessionStore _store;
    private readonly ResendRequestManager _resendManager;

    // Events
    public event Func<Task>? OnReset;

    // Initialization
    public async Task InitializeFromStore();
    public async Task InitializeFromConfig(int? senderSeq, int? targetSeq);

    // Sequence access (single point of truth)
    public int NextSenderSeqNum => _nextSenderSeqNum;
    public int ExpectedTargetSeqNum => _expectedTargetSeqNum;

    // Sequence mutations (controlled)
    public int ConsumeNextSenderSeqNum();  // Returns and increments
    public void OnMessageReceived(int seqNum, bool possDup);
    public void OnGapFillReceived(int seqNum, int newSeqNo);

    // Reset operations (coordinated)
    public async Task ResetBoth(string reason);
    public async Task ResetForReconnect();  // Clears transient state only
    public async Task HandlePeerReset(int peerSeqNum, bool weAlsoReset);

    // Resend management (delegated)
    public ResendAction OnGapDetected(int expected, int received);
    public void RecordResendRequestSent(int begin, int end);
}
```

### Benefits
1. **Single truth**: All sequence state in one place
2. **Coordinated resets**: One method handles store + encoder + state
3. **Testable**: Unit test all scenarios without full session
4. **Clear ownership**: Coordinator owns sequences, others query it
5. **Encapsulated resend**: ResendRequestManager hidden inside
