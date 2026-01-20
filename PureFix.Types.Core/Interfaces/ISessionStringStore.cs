using System;
using System.Collections.Generic;

namespace PureFix.Types.Core;

/// <summary>
/// Interface for session-scoped string interning store.
/// Stores strings keyed by raw bytes to avoid allocating before checking existence.
/// Used for header fields like SenderCompID, TargetCompID that are constant per session.
/// </summary>
public interface ISessionStringStore
{
    /// <summary>
    /// Returns interned string for the given bytes. First call allocates,
    /// subsequent calls return the same instance.
    /// </summary>
    /// <param name="tag">The FIX tag number (used for per-tag tracking)</param>
    /// <param name="bytes">The raw byte value to intern</param>
    /// <returns>The interned string</returns>
    string GetOrAdd(int tag, ReadOnlySpan<byte> bytes);

    /// <summary>
    /// Clears all interned strings. Called on session stop.
    /// </summary>
    void Clear();

    /// <summary>
    /// Returns statistics for observability.
    /// </summary>
    SessionStringStoreStats GetStats();
}

/// <summary>
/// Statistics from a session string store.
/// </summary>
public readonly struct SessionStringStoreStats
{
    /// <summary>Number of distinct tags being tracked.</summary>
    public int TotalTags { get; init; }

    /// <summary>Total number of unique strings stored across all tags.</summary>
    public int TotalStrings { get; init; }

    /// <summary>Total cache hits (string returned from store).</summary>
    public long TotalHits { get; init; }

    /// <summary>Total cache misses (new string allocated).</summary>
    public long TotalMisses { get; init; }

    /// <summary>Per-tag statistics.</summary>
    public IReadOnlyDictionary<int, TagStats>? PerTagStats { get; init; }

    public override string ToString() =>
        $"Tags={TotalTags}, Strings={TotalStrings}, Hits={TotalHits}, Misses={TotalMisses}";
}

/// <summary>
/// Per-tag statistics from the string store.
/// </summary>
public readonly struct TagStats
{
    /// <summary>Number of unique values seen for this tag.</summary>
    public int UniqueValues { get; init; }

    /// <summary>Number of cache hits for this tag.</summary>
    public long Hits { get; init; }

    /// <summary>Number of cache misses for this tag.</summary>
    public long Misses { get; init; }

    public override string ToString() =>
        $"Unique={UniqueValues}, Hits={Hits}, Misses={Misses}";
}
