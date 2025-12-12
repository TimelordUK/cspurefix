using System;

namespace PureFix.Types.FIX44
{
	public static class InstrAttribTypeValues
	{
		public const int Flat = 1;
		public const int ZeroCoupon = 2;
		public const int InterestBearing = 3;
		public const int NoPeriodicPayments = 4;
		public const int VariableRate = 5;
		public const int LessFeeForPut = 6;
		public const int SteppedCoupon = 7;
		public const int CouponPeriod = 8;
		public const int When = 9;
		public const int OriginalIssueDiscount = 10;
		public const int Callable = 11;
		public const int EscrowedToMaturity = 12;
		public const int EscrowedToRedemptionDate = 13;
		public const int PreRefunded = 14;
		public const int InDefault = 15;
		public const int Unrated = 16;
		public const int Taxable = 17;
		public const int Indexed = 18;
		public const int SubjectToAlternativeMinimumTax = 19;
		public const int OriginalIssueDiscountPrice = 20;
		public const int CallableBelowMaturityValue = 21;
		public const int CallableWithoutNotice = 22;
		public const int Text = 99;
	}
}
