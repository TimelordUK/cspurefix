namespace PureFix.LogMessageParser
{
    /// <summary>
    /// Metadata for a FIX dictionary registry entry.
    /// Supports matching by SenderCompID (tag 49) and TargetCompID (tag 56).
    /// </summary>
    public class DictMeta
    {
        /// <summary>Unique identifier for this entry (e.g., "FIX50SP2")</summary>
        public string? Name { get; set; }

        /// <summary>Human-readable display name for UI</summary>
        public string? DisplayName { get; set; }

        /// <summary>Path to QuickFix XML dictionary file</summary>
        public string? Dict { get; set; }

        /// <summary>Root namespace of generated types (e.g., "PureFix.Types.FIX50SP2")</summary>
        public string? Type { get; set; }

        /// <summary>Path to the types assembly DLL (relative or absolute). If not specified, derived from Type.</summary>
        public string? Assembly { get; set; }

        /// <summary>Legacy single sender field (deprecated, use SenderCompIds)</summary>
        public string? Sender { get; set; }

        /// <summary>SenderCompIDs (tag 49) that should use this dictionary. Use "*" for wildcard.</summary>
        public List<string>? SenderCompIds { get; set; }

        /// <summary>TargetCompIDs (tag 56) that should use this dictionary. Use "*" for wildcard.</summary>
        public List<string>? TargetCompIds { get; set; }

        /// <summary>FIX version string (e.g., "FIX.5.0SP2")</summary>
        public string? Version { get; set; }

        /// <summary>Whether this is the default/fallback dictionary</summary>
        public bool IsDefault { get; set; }

        /// <summary>Whether this entry is enabled</summary>
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// Check if this entry matches the given SenderCompID and TargetCompID.
        /// Supports wildcard "*" matching.
        /// </summary>
        public bool Matches(string? senderCompId, string? targetCompId)
        {
            if (!Enabled) return false;

            var senderMatch = MatchesCompId(SenderCompIds, senderCompId);
            var targetMatch = MatchesCompId(TargetCompIds, targetCompId);

            return senderMatch && targetMatch;
        }

        /// <summary>
        /// Check if this entry matches exactly (no wildcards) for the given CompIDs.
        /// Used to prioritize exact matches over wildcard matches.
        /// </summary>
        public bool IsExactMatch(string? senderCompId, string? targetCompId)
        {
            if (!Enabled) return false;

            var senderExact = IsExactCompIdMatch(SenderCompIds, senderCompId);
            var targetExact = IsExactCompIdMatch(TargetCompIds, targetCompId);

            return senderExact && targetExact;
        }

        private static bool MatchesCompId(List<string>? patterns, string? value)
        {
            // No patterns means match anything (implicit wildcard)
            if (patterns == null || patterns.Count == 0) return true;

            // Null value only matches wildcard
            if (string.IsNullOrEmpty(value))
                return patterns.Contains("*");

            // Check for exact match or wildcard
            return patterns.Contains(value) || patterns.Contains("*");
        }

        private static bool IsExactCompIdMatch(List<string>? patterns, string? value)
        {
            // No patterns or empty value can't be exact
            if (patterns == null || patterns.Count == 0) return false;
            if (string.IsNullOrEmpty(value)) return false;

            // Must contain the exact value (not just wildcard)
            return patterns.Contains(value);
        }
    }

    public class DictMetaSet
    {
        public List<DictMeta> Metas { get; set; } = [];
    }
}
