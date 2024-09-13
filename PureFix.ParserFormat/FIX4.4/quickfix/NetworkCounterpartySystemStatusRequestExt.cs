using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("BC", FixVersion.FIX44)]
	public static class NetworkCounterpartySystemStatusRequestExt
	{
		public static void Parse(this NetworkCounterpartySystemStatusRequest instance, MsgView? view)
		{
			instance.StandardHeader?.Parse(view?.GetView("StandardHeader"));
			instance.NetworkRequestType = view?.GetInt32(935);
			instance.NetworkRequestID = view?.GetString(933);
			instance.CompIDReqGrp?.Parse(view?.GetView("CompIDReqGrp"));
			instance.StandardTrailer?.Parse(view?.GetView("StandardTrailer"));
		}
	}
}
