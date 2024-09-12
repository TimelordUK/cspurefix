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
		[TagDetails(167, TagType.String)]
		public string? SecurityType { get; set; }
		
		[TagDetails(762, TagType.String)]
		public string? SecuritySubType { get; set; }
		
		[TagDetails(460, TagType.Int)]
		public int? Product { get; set; }
		
		[TagDetails(461, TagType.String)]
		public string? CFICode { get; set; }
		
	}
}
