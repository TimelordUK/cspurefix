using PureFix.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.ConsoleApp
{
    internal static class FactoryHelper
    {
        public static IFixMessageFactory GetFactory(string path)
        {
            if (path.EndsWith("FIX50SP2.xml"))
            {
                return new Types.FIX50SP2.FixMessageFactory();
            }

            if (path.EndsWith("FIX44.xml"))
            {
                return new Types.FIX44.FixMessageFactory();
            }

            // FIX43 and FIX42 not yet migrated to new type system
            // Default to FIX44
            return new Types.FIX44.FixMessageFactory();
        }
    }
}
