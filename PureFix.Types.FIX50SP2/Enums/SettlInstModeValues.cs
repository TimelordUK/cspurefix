using System;

namespace PureFix.Types.FIX50SP2
{
	public static class SettlInstModeValues
	{
		public const string Default = "0";
		public const string StandingInstructionsProvided = "1";
		public const string SpecificAllocationAccountOverriding = "2";
		public const string SpecificAllocationAccountStanding = "3";
		public const string SpecificOrderForASingleAccount = "4";
		public const string RequestReject = "5";
	}
}
