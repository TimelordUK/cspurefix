using System;

namespace PureFix.Types.FIX50SP2
{
	public static class AllocIntermedReqTypeValues
	{
		public const int PendingAccept = 1;
		public const int PendingRelease = 2;
		public const int PendingReversal = 3;
		public const int Accept = 4;
		public const int BlockLevelReject = 5;
		public const int AccountLevelReject = 6;
	}
}
