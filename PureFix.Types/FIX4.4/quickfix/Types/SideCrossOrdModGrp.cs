using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class SideCrossOrdModGrp : IFixValidator, IFixEncoder
	{
		[Group(NoOfTag = 552, Offset = 0, Required = true)]
		public SideCrossOrdModGrpNoSides[]? NoSides { get; set; }
		
	}
}
