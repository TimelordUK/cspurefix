namespace PureFix.Types.FIX43.QuickFix.Types
{
	public static class SecurityResponseTypeValues
	{
		public const int RejectSecurityProposal = 5;
		public const int AcceptSecurityProposalAsIs = 1;
		public const int CanNotMatchSelectionCriteria = 6;
		public const int AcceptSecurityProposalWithRevisionsAsIndicatedInTheMessage = 2;
		public const int ListOfSecuritiesReturnedPerRequest = 4;
		public const int ListOfSecurityTypesReturnedPerRequest = 3;
	}
}
