using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class BidDescReqGrp
	{
		[Group(NoOfTag = 398, Offset = 0, Required = false)]
		public NoBidDescriptors[]? NoBidDescriptors { get; set; }
		
	}
}
