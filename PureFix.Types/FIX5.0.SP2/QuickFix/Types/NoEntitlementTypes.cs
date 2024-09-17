using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoEntitlementTypes : IFixGroup
	{
		[TagDetails(Tag = 1775, Type = TagType.Int, Offset = 0, Required = false)]
		public int? EntitlementType {get; set;}
		
		[TagDetails(Tag = 2402, Type = TagType.Int, Offset = 1, Required = false)]
		public int? EntitlementSubType {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (EntitlementType is not null) writer.WriteWholeNumber(1775, EntitlementType.Value);
			if (EntitlementSubType is not null) writer.WriteWholeNumber(2402, EntitlementSubType.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			EntitlementType = view.GetInt32(1775);
			EntitlementSubType = view.GetInt32(2402);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "EntitlementType":
					value = EntitlementType;
					break;
				case "EntitlementSubType":
					value = EntitlementSubType;
					break;
				default: return false;
			}
			return true;
		}
	}
}
