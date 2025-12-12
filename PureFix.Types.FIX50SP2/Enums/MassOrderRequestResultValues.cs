using System;

namespace PureFix.Types.FIX50SP2
{
	public static class MassOrderRequestResultValues
	{
		public const int Successful = 0;
		public const int ResponseLevelNotSupported = 1;
		public const int InvalidMarket = 2;
		public const int InvalidMarketSegment = 3;
		public const int Other = 99;
	}
}
