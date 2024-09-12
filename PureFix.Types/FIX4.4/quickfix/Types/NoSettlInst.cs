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
		[TagDetails(162)]
		public string? SettlInstID { get; set; } // STRING
		
		[TagDetails(163)]
		public string? SettlInstTransType { get; set; } // CHAR
		
		[TagDetails(214)]
		public string? SettlInstRefID { get; set; } // STRING
		
		public Parties? Parties { get; set; }
		[TagDetails(54)]
		public string? Side { get; set; } // CHAR
		
		[TagDetails(460)]
		public int? Product { get; set; } // INT
		
		[TagDetails(167)]
		public string? SecurityType { get; set; } // STRING
		
		[TagDetails(461)]
		public string? CFICode { get; set; } // STRING
		
		[TagDetails(168)]
		public DateTime? EffectiveTime { get; set; } // UTCTIMESTAMP
		
		[TagDetails(126)]
		public DateTime? ExpireTime { get; set; } // UTCTIMESTAMP
		
		[TagDetails(779)]
		public DateTime? LastUpdateTime { get; set; } // UTCTIMESTAMP
		
		public SettlInstructionsData? SettlInstructionsData { get; set; }
		[TagDetails(492)]
		public int? PaymentMethod { get; set; } // INT
		
		[TagDetails(476)]
		public string? PaymentRef { get; set; } // STRING
		
		[TagDetails(488)]
		public string? CardHolderName { get; set; } // STRING
		
		[TagDetails(489)]
		public string? CardNumber { get; set; } // STRING
		
		[TagDetails(503)]
		public DateTime? CardStartDate { get; set; } // LOCALMKTDATE
		
		[TagDetails(490)]
		public DateTime? CardExpDate { get; set; } // LOCALMKTDATE
		
		[TagDetails(491)]
		public string? CardIssNum { get; set; } // STRING
		
		[TagDetails(504)]
		public DateTime? PaymentDate { get; set; } // LOCALMKTDATE
		
		[TagDetails(505)]
		public string? PaymentRemitterID { get; set; } // STRING
		
	}
}
