using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types;
using PureFix.Types.FIX43.Components;

namespace PureFix.Types.FIX43.Components
{
	public sealed partial class YieldData : IFixComponent
	{
		[TagDetails(Tag = 235, Type = TagType.String, Offset = 0, Required = false)]
		public string? YieldType {get; set;}
		
		[TagDetails(Tag = 236, Type = TagType.Float, Offset = 1, Required = false)]
		public double? Yield {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (YieldType is not null) writer.WriteString(235, YieldType);
			if (Yield is not null) writer.WriteNumber(236, Yield.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			YieldType = view.GetString(235);
			Yield = view.GetDouble(236);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "YieldType":
				{
					value = YieldType;
					break;
				}
				case "Yield":
				{
					value = Yield;
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
			YieldType = null;
			Yield = null;
		}
	}
}
