using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX4._4.quickfix.set;
namespace PureFix.Types.FIX4._4.quickfix.set
{
	public class NoNestedPartyIDs
	{
		public string? NestedPartyID { get; set; } // 524 STRING
		public string? NestedPartyIDSource { get; set; } // 525 CHAR
		public int? NestedPartyRole { get; set; } // 538 INT
		public NstdPtysSubGrp? NstdPtysSubGrp { get; set; }
	}
}
