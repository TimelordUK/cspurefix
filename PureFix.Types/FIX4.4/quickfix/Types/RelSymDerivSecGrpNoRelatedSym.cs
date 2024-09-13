using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class RelSymDerivSecGrpNoRelatedSym
	{
		[Component(Offset = 0, Required = false)]
		public Instrument? Instrument { get; set; }
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 1, Required = false)]
		public string? Currency { get; set; }
		
		[TagDetails(Tag = 827, Type = TagType.Int, Offset = 2, Required = false)]
		public int? ExpirationCycle { get; set; }
		
		[Component(Offset = 3, Required = false)]
		public InstrumentExtension? InstrumentExtension { get; set; }
		
		[Component(Offset = 4, Required = false)]
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		
		[TagDetails(Tag = 336, Type = TagType.String, Offset = 5, Required = false)]
		public string? TradingSessionID { get; set; }
		
		[TagDetails(Tag = 625, Type = TagType.String, Offset = 6, Required = false)]
		public string? TradingSessionSubID { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 7, Required = false)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 8, Required = false, LinksToTag = 355)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 9, Required = false, LinksToTag = 354)]
		public byte[]? EncodedText { get; set; }
		
	}
}
