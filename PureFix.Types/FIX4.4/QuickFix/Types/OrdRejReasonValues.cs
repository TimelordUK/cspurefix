namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class OrdRejReasonValues
	{
		public const int Broker = 0;
		public const int UnknownSymbol = 1;
		public const int ExchangeClosed = 2;
		public const int OrderExceedsLimit = 3;
		public const int TooLateToEnter = 4;
		public const int UnknownOrder = 5;
		public const int DuplicateOrder = 6;
		public const int DuplicateOfAVerballyCommunicatedOrder = 7;
		public const int StaleOrder = 8;
		public const int TradeAlongRequired = 9;
		public const int InvalidInvestorId = 10;
		public const int UnsupportedOrderCharacteristic12SurveillenceOption = 11;
		public const int IncorrectQuantity = 13;
		public const int IncorrectAllocatedQuantity = 14;
		public const int UnknownAccount = 15;
		public const int Other = 99;
	}
}
