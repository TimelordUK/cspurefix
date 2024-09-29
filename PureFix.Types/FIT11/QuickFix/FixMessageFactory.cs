using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIXT11.QuickFix.Types;

namespace PureFix.Types.FIXT11.QuickFix.Types
{
	public class FixMessageFactory : IFixMessageFactory
	{
		public IFixMessage? ToFixMessage(IMessageView view)
		{
			var msgType = view.GetString((int)MsgTag.MsgType);
			switch (msgType)
			{
				case "0":
				{
					var o = new Heartbeat();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "1":
				{
					var o = new TestRequest();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "2":
				{
					var o = new ResendRequest();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "3":
				{
					var o = new Reject();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "4":
				{
					var o = new SequenceReset();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "5":
				{
					var o = new Logout();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "A":
				{
					var o = new Logon();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "n":
				{
					var o = new XMLnonFIX();
					((IFixParser)o).Parse(view);
					return o;
				}
			}
			return null;
		}
	}
}
