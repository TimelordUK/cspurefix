using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("AH", FixVersion.FIX44)]
	public static class RFQRequestExt
	{
		public static void Parse(this RFQRequest instance, MsgView? view)
		{
			instance.StandardHeader = new StandardHeader();
			instance.StandardHeader?.Parse(view?.GetView("StandardHeader"));
			instance.RFQReqID = view?.GetString(644);
			instance.RFQReqGrp = new RFQReqGrp();
			instance.RFQReqGrp?.Parse(view?.GetView("RFQReqGrp"));
			instance.SubscriptionRequestType = view?.GetString(263);
			instance.StandardTrailer = new StandardTrailer();
			instance.StandardTrailer?.Parse(view?.GetView("StandardTrailer"));
		}
	}
}
