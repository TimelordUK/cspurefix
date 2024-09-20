using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Types
{
    /// <summary>
    /// Defines the generic behaviour of a FIX factory for creating messages from a view
    /// </summary>
    public interface IFixMessageFactory
    {
        /// <summary>
        /// Attempts to parse a FIX view by determining the type of message, creating an instance and populating it
        /// </summary>
        /// <param name="view">The view to read from</param>
        /// <param name="message">On success the FIX message for the data in the view</param>
        /// <returns>true if the message could be created and parsed, otherwise false</returns>
        public bool TryParse(IMessageView view, [NotNullWhen(true)] out IFixMessage? message);

        /// <summary>
        /// Creates a FIX message and populates it
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="view"></param>
        /// <returns></returns>
        public T MakeAndParse<T>(IMessageView view) where T : IFixMessage, new()
        {
            var message = new T();
            message.Parse(view);

            return message;
        }
    }
}
