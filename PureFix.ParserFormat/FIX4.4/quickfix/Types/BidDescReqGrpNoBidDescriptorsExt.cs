using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class BidDescReqGrpNoBidDescriptorsExt
	{
		public static void Parse(this BidDescReqGrpNoBidDescriptors instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.BidDescriptorType = view.GetInt32(399);
			instance.BidDescriptor = view.GetString(400);
			instance.SideValueInd = view.GetInt32(401);
			instance.LiquidityValue = view.GetDouble(404);
			instance.LiquidityNumSecurities = view.GetInt32(441);
			instance.LiquidityPctLow = view.GetDouble(402);
			instance.LiquidityPctHigh = view.GetDouble(403);
			instance.EFPTrackingError = view.GetDouble(405);
			instance.FairValue = view.GetDouble(406);
			instance.OutsideIndexPct = view.GetDouble(407);
			instance.ValueOfFutures = view.GetDouble(408);
		}
	}
}
