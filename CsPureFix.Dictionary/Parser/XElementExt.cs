using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
