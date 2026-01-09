using System;

namespace PureFix.Types.FIX42
{
	public static class CxlRejReasonValues
	{
		public const int TooLateToCancel = 0;
		public const int UnknownOrder = 1;
		public const int BrokerCredit = 2;
		public const int OrderAlreadyInPendingStatus = 3;
	}
}
