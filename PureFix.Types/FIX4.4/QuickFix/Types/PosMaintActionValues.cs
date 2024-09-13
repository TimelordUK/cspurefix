namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class PosMaintActionValues
	{
		public const int NewUsedToIncrementTheOverallTransactionQuantity = 1;
		public const int ReplaceUsedToOverrideTheOverallTransactionQuantityOrSpecificAddMessagesBasedOnTheReferenceId = 2;
		public const int CancelUsedToRemoveTheOverallTransactionOrSpecificAddMessagesBasedOnReferenceId = 3;
	}
}
