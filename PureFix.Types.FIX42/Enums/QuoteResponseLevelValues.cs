using System;

namespace PureFix.Types.FIX42
{
	public static class QuoteResponseLevelValues
	{
		public const int NoAcknowledgement = 0;
		public const int AcknowledgeOnlyNegativeOrErroneousQuotes = 1;
		public const int AcknowledgeEachQuoteMessage = 2;
	}
}
