using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class ResendRequest : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		[TagDetails(7)]
		public int? BeginSeqNo { get; set; } // SEQNUM
		
		[TagDetails(16)]
		public int? EndSeqNo { get; set; } // SEQNUM
		
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
