using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoUnderlyingSettlMethodElectionDateBusinessCenters : IFixGroup
	{
		[TagDetails(Tag = 43075, Type = TagType.String, Offset = 0, Required = false)]
		public string? UnderlyingSettlMethodElectionDateBusinessCenter {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (UnderlyingSettlMethodElectionDateBusinessCenter is not null) writer.WriteString(43075, UnderlyingSettlMethodElectionDateBusinessCenter);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			UnderlyingSettlMethodElectionDateBusinessCenter = view.GetString(43075);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "UnderlyingSettlMethodElectionDateBusinessCenter":
					value = UnderlyingSettlMethodElectionDateBusinessCenter;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			UnderlyingSettlMethodElectionDateBusinessCenter = null;
		}
	}
}
