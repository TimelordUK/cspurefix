namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class CxlRejReasonValues
	{
		public const int TooLateToCancel = 0;
		public const int UnknownOrder = 1;
		public const int Broker = 2;
		public const int OrderAlreadyInPendingCancelOrPendingReplaceStatus = 3;
		public const int UnableToProcessOrderMassCancelRequest = 4;
		public const int Origordmodtime = 5;
		public const int DuplicateClordid = 6;
		public const int Other = 99;
	}
}
