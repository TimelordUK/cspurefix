namespace PureFix.Types.FIX42.QuickFix.Types
{
	public static class SecurityResponseTypeValues
	{
		public const int AcceptSecurityProposalAsIs = 1;
		public const int AcceptSecurityProposalWithRevisionsAsIndicatedInTheMessage = 2;
		public const int ListOfSecurityTypesReturnedPerRequest = 3;
		public const int ListOfSecuritiesReturnedPerRequest = 4;
		public const int RejectSecurityProposal = 5;
		public const int CanNotMatchSelectionCriteria = 6;
	}
}
