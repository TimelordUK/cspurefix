using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PureFix.Dictionary.Parser
{
    public static class XElementExt
    {
        public static Dictionary<string, string> AsAttributeDict(this XElement element)
        {
            return element.Attributes().ToDictionary(a => a.Name.LocalName, a => a.Value);
        }

        public static int AsInt(this XElement element, string name, int def = -1)
        {
            var res = element?.Element(name)?.Value;
            if (element == null || res == null) return def;
            int tag = int.Parse(res);
            return tag;
        }

        public static string AsString(this XElement element, string name, string def = "")
        {
            var res = element?.Element(name)?.Value;
            return res ?? def;
        }
    }
}
