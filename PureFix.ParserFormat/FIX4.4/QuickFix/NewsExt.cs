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
			
			if (view.GetView("StandardHeader") is MsgView groupViewStandardHeader)
			{
				instance.StandardHeader = new StandardHeader();
				instance.StandardHeader!.Parse(groupViewStandardHeader);
			}
			instance.OrigTime = view.GetDateTime(42);
			instance.Urgency = view.GetString(61);
			instance.Headline = view.GetString(148);
			instance.EncodedHeadlineLen = view.GetInt32(358);
			instance.EncodedHeadline = view.GetByteArray(359);
			if (view.GetView("RoutingGrp") is MsgView groupViewRoutingGrp)
			{
				instance.RoutingGrp = new RoutingGrp();
				instance.RoutingGrp!.Parse(groupViewRoutingGrp);
			}
			if (view.GetView("InstrmtGrp") is MsgView groupViewInstrmtGrp)
			{
				instance.InstrmtGrp = new InstrmtGrp();
				instance.InstrmtGrp!.Parse(groupViewInstrmtGrp);
			}
			if (view.GetView("InstrmtLegGrp") is MsgView groupViewInstrmtLegGrp)
			{
				instance.InstrmtLegGrp = new InstrmtLegGrp();
				instance.InstrmtLegGrp!.Parse(groupViewInstrmtLegGrp);
			}
			if (view.GetView("UndInstrmtGrp") is MsgView groupViewUndInstrmtGrp)
			{
				instance.UndInstrmtGrp = new UndInstrmtGrp();
				instance.UndInstrmtGrp!.Parse(groupViewUndInstrmtGrp);
			}
			if (view.GetView("LinesOfTextGrp") is MsgView groupViewLinesOfTextGrp)
			{
				instance.LinesOfTextGrp = new LinesOfTextGrp();
				instance.LinesOfTextGrp!.Parse(groupViewLinesOfTextGrp);
			}
			instance.URLLink = view.GetString(149);
			instance.RawDataLength = view.GetInt32(95);
			instance.RawData = view.GetByteArray(96);
			if (view.GetView("StandardTrailer") is MsgView groupViewStandardTrailer)
			{
				instance.StandardTrailer = new StandardTrailer();
				instance.StandardTrailer!.Parse(groupViewStandardTrailer);
			}
		}
	}
}
