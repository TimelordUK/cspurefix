using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class EntitlementAttribGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 1777, Offset = 0, Required = false)]
		public NoEntitlementAttrib[]? NoEntitlementAttrib {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoEntitlementAttrib is not null && NoEntitlementAttrib.Length != 0)
			{
				writer.WriteWholeNumber(1777, NoEntitlementAttrib.Length);
				for (int i = 0; i < NoEntitlementAttrib.Length; i++)
				{
					((IFixEncoder)NoEntitlementAttrib[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoEntitlementAttrib") is IMessageView viewNoEntitlementAttrib)
			{
				var count = viewNoEntitlementAttrib.GroupCount();
				NoEntitlementAttrib = new NoEntitlementAttrib[count];
				for (int i = 0; i < count; i++)
				{
					NoEntitlementAttrib[i] = new();
					((IFixParser)NoEntitlementAttrib[i]).Parse(viewNoEntitlementAttrib.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoEntitlementAttrib":
					value = NoEntitlementAttrib;
					break;
				default: return false;
			}
			return true;
		}
	}
}
