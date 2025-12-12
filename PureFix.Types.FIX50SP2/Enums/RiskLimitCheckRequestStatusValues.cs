using System;

namespace PureFix.Types.FIX50SP2
{
	public static class RiskLimitCheckRequestStatusValues
	{
		public const int Approved = 0;
		public const int PartiallyApproved = 1;
		public const int Rejected = 2;
		public const int ApprovalPending = 3;
		public const int Cancelled = 4;
	}
}
