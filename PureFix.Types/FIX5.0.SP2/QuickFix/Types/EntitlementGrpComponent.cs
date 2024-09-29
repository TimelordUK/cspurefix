using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class EntitlementGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 1773, Offset = 0, Required = false)]
		public NoEntitlements[]? NoEntitlements {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoEntitlements is not null && NoEntitlements.Length != 0)
			{
				writer.WriteWholeNumber(1773, NoEntitlements.Length);
				for (int i = 0; i < NoEntitlements.Length; i++)
				{
					((IFixEncoder)NoEntitlements[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoEntitlements") is IMessageView viewNoEntitlements)
			{
				var count = viewNoEntitlements.GroupCount();
				NoEntitlements = new NoEntitlements[count];
				for (int i = 0; i < count; i++)
				{
					NoEntitlements[i] = new();
					((IFixParser)NoEntitlements[i]).Parse(viewNoEntitlements.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoEntitlements":
					value = NoEntitlements;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoEntitlements = null;
		}
	}
}
