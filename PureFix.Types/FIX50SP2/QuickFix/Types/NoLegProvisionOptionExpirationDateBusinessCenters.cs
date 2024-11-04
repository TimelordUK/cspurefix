using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoLegProvisionOptionExpirationDateBusinessCenters : IFixGroup
	{
		[TagDetails(Tag = 40500, Type = TagType.String, Offset = 0, Required = false)]
		public string? LegProvisionOptionExpirationDateBusinessCenter {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (LegProvisionOptionExpirationDateBusinessCenter is not null) writer.WriteString(40500, LegProvisionOptionExpirationDateBusinessCenter);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			LegProvisionOptionExpirationDateBusinessCenter = view.GetString(40500);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "LegProvisionOptionExpirationDateBusinessCenter":
					value = LegProvisionOptionExpirationDateBusinessCenter;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			LegProvisionOptionExpirationDateBusinessCenter = null;
		}
	}
}
