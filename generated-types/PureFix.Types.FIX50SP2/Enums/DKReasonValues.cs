using System;

namespace PureFix.Types.FIX50SP2
{
	public static class DKReasonValues
	{
		public const string UnknownSymbol = "A";
		public const string WrongSide = "B";
		public const string QuantityExceedsOrder = "C";
		public const string NoMatchingOrder = "D";
		public const string PriceExceedsLimit = "E";
		public const string CalculationDifference = "F";
		public const string NoMatchingExecutionReport = "G";
		public const string Other = "Z";
	}
}
