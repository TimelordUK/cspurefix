using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class ListStrikePrice : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		[TagDetails(66)]
		public string? ListID { get; set; } // STRING
		
		[TagDetails(422)]
		public int? TotNoStrikes { get; set; } // INT
		
		[TagDetails(893)]
		public bool? LastFragment { get; set; } // BOOLEAN
		
		public InstrmtStrkPxGrp? InstrmtStrkPxGrp { get; set; }
		public UndInstrmtStrkPxGrp? UndInstrmtStrkPxGrp { get; set; }
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
