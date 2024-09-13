using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("AV", FixVersion.FIX44)]
	public sealed class SettlementInstructionRequest : FixMsg
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 791, Type = TagType.String, Offset = 1, Required = true)]
		public string? SettlInstReqID { get; set; }
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 2, Required = true)]
		public DateTime? TransactTime { get; set; }
		
		[Component(Offset = 3, Required = false)]
		public Parties? Parties { get; set; }
		
		[TagDetails(Tag = 79, Type = TagType.String, Offset = 4, Required = false)]
		public string? AllocAccount { get; set; }
		
		[TagDetails(Tag = 661, Type = TagType.Int, Offset = 5, Required = false)]
		public int? AllocAcctIDSource { get; set; }
		
		[TagDetails(Tag = 54, Type = TagType.String, Offset = 6, Required = false)]
		public string? Side { get; set; }
		
		[TagDetails(Tag = 460, Type = TagType.Int, Offset = 7, Required = false)]
		public int? Product { get; set; }
		
		[TagDetails(Tag = 167, Type = TagType.String, Offset = 8, Required = false)]
		public string? SecurityType { get; set; }
		
		[TagDetails(Tag = 461, Type = TagType.String, Offset = 9, Required = false)]
		public string? CFICode { get; set; }
		
		[TagDetails(Tag = 168, Type = TagType.UtcTimestamp, Offset = 10, Required = false)]
		public DateTime? EffectiveTime { get; set; }
		
		[TagDetails(Tag = 126, Type = TagType.UtcTimestamp, Offset = 11, Required = false)]
		public DateTime? ExpireTime { get; set; }
		
		[TagDetails(Tag = 779, Type = TagType.UtcTimestamp, Offset = 12, Required = false)]
		public DateTime? LastUpdateTime { get; set; }
		
		[TagDetails(Tag = 169, Type = TagType.Int, Offset = 13, Required = false)]
		public int? StandInstDbType { get; set; }
		
		[TagDetails(Tag = 170, Type = TagType.String, Offset = 14, Required = false)]
		public string? StandInstDbName { get; set; }
		
		[TagDetails(Tag = 171, Type = TagType.String, Offset = 15, Required = false)]
		public string? StandInstDbID { get; set; }
		
		[Component(Offset = 16, Required = true)]
		public StandardTrailer? StandardTrailer { get; set; }
		public override string? MsgType => StandardHeader?.MsgType;
		public override int? BodyLength => StandardHeader?.BodyLength;
	}
}
