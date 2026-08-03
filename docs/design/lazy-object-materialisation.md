# Lazy object materialisation — design exploration

**Status:** not started. Seed notes only, written 2026-08-03 so the next session starts
from the right mental model rather than re-deriving it.

## The idea

Today `IFixMessageFactory.ToFixMessage(view)` produces a fully expanded typed object
graph. We would like an opt-in mode where a property is resolved from the underlying
view only when it is actually read.

Most callers want the whole object and the current behaviour is the most useful thing.
But a caller that reads two fields off a 300-field ExecutionReport, or that only needs
the first instrument of a 200-entry MarketDataSnapshot, pays for everything.

## Two layers of laziness — do not conflate them

This is the first thing to be clear about, because "the view traverses all the way down"
is true of one layer and not the other.

**Layer 1 — view structure parsing. Already lazy.**
`AsciiParser.GetView` rents an `AsciiView` without parsing structure. The comment at
`AsciiParser.cs:119` is explicit: structure is parsed on demand when component or group
access is needed (`GetView`, `GetGroupInstance`); simple field-by-tag access uses a
linear scan and needs no structure. So `view.GetString(55)` is already cheap.

**Layer 2 — object materialisation. Eager. This is the target.**
The generated `IFixParser.Parse(IMessageView)` walks every field and recurses into every
nested component and group. From `PureFix.Types.FIX44/NewOrderSingle.cs:340`:

```csharp
void IFixParser.Parse(IMessageView? view)
{
    if (view is null) return;
    if (view.GetView("StandardHeader") is IMessageView v) { StandardHeader = new(); ((IFixParser)StandardHeader).Parse(v); }
    ClOrdID = view.GetString(11);
    SecondaryClOrdID = view.GetString(526);
    ...                                   // every field, then every nested component
}
```

Each `view.GetView("Parties")` forces layer-1 structure parsing for that subtree and then
allocates and fills the whole subtree eagerly. That is the waste.

So the feature is: **make layer 2 lazy**, per property and per nested component/group.

## The hard part: view lifetime

This is the constraint that shapes everything else, and the easiest way to ship a
memory-corruption bug.

`FixSession.OnRx` returns the view and its storage to their pools as soon as the
message callback completes:

```csharp
finally
{
    foreach (var msg in _messages)
    {
        var view = (AsciiView)msg;
        m_parser.Return(view.Storage);   // storage back to StoragePool
        view.Return();                   // view back to its ObjectPool
    }
    ArrayPool<byte>.Shared.Return(buffer);
}
```

A lazily-materialised object holds a reference to that view. If the application stashes
the object and reads a property after the callback returns, it reads a recycled buffer -
silently returning another message's bytes. Eager materialisation is what makes the
current pooling safe.

Options to consider, roughly in increasing cost:

1. **Scope-confined laziness.** Lazy only for the duration of the `OnApplicationMsg`
   callback; force full materialisation before the view is returned. Safe, but the win
   evaporates for anyone who queues messages for later processing - which is common.
2. **Detach on escape.** Object holds the view; an explicit `Detach()`/`Materialise()`
   forces the remainder. Puts the burden on the caller and will be got wrong.
3. **Clone the buffer.** `AsciiView.Clone()` already exists (`AsciiView.cs:669`) and
   clones the underlying `ElasticBuffer`. A lazy object could own a cloned view. Trades
   the object-graph allocation for one buffer copy - still a large win for wide messages
   with few reads, and safe. Probably the pragmatic default.
4. **Refcount the storage.** Cleanest lifetime story, biggest change to `StoragePool`
   and every return site.

A guard worth having regardless: make `AsciiView.Return()` poison the instance (version
counter / generation stamp) so a stale lazy read throws a clear
`ObjectDisposedException` instead of returning wrong data. Cheap, and turns the worst
failure mode into a loud one.

## Other edge cases to think through

- **Mutability.** Generated properties are settable and the encode path relies on it
  (`hdr.MergeFrom(message.StandardHeader)`, then `Reset()` on header and trailer in
  `AsciiEncoder.Encode`). A lazy backing store needs a per-property tri-state:
  unresolved / resolved-from-wire / set-by-user. `Encode` must serialise a partially
  materialised object correctly, and `Reset()` has to clear both the resolved values and
  the pending-resolution state.
- **Repeating groups.** The biggest win and the fiddliest. Lazy per instance, or lazy
  for the whole array? A caller doing `foreach` over `NoRelatedSym` wants all of them; a
  caller doing `[0]` wants one. Indexed access with materialise-on-touch is the obvious
  shape but interacts badly with `Count`, LINQ and enumeration.
- **Anything that observes the whole object forces full materialisation:**
  `ToString()`, JSON serialisation (`JsonHelper.ToJson`, used in the demo's
  `OnApplicationMsg`), equality, debugger display. These need a defined, documented
  trigger, and the debugger case matters - stepping through must not silently change
  behaviour.
- **Thread affinity.** `AsciiView` and the pools behind it are not thread-safe. A lazy
  object handed to another thread is unsafe in a way an eager one is not. Interacts
  directly with the per-connection scope work in 0.3.0.
- **Missing vs unresolved.** `GetString` returning null today means "absent". A lazy
  property must not confuse "not yet resolved" with "absent on the wire", especially for
  nullable value types.
- **Validation.** `ProtocolValidator` and the structural checks run against the view, so
  they should be unaffected - worth confirming rather than assuming.

## Shape of the opt-in

Flag on the message factory or the config, e.g.
`ToFixMessage(view, MaterialisationMode.Lazy)`, defaulting to eager so nothing changes
for existing callers. Generation would emit a second parse path rather than changing the
existing one - `ModularGenerator` already emits per-type `Parse`, so a parallel
`ParseLazy` plus backing fields is mechanical once the semantics are settled.

## Suggested first step

Do not start with code generation. Start by hand-writing the lazy variant of one wide
message (ExecutionReport) and one with a large repeating group
(MarketDataSnapshotFullRefresh), then benchmark against eager for three access patterns:
read-2-fields, read-all-fields, iterate-group. If read-all-fields is materially slower
than eager, the design needs the tri-state to be cheaper before going further.

`PureFix.Benchmarks` already has `SpanApiAccessBenchmarks` which clones views for reuse -
a reasonable place to add these.
