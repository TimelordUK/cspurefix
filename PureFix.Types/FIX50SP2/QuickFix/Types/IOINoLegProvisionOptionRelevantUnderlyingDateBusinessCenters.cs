using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class IOINoLegProvisionOptionRelevantUnderlyingDateBusinessCenters : IFixGroup
	{
		[TagDetails(Tag = 40510, Type = TagType.String, Offset = 0, Required = false)]
		public string? LegProvisionOptionRelevantUnderlyingDateBusinessCenter {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (LegProvisionOptionRelevantUnderlyingDateBusinessCenter is not null) writer.WriteString(40510, LegProvisionOptionRelevantUnderlyingDateBusinessCenter);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			LegProvisionOptionRelevantUnderlyingDateBusinessCenter = view.GetString(40510);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "LegProvisionOptionRelevantUnderlyingDateBusinessCenter":
					value = LegProvisionOptionRelevantUnderlyingDateBusinessCenter;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			LegProvisionOptionRelevantUnderlyingDateBusinessCenter = null;
		}
	}
}
