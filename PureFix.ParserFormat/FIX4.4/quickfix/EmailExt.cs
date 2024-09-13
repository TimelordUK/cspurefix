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
			instance.StandardHeader?.Parse(view?.GetView("StandardHeader"));
			instance.EmailThreadID = view?.GetString(164);
			instance.EmailType = view?.GetString(94);
			instance.OrigTime = view?.GetDateTime(42);
			instance.Subject = view?.GetString(147);
			instance.EncodedSubjectLen = view?.GetInt32(356);
			instance.EncodedSubject = view?.GetByteArray(357);
			instance.RoutingGrp?.Parse(view?.GetView("RoutingGrp"));
			instance.InstrmtGrp?.Parse(view?.GetView("InstrmtGrp"));
			instance.UndInstrmtGrp?.Parse(view?.GetView("UndInstrmtGrp"));
			instance.InstrmtLegGrp?.Parse(view?.GetView("InstrmtLegGrp"));
			instance.OrderID = view?.GetString(37);
			instance.ClOrdID = view?.GetString(11);
			instance.LinesOfTextGrp?.Parse(view?.GetView("LinesOfTextGrp"));
			instance.RawDataLength = view?.GetInt32(95);
			instance.RawData = view?.GetByteArray(96);
			instance.StandardTrailer?.Parse(view?.GetView("StandardTrailer"));
		}
	}
}
