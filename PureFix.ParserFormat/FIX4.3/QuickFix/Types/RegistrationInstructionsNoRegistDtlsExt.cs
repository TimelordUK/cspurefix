using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX43.QuickFix.Types
{
	public static class RegistrationInstructionsNoRegistDtlsExt
	{
		public static void Parse(this RegistrationInstructionsNoRegistDtls instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.RegistDetls = view.GetString(509);
			instance.RegistEmail = view.GetString(511);
			instance.MailingDtls = view.GetString(474);
			instance.MailingInst = view.GetString(482);
			if (view.GetView("NestedParties") is MsgView groupViewNestedParties)
			{
				instance.NestedParties = new NestedParties();
				instance.NestedParties!.Parse(groupViewNestedParties);
			}
			instance.OwnerType = view.GetInt32(522);
			instance.DateOfBirth = view.GetDateOnly(486);
			instance.InvestorCountryOfResidence = view.GetString(475);
		}
	}
}
