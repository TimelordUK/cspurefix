using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("o", FixVersion.FIX44)]
	public static class RegistrationInstructionsExt
	{
		public static void Parse(this RegistrationInstructions instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.StandardHeader = new StandardHeader();
			instance.StandardHeader?.Parse(view.GetView("StandardHeader"));
			instance.RegistID = view.GetString(513);
			instance.RegistTransType = view.GetString(514);
			instance.RegistRefID = view.GetString(508);
			instance.ClOrdID = view.GetString(11);
			instance.Parties = new Parties();
			instance.Parties?.Parse(view.GetView("Parties"));
			instance.Account = view.GetString(1);
			instance.AcctIDSource = view.GetInt32(660);
			instance.RegistAcctType = view.GetString(493);
			instance.TaxAdvantageType = view.GetInt32(495);
			instance.OwnershipType = view.GetString(517);
			instance.RgstDtlsGrp = new RgstDtlsGrp();
			instance.RgstDtlsGrp?.Parse(view.GetView("RgstDtlsGrp"));
			instance.RgstDistInstGrp = new RgstDistInstGrp();
			instance.RgstDistInstGrp?.Parse(view.GetView("RgstDistInstGrp"));
			instance.StandardTrailer = new StandardTrailer();
			instance.StandardTrailer?.Parse(view.GetView("StandardTrailer"));
		}
	}
}
