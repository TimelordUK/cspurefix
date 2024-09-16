using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public static class LogonNoMsgTypesExt
	{
		public static void Parse(this LogonNoMsgTypes instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.RefMsgType = view.GetString(372);
			instance.MsgDirection = view.GetString(385);
		}
	}
}
