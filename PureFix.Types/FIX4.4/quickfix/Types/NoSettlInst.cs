using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class NoSettlInst
	{
		[TagDetails(162, TagType.String)]
		public string? SettlInstID { get; set; }
		
		[TagDetails(163, TagType.String)]
		public string? SettlInstTransType { get; set; }
		
		[TagDetails(214, TagType.String)]
		public string? SettlInstRefID { get; set; }
		
		[Component]
		public Parties? Parties { get; set; }
		
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
		
		[Component]
		public SettlInstructionsData? SettlInstructionsData { get; set; }
		
		[TagDetails(492, TagType.Int)]
		public int? PaymentMethod { get; set; }
		
		[TagDetails(476, TagType.String)]
		public string? PaymentRef { get; set; }
		
		[TagDetails(488, TagType.String)]
		public string? CardHolderName { get; set; }
		
		[TagDetails(489, TagType.String)]
		public string? CardNumber { get; set; }
		
		[TagDetails(503, TagType.LocalDate)]
		public DateTime? CardStartDate { get; set; }
		
		[TagDetails(490, TagType.LocalDate)]
		public DateTime? CardExpDate { get; set; }
		
		[TagDetails(491, TagType.String)]
		public string? CardIssNum { get; set; }
		
		[TagDetails(504, TagType.LocalDate)]
		public DateTime? PaymentDate { get; set; }
		
		[TagDetails(505, TagType.String)]
		public string? PaymentRemitterID { get; set; }
		
	}
}
