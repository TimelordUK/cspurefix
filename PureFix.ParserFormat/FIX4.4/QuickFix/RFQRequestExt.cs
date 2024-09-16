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
			if (view is null) return;
			
			if (view.GetView("StandardHeader") is MsgView groupViewStandardHeader)
			{
				instance.StandardHeader = new StandardHeader();
				instance.StandardHeader!.Parse(groupViewStandardHeader);
			}
			instance.RFQReqID = view.GetString(644);
			if (view.GetView("RFQReqGrp") is MsgView groupViewRFQReqGrp)
			{
				instance.RFQReqGrp = new RFQReqGrp();
				instance.RFQReqGrp!.Parse(groupViewRFQReqGrp);
			}
			instance.SubscriptionRequestType = view.GetString(263);
			if (view.GetView("StandardTrailer") is MsgView groupViewStandardTrailer)
			{
				instance.StandardTrailer = new StandardTrailer();
				instance.StandardTrailer!.Parse(groupViewStandardTrailer);
			}
		}
	}
}
