using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.MessageStore.TextFileStore
{
    /// <summary>
    /// Configuration information for the FileMessageStore
    /// </summary>
    public sealed class TextFileMessageStoreConfig
    {
        /// <summary>
        /// The name of the file to store FIX messages in
        /// </summary>
        public required string Filename{get; init;}

        /// <summary>
        /// The byte to replace the FIX SOH character with then writing to file.
        /// If zero then no replacement will occur
        /// </summary>
        public byte SohReplacement{get; init;}

        /// <summary>
        /// Returns true if SOH replacement is enabled
        /// </summary>
        public bool ShouldReplaceSoh
        {
            get{return SohReplacement != 0;}
        }
    }
}
