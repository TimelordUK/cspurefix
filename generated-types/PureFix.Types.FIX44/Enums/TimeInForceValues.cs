using System;

namespace PureFix.Types.FIX44
{
	public static class TimeInForceValues
	{
		public const string Day = "0";
		public const string GoodTillCancel = "1";
		public const string AtTheOpening = "2";
		public const string ImmediateOrCancel = "3";
		public const string FillOrKill = "4";
		public const string GoodTillCrossing = "5";
		public const string GoodTillDate = "6";
		public const string AtTheClose = "7";
	}
}
