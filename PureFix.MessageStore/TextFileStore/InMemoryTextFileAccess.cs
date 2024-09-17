using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.MessageStore.TextFileStore
{
    /// <summary>
    /// Stores data in memory (as a string) rather than as a file
    /// </summary>
    public sealed class InMemoryTextFileAccess : ITextFileAccess
    {
        private readonly StringBuilder m_Builder = new();

        /// <summary>
        /// Initializes the instance
        /// </summary>
        /// <param name="initialLines">The initial lines of the file</param>
        public InMemoryTextFileAccess(params string[] initialLines)
        {
            foreach(var line in initialLines)
            {
                m_Builder.AppendLine(line);
            }
        }

        /// <inheritdoc/>
        public TextWriter MakeWriterForAppend(string filename)
        {
            return new StringWriter(m_Builder);
        }

        /// <inheritdoc/>
        public bool TryMakeReader(string filename, [NotNullWhen(true)] out TextReader? reader)
        {
            reader = new StringReader(m_Builder.ToString());
            return true;
        }

        /// <summary>
        /// Returns the "content" of the file
        /// </summary>
        /// <returns></returns>
        public string GetContents()
        {
            return m_Builder.ToString();
        }
    }
}
