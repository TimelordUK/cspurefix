using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public class NoSecurityTypes
	{
		public string? SecurityType { get; set; } // 167 STRING
		public string? SecuritySubType { get; set; } // 762 STRING
		public int? Product { get; set; } // 460 INT
		public string? CFICode { get; set; } // 461 STRING
	}
}
