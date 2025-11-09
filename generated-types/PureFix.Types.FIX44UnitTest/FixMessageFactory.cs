using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types;
using PureFix.Types.FIX44UnitTest.Components;

namespace PureFix.Types.FIX44UnitTest
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
				case "J":
				{
					var o = new AllocationInstruction();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "8":
				{
					var o = new ExecutionReport();
					((IFixParser)o).Parse(view);
					return o;
				}
				case "AE":
				{
					var o = new TradeCaptureReport();
					((IFixParser)o).Parse(view);
					return o;
				}
			}
			return null;
		}
	}
}
