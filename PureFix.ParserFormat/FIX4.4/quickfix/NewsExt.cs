using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("B", FixVersion.FIX44)]
	public static class NewsExt
	{
		public static void Parse(this News instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.StandardHeader?.Parse(view.GetView("StandardHeader"));
			instance.OrigTime = view.GetDateTime(42);
			instance.Urgency = view.GetString(61);
			instance.Headline = view.GetString(148);
			instance.EncodedHeadlineLen = view.GetInt32(358);
			instance.EncodedHeadline = view.GetByteArray(359);
			instance.RoutingGrp?.Parse(view.GetView("RoutingGrp"));
			instance.InstrmtGrp?.Parse(view.GetView("InstrmtGrp"));
			instance.InstrmtLegGrp?.Parse(view.GetView("InstrmtLegGrp"));
			instance.UndInstrmtGrp?.Parse(view.GetView("UndInstrmtGrp"));
			instance.LinesOfTextGrp?.Parse(view.GetView("LinesOfTextGrp"));
			instance.URLLink = view.GetString(149);
			instance.RawDataLength = view.GetInt32(95);
			instance.RawData = view.GetByteArray(96);
			instance.StandardTrailer?.Parse(view.GetView("StandardTrailer"));
		}
	}
}
