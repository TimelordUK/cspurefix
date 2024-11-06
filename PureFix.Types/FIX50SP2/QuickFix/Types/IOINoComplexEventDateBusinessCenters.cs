using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class IOINoComplexEventDateBusinessCenters : IFixGroup
	{
		[TagDetails(Tag = 41019, Type = TagType.String, Offset = 0, Required = false)]
		public string? ComplexEventDateBusinessCenter {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (ComplexEventDateBusinessCenter is not null) writer.WriteString(41019, ComplexEventDateBusinessCenter);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			ComplexEventDateBusinessCenter = view.GetString(41019);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "ComplexEventDateBusinessCenter":
					value = ComplexEventDateBusinessCenter;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			ComplexEventDateBusinessCenter = null;
		}
	}
}
