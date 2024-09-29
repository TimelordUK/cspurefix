using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoMandatoryClearingJurisdictions : IFixGroup
	{
		[TagDetails(Tag = 41313, Type = TagType.String, Offset = 0, Required = false)]
		public string? MandatoryClearingJurisdiction {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (MandatoryClearingJurisdiction is not null) writer.WriteString(41313, MandatoryClearingJurisdiction);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			MandatoryClearingJurisdiction = view.GetString(41313);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "MandatoryClearingJurisdiction":
					value = MandatoryClearingJurisdiction;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			MandatoryClearingJurisdiction = null;
		}
	}
}
