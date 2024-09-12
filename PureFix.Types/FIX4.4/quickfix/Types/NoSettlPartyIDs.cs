using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class NoSettlPartyIDs
	{
		[TagDetails(782, TagType.String)]
		public string? SettlPartyID { get; set; }
		
		[TagDetails(783, TagType.String)]
		public string? SettlPartyIDSource { get; set; }
		
		[TagDetails(784, TagType.Int)]
		public int? SettlPartyRole { get; set; }
		
		[Component]
		public SettlPtysSubGrp? SettlPtysSubGrp { get; set; }
		
	}
}
