using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("K", FixVersion.FIX44)]
	public sealed class ListCancelRequest : FixMsg
	{
		[Component(Offset = 0, Required = true)]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 66, Type = TagType.String, Offset = 1, Required = true)]
		public string? ListID { get; set; }
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 2, Required = true)]
		public DateTime? TransactTime { get; set; }
		
		[TagDetails(Tag = 229, Type = TagType.LocalDate, Offset = 3, Required = false)]
		public DateTime? TradeOriginationDate { get; set; }
		
		[TagDetails(Tag = 75, Type = TagType.LocalDate, Offset = 4, Required = false)]
		public DateTime? TradeDate { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 5, Required = false)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 6, Required = false)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 7, Required = false)]
		public byte[]? EncodedText { get; set; }
		
		[Component(Offset = 8, Required = true)]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
