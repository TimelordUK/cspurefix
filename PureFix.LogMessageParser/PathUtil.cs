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
            string path = Directory.GetCurrentDirectory();
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string fpath = Uri.UnescapeDataString(uri.Path);
            fpath = Path.GetDirectoryName(fpath);
            return Path.Join(fpath, file);
        }
    }
}
