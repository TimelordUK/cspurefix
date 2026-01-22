using PureFix.Buffer.Ascii;
using PureFix.Dictionary.Definition;
using PureFix.Types.Core;

namespace PureFix.Buffer.Segment;

/// <summary>
/// Interface for segment parsers that analyze FIX message structure.
/// Different implementations can use different strategies:
/// - Stack-based (default): Fast, assumes tags are in definition order
/// - Tag-by-tag: Handles out-of-order tags by direct placement
/// - Hybrid: Detects contiguity and optimizes representation
/// </summary>
public interface ISegmentParser
{
    /// <summary>
    /// The FIX definitions used for message structure lookup.
    /// </summary>
    IFixDefinitions Definitions { get; }

    /// <summary>
    /// Parses the structure of a FIX message, identifying components, groups, and fields.
    /// </summary>
    /// <param name="msgType">The FIX message type (e.g., "8" for ExecutionReport)</param>
    /// <param name="tags">The parsed tags from the message</param>
    /// <param name="last">Index of the last tag to process</param>
    /// <returns>The parsed structure, or null if message type is unknown</returns>
    Structure? Parse(string msgType, Tags tags, int last);
}

/// <summary>
/// Segment parser strategy selection.
/// </summary>
public enum SegmentParserStrategy
{
    /// <summary>
    /// Stack-based recursive descent. Fast, requires tags in definition order.
    /// This is the default and most efficient for well-formed messages.
    /// </summary>
    StackBased,

    /// <summary>
    /// Tag-by-tag placement using dictionary lookups.
    /// Handles out-of-order tags (e.g., Bloomberg-style messages).
    /// More expensive but robust for any tag ordering.
    /// </summary>
    TagByTag,

    /// <summary>
    /// Hybrid approach: uses tag-by-tag placement but optimizes
    /// contiguous segments to range representation.
    /// </summary>
    Hybrid
}
