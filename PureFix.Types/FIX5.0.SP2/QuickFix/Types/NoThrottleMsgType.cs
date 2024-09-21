using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoThrottleMsgType : IFixGroup
	{
		[TagDetails(Tag = 1619, Type = TagType.String, Offset = 0, Required = false)]
		public string? ThrottleMsgType {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (ThrottleMsgType is not null) writer.WriteString(1619, ThrottleMsgType);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			ThrottleMsgType = view.GetString(1619);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "ThrottleMsgType":
					value = ThrottleMsgType;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			ThrottleMsgType = null;
		}
	}
}
