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
		[TagDetails(Tag = 162, Type = TagType.String, Offset = 0)]
		public string? SettlInstID { get; set; }
		
		[TagDetails(Tag = 163, Type = TagType.String, Offset = 1)]
		public string? SettlInstTransType { get; set; }
		
		[TagDetails(Tag = 214, Type = TagType.String, Offset = 2)]
		public string? SettlInstRefID { get; set; }
		
		[Component(Offset = 3)]
		public Parties? Parties { get; set; }
		
		[TagDetails(Tag = 54, Type = TagType.String, Offset = 4)]
		public string? Side { get; set; }
		
		[TagDetails(Tag = 460, Type = TagType.Int, Offset = 5)]
		public int? Product { get; set; }
		
		[TagDetails(Tag = 167, Type = TagType.String, Offset = 6)]
		public string? SecurityType { get; set; }
		
		[TagDetails(Tag = 461, Type = TagType.String, Offset = 7)]
		public string? CFICode { get; set; }
		
		[TagDetails(Tag = 168, Type = TagType.UtcTimestamp, Offset = 8)]
		public DateTime? EffectiveTime { get; set; }
		
		[TagDetails(Tag = 126, Type = TagType.UtcTimestamp, Offset = 9)]
		public DateTime? ExpireTime { get; set; }
		
		[TagDetails(Tag = 779, Type = TagType.UtcTimestamp, Offset = 10)]
		public DateTime? LastUpdateTime { get; set; }
		
		[Component(Offset = 11)]
		public SettlInstructionsData? SettlInstructionsData { get; set; }
		
		[TagDetails(Tag = 492, Type = TagType.Int, Offset = 12)]
		public int? PaymentMethod { get; set; }
		
		[TagDetails(Tag = 476, Type = TagType.String, Offset = 13)]
		public string? PaymentRef { get; set; }
		
		[TagDetails(Tag = 488, Type = TagType.String, Offset = 14)]
		public string? CardHolderName { get; set; }
		
		[TagDetails(Tag = 489, Type = TagType.String, Offset = 15)]
		public string? CardNumber { get; set; }
		
		[TagDetails(Tag = 503, Type = TagType.LocalDate, Offset = 16)]
		public DateTime? CardStartDate { get; set; }
		
		[TagDetails(Tag = 490, Type = TagType.LocalDate, Offset = 17)]
		public DateTime? CardExpDate { get; set; }
		
		[TagDetails(Tag = 491, Type = TagType.String, Offset = 18)]
		public string? CardIssNum { get; set; }
		
		[TagDetails(Tag = 504, Type = TagType.LocalDate, Offset = 19)]
		public DateTime? PaymentDate { get; set; }
		
		[TagDetails(Tag = 505, Type = TagType.String, Offset = 20)]
		public string? PaymentRemitterID { get; set; }
		
	}
}
