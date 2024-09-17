namespace PureFix.Types.FIX43.QuickFix.Types
{
	public static class CxlRejReasonValues
	{
		public const int UnknownOrder = 1;
		public const int TooLateToCancel = 0;
		public const int DuplicateClordidReceived = 6;
		public const int OrigordmodtimeDidNotMatchLastTransacttimeOfOrder = 5;
		public const int UnableToProcessOrderMassCancelRequest = 4;
		public const int OrderAlreadyInPendingCancelOrPendingReplaceStatus = 3;
		public const int Broker = 2;
	}
}
