using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX4._4.quickfix.set;
namespace PureFix.Types.FIX4._4.quickfix
{
	public class ResendRequest : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		public int? BeginSeqNo { get; set; } // 7 SEQNUM
		public int? EndSeqNo { get; set; } // 16 SEQNUM
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
