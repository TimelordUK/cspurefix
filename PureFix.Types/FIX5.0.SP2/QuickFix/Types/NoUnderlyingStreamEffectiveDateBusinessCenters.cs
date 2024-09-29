using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoUnderlyingStreamEffectiveDateBusinessCenters : IFixGroup
	{
		[TagDetails(Tag = 40059, Type = TagType.String, Offset = 0, Required = false)]
		public string? UnderlyingStreamEffectiveDateBusinessCenter {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (UnderlyingStreamEffectiveDateBusinessCenter is not null) writer.WriteString(40059, UnderlyingStreamEffectiveDateBusinessCenter);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			UnderlyingStreamEffectiveDateBusinessCenter = view.GetString(40059);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "UnderlyingStreamEffectiveDateBusinessCenter":
					value = UnderlyingStreamEffectiveDateBusinessCenter;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			UnderlyingStreamEffectiveDateBusinessCenter = null;
		}
	}
}
