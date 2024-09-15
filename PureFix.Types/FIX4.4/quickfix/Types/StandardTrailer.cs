using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class StandardTrailer : IStandardTrailer, IFixEncoder
    {
		[TagDetails(Tag = 93, Type = TagType.Length, Offset = 0, Required = false, LinksToTag = 89)]
		public int? SignatureLength { get; set; }
		
		[TagDetails(Tag = 89, Type = TagType.RawData, Offset = 1, Required = false, LinksToTag = 93)]
		public byte[]? Signature { get; set; }
		
		[TagDetails(Tag = 10, Type = TagType.String, Offset = 2, Required = true)]
		public string? CheckSum { get; set; }
		
	}
}
