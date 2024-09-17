using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.MessageStore.BinaryFileStore
{
    public sealed class BinaryFileMessageStoreConfig
    {
        /// <summary>
        /// The name of the file to store FIX messages in
        /// </summary>
        public required string Filename{get; init;}
    }
}
