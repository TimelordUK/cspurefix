using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("BE", FixVersion.FIX44)]
	public sealed partial class UserRequest : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeaderComponent? StandardHeader {get; set;}
		
		[TagDetails(Tag = 923, Type = TagType.String, Offset = 1, Required = true)]
		public string? UserRequestID {get; set;}
		
		[TagDetails(Tag = 924, Type = TagType.Int, Offset = 2, Required = true)]
		public int? UserRequestType {get; set;}
		
		[TagDetails(Tag = 553, Type = TagType.String, Offset = 3, Required = true)]
		public string? Username {get; set;}
		
		[TagDetails(Tag = 554, Type = TagType.String, Offset = 4, Required = false)]
		public string? Password {get; set;}
		
		[TagDetails(Tag = 925, Type = TagType.String, Offset = 5, Required = false)]
		public string? NewPassword {get; set;}
		
		[TagDetails(Tag = 95, Type = TagType.Length, Offset = 6, Required = false, LinksToTag = 96)]
		public int? RawDataLength {get; set;}
		
		[TagDetails(Tag = 96, Type = TagType.RawData, Offset = 7, Required = false, LinksToTag = 95)]
		public byte[]? RawData {get; set;}
		
		[Component(Offset = 8, Required = true)]
		public StandardTrailerComponent? StandardTrailer {get; set;}
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& UserRequestID is not null
				&& UserRequestType is not null
				&& Username is not null
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (UserRequestID is not null) writer.WriteString(923, UserRequestID);
			if (UserRequestType is not null) writer.WriteWholeNumber(924, UserRequestType.Value);
			if (Username is not null) writer.WriteString(553, Username);
			if (Password is not null) writer.WriteString(554, Password);
			if (NewPassword is not null) writer.WriteString(925, NewPassword);
			if (RawData is not null)
			{
				writer.WriteWholeNumber(95, RawData.Length);
				writer.WriteBuffer(96, RawData);
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
			UserRequestID = view.GetString(923);
			UserRequestType = view.GetInt32(924);
			Username = view.GetString(553);
			Password = view.GetString(554);
			NewPassword = view.GetString(925);
			RawDataLength = view.GetInt32(95);
			RawData = view.GetByteArray(96);
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
				case "UserRequestID":
					value = UserRequestID;
					break;
				case "UserRequestType":
					value = UserRequestType;
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
				case "RawDataLength":
					value = RawDataLength;
					break;
				case "RawData":
					value = RawData;
					break;
				case "StandardTrailer":
					value = StandardTrailer;
					break;
				default: return false;
			}
			return true;
		}
	}
}
