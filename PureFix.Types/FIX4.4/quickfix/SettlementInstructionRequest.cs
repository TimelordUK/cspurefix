using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class SettlementInstructionRequest : FixMsg
	{
		[Component]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(791, TagType.String)]
		public string? SettlInstReqID { get; set; }
		
		[TagDetails(60, TagType.UtcTimestamp)]
		public DateTime? TransactTime { get; set; }
		
		[Component]
		public Parties? Parties { get; set; }
		
		[TagDetails(79, TagType.String)]
		public string? AllocAccount { get; set; }
		
		[TagDetails(661, TagType.Int)]
		public int? AllocAcctIDSource { get; set; }
		
		[TagDetails(54, TagType.String)]
		public string? Side { get; set; }
		
		[TagDetails(460, TagType.Int)]
		public int? Product { get; set; }
		
		[TagDetails(167, TagType.String)]
		public string? SecurityType { get; set; }
		
		[TagDetails(461, TagType.String)]
		public string? CFICode { get; set; }
		
		[TagDetails(168, TagType.UtcTimestamp)]
		public DateTime? EffectiveTime { get; set; }
		
		[TagDetails(126, TagType.UtcTimestamp)]
		public DateTime? ExpireTime { get; set; }
		
		[TagDetails(779, TagType.UtcTimestamp)]
		public DateTime? LastUpdateTime { get; set; }
		
		[TagDetails(169, TagType.Int)]
		public int? StandInstDbType { get; set; }
		
		[TagDetails(170, TagType.String)]
		public string? StandInstDbName { get; set; }
		
		[TagDetails(171, TagType.String)]
		public string? StandInstDbID { get; set; }
		
		[Component]
		public override StandardTrailer? StandardTrailer { get; set; }
		
	}
}
