using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class UserResponse : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		public string? UserRequestID { get; set; } // 923 STRING
		public string? Username { get; set; } // 553 STRING
		public int? UserStatus { get; set; } // 926 INT
		public string? UserStatusText { get; set; } // 927 STRING
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
