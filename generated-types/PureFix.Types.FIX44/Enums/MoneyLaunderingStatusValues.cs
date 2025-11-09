using System;

namespace PureFix.Types.FIX44
{
	public static class MoneyLaunderingStatusValues
	{
		public const string Passed = "Y";
		public const string NotChecked = "N";
		public const string ExemptBelowLimit = "1";
		public const string ExemptMoneyType = "2";
		public const string ExemptAuthorised = "3";
	}
}
