using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("BF", FixVersion.FIX44)]
	public sealed partial class UserResponse : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 923, Type = TagType.String, Offset = 1, Required = true)]
		public string? UserRequestID { get; set; }
		
		[TagDetails(Tag = 553, Type = TagType.String, Offset = 2, Required = true)]
		public string? Username { get; set; }
		
		[TagDetails(Tag = 926, Type = TagType.Int, Offset = 3, Required = false)]
		public int? UserStatus { get; set; }
		
		[TagDetails(Tag = 927, Type = TagType.String, Offset = 4, Required = false)]
		public string? UserStatusText { get; set; }
		
		[Component(Offset = 5, Required = true)]
		public StandardTrailer? StandardTrailer { get; set; }
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& UserRequestID is not null
				&& Username is not null
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (UserRequestID is not null) writer.WriteString(923, UserRequestID);
			if (Username is not null) writer.WriteString(553, Username);
			if (UserStatus is not null) writer.WriteWholeNumber(926, UserStatus.Value);
			if (UserStatusText is not null) writer.WriteString(927, UserStatusText);
			if (StandardTrailer is not null) ((IFixEncoder)StandardTrailer).Encode(writer);
		}
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
	}
}
