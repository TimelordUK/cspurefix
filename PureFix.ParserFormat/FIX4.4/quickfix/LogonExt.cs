using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("A", FixVersion.FIX44)]
	public static class LogonExt
	{
		public static void Parse(this Logon instance, MsgView? view)
		{
			if (view is null) return;
			
			if (view.GetView("StandardHeader") is MsgView groupViewStandardHeader)
			{
				instance.StandardHeader = new StandardHeader();
				instance.StandardHeader!.Parse(groupViewStandardHeader);
			}
			instance.StandardHeader = new StandardHeader();
			instance.StandardHeader?.Parse(view.GetView("StandardHeader"));
			instance.EncryptMethod = view.GetInt32(98);
			instance.HeartBtInt = view.GetInt32(108);
			instance.RawDataLength = view.GetInt32(95);
			instance.RawData = view.GetByteArray(96);
			instance.ResetSeqNumFlag = view.GetBool(141);
			instance.NextExpectedMsgSeqNum = view.GetInt32(789);
			instance.MaxMessageSize = view.GetInt32(383);
			var groupView = view.GetView("NoMsgTypes");
			if (groupView is null) return;
			
			var count = groupView.GroupCount();
			instance.NoMsgTypes = new LogonNoMsgTypes[count];
			for (var i = 0; i < count; ++i)
			{
				instance.NoMsgTypes[i] = new();
				instance.NoMsgTypes[i].Parse(groupView[i]);
			}
			instance.TestMessageIndicator = view.GetBool(464);
			instance.Username = view.GetString(553);
			instance.Password = view.GetString(554);
			if (view.GetView("StandardTrailer") is MsgView groupViewStandardTrailer)
			{
				instance.StandardTrailer = new StandardTrailer();
				instance.StandardTrailer!.Parse(groupViewStandardTrailer);
			}
			instance.StandardTrailer = new StandardTrailer();
			instance.StandardTrailer?.Parse(view.GetView("StandardTrailer"));
		}
	}
}
