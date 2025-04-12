namespace PureFix.Types.FIX50SP2.FixRepo.Types
{
	public static class TradeRequestTypeValues
	{
		public const int AllTrades = 0;
		public const int MatchedTradesMatchingCriteria = 1;
		public const int UnmatchedTradesThatMatchCriteria = 2;
		public const int UnreportedTradesThatMatchCriteria = 3;
		public const int AdvisoriesThatMatchCriteria = 4;
	}
}
