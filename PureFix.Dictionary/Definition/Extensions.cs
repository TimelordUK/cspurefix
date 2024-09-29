using PureFix.Dictionary.Parser;
using PureFix.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Dictionary.Definition
{
    public static class FixDefinitionExt
    {
        public static int GetMajor(this IFixDefinitions definitions)
        {
            return FixVersionParser.GetMajor(definitions.Version);
        }

        public static int GetMinor(this IFixDefinitions definitions)
        {
            return FixVersionParser.GetMinor(definitions.Version);
        }

        public static int GetServicePack(this IFixDefinitions definitions)
        {
            return FixVersionParser.GetServicePack(definitions.Version);
        }
    }
}
