using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Types
{
    /// <summary>
    /// Populates the FIX type from a view
    /// </summary>
    public interface IFixParser
    {
        /// <summary>
        /// Parses the FIX type from a view
        /// </summary>
        /// <param name="view"></param>
        void Parse(IMessageView ?view);
    }
}
