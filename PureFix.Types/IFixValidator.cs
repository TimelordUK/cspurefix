using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace PureFix.Types
{
    public interface IFixValidator
    {
        /// <summary>
        /// Checks if the message is valid
        /// </summary>
        /// <returns></returns>
        public bool IsValid()
        {
            return true;
        }
    }
}
