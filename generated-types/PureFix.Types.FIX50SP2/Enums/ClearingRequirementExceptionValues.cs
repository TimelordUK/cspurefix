using System;

namespace PureFix.Types.FIX50SP2
{
	public static class ClearingRequirementExceptionValues
	{
		public const int NoException = 0;
		public const int Exception = 1;
		public const int EndUserException = 2;
		public const int InterAffiliateException = 3;
		public const int TreasuryAffiliateException = 4;
		public const int CooperativeException = 5;
	}
}
