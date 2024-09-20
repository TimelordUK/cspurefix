using System.Diagnostics.CodeAnalysis;

namespace PureFix.Types.FIXT11.QuickFix.Types
{
	public class FixMessageFactory : IFixMessageFactory
	{
		public bool TryParse(IMessageView view, [NotNullWhen(true)] out IFixMessage? message)
		{
			IFixMessageFactory self = this;
			var messageType = view.GetString((int)MsgTag.MsgType);
			message = messageType switch
			{
				"0" => self.MakeAndParse<Heartbeat>(view),
				"1" => self.MakeAndParse<TestRequest>(view),
				"2" => self.MakeAndParse<ResendRequest>(view),
				"3" => self.MakeAndParse<Reject>(view),
				"4" => self.MakeAndParse<SequenceReset>(view),
				"5" => self.MakeAndParse<Logout>(view),
				"A" => self.MakeAndParse<Logon>(view),
				"n" => self.MakeAndParse<XMLnonFIX>(view),
				_ => null
			};
			return message is not null;
		}
	}
}
