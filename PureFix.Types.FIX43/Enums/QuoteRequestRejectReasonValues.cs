using System;

namespace PureFix.Types.FIX43
{
	public static class QuoteRequestRejectReasonValues
	{
		public const int UnknownSymbol = 1;
		public const int Exchange = 2;
		public const int QuoteRequestExceedsLimit = 3;
		public const int TooLateToEnter = 4;
		public const int InvalidPrice = 5;
		public const int NotAuthorizedToRequestQuote = 6;
	}
}
