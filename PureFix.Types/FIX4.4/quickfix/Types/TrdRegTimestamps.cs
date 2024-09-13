using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class TrdRegTimestamps
	{
		[Group(NoOfTag = 768, Offset = 0, Required = false)]
		public TrdRegTimestampsNoTrdRegTimestamps[]? NoTrdRegTimestamps { get; set; }
		
	}
}
