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
        public static int GetMajor(this FixDefinitions definitions)
        {
            return FixVersionParser.GetMajor(definitions.Version);
        }

        public static int GetMinor(this FixDefinitions definitions)
        {
            return FixVersionParser.GetMinor(definitions.Version);
        }

        public static int GetServicePack(this FixDefinitions definitions)
        {
            return FixVersionParser.GetServicePack(definitions.Version);
        }
    }
}
