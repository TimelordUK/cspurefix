using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("W", FixVersion.FIX44)]
	public sealed class MarketDataSnapshotFullRefresh : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 262, Type = TagType.String, Offset = 1, Required = false)]
		public string? MDReqID { get; set; }
		
		[Component(Offset = 2, Required = true)]
		public Instrument? Instrument { get; set; }
		
		[Component(Offset = 3, Required = false)]
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		
		[Component(Offset = 4, Required = false)]
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		
		[TagDetails(Tag = 291, Type = TagType.String, Offset = 5, Required = false)]
		public string? FinancialStatus { get; set; }
		
		[TagDetails(Tag = 292, Type = TagType.String, Offset = 6, Required = false)]
		public string? CorporateAction { get; set; }
		
		[TagDetails(Tag = 451, Type = TagType.Float, Offset = 7, Required = false)]
		public double? NetChgPrevDay { get; set; }
		
		[Component(Offset = 8, Required = true)]
		public MDFullGrp? MDFullGrp { get; set; }
		
		[TagDetails(Tag = 813, Type = TagType.Int, Offset = 9, Required = false)]
		public int? ApplQueueDepth { get; set; }
		
		[TagDetails(Tag = 814, Type = TagType.Int, Offset = 10, Required = false)]
		public int? ApplQueueResolution { get; set; }
		
		[Component(Offset = 11, Required = true)]
		public StandardTrailer? StandardTrailer { get; set; }
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
	}
}
