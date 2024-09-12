using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class StandardTrailer
	{
		[TagDetails(93)]
		public int? SignatureLength { get; set; } // LENGTH
		
		[TagDetails(89)]
		public byte[]? Signature { get; set; } // DATA
		
		[TagDetails(10)]
		public string? CheckSum { get; set; } // STRING
		
	}
}
