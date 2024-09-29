using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Types
{
    internal interface IFixMerge<in T> where T : class, IFixParser
    {
        public void Merge(T rhs) { }
    }
}
