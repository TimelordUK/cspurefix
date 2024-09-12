using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX4._4.quickfix.set;
namespace PureFix.Types.FIX4._4.quickfix.set
{
	public class NoSecurityTypes
	{
		public string? SecurityType { get; set; } // 167 STRING
		public string? SecuritySubType { get; set; } // 762 STRING
		public int? Product { get; set; } // 460 INT
		public string? CFICode { get; set; } // 461 STRING
	}
}
