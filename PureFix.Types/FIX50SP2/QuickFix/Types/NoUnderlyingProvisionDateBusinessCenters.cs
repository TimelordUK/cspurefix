using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoUnderlyingProvisionDateBusinessCenters : IFixGroup
	{
		[TagDetails(Tag = 42191, Type = TagType.String, Offset = 0, Required = false)]
		public string? UnderlyingProvisionDateBusinessCenter {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (UnderlyingProvisionDateBusinessCenter is not null) writer.WriteString(42191, UnderlyingProvisionDateBusinessCenter);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			UnderlyingProvisionDateBusinessCenter = view.GetString(42191);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "UnderlyingProvisionDateBusinessCenter":
					value = UnderlyingProvisionDateBusinessCenter;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			UnderlyingProvisionDateBusinessCenter = null;
		}
	}
}
