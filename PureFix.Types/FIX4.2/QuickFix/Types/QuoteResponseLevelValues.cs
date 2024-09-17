namespace PureFix.Types.FIX42.QuickFix.Types
{
	public static class QuoteResponseLevelValues
	{
		public const int NoAcknowledgement = 0;
		public const int AcknowledgeOnlyNegativeOrErroneousQuotes = 1;
		public const int AcknowledgeEachQuoteMessages = 2;
	}
}
