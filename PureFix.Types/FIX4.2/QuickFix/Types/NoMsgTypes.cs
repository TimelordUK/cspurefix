using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX42.QuickFix.Types;

namespace PureFix.Types.FIX42.QuickFix.Types
{
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
					value = RefMsgType;
					break;
				case "MsgDirection":
					value = MsgDirection;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			RefMsgType = null;
			MsgDirection = null;
		}
	}
}
