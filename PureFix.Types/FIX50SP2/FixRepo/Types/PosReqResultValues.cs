namespace PureFix.Types.FIX50SP2.FixRepo.Types
{
	public static class PosReqResultValues
	{
		public const int ValidRequest = 0;
		public const int InvalidOrUnsupportedRequest = 1;
		public const int NoPositionsFoundThatMatchCriteria = 2;
		public const int NotAuthorizedToRequestPositions = 3;
		public const int RequestForPositionNotSupported = 4;
		public const int Other = 99;
	}
}
