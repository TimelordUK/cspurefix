using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX42.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX42.QuickFix
{
	[MessageType("P", FixVersion.FIX42)]
	public static class AllocationInstructionAckExt
	{
		public static void Parse(this AllocationInstructionAck instance, MsgView? view)
		{
			if (view is null) return;
			
			if (view.GetView("StandardHeader") is MsgView groupViewStandardHeader)
			{
				instance.StandardHeader = new StandardHeader();
				instance.StandardHeader!.Parse(groupViewStandardHeader);
			}
			instance.ClientID = view.GetString(109);
			instance.ExecBroker = view.GetString(76);
			instance.AllocID = view.GetString(70);
			instance.TradeDate = view.GetDateOnly(75);
			instance.TransactTime = view.GetDateTime(60);
			instance.AllocStatus = view.GetInt32(87);
			instance.AllocRejCode = view.GetInt32(88);
			instance.Text = view.GetString(58);
			instance.EncodedTextLen = view.GetInt32(354);
			instance.EncodedText = view.GetByteArray(355);
			if (view.GetView("StandardTrailer") is MsgView groupViewStandardTrailer)
			{
				instance.StandardTrailer = new StandardTrailer();
				instance.StandardTrailer!.Parse(groupViewStandardTrailer);
			}
		}
	}
}
