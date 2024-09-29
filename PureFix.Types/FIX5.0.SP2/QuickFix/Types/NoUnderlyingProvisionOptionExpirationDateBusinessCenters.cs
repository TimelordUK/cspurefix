using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoUnderlyingProvisionOptionExpirationDateBusinessCenters : IFixGroup
	{
		[TagDetails(Tag = 42187, Type = TagType.String, Offset = 0, Required = false)]
		public string? UnderlyingProvisionOptionExpirationDateBusinessCenter {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (UnderlyingProvisionOptionExpirationDateBusinessCenter is not null) writer.WriteString(42187, UnderlyingProvisionOptionExpirationDateBusinessCenter);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			UnderlyingProvisionOptionExpirationDateBusinessCenter = view.GetString(42187);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "UnderlyingProvisionOptionExpirationDateBusinessCenter":
					value = UnderlyingProvisionOptionExpirationDateBusinessCenter;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			UnderlyingProvisionOptionExpirationDateBusinessCenter = null;
		}
	}
}
