using System;

namespace PureFix.Types.FIX42
{
	public static class QuoteCancelTypeValues
	{
		public const int CancelForOneOrMoreSecurities = 1;
		public const int CancelForSecurityType = 2;
		public const int CancelForUnderlyingSecurity = 3;
		public const int CancelAllQuotes = 4;
	}
}
