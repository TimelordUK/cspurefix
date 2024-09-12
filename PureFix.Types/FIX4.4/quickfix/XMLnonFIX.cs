using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("n", FixVersion.FIX44)]
	public sealed class XMLnonFIX : FixMsg
	{
		[Component(Offset = 0)]
		public override StandardHeader? StandardHeader { get; set; }
		
		[Component(Offset = 1)]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
