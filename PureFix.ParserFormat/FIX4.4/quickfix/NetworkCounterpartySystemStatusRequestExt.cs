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
			if (view is null) return;
			
			if (view.GetView("StandardHeader") is MsgView groupViewStandardHeader)
			{
				instance.StandardHeader = new StandardHeader();
				instance.StandardHeader!.Parse(groupViewStandardHeader);
			}
			instance.StandardHeader = new StandardHeader();
			instance.StandardHeader?.Parse(view.GetView("StandardHeader"));
			instance.NetworkRequestType = view.GetInt32(935);
			instance.NetworkRequestID = view.GetString(933);
			if (view.GetView("CompIDReqGrp") is MsgView groupViewCompIDReqGrp)
			{
				instance.CompIDReqGrp = new CompIDReqGrp();
				instance.CompIDReqGrp!.Parse(groupViewCompIDReqGrp);
			}
			instance.CompIDReqGrp = new CompIDReqGrp();
			instance.CompIDReqGrp?.Parse(view.GetView("CompIDReqGrp"));
			if (view.GetView("StandardTrailer") is MsgView groupViewStandardTrailer)
			{
				instance.StandardTrailer = new StandardTrailer();
				instance.StandardTrailer!.Parse(groupViewStandardTrailer);
			}
			instance.StandardTrailer = new StandardTrailer();
			instance.StandardTrailer?.Parse(view.GetView("StandardTrailer"));
		}
	}
}
