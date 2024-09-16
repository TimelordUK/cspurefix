using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("y", FixVersion.FIX44)]
	public static class SecurityListExt
	{
		public static void Parse(this SecurityList instance, MsgView? view)
		{
			if (view is null) return;
			
			if (view.GetView("StandardHeader") is MsgView groupViewStandardHeader)
			{
				instance.StandardHeader = new StandardHeader();
				instance.StandardHeader!.Parse(groupViewStandardHeader);
			}
			instance.SecurityReqID = view.GetString(320);
			instance.SecurityResponseID = view.GetString(322);
			instance.SecurityRequestResult = view.GetInt32(560);
			instance.TotNoRelatedSym = view.GetInt32(393);
			instance.LastFragment = view.GetBool(893);
			if (view.GetView("SecListGrp") is MsgView groupViewSecListGrp)
			{
				instance.SecListGrp = new SecListGrp();
				instance.SecListGrp!.Parse(groupViewSecListGrp);
			}
			if (view.GetView("StandardTrailer") is MsgView groupViewStandardTrailer)
			{
				instance.StandardTrailer = new StandardTrailer();
				instance.StandardTrailer!.Parse(groupViewStandardTrailer);
			}
		}
	}
}
