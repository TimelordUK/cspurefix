using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("C", FixVersion.FIX44)]
	public static class EmailExt
	{
		public static void Parse(this Email instance, MsgView? view)
		{
			if (view is null) return;
			
			if (view.GetView("StandardHeader") is MsgView groupViewStandardHeader)
			{
				instance.StandardHeader = new StandardHeader();
				instance.StandardHeader!.Parse(groupViewStandardHeader);
			}
			instance.EmailThreadID = view.GetString(164);
			instance.EmailType = view.GetString(94);
			instance.OrigTime = view.GetDateTime(42);
			instance.Subject = view.GetString(147);
			instance.EncodedSubjectLen = view.GetInt32(356);
			instance.EncodedSubject = view.GetByteArray(357);
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
			if (view.GetView("UndInstrmtGrp") is MsgView groupViewUndInstrmtGrp)
			{
				instance.UndInstrmtGrp = new UndInstrmtGrp();
				instance.UndInstrmtGrp!.Parse(groupViewUndInstrmtGrp);
			}
			if (view.GetView("InstrmtLegGrp") is MsgView groupViewInstrmtLegGrp)
			{
				instance.InstrmtLegGrp = new InstrmtLegGrp();
				instance.InstrmtLegGrp!.Parse(groupViewInstrmtLegGrp);
			}
			instance.OrderID = view.GetString(37);
			instance.ClOrdID = view.GetString(11);
			if (view.GetView("LinesOfTextGrp") is MsgView groupViewLinesOfTextGrp)
			{
				instance.LinesOfTextGrp = new LinesOfTextGrp();
				instance.LinesOfTextGrp!.Parse(groupViewLinesOfTextGrp);
			}
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
