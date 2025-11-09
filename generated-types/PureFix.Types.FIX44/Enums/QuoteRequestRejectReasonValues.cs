using System;

namespace PureFix.Types.FIX44
{
	public static class QuoteRequestRejectReasonValues
	{
		public const int UnknownSymbol = 1;
		public const int Exchange = 2;
		public const int QuoteRequestExceedsLimit = 3;
		public const int TooLateToEnter = 4;
		public const int InvalidPrice = 5;
		public const int NotAuthorizedToRequestQuote = 6;
		public const int NoMatchForInquiry = 7;
		public const int NoMarketForInstrument = 8;
		public const int NoInventory = 9;
		public const int Pass = 10;
		public const int Other = 99;
	}
}
