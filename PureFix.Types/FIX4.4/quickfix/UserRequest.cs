using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public class UserRequest : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		public string? UserRequestID { get; set; } // 923 STRING
		public int? UserRequestType { get; set; } // 924 INT
		public string? Username { get; set; } // 553 STRING
		public string? Password { get; set; } // 554 STRING
		public string? NewPassword { get; set; } // 925 STRING
		public int? RawDataLength { get; set; } // 95 LENGTH
		public byte[]? RawData { get; set; } // 96 DATA
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
