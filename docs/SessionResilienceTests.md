# Session Resilience Unit Tests

This document outlines unit tests needed to ensure session resilience logic remains stable. These tests should be implemented once real-world soak testing validates the current implementation.

## Background

The session resilience features handle complex scenarios including:
- Sleep/wake reconnection
- Sequence number gaps and ResendRequests
- Out-of-order message delivery
- Concurrent/overlapping ResendRequests
- ResetSeqNumFlag handling

These scenarios are difficult to reproduce in unit tests but critical to verify to prevent regressions.

## Suggested Unit Tests

### 1. Gap Detection and ResendRequest

```
Test: Single_Gap_Sends_One_ResendRequest
- Receive message with seq 10 when expecting seq 8
- Verify ResendRequest is sent for range 8-9
- Verify message seq 10 is still processed (not blocked)

Test: Circuit_Breaker_Blocks_Duplicate_ResendRequest
- Receive message with seq 10 when expecting seq 8
- First ResendRequest sent for 8-9
- Receive message with seq 12 when expecting seq 11
- Verify second ResendRequest is BLOCKED (only one pending allowed)
- Verify warning logged about pending request
```

### 2. Delayed Message Acceptance

```
Test: Delayed_Message_In_Gap_Range_Accepted
- Receive seq 10, gap detected, ResendRequest sent for 8-9
- LastPeerMsgSeqNum is now 10
- Receive seq 9 (delayed original, no PossDupFlag)
- Verify seq 9 is ACCEPTED (not rejected as "too low")
- Verify message is processed normally

Test: Delayed_Message_Outside_Gap_Range_Rejected
- Receive seq 10, gap detected, ResendRequest sent for 8-9
- Receive seq 5 (outside gap range)
- Verify seq 5 is REJECTED as "MsgSeqNum too low"
- Verify session terminates
```

### 3. PossDupFlag Handling

```
Test: PossDupFlag_Message_Bypasses_Sequence_Check
- Set LastPeerMsgSeqNum to 10
- Receive message with seq 5 and PossDupFlag=Y
- Verify message is ACCEPTED (bypass sequence check)
- Verify LastPeerMsgSeqNum unchanged (still 10)

Test: Gap_Fill_With_PossDupFlag_Clears_Circuit_Breaker
- Pending ResendRequest for range 8-10
- Receive seq 10 with PossDupFlag=Y (end of gap)
- Verify circuit breaker is cleared
- Verify new ResendRequests can now be sent
```

### 4. SequenceReset Handling

```
Test: SequenceReset_Advances_Expected_Sequence
- LastPeerMsgSeqNum is 5
- Receive SequenceReset with NewSeqNo=20
- Verify LastPeerMsgSeqNum is now 19 (20-1)
- Verify next expected sequence is 20

Test: SequenceReset_Clears_ResendRequest_Circuit_Breaker
- Pending ResendRequest for range 8-15
- Receive SequenceReset with NewSeqNo=20
- Verify circuit breaker is cleared
- Verify new ResendRequests can be sent
```

### 5. ResetSeqNumFlag Handling

```
Test: Initiator_Resets_Before_Logon_When_ResetSeqNumFlag
- Configure initiator with ResetSeqNumFlag=Y
- Session store has SenderSeqNum=100
- Initiate logon
- Verify logon is sent with MsgSeqNum=1 (not 100)
- Verify encoder is reset to 1 before sending

Test: Acceptor_Resets_When_Peer_Sends_ResetSeqNumFlag
- Acceptor receives logon with ResetSeqNumFlag=Y and seq=1
- Acceptor had LastPeerMsgSeqNum=500
- Verify acceptor resets to expect seq 2
- Verify session store is cleared
- Verify OnSessionReset is called (recovery store cleared)

Test: Both_Sides_Reset_Encoder_Preserved
- Both sides have ResetSeqNumFlag=Y
- Initiator sends logon with seq=1, encoder advances to 2
- Acceptor responds with logon seq=1, ResetSeqNumFlag=Y
- Verify initiator encoder stays at 2 (not reset again)
```

### 6. ResendGapFillOnly Safety Mode

```
Test: GapFillOnly_Mode_Never_Replays_Messages
- Configure ResendGapFillOnly=true
- Store has messages for seq 10-20
- Receive ResendRequest for seq 10-20
- Verify response is single SequenceReset-GapFill to seq 21
- Verify NO stored messages are replayed

Test: GapFillOnly_Mode_False_Replays_Messages
- Configure ResendGapFillOnly=false (or null)
- Store has messages for seq 10-20
- Receive ResendRequest for seq 10-20
- Verify stored messages ARE replayed with PossDupFlag=Y
```

### 7. Timeout Recovery

```
Test: Timeout_Recovery_Allows_Multiple_Attempts
- Session enters AwaitingProcessingResponseToTestRequest
- First timeout occurs
- Verify session attempts recovery (attempt 1/3)
- Verify session does NOT terminate
- Second timeout occurs
- Verify attempt 2/3
- Third timeout occurs
- Verify attempt 3/3, then session terminates

Test: Message_Receipt_Resets_Timeout_Counter
- Timeout recovery attempt 2/3
- Receive valid message
- Verify timeout counter resets to 0
- Next timeout starts at attempt 1/3 again
```

### 8. Reconnection Scenarios

```
Test: Reconnect_Clears_Circuit_Breakers
- Session has pending ResendRequest
- Connection drops, PrepareForReconnect called
- Verify m_lastResendRequestBeginSeq is null
- Verify m_pendingResendEndSeq is null
- Verify m_resendRequestDuplicateCount is 0

Test: Reconnect_With_ResetSeqNumFlag_Clears_Recovery_Store
- Session has recovery store with messages
- Reconnect with ResetSeqNumFlag=Y on both sides
- Verify recovery store is cleared (IFixLogRecovery.Clear called)
```

### 9. Edge Cases

```
Test: Zero_Gap_No_ResendRequest
- Receive message with expected sequence (no gap)
- Verify no ResendRequest is sent
- Verify message processed normally

Test: Negative_Sequence_Delta_Rejected
- LastPeerMsgSeqNum is 10
- Receive message with seq 5 (not in any gap range)
- Verify rejected as "MsgSeqNum too low"

Test: Very_Large_Gap_Single_ResendRequest
- Receive seq 1000 when expecting seq 10
- Verify single ResendRequest for 10-999
- Verify message 1000 still processed

Test: SequenceReset_Does_Not_Rewind_LastPeerMsgSeqNum
- Receive seq 45 (out of order, gap 42-44), LastPeerMsgSeqNum = 45
- Receive seq 46, LastPeerMsgSeqNum = 46
- Receive SequenceReset GapFill with NewSeqNo=45 (filling gap 42-44)
- Verify LastPeerMsgSeqNum stays at 46 (NOT rewound to 44)
- Verify next expected seq is still 47
- Note: This prevents cascading ResendRequests when messages arrive out of order
```

## Test Infrastructure Needed

1. **MockTransport** - Simulate message delivery with controlled timing/ordering
2. **MockClock** - Control time for timeout testing
3. **MockSessionStore** - Track sequence numbers and stored messages
4. **TestSession** - Expose internal state for verification

## Priority Order

1. ResendGapFillOnly tests (safety critical)
2. Gap detection and circuit breaker tests
3. Delayed message acceptance tests
4. ResetSeqNumFlag tests
5. Timeout recovery tests
6. Edge cases

## Notes

- Real-world soak testing (sleep/wake cycles) has been used to validate these scenarios
- Unit tests should be added to prevent regressions as the codebase evolves
- Consider using a test harness that can simulate network delays and message reordering
