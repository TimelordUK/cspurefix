using System;

namespace PureFix.Types.FIX43
{
	public static class ExecPriceTypeValues
	{
		public const string SinglePrice = "S";
		public const string OfferPriceMinusAdjustmentAmount = "Q";
		public const string OfferPriceMinusAdjustmentPercent = "P";
		public const string OfferPrice = "O";
		public const string CreationPricePlusAdjustmentAmount = "E";
		public const string CreationPricePlusAdjustmentPercent = "D";
		public const string CreationPrice = "C";
		public const string BidPrice = "B";
	}
}
