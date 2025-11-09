using System;

namespace PureFix.Types.FIX50SP2
{
	public static class PartyDetailRequestResultValues
	{
		public const int Successful = 0;
		public const int InvalidParty = 1;
		public const int InvalidRelatedParty = 2;
		public const int InvalidPartyStatus = 3;
		public const int NotAuthorized = 98;
		public const int Other = 99;
	}
}
