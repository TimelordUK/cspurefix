using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class EntitlementTypeGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 2345, Offset = 0, Required = false)]
		public NoEntitlementTypes[]? NoEntitlementTypes {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoEntitlementTypes is not null && NoEntitlementTypes.Length != 0)
			{
				writer.WriteWholeNumber(2345, NoEntitlementTypes.Length);
				for (int i = 0; i < NoEntitlementTypes.Length; i++)
				{
					((IFixEncoder)NoEntitlementTypes[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoEntitlementTypes") is IMessageView viewNoEntitlementTypes)
			{
				var count = viewNoEntitlementTypes.GroupCount();
				NoEntitlementTypes = new NoEntitlementTypes[count];
				for (int i = 0; i < count; i++)
				{
					NoEntitlementTypes[i] = new();
					((IFixParser)NoEntitlementTypes[i]).Parse(viewNoEntitlementTypes.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoEntitlementTypes":
					value = NoEntitlementTypes;
					break;
				default: return false;
			}
			return true;
		}
	}
}
