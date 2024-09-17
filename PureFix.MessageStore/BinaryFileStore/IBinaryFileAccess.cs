using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.MessageStore.BinaryFileStore
{
    public interface IBinaryFileAccess
    {
        /// <summary>
        /// Opens, or creates and opens the specified file for reading and writing.
        /// The caller is responsible for closing the stream
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public Stream MakeWriterForAppend(string filename);
    }
}
