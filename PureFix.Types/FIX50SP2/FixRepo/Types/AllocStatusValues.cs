namespace PureFix.Types.FIX50SP2.FixRepo.Types
{
	public static class AllocStatusValues
	{
		public const int Accepted = 0;
		public const int BlockLevelReject = 1;
		public const int AccountLevelReject = 2;
		public const int Received = 3;
		public const int Incomplete = 4;
		public const int RejectedByIntermediary = 5;
		public const int AllocationPending = 6;
		public const int Reversed = 7;
	}
}
