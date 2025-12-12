using System;

namespace PureFix.Types.FIX50SP2
{
	public static class OrderResponseLevelValues
	{
		public const int NoAck = 0;
		public const int MinimumAck = 1;
		public const int AckEach = 2;
		public const int SummaryAck = 3;
	}
}
