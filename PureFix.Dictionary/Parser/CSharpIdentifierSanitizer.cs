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
    /// Core sanitization logic
    /// </summary>
    private static string SanitizeCore(string input)
    {
        var sb = new StringBuilder(input.Length + 10);
        var needsCapitalize = true;    // Start with capital for PascalCase
        var isFirstOutputChar = true;

        foreach (var c in input)
        {
            string replacement;
            var isSeparator = false;
            var isSpecialReplacement = false;  // Track if this is a word replacement like "Minus"

            if (CharacterReplacements.TryGetValue(c, out var mapped))
            {
                replacement = mapped;
                // Empty replacements act as separators for PascalCase
                isSeparator = string.IsNullOrEmpty(mapped);
                // Non-empty replacements are special words that should be followed by capital
                isSpecialReplacement = !isSeparator;
            }
            else if (char.IsLetterOrDigit(c))
            {
                replacement = c.ToString();
            }
            else if (c == '_')
            {
                replacement = "";  // Treat underscore as separator, don't output it
                isSeparator = true;
            }
            else if (char.IsWhiteSpace(c))
            {
                replacement = "";
                isSeparator = true;
            }
            else
            {
                // Unknown character - treat as separator
                replacement = "";
                isSeparator = true;
            }

            // Track separators for PascalCase conversion
            if (isSeparator)
            {
                needsCapitalize = true;
                continue;
            }

            // Skip empty replacements
            if (string.IsNullOrEmpty(replacement))
            {
                continue;
            }

            // Handle first output character - must be letter or underscore
            // If it's a digit, we need to prefix with underscore
            if (isFirstOutputChar)
            {
                var firstChar = replacement[0];
                if (char.IsDigit(firstChar))
                {
                    sb.Append('_');
                }
                isFirstOutputChar = false;
            }

            // Apply PascalCase: capitalize first letter of each "word"
            if (needsCapitalize)
            {
                sb.Append(char.ToUpperInvariant(replacement[0]));
                if (replacement.Length > 1)
                {
                    sb.Append(replacement[1..]);
                }
                needsCapitalize = false;
            }
            else
            {
                sb.Append(replacement);
            }

            // Special replacements (like "Minus", "Plus") act as word boundaries
            // The next character should be capitalized
            if (isSpecialReplacement)
            {
                needsCapitalize = true;
            }
        }

        var result = sb.ToString();

        // Remove trailing underscores
        result = result.TrimEnd('_');

        // If empty after sanitization, use a default
        if (string.IsNullOrEmpty(result))
        {
            return "Unknown";
        }

        // Handle C# keywords by prefixing with @
        // Check case-insensitive for keywords (since we PascalCase everything)
        var lowerResult = result.ToLowerInvariant();
        if (CSharpKeywords.Contains(lowerResult) || ContextualKeywords.Contains(lowerResult))
        {
            return $"@{lowerResult}";
        }

        return result;
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
