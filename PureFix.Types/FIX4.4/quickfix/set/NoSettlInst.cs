using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX4._4.quickfix.set;
namespace PureFix.Types.FIX4._4.quickfix.set
{
	public class NoSettlInst
	{
		public string? SettlInstID { get; set; } // 162 STRING
		public string? SettlInstTransType { get; set; } // 163 CHAR
		public string? SettlInstRefID { get; set; } // 214 STRING
		public Parties? Parties { get; set; }
		public string? Side { get; set; } // 54 CHAR
		public int? Product { get; set; } // 460 INT
		public string? SecurityType { get; set; } // 167 STRING
		public string? CFICode { get; set; } // 461 STRING
		public DateTime? EffectiveTime { get; set; } // 168 UTCTIMESTAMP
		public DateTime? ExpireTime { get; set; } // 126 UTCTIMESTAMP
		public DateTime? LastUpdateTime { get; set; } // 779 UTCTIMESTAMP
		public SettlInstructionsData? SettlInstructionsData { get; set; }
		public int? PaymentMethod { get; set; } // 492 INT
		public string? PaymentRef { get; set; } // 476 STRING
		public string? CardHolderName { get; set; } // 488 STRING
		public string? CardNumber { get; set; } // 489 STRING
		public DateTime? CardStartDate { get; set; } // 503 LOCALMKTDATE
		public DateTime? CardExpDate { get; set; } // 490 LOCALMKTDATE
		public string? CardIssNum { get; set; } // 491 STRING
		public DateTime? PaymentDate { get; set; } // 504 LOCALMKTDATE
		public string? PaymentRemitterID { get; set; } // 505 STRING
	}
}
