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
		public int? SignatureLength { get; set; }
		public byte[]? Signature { get; set; }
		public string? CheckSum { get; set; }
	}
}
