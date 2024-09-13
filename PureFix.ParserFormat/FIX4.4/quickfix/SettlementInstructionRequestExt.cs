using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("AV", FixVersion.FIX44)]
	public static class SettlementInstructionRequestExt
	{
		public static void Parse(this SettlementInstructionRequest instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.StandardHeader = new StandardHeader();
			instance.StandardHeader?.Parse(view.GetView("StandardHeader"));
			instance.SettlInstReqID = view.GetString(791);
			instance.TransactTime = view.GetDateTime(60);
			instance.Parties = new Parties();
			instance.Parties?.Parse(view.GetView("Parties"));
			instance.AllocAccount = view.GetString(79);
			instance.AllocAcctIDSource = view.GetInt32(661);
			instance.Side = view.GetString(54);
			instance.Product = view.GetInt32(460);
			instance.SecurityType = view.GetString(167);
			instance.CFICode = view.GetString(461);
			instance.EffectiveTime = view.GetDateTime(168);
			instance.ExpireTime = view.GetDateTime(126);
			instance.LastUpdateTime = view.GetDateTime(779);
			instance.StandInstDbType = view.GetInt32(169);
			instance.StandInstDbName = view.GetString(170);
			instance.StandInstDbID = view.GetString(171);
			instance.StandardTrailer = new StandardTrailer();
			instance.StandardTrailer?.Parse(view.GetView("StandardTrailer"));
		}
	}
}
