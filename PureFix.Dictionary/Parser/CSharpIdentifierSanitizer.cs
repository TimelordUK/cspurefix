using System.Text;
using System.Text.RegularExpressions;

namespace PureFix.Dictionary.Parser;

/// <summary>
/// Sanitizes strings to be valid C# identifiers.
/// Handles edge cases like names starting with numbers, special characters,
/// C# keywords, and detects potential collisions.
/// </summary>
public partial class CSharpIdentifierSanitizer
{
    private readonly Dictionary<string, string> _sanitizedToOriginal = new();
    private readonly Dictionary<string, int> _collisionCounts = new();

    /// <summary>
    /// C# reserved keywords that cannot be used as identifiers
    /// </summary>
    private static readonly HashSet<string> CSharpKeywords = new(StringComparer.Ordinal)
    {
        "abstract", "as", "base", "bool", "break", "byte", "case", "catch", "char", "checked",
        "class", "const", "continue", "decimal", "default", "delegate", "do", "double", "else",
        "enum", "event", "explicit", "extern", "false", "finally", "fixed", "float", "for",
        "foreach", "goto", "if", "implicit", "in", "int", "interface", "internal", "is", "lock",
        "long", "namespace", "new", "null", "object", "operator", "out", "override", "params",
        "private", "protected", "public", "readonly", "ref", "return", "sbyte", "sealed", "short",
        "sizeof", "stackalloc", "static", "string", "struct", "switch", "this", "throw", "true",
        "try", "typeof", "uint", "ulong", "unchecked", "unsafe", "ushort", "using", "virtual",
        "void", "volatile", "while"
    };

    /// <summary>
    /// Contextual keywords that should also be avoided
    /// </summary>
    private static readonly HashSet<string> ContextualKeywords = new(StringComparer.Ordinal)
    {
        "add", "alias", "and", "ascending", "args", "async", "await", "by", "descending",
        "dynamic", "equals", "file", "from", "get", "global", "group", "init", "into", "join",
        "let", "managed", "nameof", "nint", "not", "notnull", "nuint", "on", "or", "orderby",
        "partial", "record", "remove", "required", "scoped", "select", "set", "unmanaged",
        "value", "var", "when", "where", "with", "yield"
    };

    /// <summary>
    /// Character replacements for common symbols
    /// </summary>
    private static readonly Dictionary<char, string> CharacterReplacements = new()
    {
        { '+', "Plus" },
        { '-', "Minus" },
        { '*', "Star" },
        { '/', "Slash" },
        { '\\', "Backslash" },
        { '&', "And" },
        { '|', "Or" },
        { '#', "Hash" },
        { '@', "At" },
        { '$', "Dollar" },
        { '%', "Percent" },
        { '^', "Caret" },
        { '~', "Tilde" },
        { '=', "Equals" },
        { '<', "Lt" },
        { '>', "Gt" },
        { '!', "Not" },
        { '?', "Question" },
        { ':', "Colon" },
        { ';', "Semi" },
        { ',', "Comma" },
        { '.', "Dot" },
        { '\'', "Apos" },
        { '"', "Quote" },
        { '`', "Tick" },
        { '(', "" },
        { ')', "" },
        { '[', "" },
        { ']', "" },
        { '{', "" },
        { '}', "" },
        { ' ', "" },
        { '\t', "" },
        { '\n', "" },
        { '\r', "" },
    };

    /// <summary>
    /// Result of sanitization including collision info
    /// </summary>
    public record SanitizeResult(
        string Original,
        string Sanitized,
        bool WasModified,
        bool HadCollision,
        string? CollisionWith);

    /// <summary>
    /// Sanitize a string to be a valid C# identifier.
    /// Tracks sanitized values to detect and resolve collisions.
    /// </summary>
    public SanitizeResult Sanitize(string input, string context = "")
    {
        if (string.IsNullOrEmpty(input))
        {
            return new SanitizeResult(input, "Unknown", true, false, null);
        }

        var sanitized = SanitizeCore(input);
        var wasModified = sanitized != input;

        // Check for collision
        var key = $"{context}:{sanitized}";
        string? collidedWith = null;
        var hadCollision = false;

        if (_sanitizedToOriginal.TryGetValue(key, out var existing))
        {
            if (existing != input)
            {
                // Collision detected - need to disambiguate
                hadCollision = true;
                collidedWith = existing;

                // Increment collision counter and append suffix
                if (!_collisionCounts.TryGetValue(key, out var count))
                {
                    count = 1;
                }
                _collisionCounts[key] = count + 1;
                sanitized = $"{sanitized}_{count + 1}";
                wasModified = true;
            }
        }
        else
        {
            _sanitizedToOriginal[key] = input;
        }

        return new SanitizeResult(input, sanitized, wasModified, hadCollision, collidedWith);
    }

