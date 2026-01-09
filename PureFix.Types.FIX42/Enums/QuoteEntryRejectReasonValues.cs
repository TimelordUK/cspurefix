using System;

namespace PureFix.Types.FIX42
{
	public static class QuoteEntryRejectReasonValues
	{
		public const int UnknownSymbol = 1;
		public const int Exchange = 2;
		public const int QuoteExceedsLimit = 3;
		public const int TooLateToEnter = 4;
		public const int UnknownQuote = 5;
		public const int DuplicateQuote = 6;
		public const int InvalidBidAskSpread = 7;
		public const int InvalidPrice = 8;
		public const int NotAuthorizedToQuoteSecurity = 9;
	}
}
