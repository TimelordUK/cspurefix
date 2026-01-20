using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PureFix.Types.Core;

namespace PureFix.Buffer;

/// <summary>
/// Per-session string interning store that keys on raw bytes.
/// First access for a given tag+value allocates, subsequent accesses return the same string.
/// Thread-safe via locking (FIX sessions are typically single-threaded, so contention is minimal).
/// </summary>
public class SessionStringStore : ISessionStringStore
{
    // Per-tag storage: tag → list of (byte key, string value)
    // Using List for cache-friendly iteration (typically 1-2 entries per tag for CompIDs)
    private readonly Dictionary<int, List<(byte[] Key, string Value)>> _store = new();

    // Stats tracking
    private readonly Dictionary<int, (long hits, long misses)> _stats = new();
    private readonly object _lock = new();

    /// <inheritdoc/>
    public string GetOrAdd(int tag, ReadOnlySpan<byte> bytes)
    {
        lock (_lock)
        {
            if (!_store.TryGetValue(tag, out var entries))
            {
                entries = new List<(byte[], string)>(capacity: 2);
                _store[tag] = entries;
                _stats[tag] = (0, 0);
            }

            // Check existing entries (typically 0-2 iterations for header tags)
            foreach (var (key, value) in entries)
            {
                if (bytes.SequenceEqual(key))
                {
                    // HIT - return existing string
                    var s = _stats[tag];
                    _stats[tag] = (s.hits + 1, s.misses);
                    return value;
                }
            }

            // MISS - allocate once, store for future
            var newKey = bytes.ToArray();
            var newValue = Encoding.ASCII.GetString(bytes);
            entries.Add((newKey, newValue));

            var stats = _stats[tag];
            _stats[tag] = (stats.hits, stats.misses + 1);

            return newValue;
        }
    }

    /// <inheritdoc/>
    public void Clear()
    {
        lock (_lock)
        {
            _store.Clear();
            _stats.Clear();
        }
    }

    /// <inheritdoc/>
    public SessionStringStoreStats GetStats()
    {
        lock (_lock)
        {
            var perTag = _stats.ToDictionary(
                kvp => kvp.Key,
                kvp => new TagStats
                {
                    UniqueValues = _store.TryGetValue(kvp.Key, out var e) ? e.Count : 0,
                    Hits = kvp.Value.hits,
                    Misses = kvp.Value.misses
                });

            return new SessionStringStoreStats
            {
                TotalTags = _store.Count,
                TotalStrings = _store.Values.Sum(v => v.Count),
                TotalHits = _stats.Values.Sum(v => v.hits),
                TotalMisses = _stats.Values.Sum(v => v.misses),
                PerTagStats = perTag
            };
        }
    }
}
