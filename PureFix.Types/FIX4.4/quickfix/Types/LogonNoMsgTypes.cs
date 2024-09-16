using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class LogonNoMsgTypes : IFixValidator, IFixEncoder
	{
		[TagDetails(Tag = 372, Type = TagType.String, Offset = 0, Required = false)]
		public string? RefMsgType { get; set; }
		
		[TagDetails(Tag = 385, Type = TagType.String, Offset = 1, Required = false)]
		public string? MsgDirection { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (RefMsgType is not null) writer.WriteString(372, RefMsgType);
			if (MsgDirection is not null) writer.WriteString(385, MsgDirection);
		}
	}
}
