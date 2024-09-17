using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace PureFix.Types
{
    /// <summary>
    /// Checks to see if the structure and content of a FIX message is correct
    /// </summary>
    public interface IFixValidator
    {
        /// <summary>
        /// Checks if the message is valid
        /// </summary>
        /// <returns></returns>
        public bool IsValid(in FixValidatorConfig config);
    }

    public static class FixValidator
    {
        /// <summary>
        /// Checks if all items in a group are valid
        /// </summary>
        /// <param name="group"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public static bool IsValid(IFixValidator[] group, in FixValidatorConfig config)
        {
            for(int i = 0, length = group.Length; i < length; i++)
            {
                if(!group[i].IsValid(in config)) return false;
            }

            return true;
        }
    }
}
