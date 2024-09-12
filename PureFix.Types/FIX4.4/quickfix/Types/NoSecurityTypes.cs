using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class NoSecurityTypes
	{
		[TagDetails(167)]
		public string? SecurityType { get; set; } // STRING
		
		[TagDetails(762)]
		public string? SecuritySubType { get; set; } // STRING
		
		[TagDetails(460)]
		public int? Product { get; set; } // INT
		
		[TagDetails(461)]
		public string? CFICode { get; set; } // STRING
		
	}
}
