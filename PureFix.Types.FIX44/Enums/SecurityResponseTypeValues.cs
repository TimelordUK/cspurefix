using System;

namespace PureFix.Types.FIX44
{
	public static class SecurityResponseTypeValues
	{
		public const int AcceptAsIs = 1;
		public const int AcceptWithRevisions = 2;
		public const int RejectSecurityProposal = 5;
		public const int CannotMatchSelectionCriteria = 6;
	}
}
