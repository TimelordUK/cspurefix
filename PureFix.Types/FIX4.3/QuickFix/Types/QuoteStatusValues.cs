namespace PureFix.Types.FIX43.QuickFix.Types
{
	public static class QuoteStatusValues
	{
		public const int RemovedFromMarket = 6;
		public const int CanceledForSymbol = 1;
		public const int Pending = 10;
		public const int QuoteNotFound = 9;
		public const int Query = 8;
		public const int Expired = 7;
		public const int Rejected = 5;
		public const int CanceledAll = 4;
		public const int CanceledForUnderlying = 3;
		public const int CanceledForSecurityType = 2;
		public const int Accepted = 0;
	}
}
