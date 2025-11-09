using System;

namespace PureFix.Types.FIX44
{
	public static class TrdTypeValues
	{
		public const int RegularTrade = 0;
		public const int BlockTrade = 1;
		public const int Efp = 2;
		public const int Transfer = 3;
		public const int LateTrade = 4;
		public const int TTrade = 5;
		public const int WeightedAveragePriceTrade = 6;
		public const int BunchedTrade = 7;
		public const int LateBunchedTrade = 8;
		public const int PriorReferencePriceTrade = 9;
		public const int AfterHoursTrade = 10;
	}
}
