using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class DlvyInstGrpNoDlvyInst
	{
		[TagDetails(Tag = 165, Type = TagType.String, Offset = 0, Required = false)]
		public string? SettlInstSource { get; set; }
		
		[TagDetails(Tag = 787, Type = TagType.String, Offset = 1, Required = false)]
		public string? DlvyInstType { get; set; }
		
		[Component(Offset = 2, Required = false)]
		public SettlParties? SettlParties { get; set; }
		
	}
}
