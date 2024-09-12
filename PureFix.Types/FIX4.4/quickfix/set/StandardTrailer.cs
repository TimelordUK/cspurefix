using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX4._4.quickfix.set;
namespace PureFix.Types.FIX4._4.quickfix.set
{
	public class StandardTrailer
	{
		public int? SignatureLength { get; set; } // 93 LENGTH
		public byte[]? Signature { get; set; } // 89 DATA
		public string? CheckSum { get; set; } // 10 STRING
	}
}
