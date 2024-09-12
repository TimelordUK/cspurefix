using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class NoNestedPartyIDs
	{
		[TagDetails(524)]
		public string? NestedPartyID { get; set; } // STRING
		
		[TagDetails(525)]
		public string? NestedPartyIDSource { get; set; } // CHAR
		
		[TagDetails(538)]
		public int? NestedPartyRole { get; set; } // INT
		
		public NstdPtysSubGrp? NstdPtysSubGrp { get; set; }
	}
}
