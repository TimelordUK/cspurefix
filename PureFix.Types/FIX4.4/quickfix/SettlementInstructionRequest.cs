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
		public override StandardHeader? StandardHeader { get; set; }
		[TagDetails(791)]
		public string? SettlInstReqID { get; set; } // STRING
		
		[TagDetails(60)]
		public DateTime? TransactTime { get; set; } // UTCTIMESTAMP
		
		public Parties? Parties { get; set; }
		[TagDetails(79)]
		public string? AllocAccount { get; set; } // STRING
		
		[TagDetails(661)]
		public int? AllocAcctIDSource { get; set; } // INT
		
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
		
		[TagDetails(169)]
		public int? StandInstDbType { get; set; } // INT
		
		[TagDetails(170)]
		public string? StandInstDbName { get; set; } // STRING
		
		[TagDetails(171)]
		public string? StandInstDbID { get; set; } // STRING
		
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
