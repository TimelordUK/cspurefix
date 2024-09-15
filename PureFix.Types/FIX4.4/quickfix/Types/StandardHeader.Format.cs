using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class StandardHeader
	{
		void IFixEncoder.Encode(ElasticBuffer storage, Tags tags, byte delimiter)
		{
			
			if (BeginString != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(8);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)BeginString);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 8);
			}
			if (BodyLength != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(9);
				storage.WriteChar((byte)'=');
				storage.WriteWholeNumber((int)BodyLength);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 9);
			}
			if (MsgType != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(35);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)MsgType);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 35);
			}
			if (SenderCompID != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(49);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)SenderCompID);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 49);
			}
			if (TargetCompID != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(56);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)TargetCompID);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 56);
			}
			if (OnBehalfOfCompID != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(115);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)OnBehalfOfCompID);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 115);
			}
			if (DeliverToCompID != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(128);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)DeliverToCompID);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 128);
			}
			if (SecureDataLen != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(90);
				storage.WriteChar((byte)'=');
				storage.WriteWholeNumber((int)SecureDataLen);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 90);
			}
			if (SecureData != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(91);
				storage.WriteChar((byte)'=');
				storage.WriteBuffer((byte[])SecureData);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 91);
			}
			if (MsgSeqNum != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(34);
				storage.WriteChar((byte)'=');
				storage.WriteWholeNumber((int)MsgSeqNum);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 34);
			}
			if (SenderSubID != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(50);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)SenderSubID);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 50);
			}
			if (SenderLocationID != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(142);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)SenderLocationID);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 142);
			}
			if (TargetSubID != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(57);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)TargetSubID);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 57);
			}
			if (TargetLocationID != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(143);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)TargetLocationID);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 143);
			}
			if (OnBehalfOfSubID != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(116);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)OnBehalfOfSubID);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 116);
			}
			if (OnBehalfOfLocationID != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(144);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)OnBehalfOfLocationID);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 144);
			}
			if (DeliverToSubID != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(129);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)DeliverToSubID);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 129);
			}
			if (DeliverToLocationID != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(145);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)DeliverToLocationID);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 145);
			}
			if (PossDupFlag != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(43);
				storage.WriteChar((byte)'=');
				storage.WriteBoolean((bool)PossDupFlag);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 43);
			}
			if (PossResend != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(97);
				storage.WriteChar((byte)'=');
				storage.WriteBoolean((bool)PossResend);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 97);
			}
			if (SendingTime != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(52);
				storage.WriteChar((byte)'=');
				storage.WriteUtcTimeStamp((DateTime)SendingTime);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 52);
			}
			if (OrigSendingTime != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(122);
				storage.WriteChar((byte)'=');
				storage.WriteUtcTimeStamp((DateTime)OrigSendingTime);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 122);
			}
			if (XmlDataLen != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(212);
				storage.WriteChar((byte)'=');
				storage.WriteWholeNumber((int)XmlDataLen);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 212);
			}
			if (XmlData != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(213);
				storage.WriteChar((byte)'=');
				storage.WriteBuffer((byte[])XmlData);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 213);
			}
			if (MessageEncoding != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(347);
				storage.WriteChar((byte)'=');
				storage.WriteString((string)MessageEncoding);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 347);
			}
			if (LastMsgSeqNumProcessed != null)
			{
				var at = storage.Pos;
				storage.WriteWholeNumber(369);
				storage.WriteChar((byte)'=');
				storage.WriteWholeNumber((int)LastMsgSeqNumProcessed);
				storage.WriteChar(delimiter);
				tags.Store(at, storage.Pos - at, 369);
			}
		}
	}
}
