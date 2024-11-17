using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.LogMessageParser
{
    public static class PathUtil
    {
        public static string GetPath(string file)
        {
            var codeBase = Assembly.GetExecutingAssembly().Location;
            var assemblyPath = Path.GetDirectoryName(codeBase);
            return Path.Join(assemblyPath, file);
        }
    }
}
