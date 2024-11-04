using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoLegBusinessCenters : IFixGroup
	{
		[TagDetails(Tag = 40924, Type = TagType.String, Offset = 0, Required = false)]
		public string? LegBusinessCenter {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (LegBusinessCenter is not null) writer.WriteString(40924, LegBusinessCenter);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			LegBusinessCenter = view.GetString(40924);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "LegBusinessCenter":
					value = LegBusinessCenter;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			LegBusinessCenter = null;
		}
	}
}
