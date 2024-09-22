using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Types
{
    public interface ILogFactory
    {
        public ILogger MakeLogger(string name);
        public ILogger MakePlainLogger(string name);
    }
}
