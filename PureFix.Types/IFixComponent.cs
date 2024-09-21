using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Types
{
    /// <summary>
    /// Implemented by FIX components
    /// </summary>
    public interface IFixComponent : IFixValidator, IFixEncoder, IFixParser, IFixLookup, IFixReset
    {
    }
}
