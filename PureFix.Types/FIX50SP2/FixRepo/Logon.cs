using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.FixRepo.Types;

namespace PureFix.Types.FIX50SP2.FixRepo
{
	[MessageType("Logon", FixVersion.FIX50SP2)]
	public sealed partial class Logon : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeaderComponent? StandardHeader {get; set;}
		
		[TagDetails(Tag = 98, Type = TagType.Int, Offset = 1, Required = true)]
		public int? EncryptMethod {get; set;}
		
		[TagDetails(Tag = 108, Type = TagType.Int, Offset = 2, Required = true)]
		public int? HeartBtInt {get; set;}
		
		[TagDetails(Tag = 95, Type = TagType.Length, Offset = 3, Required = false, LinksToTag = 96)]
		public int? RawDataLength {get; set;}
		
		[TagDetails(Tag = 96, Type = TagType.RawData, Offset = 4, Required = false, LinksToTag = 95)]
		public byte[]? RawData {get; set;}
		
		[TagDetails(Tag = 141, Type = TagType.Boolean, Offset = 5, Required = false)]
		public bool? ResetSeqNumFlag {get; set;}
		
		[TagDetails(Tag = 789, Type = TagType.Int, Offset = 6, Required = false)]
		public int? NextExpectedMsgSeqNum {get; set;}
		
		[TagDetails(Tag = 383, Type = TagType.Length, Offset = 7, Required = false)]
		public int? MaxMessageSize {get; set;}
		
		[TagDetails(Tag = 464, Type = TagType.Boolean, Offset = 8, Required = false)]
		public bool? TestMessageIndicator {get; set;}
		
		[TagDetails(Tag = 553, Type = TagType.String, Offset = 9, Required = false)]
		public string? Username {get; set;}
		
		[TagDetails(Tag = 554, Type = TagType.String, Offset = 10, Required = false)]
		public string? Password {get; set;}
		
		[TagDetails(Tag = 925, Type = TagType.String, Offset = 11, Required = false)]
		public string? NewPassword {get; set;}
		
		[TagDetails(Tag = 1400, Type = TagType.Int, Offset = 12, Required = false)]
		public int? EncryptedPasswordMethod {get; set;}
		
		[TagDetails(Tag = 1401, Type = TagType.Length, Offset = 13, Required = false, LinksToTag = 1402)]
		public int? EncryptedPasswordLen {get; set;}
		
		[TagDetails(Tag = 1402, Type = TagType.RawData, Offset = 14, Required = false, LinksToTag = 1401)]
		public byte[]? EncryptedPassword {get; set;}
		
		[TagDetails(Tag = 1403, Type = TagType.Length, Offset = 15, Required = false, LinksToTag = 1404)]
		public int? EncryptedNewPasswordLen {get; set;}
		
		[TagDetails(Tag = 1404, Type = TagType.RawData, Offset = 16, Required = false, LinksToTag = 1403)]
		public byte[]? EncryptedNewPassword {get; set;}
		
		[TagDetails(Tag = 1409, Type = TagType.Int, Offset = 17, Required = false)]
		public int? SessionStatus {get; set;}
		
		[TagDetails(Tag = 1137, Type = TagType.String, Offset = 18, Required = true)]
		public string? DefaultApplVerID {get; set;}
		
		[TagDetails(Tag = 1407, Type = TagType.Int, Offset = 19, Required = false)]
		public int? DefaultApplExtID {get; set;}
		
