using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoStreamEffectiveDateBusinessCenters : IFixGroup
	{
		[TagDetails(Tag = 40909, Type = TagType.String, Offset = 0, Required = false)]
		public string? StreamEffectiveDateBusinessCenter {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StreamEffectiveDateBusinessCenter is not null) writer.WriteString(40909, StreamEffectiveDateBusinessCenter);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			StreamEffectiveDateBusinessCenter = view.GetString(40909);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "StreamEffectiveDateBusinessCenter":
					value = StreamEffectiveDateBusinessCenter;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			StreamEffectiveDateBusinessCenter = null;
		}
	}
}
