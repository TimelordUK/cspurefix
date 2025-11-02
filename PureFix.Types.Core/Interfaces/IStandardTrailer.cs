using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Types
{
    public interface IStandardTrailer : IFixParser, IFixEncoder, IFixReset
    {
		public string? CheckSum{get;}
    }
}
