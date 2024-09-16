using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Types
{
    /// <summary>
    /// Provides an easy way to get data out of FIX messages using the tag name
    /// </summary>
    public static class QuickLookup
    {
        /// <summary>
        /// Looks up a tag against a FIX structure
        /// </summary>
        /// <param name="lookup"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static Data Lookup(IFixLookup lookup, string name)
        {
            if(lookup.TryGetByTag(name, out var value))
            {
                return new(value);
            }

            throw new ArgumentException($"no such tag: {name}");
        }

        public readonly ref struct Data
        {
            private readonly object? m_Result;

            public Data(object? result)
            {
                m_Result = result;
            }

            /// <summary>
            /// Returns the lookedup data as the required type
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <returns></returns>
            public T ValueAs<T>()
            {
                return (T)m_Result!;
            }

            /// <summary>
            /// Looks up a tag against a FIX structure
            /// </summary>
            /// <param name="name"></param>
            /// <returns></returns>
            /// <exception cref="InvalidOperationException"></exception>
            public Data Lookup(string name)
            {
                if(m_Result is IFixLookup lookup)
                {
                    return QuickLookup.Lookup(lookup, name);
                }
                
                throw new InvalidOperationException("data is not an IFixLookup");
            }
        }
    }
}
