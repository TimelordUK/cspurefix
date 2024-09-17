namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class BookingUnitValues
	{
		public const string EachPartialExecutionIsABookableUnit = "0";
		public const string AggregatePartialExecutionsOnThisOrderAndBookOneTradePerOrder = "1";
		public const string AggregateExecutionsForThisSymbolSideAndSettlementDate = "2";
	}
}
