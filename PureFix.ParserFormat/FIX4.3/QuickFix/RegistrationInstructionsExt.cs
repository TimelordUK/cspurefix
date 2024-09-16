using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX43.QuickFix
{
	[MessageType("o", FixVersion.FIX43)]
	public static class RegistrationInstructionsExt
	{
		public static void Parse(this RegistrationInstructions instance, MsgView? view)
		{
			if (view is null) return;
			
			if (view.GetView("StandardHeader") is MsgView groupViewStandardHeader)
			{
				instance.StandardHeader = new StandardHeader();
				instance.StandardHeader!.Parse(groupViewStandardHeader);
			}
			instance.RegistID = view.GetString(513);
			instance.RegistTransType = view.GetString(514);
			instance.RegistRefID = view.GetString(508);
			instance.ClOrdID = view.GetString(11);
			if (view.GetView("Parties") is MsgView groupViewParties)
			{
				instance.Parties = new Parties();
				instance.Parties!.Parse(groupViewParties);
			}
			instance.Account = view.GetString(1);
			instance.RegistAcctType = view.GetString(493);
			instance.TaxAdvantageType = view.GetInt32(495);
			instance.OwnershipType = view.GetString(517);
			var groupViewNoRegistDtls = view.GetView("NoRegistDtls");
			if (groupViewNoRegistDtls is null) return;
			
			var countNoRegistDtls = groupViewNoRegistDtls.GroupCount();
			instance.NoRegistDtls = new RegistrationInstructionsNoRegistDtls[countNoRegistDtls];
			for (var i = 0; i < countNoRegistDtls; ++i)
			{
				instance.NoRegistDtls[i] = new();
				instance.NoRegistDtls[i].Parse(groupViewNoRegistDtls[i]);
			}
			var groupViewNoDistribInsts = view.GetView("NoDistribInsts");
			if (groupViewNoDistribInsts is null) return;
			
			var countNoDistribInsts = groupViewNoDistribInsts.GroupCount();
			instance.NoDistribInsts = new RegistrationInstructionsNoDistribInsts[countNoDistribInsts];
			for (var i = 0; i < countNoDistribInsts; ++i)
			{
				instance.NoDistribInsts[i] = new();
				instance.NoDistribInsts[i].Parse(groupViewNoDistribInsts[i]);
			}
			if (view.GetView("StandardTrailer") is MsgView groupViewStandardTrailer)
			{
				instance.StandardTrailer = new StandardTrailer();
				instance.StandardTrailer!.Parse(groupViewStandardTrailer);
			}
		}
	}
}
