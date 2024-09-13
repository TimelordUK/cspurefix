using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("p", FixVersion.FIX44)]
	public static class RegistrationInstructionsResponseExt
	{
		public static void Parse(this RegistrationInstructionsResponse instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.StandardHeader?.Parse(view.GetView("StandardHeader"));
			instance.RegistID = view.GetString(513);
			instance.RegistTransType = view.GetString(514);
			instance.RegistRefID = view.GetString(508);
			instance.ClOrdID = view.GetString(11);
			instance.Parties?.Parse(view.GetView("Parties"));
			instance.Account = view.GetString(1);
			instance.AcctIDSource = view.GetInt32(660);
			instance.RegistStatus = view.GetString(506);
			instance.RegistRejReasonCode = view.GetInt32(507);
			instance.RegistRejReasonText = view.GetString(496);
			instance.StandardTrailer?.Parse(view.GetView("StandardTrailer"));
		}
	}
}
