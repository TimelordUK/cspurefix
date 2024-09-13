using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("BE", FixVersion.FIX44)]
	public static class UserRequestExt
	{
		public static void Parse(this UserRequest instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.StandardHeader = new StandardHeader();
			instance.StandardHeader?.Parse(view.GetView("StandardHeader"));
			instance.UserRequestID = view.GetString(923);
			instance.UserRequestType = view.GetInt32(924);
			instance.Username = view.GetString(553);
			instance.Password = view.GetString(554);
			instance.NewPassword = view.GetString(925);
			instance.RawDataLength = view.GetInt32(95);
			instance.RawData = view.GetByteArray(96);
			instance.StandardTrailer = new StandardTrailer();
			instance.StandardTrailer?.Parse(view.GetView("StandardTrailer"));
		}
	}
}
