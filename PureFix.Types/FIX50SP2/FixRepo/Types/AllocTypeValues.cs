namespace PureFix.Types.FIX50SP2.FixRepo.Types
{
	public static class AllocTypeValues
	{
		public const int Calculated = 1;
		public const int Preliminary = 2;
		public const int SellsideCalculatedUsingPreliminary = 3;
		public const int SellsideCalculatedWithoutPreliminary = 4;
		public const int ReadyToBook = 5;
		public const int BuysideReadyToBook = 6;
		public const int WarehouseInstruction = 7;
		public const int RequestToIntermediary = 8;
		public const int Accept = 9;
		public const int Reject = 10;
		public const int AcceptPending = 11;
		public const int IncompleteGroup = 12;
		public const int CompleteGroup = 13;
		public const int ReversalPending = 14;
	}
}
