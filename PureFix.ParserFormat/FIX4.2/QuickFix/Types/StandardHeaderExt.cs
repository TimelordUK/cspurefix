using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX42.QuickFix.Types;
using PureFix.Buffer.Ascii;

namespace PureFix.Types.FIX42.QuickFix.Types
{
	public static class StandardHeaderExt
	{
		public static void Parse(this StandardHeader instance, MsgView? view)
		{
			if (view is null) return;
			
			instance.BeginString = view.GetString(8);
			instance.BodyLength = view.GetInt32(9);
			instance.MsgType = view.GetString(35);
			instance.SenderCompID = view.GetString(49);
			instance.TargetCompID = view.GetString(56);
			instance.OnBehalfOfCompID = view.GetString(115);
			instance.DeliverToCompID = view.GetString(128);
			instance.SecureDataLen = view.GetInt32(90);
			instance.SecureData = view.GetByteArray(91);
			instance.MsgSeqNum = view.GetInt32(34);
			instance.SenderSubID = view.GetString(50);
			instance.SenderLocationID = view.GetString(142);
			instance.TargetSubID = view.GetString(57);
			instance.TargetLocationID = view.GetString(143);
			instance.OnBehalfOfSubID = view.GetString(116);
			instance.OnBehalfOfLocationID = view.GetString(144);
			instance.DeliverToSubID = view.GetString(129);
			instance.DeliverToLocationID = view.GetString(145);
			instance.PossDupFlag = view.GetBool(43);
			instance.PossResend = view.GetBool(97);
			instance.SendingTime = view.GetDateTime(52);
			instance.OrigSendingTime = view.GetDateTime(122);
			instance.XmlDataLen = view.GetInt32(212);
			instance.XmlData = view.GetByteArray(213);
			instance.MessageEncoding = view.GetString(347);
			instance.LastMsgSeqNumProcessed = view.GetInt32(369);
			instance.OnBehalfOfSendingTime = view.GetDateTime(370);
		}
	}
}
