namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class TradeRequestResultValues
	{
		public const int Successful = 0;
		public const int InvalidOrUnknownInstrument = 1;
		public const int InvalidTypeOfTradeRequested = 2;
		public const int InvalidParties = 3;
		public const int InvalidTransportTypeRequested = 4;
		public const int InvalidDestinationRequested = 5;
		public const int TraderequesttypeNotSupported = 8;
		public const int UnauthorizedForTradeCaptureReportRequest = 9;
		public const int Other = 99;
	}
}
