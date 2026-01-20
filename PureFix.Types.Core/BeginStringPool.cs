using System;
using System.Text;

namespace PureFix.Types.Core;

/// <summary>
/// Static pool for FIX BeginString (tag 8) values.
/// Returns the same string instance for known FIX versions without allocation.
/// </summary>
public static class BeginStringPool
{
    // All known FIX versions as const strings
    public const string Fix40 = "FIX.4.0";
    public const string Fix41 = "FIX.4.1";
    public const string Fix42 = "FIX.4.2";
    public const string Fix43 = "FIX.4.3";
    public const string Fix44 = "FIX.4.4";
    public const string Fixt11 = "FIXT.1.1";
    public const string Fix50 = "FIX.5.0";
    public const string Fix50SP1 = "FIX.5.0SP1";
    public const string Fix50SP2 = "FIX.5.0SP2";

    /// <summary>
    /// Returns an interned string for the given BeginString bytes.
    /// Known FIX versions return const strings (zero allocation).
    /// Unknown versions fall back to allocation.
    /// </summary>
    public static string Intern(ReadOnlySpan<byte> bytes)
    {
        // Fast path: check length first to narrow down possibilities
        switch (bytes.Length)
        {
            case 7:
                // "FIX.4.X" or "FIX.5.0" pattern
                if (bytes[0] == 'F' && bytes[1] == 'I' && bytes[2] == 'X' && bytes[3] == '.')
                {
                    if (bytes[4] == '4' && bytes[5] == '.')
                    {
                        // FIX 4.x
                        return bytes[6] switch
                        {
                            (byte)'0' => Fix40,
                            (byte)'1' => Fix41,
                            (byte)'2' => Fix42,
                            (byte)'3' => Fix43,
                            (byte)'4' => Fix44,
                            _ => Encoding.ASCII.GetString(bytes)
                        };
                    }
                    else if (bytes[4] == '5' && bytes[5] == '.' && bytes[6] == '0')
                    {
                        // FIX.5.0
                        return Fix50;
                    }
                }
                break;

            case 8:
                // "FIXT.1.1" pattern
                if (bytes.SequenceEqual("FIXT.1.1"u8))
                {
                    return Fixt11;
                }
                break;

            case 10:
                // "FIX.5.0SP1" or "FIX.5.0SP2" pattern
                if (bytes[0] == 'F' && bytes[1] == 'I' && bytes[2] == 'X' &&
                    bytes[3] == '.' && bytes[4] == '5' && bytes[5] == '.' &&
                    bytes[6] == '0' && bytes[7] == 'S' && bytes[8] == 'P')
                {
                    return bytes[9] switch
                    {
                        (byte)'1' => Fix50SP1,
                        (byte)'2' => Fix50SP2,
                        _ => Encoding.ASCII.GetString(bytes)
                    };
                }
                break;
        }

        // Unknown version - allocate (should be rare in production)
        return Encoding.ASCII.GetString(bytes);
    }
}
