using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Types
{
    public readonly ref struct FixValidatorConfig
    {
        /// <summary>
        /// True to check the standard header in a message when validating, false to skip it
        /// </summary>
        public bool CheckStandardHeader{get; init;}

        /// <summary>
        /// True to check the standard trailer in a message when validating, false to skip it
        /// </summary>
        public bool CheckStandardTrailer{get; init;}
    }
}
