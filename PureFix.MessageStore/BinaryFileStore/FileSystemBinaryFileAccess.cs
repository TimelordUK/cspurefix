using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.MessageStore.BinaryFileStore
{
    /// <summary>
    /// Provides access to the underlying file system
    /// </summary>
    public sealed class FileSystemBinaryFileAccess : IBinaryFileAccess
    {
        /// <inheritdoc/>
        public Stream MakeWriterForAppend(string filename)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(filename);

            return new FileStream(filename, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read, 0, true);
        }
    }
}
