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
		[TagDetails(Tag = 162, Type = TagType.String, Offset = 0, Required = false)]
		public string? SettlInstID { get; set; }
		
		[TagDetails(Tag = 163, Type = TagType.String, Offset = 1, Required = false)]
		public string? SettlInstTransType { get; set; }
		
		[TagDetails(Tag = 214, Type = TagType.String, Offset = 2, Required = false)]
		public string? SettlInstRefID { get; set; }
		
		[Component(Offset = 3, Required = false)]
		public Parties? Parties { get; set; }
		
		[TagDetails(Tag = 54, Type = TagType.String, Offset = 4, Required = false)]
		public string? Side { get; set; }
		
		[TagDetails(Tag = 460, Type = TagType.Int, Offset = 5, Required = false)]
		public int? Product { get; set; }
		
		[TagDetails(Tag = 167, Type = TagType.String, Offset = 6, Required = false)]
		public string? SecurityType { get; set; }
		
		[TagDetails(Tag = 461, Type = TagType.String, Offset = 7, Required = false)]
		public string? CFICode { get; set; }
		
		[TagDetails(Tag = 168, Type = TagType.UtcTimestamp, Offset = 8, Required = false)]
		public DateTime? EffectiveTime { get; set; }
		
		[TagDetails(Tag = 126, Type = TagType.UtcTimestamp, Offset = 9, Required = false)]
		public DateTime? ExpireTime { get; set; }
		
		[TagDetails(Tag = 779, Type = TagType.UtcTimestamp, Offset = 10, Required = false)]
		public DateTime? LastUpdateTime { get; set; }
		
		[Component(Offset = 11, Required = false)]
		public SettlInstructionsData? SettlInstructionsData { get; set; }
		
		[TagDetails(Tag = 492, Type = TagType.Int, Offset = 12, Required = false)]
		public int? PaymentMethod { get; set; }
		
		[TagDetails(Tag = 476, Type = TagType.String, Offset = 13, Required = false)]
		public string? PaymentRef { get; set; }
		
		[TagDetails(Tag = 488, Type = TagType.String, Offset = 14, Required = false)]
		public string? CardHolderName { get; set; }
		
		[TagDetails(Tag = 489, Type = TagType.String, Offset = 15, Required = false)]
		public string? CardNumber { get; set; }
		
		[TagDetails(Tag = 503, Type = TagType.LocalDate, Offset = 16, Required = false)]
		public DateTime? CardStartDate { get; set; }
		
		[TagDetails(Tag = 490, Type = TagType.LocalDate, Offset = 17, Required = false)]
		public DateTime? CardExpDate { get; set; }
		
		[TagDetails(Tag = 491, Type = TagType.String, Offset = 18, Required = false)]
		public string? CardIssNum { get; set; }
		
		[TagDetails(Tag = 504, Type = TagType.LocalDate, Offset = 19, Required = false)]
		public DateTime? PaymentDate { get; set; }
		
		[TagDetails(Tag = 505, Type = TagType.String, Offset = 20, Required = false)]
		public string? PaymentRemitterID { get; set; }
		
	}
}
