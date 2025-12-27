using System;

namespace PureFix.Types.FIX43
{
	public static class BusinessRejectReasonValues
	{
		public const int UnsupportedMessageType = 3;
		public const int DeliverToFirmNotAvailableAtThisTime = 7;
		public const int ApplicationNotAvailable = 4;
		public const int NotAuthorized = 6;
		public const int Other = 0;
		public const int ConditionallyRequiredFieldMissing = 5;
		public const int UnknownId = 1;
		public const int UnknownSecurity = 2;
	}
}
