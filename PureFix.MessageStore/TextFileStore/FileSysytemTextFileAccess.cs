using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PureFix.MessageStore.TextFileStore
{
    /// <summary>
    /// A default implementation of file access that writes to the underlying file system
    /// </summary>
    public sealed class FileSysytemTextFileAccess : ITextFileAccess
    {
        /// <inheritdoc/>
        public TextWriter MakeWriterForAppend(string filename)
        {
            var stream = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read, 0, true);
            return new StreamWriter(stream, Encoding.ASCII);
        }

        /// <inheritdoc/>
        public bool TryMakeReader(string filename, [NotNullWhen(true)] out TextReader? reader)
        {
            if(File.Exists(filename))
            {
                var stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read, 0, true);
                reader = new StreamReader(stream);
                return true;
            }

            reader = null;
            return false;
        }
    }
}
