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
		[TagDetails(Tag = 93, Type = TagType.Length, Offset = 0)]
		public int? SignatureLength { get; set; }
		
		[TagDetails(Tag = 89, Type = TagType.RawData, Offset = 1)]
		public byte[]? Signature { get; set; }
		
		[TagDetails(Tag = 10, Type = TagType.String, Offset = 2)]
		public string? CheckSum { get; set; }
		
	}
}
