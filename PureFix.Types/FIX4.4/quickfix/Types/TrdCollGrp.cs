using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class TrdCollGrp
	{
		[Group(NoOfTag = 897, Offset = 0, Required = false)]
		public TrdCollGrpNoTrades[]? NoTrades { get; set; }
		
	}
}
