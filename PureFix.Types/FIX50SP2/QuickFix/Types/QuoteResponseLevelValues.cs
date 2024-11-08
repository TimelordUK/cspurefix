namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public static class QuoteResponseLevelValues
	{
		public const int NoAcknowledgement = 0;
		public const int AcknowledgeOnlyNegativeOrErroneousQuotes = 1;
		public const int AcknowledgeEachQuoteMessage = 2;
		public const int SummaryAcknowledgement = 3;
	}
}
