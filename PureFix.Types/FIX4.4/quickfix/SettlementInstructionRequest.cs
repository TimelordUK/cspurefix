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
		public string? SettlInstReqID { get; set; } // 791 STRING
		public DateTime? TransactTime { get; set; } // 60 UTCTIMESTAMP
		public Parties? Parties { get; set; }
		public string? AllocAccount { get; set; } // 79 STRING
		public int? AllocAcctIDSource { get; set; } // 661 INT
		public string? Side { get; set; } // 54 CHAR
		public int? Product { get; set; } // 460 INT
		public string? SecurityType { get; set; } // 167 STRING
		public string? CFICode { get; set; } // 461 STRING
		public DateTime? EffectiveTime { get; set; } // 168 UTCTIMESTAMP
		public DateTime? ExpireTime { get; set; } // 126 UTCTIMESTAMP
		public DateTime? LastUpdateTime { get; set; } // 779 UTCTIMESTAMP
		public int? StandInstDbType { get; set; } // 169 INT
		public string? StandInstDbName { get; set; } // 170 STRING
		public string? StandInstDbID { get; set; } // 171 STRING
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
