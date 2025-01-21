namespace PureFix.Types.FIX50SP2.FixRepo.Types
{
	public static class QuoteRejectReasonValues
	{
		public const int UnknownSymbol = 1;
		public const int Exchange = 2;
		public const int QuoteRequestExceedsLimit = 3;
		public const int TooLateToEnter = 4;
		public const int UnknownQuote = 5;
		public const int DuplicateQuote = 6;
		public const int InvalidBid = 7;
		public const int InvalidPrice = 8;
		public const int NotAuthorizedToQuoteSecurity = 9;
		public const int PriceExceedsCurrentPriceBand = 10;
		public const int QuoteLocked = 11;
		public const int Other = 99;
		public const int InvalidOrUnknownSecurityIssuer = 12;
		public const int InvalidOrUnknownIssuerOfUnderlyingSecurity = 13;
	}
}
