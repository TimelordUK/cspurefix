using System;

namespace PureFix.Types.FIX42
{
	public static class QuoteAckStatusValues
	{
		public const int Accepted = 0;
		public const int CancelForSymbol = 1;
		public const int CanceledForSecurityType = 2;
		public const int CanceledForUnderlying = 3;
		public const int CanceledAll = 4;
		public const int Rejected = 5;
	}
}
