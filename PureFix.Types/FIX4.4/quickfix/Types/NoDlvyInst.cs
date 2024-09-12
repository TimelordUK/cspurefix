using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class NoDlvyInst
	{
		[TagDetails(165, TagType.String)]
		public string? SettlInstSource { get; set; }
		
		[TagDetails(787, TagType.String)]
		public string? DlvyInstType { get; set; }
		
		[Component]
		public SettlParties? SettlParties { get; set; }
		
	}
}
