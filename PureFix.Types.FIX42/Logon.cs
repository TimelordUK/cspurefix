using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types;
using PureFix.Types.FIX42.Components;

namespace PureFix.Types.FIX42
{
	[MessageType("A", FixVersion.FIX42)]
	public sealed partial class Logon : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeader? StandardHeader {get; set;}
		
		[TagDetails(Tag = 98, Type = TagType.Int, Offset = 1, Required = true)]
		public int? EncryptMethod {get; set;}
		
		[TagDetails(Tag = 108, Type = TagType.Int, Offset = 2, Required = true)]
		public int? HeartBtInt {get; set;}
		
		[TagDetails(Tag = 95, Type = TagType.Length, Offset = 3, Required = false)]
		public int? RawDataLength {get; set;}
		
		[TagDetails(Tag = 96, Type = TagType.RawData, Offset = 4, Required = false)]
		public byte[]? RawData {get; set;}
		
		[TagDetails(Tag = 141, Type = TagType.Boolean, Offset = 5, Required = false)]
		public bool? ResetSeqNumFlag {get; set;}
		
		[TagDetails(Tag = 383, Type = TagType.Int, Offset = 6, Required = false)]
		public int? MaxMessageSize {get; set;}
		
		public sealed partial class NoMsgTypes : IFixGroup
		{
			[TagDetails(Tag = 372, Type = TagType.String, Offset = 0, Required = false)]
			public string? RefMsgType {get; set;}
			
			[TagDetails(Tag = 385, Type = TagType.String, Offset = 1, Required = false)]
			public string? MsgDirection {get; set;}
			
			
			bool IFixValidator.IsValid(in FixValidatorConfig config)
			{
				return true;
			}
			
			void IFixEncoder.Encode(IFixWriter writer)
			{
				if (RefMsgType is not null) writer.WriteString(372, RefMsgType);
				if (MsgDirection is not null) writer.WriteString(385, MsgDirection);
			}
			
			void IFixParser.Parse(IMessageView? view)
			{
				if (view is null) return;
				
				RefMsgType = view.GetString(372);
				MsgDirection = view.GetString(385);
			}
			
			bool IFixLookup.TryGetByTag(string name, out object? value)
			{
				value = null;
				switch (name)
				{
					case "RefMsgType":
					{
						value = RefMsgType;
						break;
					}
					case "MsgDirection":
					{
						value = MsgDirection;
						break;
					}
					default:
					{
						return false;
					}
				}
				return true;
			}
			
			void IFixReset.Reset()
			{
				RefMsgType = null;
				MsgDirection = null;
			}
		}
		[Group(NoOfTag = 384, Offset = 7, Required = false)]
		public NoMsgTypes[]? MsgTypes {get; set;}
		
		[TagDetails(Tag = 553, Type = TagType.String, Offset = 8, Required = false)]
		public string? Username {get; set;}
		
		[TagDetails(Tag = 554, Type = TagType.String, Offset = 9, Required = false)]
		public string? Password {get; set;}
		
		[Component(Offset = 10, Required = true)]
		public StandardTrailer? StandardTrailer {get; set;}
		
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return (!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config))) && (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (EncryptMethod is not null) writer.WriteWholeNumber(98, EncryptMethod.Value);
			if (HeartBtInt is not null) writer.WriteWholeNumber(108, HeartBtInt.Value);
			if (RawDataLength is not null) writer.WriteWholeNumber(95, RawDataLength.Value);
			if (RawData is not null) writer.WriteBuffer(96, RawData);
			if (ResetSeqNumFlag is not null) writer.WriteBoolean(141, ResetSeqNumFlag.Value);
			if (MaxMessageSize is not null) writer.WriteWholeNumber(383, MaxMessageSize.Value);
			if (MsgTypes is not null && MsgTypes.Length != 0)
			{
				writer.WriteWholeNumber(384, MsgTypes.Length);
				for (int i = 0; i < MsgTypes.Length; i++)
				{
					((IFixEncoder)MsgTypes[i]).Encode(writer);
				}
			}
			if (Username is not null) writer.WriteString(553, Username);
			if (Password is not null) writer.WriteString(554, Password);
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
			MaxMessageSize = view.GetInt32(383);
			if (view.GetView("NoMsgTypes") is IMessageView viewNoMsgTypes)
			{
				var count = viewNoMsgTypes.GroupCount();
				MsgTypes = new NoMsgTypes[count];
				for (int i = 0; i < count; i++)
				{
					MsgTypes[i] = new();
					((IFixParser)MsgTypes[i]).Parse(viewNoMsgTypes.GetGroupInstance(i));
				}
			}
			Username = view.GetString(553);
			Password = view.GetString(554);
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
				{
					value = StandardHeader;
					break;
				}
				case "EncryptMethod":
				{
					value = EncryptMethod;
					break;
				}
				case "HeartBtInt":
				{
					value = HeartBtInt;
					break;
				}
				case "RawDataLength":
				{
					value = RawDataLength;
					break;
				}
				case "RawData":
				{
					value = RawData;
					break;
				}
				case "ResetSeqNumFlag":
				{
					value = ResetSeqNumFlag;
					break;
				}
				case "MaxMessageSize":
				{
					value = MaxMessageSize;
					break;
				}
				case "NoMsgTypes":
				{
					value = MsgTypes;
					break;
				}
				case "Username":
				{
					value = Username;
					break;
				}
				case "Password":
				{
					value = Password;
					break;
				}
				case "StandardTrailer":
				{
					value = StandardTrailer;
					break;
				}
				default:
				{
					return false;
				}
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
			MaxMessageSize = null;
			MsgTypes = null;
			Username = null;
			Password = null;
			((IFixReset?)StandardTrailer)?.Reset();
		}
	}
}
