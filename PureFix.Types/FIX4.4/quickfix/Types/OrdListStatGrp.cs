using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class OrdListStatGrp : IFixValidator, IFixEncoder
	{
		[Group(NoOfTag = 73, Offset = 0, Required = true)]
		public OrdListStatGrpNoOrders[]? NoOrders { get; set; }
		
	}
}
