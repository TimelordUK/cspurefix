using System;

namespace PureFix.Types.FIX42
{
	public static class OrdRejReasonValues
	{
		public const int BrokerCredit = 0;
		public const int UnknownSymbol = 1;
		public const int ExchangeClosed = 2;
		public const int OrderExceedsLimit = 3;
		public const int TooLateToEnter = 4;
		public const int UnknownOrder = 5;
		public const int DuplicateOrder = 6;
		public const int DuplicateOfAVerballyCommunicatedOrder = 7;
		public const int StaleOrder = 8;
	}
}
