# CSPureFix Worklog

Rolling log of what was done, what is outstanding, and what to pick up next.
Newest session first. Unlike the one-shot planning docs (`IMPROVEMENT_PLAN.md`,
`PRE_RELEASE_ROADMAP.md`) this is meant to be appended to every session.

---

## 2026-08-03 — Acceptor connection isolation, TLS, per-counterparty stores

Released **0.3.1-beta**. Triggered by two jspurefix issues
([#153](https://github.com/TimelordUK/jspurefix/issues/153),
[#151](https://github.com/TimelordUK/jspurefix/issues/151)) and a deep dive to find
the same classes of defect here.

### Shipped

**0.3.0-beta — connection isolation and TLS** (`fix/acceptor-connection-isolation-and-tls`)

- **Per-connection scope.** New `ISessionScope` / `ISessionScopeFactory` /
  `DefaultSessionScopeFactory` / `ScopedFixConfig`. `ISessionFactory.MakeSession` now
  takes a scope (breaking). Every connection gets its own `AsciiParser`,
  `AsciiEncoder`, `SessionDescription` and `ISessionMessageFactory`. Previously an
  acceptor handed every session the same container singletons, so two counterparties
  interleaved bytes into one parse buffer and consumed each other's MsgSeqNum.
  The session message factory has to be per-scope too - it reads the CompIDs off the
  description every time it builds a header.
- **Wildcard TargetCompID binding deferred to peer Logon.** No longer mutates shared
  config. `SessionId`, session store, coordinator and registry registration are all
  bound once the Logon names the counterparty, before `CheckSeqNo` touches the store.
  Each peer gets its own store and registry key, so a second client no longer evicts
  the first.
- **TLS fails closed.** `AsStream` used to log and swallow handshake failures, leaving
  a live plaintext `NetworkStream` that send/receive fell back to - Logon credentials
  went out in the clear. Now tears down and throws.
- **TLS config.** Added `Ca` trust anchors (custom root trust, so a private CA no
  longer forces verification off), `RequestClientCertificate` /
  `RequireClientCertificate`, `CheckCertificateRevocation`.
  `ValidateServerCertificate` now defaults to **true**. `TlsOptions.Validate()` reports
  a populated-but-not-`enabled` block and names unrecognised keys via
  `[JsonExtensionData]`.
- **Acceptor lifecycle.** Connection handlers are awaited (was
  `Task.Factory.StartNew` with no `Unwrap`, so faults were unobserved), transports are
  disposed, TCP keep-alive via new `TcpTransportDescription.KeepAliveMs`, connection
  census logging. `MakeListenEndPoint` understands `0.0.0.0`/`::`. `FixSession.OnRx`
  returns pooled buffers and views when a handler throws.

**0.3.0-beta — per-counterparty stores** (same branch, second commit)

- `SessionId.ToFilePrefix()` sanitises each component. With wildcard binding the store
  filename comes from the peer's tag 49, so `../../..` wrote outside the store
  directory and `*`/`/`/`:` threw on connect. `GetFilePath` asserts containment.
  `ToString()` stays unsanitised so CompIDs differing only in unsafe characters remain
  distinct registry keys.
- Session store is now disposed (`FixSession.OnSessionEnded` → `AsciiSession`). It never
  was: an acceptor leaked a set of file handles per connection.
- `FileSessionStreamProvider` uses `FileShare.ReadWrite` consistently. The writers held
  `FileShare.Read` and the readers went through `File.ReadAllTextAsync` (also
  `FileShare.Read`), so the handoff to a reconnecting session threw `IOException` and
  **the client could not log on at all**.

**0.3.1-beta — shared recovery store** (`fix/shared-recovery-store-multi-client`)

Found by actually running the new demo multi-client scenario. With three concurrent
clients, every client after the first was disconnected by:

```
ArgumentException: An item with the same key has already been added. Key: 2
  at SortedList.Add / FixMsgMemoryStore.Put / FixLogRecovery.AddRecord
  at BaseApp.OnEncoded / FixSession.Send
```

`IFixLogRecovery` is a container singleton, so all sessions share one recovery store.
`BaseApp.OnEncoded` wrote to it on every send regardless of whether recovery was in
use. It now only writes when recovery is the store actually read back on restart
(`SessionStoreFactory == null`) - the same condition `OnRun` already used to decide
whether to *read* it; the write side simply never checked. `FixMsgMemoryStore.Put`
also assigns rather than `Add`s, so a duplicate sequence cannot fault a live session.

### Test coverage added

`PureFix.Test.ModularTypes/Transport/`:

- `SessionScopeIsolationTests` - distinct instances per scope, sequence numbers not
  bleeding, interleaved fragments from two connections parsing intact, plus
  `Sharing_one_parser_between_connections_corrupts_both_messages` kept as executable
  documentation of the original defect.
- `MultiClientAcceptorTests` - two clients end-to-end on one acceptor host.
- `WildcardSessionRegistryTests` - counterparties coexist; reconnect displaces only its
  own predecessor.
- `FileStorePerCounterpartyTests` - separate files and sequences, reconnect with and
  without a disposed predecessor, hostile CompIDs.
- `TlsOptionsValidationTests` - config validation.
- `SharedRecoveryStoreTests` - the 0.3.1 crash.

Every one of these was verified to **fail** against the pre-fix code.

### Demo

`purefix-standalone-demo` branch `feat/multi-client-scenario` (pushed, **awaiting
merge**). Verified green against 0.3.1-beta from NuGet.

The demo shipped a wildcard acceptor but only one initiator config, so it never ran two
clients at once - which is the only way these faults appear. It also had a hand-rolled
partial workaround (per-session parser, cloned description, per-session encoder) that
only applied in wildcard mode; that is now deleted in favour of the engine's scopes.

Added `multi-client-test.sh` (`run` / `reconnect` / `clean`) plus three client configs
and a wildcard acceptor config using a **file** store and `keepAliveMs`.

---

## Outstanding

### 1. Per-counterparty FIX logs — next patch

`BaseApp.cs:30` builds the log name from `config.Name()` (`Application.Name`), so every
counterparty on a wildcard acceptor interleaves into one file with no way to separate
them. jspurefix's demo already does per-peer (`jsfix.hedge-fund-a.txt`).

This is why `multi-client-test.sh` asserts on each *client's* log rather than the
acceptor's - the acceptor-side log cannot distinguish sessions today.

Note the same shape in the test harness: `TestLoggerFactory` gives every logger from one
host the same backing trace, so `RuntimeContainer.FixLog` cannot tell two acceptor
sessions apart either. Worth fixing together.

Related: `IFixLogRecovery` is still a container singleton bound to the *template*
config. It is now inert whenever a `SessionStoreFactory` is configured (which
`MakeConfigFromPaths` always sets), but an application constructing config by hand with
no store factory would still share one recovery store across counterparties. Consider
either scoping it or removing the legacy path.

### 2. Scope coverage gaps

`ISessionScope` covers config, parser, encoder and session message factory. It does
**not** cover:

- `IFixMessageFactory` (application-level, still a singleton) - fine today because the
  generated factories are stateless, but nothing enforces that.
- `IFixLogRecovery` - see above.

Decide whether the scope should own these, or whether it is enough to document that
anything else injected into a session must be stateless.

Also: `DefaultSessionScopeFactory.CloneDescription` only clones a concrete
`SessionDescription`; a custom `ISessionDescription` implementation is passed through
shared. Documented, but a foot-gun.

### 3. TLS has no handshake-level test coverage

`TlsOptionsValidationTests` covers config parsing and validation only. Nothing exercises
an actual handshake: certificate loading (pfx/pem), `Ca` custom root trust, mutual TLS
via `RequestClientCertificate`, or the fail-closed path. Needs a self-signed fixture and
a loopback acceptor/initiator pair.

### 4. Lazy object materialisation — design needed

See `docs/design/lazy-object-materialisation.md`. Wants careful thought before any code.

---

## Version state

- cspurefix: **0.3.1-beta** published to NuGet.
- purefix-standalone-demo: pinned to `0.3.1-beta`, branch `feat/multi-client-scenario`
  awaiting merge.
