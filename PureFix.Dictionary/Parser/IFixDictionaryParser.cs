using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Dictionary.Parser
{
    public interface IFixDictionaryParser
    {
        void Parse(string basePath);
    }
}
