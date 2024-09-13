using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("BF", FixVersion.FIX44)]
	public static class UserResponseExt
	{
		public static void Parse(this UserResponse instance, MsgView? view)
		{
			instance.StandardHeader = new StandardHeader();
			instance.StandardHeader?.Parse(view?.GetView("StandardHeader"));
			instance.UserRequestID = view?.GetString(923);
			instance.Username = view?.GetString(553);
			instance.UserStatus = view?.GetInt32(926);
			instance.UserStatusText = view?.GetString(927);
			instance.StandardTrailer = new StandardTrailer();
			instance.StandardTrailer?.Parse(view?.GetView("StandardTrailer"));
		}
	}
}
