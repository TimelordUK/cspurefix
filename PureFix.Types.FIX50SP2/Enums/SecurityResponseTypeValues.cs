using System;

namespace PureFix.Types.FIX50SP2
{
	public static class SecurityResponseTypeValues
	{
		public const int AcceptAsIs = 1;
		public const int AcceptWithRevisions = 2;
		public const int ListOfSecurityTypesReturnedPerRequest = 3;
		public const int ListOfSecuritiesReturnedPerRequest = 4;
		public const int RejectSecurityProposal = 5;
		public const int CannotMatchSelectionCriteria = 6;
	}
}