		[TagDetails(Tag = 1408, Type = TagType.String, Offset = 20, Required = false)]
		public string? DefaultCstmApplVerID {get; set;}
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 21, Required = false)]
		public string? Text {get; set;}
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 22, Required = false, LinksToTag = 355)]
		public int? EncodedTextLen {get; set;}
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 23, Required = false, LinksToTag = 354)]
		public byte[]? EncodedText {get; set;}
		
		[Component(Offset = 24, Required = true)]
		public StandardTrailerComponent? StandardTrailer {get; set;}
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& EncryptMethod is not null
				&& HeartBtInt is not null
				&& DefaultApplVerID is not null
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (EncryptMethod is not null) writer.WriteWholeNumber(98, EncryptMethod.Value);
			if (HeartBtInt is not null) writer.WriteWholeNumber(108, HeartBtInt.Value);
			if (RawData is not null)
			{
				writer.WriteWholeNumber(95, RawData.Length);
				writer.WriteBuffer(96, RawData);
			}
			if (ResetSeqNumFlag is not null) writer.WriteBoolean(141, ResetSeqNumFlag.Value);
			if (NextExpectedMsgSeqNum is not null) writer.WriteWholeNumber(789, NextExpectedMsgSeqNum.Value);
			if (MaxMessageSize is not null) writer.WriteWholeNumber(383, MaxMessageSize.Value);
			if (TestMessageIndicator is not null) writer.WriteBoolean(464, TestMessageIndicator.Value);
			if (Username is not null) writer.WriteString(553, Username);
			if (Password is not null) writer.WriteString(554, Password);
			if (NewPassword is not null) writer.WriteString(925, NewPassword);
			if (EncryptedPasswordMethod is not null) writer.WriteWholeNumber(1400, EncryptedPasswordMethod.Value);
			if (EncryptedPassword is not null)
			{
				writer.WriteWholeNumber(1401, EncryptedPassword.Length);
				writer.WriteBuffer(1402, EncryptedPassword);
			}
			if (EncryptedNewPassword is not null)
			{
				writer.WriteWholeNumber(1403, EncryptedNewPassword.Length);
				writer.WriteBuffer(1404, EncryptedNewPassword);
			}
			if (SessionStatus is not null) writer.WriteWholeNumber(1409, SessionStatus.Value);
			if (DefaultApplVerID is not null) writer.WriteString(1137, DefaultApplVerID);
			if (DefaultApplExtID is not null) writer.WriteWholeNumber(1407, DefaultApplExtID.Value);
			if (DefaultCstmApplVerID is not null) writer.WriteString(1408, DefaultCstmApplVerID);
			if (Text is not null) writer.WriteString(58, Text);
			if (EncodedText is not null)
			{
				writer.WriteWholeNumber(354, EncodedText.Length);
				writer.WriteBuffer(355, EncodedText);
			}
			if (StandardTrailer is not null) ((IFixEncoder)StandardTrailer).Encode(writer);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("StandardHeader") is IMessageView viewStandardHeader)
			{
				StandardHeader = new();
				((IFixParser)StandardHeader).Parse(viewStandardHeader);
			}
			EncryptMethod = view.GetInt32(98);
			HeartBtInt = view.GetInt32(108);
			RawDataLength = view.GetInt32(95);
			RawData = view.GetByteArray(96);
			ResetSeqNumFlag = view.GetBool(141);
			NextExpectedMsgSeqNum = view.GetInt32(789);
			MaxMessageSize = view.GetInt32(383);
			TestMessageIndicator = view.GetBool(464);
			Username = view.GetString(553);
			Password = view.GetString(554);
			NewPassword = view.GetString(925);
			EncryptedPasswordMethod = view.GetInt32(1400);
			EncryptedPasswordLen = view.GetInt32(1401);
			EncryptedPassword = view.GetByteArray(1402);
			EncryptedNewPasswordLen = view.GetInt32(1403);
			EncryptedNewPassword = view.GetByteArray(1404);
			SessionStatus = view.GetInt32(1409);
			DefaultApplVerID = view.GetString(1137);
			DefaultApplExtID = view.GetInt32(1407);
			DefaultCstmApplVerID = view.GetString(1408);
			Text = view.GetString(58);
			EncodedTextLen = view.GetInt32(354);
			EncodedText = view.GetByteArray(355);
			if (view.GetView("StandardTrailer") is IMessageView viewStandardTrailer)
			{
				StandardTrailer = new();
				((IFixParser)StandardTrailer).Parse(viewStandardTrailer);
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "StandardHeader":
					value = StandardHeader;
					break;
				case "EncryptMethod":
					value = EncryptMethod;
					break;
				case "HeartBtInt":
					value = HeartBtInt;
					break;
				case "RawDataLength":
					value = RawDataLength;
					break;
				case "RawData":
					value = RawData;
					break;
				case "ResetSeqNumFlag":
					value = ResetSeqNumFlag;
					break;
				case "NextExpectedMsgSeqNum":
					value = NextExpectedMsgSeqNum;
					break;
				case "MaxMessageSize":
					value = MaxMessageSize;
					break;
				case "TestMessageIndicator":
					value = TestMessageIndicator;
					break;
				case "Username":
					value = Username;
					break;
				case "Password":
					value = Password;
					break;
				case "NewPassword":
					value = NewPassword;
					break;
				case "EncryptedPasswordMethod":
					value = EncryptedPasswordMethod;
					break;
				case "EncryptedPasswordLen":
					value = EncryptedPasswordLen;
					break;
				case "EncryptedPassword":
					value = EncryptedPassword;
					break;
				case "EncryptedNewPasswordLen":
					value = EncryptedNewPasswordLen;
					break;
				case "EncryptedNewPassword":
					value = EncryptedNewPassword;
					break;
				case "SessionStatus":
					value = SessionStatus;
					break;
				case "DefaultApplVerID":
					value = DefaultApplVerID;
					break;
				case "DefaultApplExtID":
					value = DefaultApplExtID;
					break;
				case "DefaultCstmApplVerID":
					value = DefaultCstmApplVerID;
					break;
				case "Text":
					value = Text;
					break;
				case "EncodedTextLen":
					value = EncodedTextLen;
					break;
				case "EncodedText":
					value = EncodedText;
					break;
				case "StandardTrailer":
					value = StandardTrailer;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			((IFixReset?)StandardHeader)?.Reset();
			EncryptMethod = null;
			HeartBtInt = null;
			RawDataLength = null;
			RawData = null;
			ResetSeqNumFlag = null;
			NextExpectedMsgSeqNum = null;
			MaxMessageSize = null;
			TestMessageIndicator = null;
			Username = null;
			Password = null;
			NewPassword = null;
			EncryptedPasswordMethod = null;
			EncryptedPasswordLen = null;
			EncryptedPassword = null;
			EncryptedNewPasswordLen = null;
			EncryptedNewPassword = null;
			SessionStatus = null;
			DefaultApplVerID = null;
			DefaultApplExtID = null;
			DefaultCstmApplVerID = null;
			Text = null;
			EncodedTextLen = null;
			EncodedText = null;
			((IFixReset?)StandardTrailer)?.Reset();
		}
	}
}
