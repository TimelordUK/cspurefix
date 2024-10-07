using PureFix.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.ConsoleApp
{
    internal static class Extensions
    {
        public static IFixMessageFactory GetFactory(this CommandOptions options)
        {
            var path = options.DictPath;
            if (path.EndsWith("FIX50SP2.xml"))
            {
                return new Types.FIX50SP2.QuickFix.Types.FixMessageFactory();
            }

            if (path.EndsWith("FIX44.xml"))
            {
                return new Types.FIX44.QuickFix.Types.FixMessageFactory();
            }

            if (path.EndsWith("FIX43.xml"))
            {
                return new Types.FIX43.QuickFix.Types.FixMessageFactory();
            }

            if (path.EndsWith("FIX42.xml"))
            {
                return new Types.FIX42.QuickFix.Types.FixMessageFactory();
            }

            return new Types.FIX44.QuickFix.Types.FixMessageFactory();
        }
    }
}
