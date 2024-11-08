namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public static class QuoteCancelTypeValues
	{
		public const int CancelForOneOrMoreSecurities = 1;
		public const int CancelForSecurityType = 2;
		public const int CancelForUnderlyingSecurity = 3;
		public const int CancelAllQuotes = 4;
		public const int CancelSpecifiedSingleQuote = 5;
		public const int CancelByTypeOfQuote = 6;
		public const int CancelForSecurityIssuer = 7;
		public const int CancelForIssuerOfUnderlyingSecurity = 8;
	}
}
