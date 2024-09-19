using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Types
{
    public interface ILogger
    {
        public void Info(string messageTemplate);
        public void Warn(string messageTemplate);
        public void Debug(string messageTemplate);
        public void Error(Exception ex);
    }
}
