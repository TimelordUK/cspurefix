using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("BD", FixVersion.FIX44)]
	public static class NetworkCounterpartySystemStatusResponseExt
	{
		public static void Parse(this NetworkCounterpartySystemStatusResponse instance, MsgView? view)
		{
			instance.StandardHeader = new StandardHeader();
			instance.StandardHeader?.Parse(view?.GetView("StandardHeader"));
			instance.NetworkStatusResponseType = view?.GetInt32(937);
			instance.NetworkRequestID = view?.GetString(933);
			instance.NetworkResponseID = view?.GetString(932);
			instance.LastNetworkResponseID = view?.GetString(934);
			instance.CompIDStatGrp = new CompIDStatGrp();
			instance.CompIDStatGrp?.Parse(view?.GetView("CompIDStatGrp"));
			instance.StandardTrailer = new StandardTrailer();
			instance.StandardTrailer?.Parse(view?.GetView("StandardTrailer"));
		}
	}
}
