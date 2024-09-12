using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class NoBidComponents
	{
		[TagDetails(Tag = 66, Type = TagType.String, Offset = 0)]
		public string? ListID { get; set; }
		
		[TagDetails(Tag = 54, Type = TagType.String, Offset = 1)]
		public string? Side { get; set; }
		
		[TagDetails(Tag = 336, Type = TagType.String, Offset = 2)]
		public string? TradingSessionID { get; set; }
		
		[TagDetails(Tag = 625, Type = TagType.String, Offset = 3)]
		public string? TradingSessionSubID { get; set; }
		
		[TagDetails(Tag = 430, Type = TagType.Int, Offset = 4)]
		public int? NetGrossInd { get; set; }
		
		[TagDetails(Tag = 63, Type = TagType.String, Offset = 5)]
		public string? SettlType { get; set; }
		
		[TagDetails(Tag = 64, Type = TagType.LocalDate, Offset = 6)]
		public DateTime? SettlDate { get; set; }
		
		[TagDetails(Tag = 1, Type = TagType.String, Offset = 7)]
		public string? Account { get; set; }
		
		[TagDetails(Tag = 660, Type = TagType.Int, Offset = 8)]
		public int? AcctIDSource { get; set; }
		
	}
}
