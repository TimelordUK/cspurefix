using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class IOINoUnderlyingProvisionOptionRelevantUnderlyingDateBusinessCenters : IFixGroup
	{
		[TagDetails(Tag = 42189, Type = TagType.String, Offset = 0, Required = false)]
		public string? UnderlyingProvisionOptionRelevantUnderlyingDateBusinessCenter {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (UnderlyingProvisionOptionRelevantUnderlyingDateBusinessCenter is not null) writer.WriteString(42189, UnderlyingProvisionOptionRelevantUnderlyingDateBusinessCenter);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			UnderlyingProvisionOptionRelevantUnderlyingDateBusinessCenter = view.GetString(42189);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "UnderlyingProvisionOptionRelevantUnderlyingDateBusinessCenter":
					value = UnderlyingProvisionOptionRelevantUnderlyingDateBusinessCenter;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			UnderlyingProvisionOptionRelevantUnderlyingDateBusinessCenter = null;
		}
	}
}
