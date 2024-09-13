using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("AA", FixVersion.FIX44)]
	public static class DerivativeSecurityListExt
	{
		public static void Parse(this DerivativeSecurityList instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.StandardHeader?.Parse(view.GetView("StandardHeader"));
			instance.SecurityReqID = view.GetString(320);
			instance.SecurityResponseID = view.GetString(322);
			instance.SecurityRequestResult = view.GetInt32(560);
			instance.UnderlyingInstrument?.Parse(view.GetView("UnderlyingInstrument"));
			instance.TotNoRelatedSym = view.GetInt32(393);
			instance.LastFragment = view.GetBool(893);
			instance.RelSymDerivSecGrp?.Parse(view.GetView("RelSymDerivSecGrp"));
			instance.StandardTrailer?.Parse(view.GetView("StandardTrailer"));
		}
	}
}
