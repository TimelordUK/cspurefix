using System;

namespace PureFix.Types.FIX44
{
	public static class BookingUnitValues
	{
		public const string EachPartialExecutionIsABookableUnit = "0";
		public const string AggregatePartialExecutionsOnThisOrder = "1";
		public const string AggregateExecutionsForThisSymbol = "2";
	}
}
