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
		[TagDetails(Tag = 782, Type = TagType.String, Offset = 0)]
		public string? SettlPartyID { get; set; }
		
		[TagDetails(Tag = 783, Type = TagType.String, Offset = 1)]
		public string? SettlPartyIDSource { get; set; }
		
		[TagDetails(Tag = 784, Type = TagType.Int, Offset = 2)]
		public int? SettlPartyRole { get; set; }
		
		[Component(Offset = 3)]
		public SettlPtysSubGrp? SettlPtysSubGrp { get; set; }
		
	}
}
