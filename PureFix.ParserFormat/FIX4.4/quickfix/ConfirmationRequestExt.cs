using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("BH", FixVersion.FIX44)]
	public static class ConfirmationRequestExt
	{
		public static void Parse(this ConfirmationRequest instance, MsgView? view)
		{
			if (view is null) return;
			
			if (view.GetView("StandardHeader") is MsgView groupViewStandardHeader)
			{
				instance.StandardHeader = new StandardHeader();
				instance.StandardHeader!.Parse(groupViewStandardHeader);
			}
			instance.StandardHeader = new StandardHeader();
			instance.StandardHeader?.Parse(view.GetView("StandardHeader"));
			instance.ConfirmReqID = view.GetString(859);
			instance.ConfirmType = view.GetInt32(773);
			if (view.GetView("OrdAllocGrp") is MsgView groupViewOrdAllocGrp)
			{
				instance.OrdAllocGrp = new OrdAllocGrp();
				instance.OrdAllocGrp!.Parse(groupViewOrdAllocGrp);
			}
			instance.OrdAllocGrp = new OrdAllocGrp();
			instance.OrdAllocGrp?.Parse(view.GetView("OrdAllocGrp"));
			instance.AllocID = view.GetString(70);
			instance.SecondaryAllocID = view.GetString(793);
			instance.IndividualAllocID = view.GetString(467);
			instance.TransactTime = view.GetDateTime(60);
			instance.AllocAccount = view.GetString(79);
			instance.AllocAcctIDSource = view.GetInt32(661);
			instance.AllocAccountType = view.GetInt32(798);
			instance.Text = view.GetString(58);
			instance.EncodedTextLen = view.GetInt32(354);
			instance.EncodedText = view.GetByteArray(355);
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
