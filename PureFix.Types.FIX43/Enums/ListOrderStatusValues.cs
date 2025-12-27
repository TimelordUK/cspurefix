using System;

namespace PureFix.Types.FIX43
{
	public static class ListOrderStatusValues
	{
		public const int Cancelling = 4;
		public const int Executing = 3;
		public const int Reject = 7;
		public const int AllDone = 6;
		public const int Alert = 5;
		public const int ReceivedForExecution = 2;
		public const int InBiddingProcess = 1;
	}
}
