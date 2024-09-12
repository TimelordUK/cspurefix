using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class NoCompIDs
	{
		[TagDetails(930)]
		public string? RefCompID { get; set; } // STRING
		
		[TagDetails(931)]
		public string? RefSubID { get; set; } // STRING
		
		[TagDetails(283)]
		public string? LocationID { get; set; } // STRING
		
		[TagDetails(284)]
		public string? DeskID { get; set; } // STRING
		
	}
}
