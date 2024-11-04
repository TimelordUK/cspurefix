namespace PureFix.Types.FIX42.QuickFix.Types
{
	public static class SessionRejectReasonValues
	{
		public const int InvalidTagNumber = 0;
		public const int RequiredTagMissing = 1;
		public const int SendingTimeAccuracyProblem = 10;
		public const int InvalidMsgType = 11;
		public const int TagNotDefinedForThisMessageType = 2;
		public const int UndefinedTag = 3;
		public const int TagSpecifiedWithoutAValue = 4;
		public const int ValueIsIncorrect = 5;
		public const int IncorrectDataFormatForValue = 6;
		public const int DecryptionProblem = 7;
		public const int SignatureProblem = 8;
		public const int CompIdProblem = 9;
	}
}
