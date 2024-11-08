namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class QuoteResponseLevelValues
	{
		public const int NoAcknowledgement = 0;
		public const int AcknowledgeOnlyNegativeOrErroneousQuotes = 1;
		public const int AcknowledgeEachQuoteMessage = 2;
	}
}
