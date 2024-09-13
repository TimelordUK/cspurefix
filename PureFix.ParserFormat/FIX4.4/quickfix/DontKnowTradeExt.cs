using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("Q", FixVersion.FIX44)]
	public static class DontKnowTradeExt
	{
		public static void Parse(this DontKnowTrade instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.StandardHeader?.Parse(view.GetView("StandardHeader"));
			instance.OrderID = view.GetString(37);
			instance.SecondaryOrderID = view.GetString(198);
			instance.ExecID = view.GetString(17);
			instance.DKReason = view.GetString(127);
			instance.Instrument?.Parse(view.GetView("Instrument"));
			instance.UndInstrmtGrp?.Parse(view.GetView("UndInstrmtGrp"));
			instance.InstrmtLegGrp?.Parse(view.GetView("InstrmtLegGrp"));
			instance.Side = view.GetString(54);
			instance.OrderQtyData?.Parse(view.GetView("OrderQtyData"));
			instance.LastQty = view.GetDouble(32);
			instance.LastPx = view.GetDouble(31);
			instance.Text = view.GetString(58);
			instance.EncodedTextLen = view.GetInt32(354);
			instance.EncodedText = view.GetByteArray(355);
			instance.StandardTrailer?.Parse(view.GetView("StandardTrailer"));
		}
	}
}
