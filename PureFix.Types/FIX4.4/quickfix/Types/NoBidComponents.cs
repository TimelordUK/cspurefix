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
		[TagDetails(66, TagType.String)]
		public string? ListID { get; set; }
		
		[TagDetails(54, TagType.String)]
		public string? Side { get; set; }
		
		[TagDetails(336, TagType.String)]
		public string? TradingSessionID { get; set; }
		
		[TagDetails(625, TagType.String)]
		public string? TradingSessionSubID { get; set; }
		
		[TagDetails(430, TagType.Int)]
		public int? NetGrossInd { get; set; }
		
		[TagDetails(63, TagType.String)]
		public string? SettlType { get; set; }
		
		[TagDetails(64, TagType.LocalDate)]
		public DateTime? SettlDate { get; set; }
		
		[TagDetails(1, TagType.String)]
		public string? Account { get; set; }
		
		[TagDetails(660, TagType.Int)]
		public int? AcctIDSource { get; set; }
		
	}
}
