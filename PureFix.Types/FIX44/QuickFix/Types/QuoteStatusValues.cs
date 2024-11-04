namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class QuoteStatusValues
	{
		public const int Accepted = 0;
		public const int CancelForSymbol = 1;
		public const int CanceledForSecurityType = 2;
		public const int CanceledForUnderlying = 3;
		public const int CanceledAll = 4;
		public const int Rejected = 5;
		public const int RemovedFromMarket = 6;
		public const int Expired = 7;
		public const int Query = 8;
		public const int QuoteNotFound = 9;
		public const int Pending = 10;
		public const int Pass = 11;
		public const int LockedMarketWarning = 12;
		public const int CrossMarketWarning = 13;
		public const int CanceledDueToLockMarket = 14;
		public const int CanceledDueToCrossMarket = 15;
	}
}
