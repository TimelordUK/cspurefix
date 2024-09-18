using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Types
{
    public interface IStandardTrailer : IFixParser, IFixEncoder
    {
        public int? SignatureLength{get;}
		
		public byte[]? Signature{get;}
		
		public string? CheckSum{get;}
    }
}