    /// <summary>
    /// Sanitize without collision tracking (stateless)
    /// </summary>
    public static string SanitizeStatic(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return "Unknown";
        }
        return SanitizeCore(input);
    }

    /// <summary>
    /// Core sanitization logic.
    /// Converts input to PascalCase, handling ALL_CAPS words, special characters, etc.
    /// For example: "BUY_MINUS" -> "BuyMinus", "email@domain" -> "EmailAtDomain"
    ///
    /// The logic mirrors the old UnderscoreToCamelCase behavior:
    /// - Special chars are replaced (+ -> Plus, etc.) and act as word boundaries
    /// - Underscores and spaces act as word boundaries (removed from output)
    /// - ALL_CAPS segments become Title Case (BUY -> Buy)
    /// - Mixed case segments preserve casing (just ensure first letter is upper)
    /// </summary>
    private static string SanitizeCore(string input)
    {
        // First pass: replace special characters and collect segments
        var segments = new List<string>();
        var currentSegment = new StringBuilder();

        foreach (var c in input)
        {
            if (CharacterReplacements.TryGetValue(c, out var mapped))
            {
                // Flush current segment
                if (currentSegment.Length > 0)
                {
                    segments.Add(currentSegment.ToString());
                    currentSegment.Clear();
                }
                // Add replacement as its own segment if non-empty
                if (!string.IsNullOrEmpty(mapped))
                {
                    segments.Add(mapped);
                }
            }
            else if (c == '_' || char.IsWhiteSpace(c))
            {
                // Separator - flush current segment
                if (currentSegment.Length > 0)
                {
                    segments.Add(currentSegment.ToString());
                    currentSegment.Clear();
                }
            }
            else if (char.IsLetterOrDigit(c))
            {
                currentSegment.Append(c);
            }
            // Other chars are ignored (treated as separators)
        }

        // Flush final segment
        if (currentSegment.Length > 0)
        {
            segments.Add(currentSegment.ToString());
        }

        if (segments.Count == 0)
        {
            return "Unknown";
        }

        // Second pass: build result with proper casing
        var sb = new StringBuilder();
        var isFirstSegment = true;

        foreach (var segment in segments)
        {
            if (string.IsNullOrEmpty(segment)) continue;

            var processed = ProcessSegment(segment);

            // Handle first character of output - prefix with underscore if digit
            if (isFirstSegment && processed.Length > 0 && char.IsDigit(processed[0]))
            {
                sb.Append('_');
            }

            sb.Append(processed);
            isFirstSegment = false;
        }

        var result = sb.ToString();

        // If empty after sanitization, use a default
        if (string.IsNullOrEmpty(result))
        {
            return "Unknown";
        }

        // Handle C# keywords by prefixing with @
        var lowerResult = result.ToLowerInvariant();
        if (CSharpKeywords.Contains(lowerResult) || ContextualKeywords.Contains(lowerResult))
        {
            return $"@{lowerResult}";
        }

        return result;
    }

    /// <summary>
    /// Process a single segment (word) for PascalCase.
    /// - If all uppercase letters, convert to Title Case (BUY -> Buy)
    /// - Otherwise, just capitalize first letter (preserves mixed case like "iPhone")
    /// </summary>
    private static string ProcessSegment(string segment)
    {
        if (string.IsNullOrEmpty(segment)) return segment;

        // Check if segment is all uppercase (ignoring digits)
        var letters = segment.Where(char.IsLetter).ToArray();
        var isAllUppercase = letters.Length > 0 && letters.All(char.IsUpper);

        if (isAllUppercase)
        {
            // Convert to Title Case: first letter upper, rest lower
            return char.ToUpperInvariant(segment[0]) + segment[1..].ToLowerInvariant();
        }
        else
        {
            // Just ensure first letter is uppercase, preserve rest
            return char.ToUpperInvariant(segment[0]) + segment[1..];
        }
    }

    /// <summary>
    /// Reset collision tracking state
    /// </summary>
    public void Reset()
    {
        _sanitizedToOriginal.Clear();
        _collisionCounts.Clear();
    }

    /// <summary>
    /// Check if a string is a valid C# identifier without sanitization
    /// </summary>
    public static bool IsValidIdentifier(string input)
    {
        if (string.IsNullOrEmpty(input)) return false;
        if (CSharpKeywords.Contains(input)) return false;
        if (!char.IsLetter(input[0]) && input[0] != '_') return false;

        return input.All(c => char.IsLetterOrDigit(c) || c == '_');
    }

    /// <summary>
    /// Get all detected collisions for reporting
    /// </summary>
    public IReadOnlyDictionary<string, int> GetCollisions() => _collisionCounts;
}
