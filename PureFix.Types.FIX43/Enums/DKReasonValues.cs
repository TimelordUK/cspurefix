using System;

namespace PureFix.Types.FIX43
{
	public static class DKReasonValues
	{
		public const string WrongSide = "B";
		public const string QuantityExceedsOrder = "C";
		public const string NoMatchingOrder = "D";
		public const string PriceExceedsLimit = "E";
		public const string Other = "Z";
		public const string UnknownSymbol = "A";
	}
}
