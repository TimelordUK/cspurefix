using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class UndInstrmtStrkPxGrpNoUnderlyings
	{
		[Component(Offset = 0, Required = false)]
		public UnderlyingInstrument? UnderlyingInstrument { get; set; }
		
		[TagDetails(Tag = 140, Type = TagType.Float, Offset = 1, Required = false)]
		public double? PrevClosePx { get; set; }
		
		[TagDetails(Tag = 11, Type = TagType.String, Offset = 2, Required = false)]
		public string? ClOrdID { get; set; }
		
		[TagDetails(Tag = 526, Type = TagType.String, Offset = 3, Required = false)]
		public string? SecondaryClOrdID { get; set; }
		
		[TagDetails(Tag = 54, Type = TagType.String, Offset = 4, Required = false)]
		public string? Side { get; set; }
		
		[TagDetails(Tag = 44, Type = TagType.Float, Offset = 5, Required = true)]
		public double? Price { get; set; }
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 6, Required = false)]
		public string? Currency { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 7, Required = false)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 8, Required = false, LinksToTag = 355)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 9, Required = false, LinksToTag = 354)]
		public byte[]? EncodedText { get; set; }
		
	}
}
