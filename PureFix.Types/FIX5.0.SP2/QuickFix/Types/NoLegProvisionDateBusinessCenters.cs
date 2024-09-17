using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoLegProvisionDateBusinessCenters : IFixGroup
	{
		[TagDetails(Tag = 40452, Type = TagType.String, Offset = 0, Required = false)]
		public string? LegProvisionDateBusinessCenter {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (LegProvisionDateBusinessCenter is not null) writer.WriteString(40452, LegProvisionDateBusinessCenter);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			LegProvisionDateBusinessCenter = view.GetString(40452);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "LegProvisionDateBusinessCenter":
					value = LegProvisionDateBusinessCenter;
					break;
				default: return false;
			}
			return true;
		}
	}
}
