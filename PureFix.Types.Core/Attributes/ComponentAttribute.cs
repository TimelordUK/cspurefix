using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Types
{
    /// <summary>
    /// Flags a property as being a FIX component
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true)]
    public sealed class ComponentAttribute : FixAttribute, ITagOffset
    {
        /// <summary>
        /// The zero-based position of the field within the message, component or group
        /// </summary>
        public required int Offset{get; init;}

        public required bool Required{get; init;}
    }
}
