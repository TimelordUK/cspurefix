using System;

namespace PureFix.Types.FIX44
{
	public static class MassCancelRejectReasonValues
	{
		public const string MassCancelNotSupported = "0";
		public const string InvalidOrUnknownSecurity = "1";
		public const string InvalidOrUnkownUnderlyingSecurity = "2";
		public const string InvalidOrUnknownProduct = "3";
		public const string InvalidOrUnknownCfiCode = "4";
		public const string InvalidOrUnknownSecurityType = "5";
		public const string InvalidOrUnknownTradingSession = "6";
		public const string Other = "99";
	}
}
