using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX4._4.quickfix.set;
namespace PureFix.Types.FIX4._4.quickfix
{
	public class SequenceReset : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		public bool? GapFillFlag { get; set; } // 123 BOOLEAN
		public int? NewSeqNo { get; set; } // 36 SEQNUM
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
