using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class NestedParties2 : IFixValidator, IFixEncoder
	{
		[Group(NoOfTag = 756, Offset = 0, Required = false)]
		public NestedParties2NoNested2PartyIDs[]? NoNested2PartyIDs { get; set; }
		
	}
}
