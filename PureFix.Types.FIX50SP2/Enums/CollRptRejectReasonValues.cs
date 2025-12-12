using System;

namespace PureFix.Types.FIX50SP2
{
	public static class CollRptRejectReasonValues
	{
		public const int UnknownTrade = 0;
		public const int UnknownInstrument = 1;
		public const int UnknownCounterparty = 2;
		public const int UnknownPosition = 3;
		public const int UnacceptableCollateral = 4;
		public const int Other = 99;
	}
}
