namespace PureFix.Types.FIX50SP2.FixRepo.Types
{
	public static class SessionStatusValues
	{
		public const int SessionActive = 0;
		public const int SessionPasswordChanged = 1;
		public const int SessionPasswordDueToExpire = 2;
		public const int NewSessionPasswordDoesNotComplyWithPolicy = 3;
		public const int SessionLogoutComplete = 4;
		public const int InvalidUsernameOrPassword = 5;
		public const int AccountLocked = 6;
		public const int LogonsAreNotAllowedAtThisTime = 7;
		public const int PasswordExpired = 8;
	}
}
