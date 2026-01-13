using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Transport.Session
{
    public static class Extensions
    {
        public static bool IsInitiator(this IFixConfig config)
        {
            return (config?.Description?.Application?.Type == "initiator");
        }

        public static string Name(this IFixConfig config)
        {
            return config?.Description?.Application?.Name ?? config?.Description?.Application?.Type ?? string.Empty;
        }

        public static bool ResetSeqNumFlag(this IFixConfig config)
        {
            return config?.Description?.ResetSeqNumFlag ?? false;
        }

        /// <summary>
        /// Returns true if the session should always respond to ResendRequest with GapFill
        /// instead of replaying stored messages. Critical safety feature for clients.
        /// </summary>
        public static bool ResendGapFillOnly(this IFixConfig config)
        {
            return config?.Description?.ResendGapFillOnly ?? false;
        }
    }
}
