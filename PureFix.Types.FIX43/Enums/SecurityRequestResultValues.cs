using System;

namespace PureFix.Types.FIX43
{
	public static class SecurityRequestResultValues
	{
		public const int InstrumentDataTemporarilyUnavailable = 4;
		public const int ValidRequest = 0;
		public const int InvalidOrUnsupportedRequest = 1;
		public const int RequestForInstrumentDataNotSupported = 5;
		public const int NotAuthorizedToRetrieveInstrumentData = 3;
		public const int NoInstrumentsFound = 2;
	}
}
