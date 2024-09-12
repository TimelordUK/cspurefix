using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Types
{
    /// <summary>
    /// Base class for all FIX attributes
    /// </summary>
    public abstract class FixAttribute : Attribute
    {
        protected FixAttribute()
        {
        }
    }
}
