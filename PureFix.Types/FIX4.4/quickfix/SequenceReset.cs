using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class SequenceReset : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		[TagDetails(123)]
		public bool? GapFillFlag { get; set; } // BOOLEAN
		
		[TagDetails(36)]
		public int? NewSeqNo { get; set; } // SEQNUM
		
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
