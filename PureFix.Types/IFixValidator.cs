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
        public bool IsValid(in FixValidatorConfig config);
    }

    public static class FixValidator
    {
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
