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
		[TagDetails(93, TagType.Length)]
		public int? SignatureLength { get; set; }
		
		[TagDetails(89, TagType.RawData)]
		public byte[]? Signature { get; set; }
		
		[TagDetails(10, TagType.String)]
		public string? CheckSum { get; set; }
		
	}
}
