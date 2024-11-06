namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public static class QuoteEntryStatusValues
	{
		public const int Accepted = 0;
		public const int Rejected = 5;
		public const int RemovedFromMarket = 6;
		public const int Expired = 7;
		public const int LockedMarketWarning = 12;
		public const int CrossMarketWarning = 13;
		public const int CanceledDueToLockMarket = 14;
		public const int CanceledDueToCrossMarket = 15;
		public const int Active = 16;
	}
}
