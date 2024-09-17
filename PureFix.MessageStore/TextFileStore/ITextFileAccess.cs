using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.MessageStore.TextFileStore
{
    /// <summary>
    /// Provides the file access methods required by a file store
    /// </summary>
    public interface ITextFileAccess
    {
        /// <summary>
        /// Creates or opens a file and places the position at the end of the file
        /// </summary>
        /// <param name="filename"></param>
        /// <returns>A writer to the file. It is the callers responsibility to dispose of the instance</returns>
        public TextWriter MakeWriterForAppend(string filename);

        
        /// <summary>
        /// Tries to opens a file for reading (if it exists), placing the position at the start of the file
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="reader">On success a reader for the file. It is the callers responsibility to dispose of the instance</param>
        /// <returns></returns>
        public bool TryMakeReader(string filename, [NotNullWhen(true)] out TextReader? reader);
    }
}
