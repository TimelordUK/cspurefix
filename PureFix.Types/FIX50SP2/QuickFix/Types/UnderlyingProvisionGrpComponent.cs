using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingProvisionGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 42149, Offset = 0, Required = false)]
		public IOINoUnderlyingProvisions[]? NoUnderlyingProvisions {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingProvisions is not null && NoUnderlyingProvisions.Length != 0)
			{
				writer.WriteWholeNumber(42149, NoUnderlyingProvisions.Length);
				for (int i = 0; i < NoUnderlyingProvisions.Length; i++)
				{
					((IFixEncoder)NoUnderlyingProvisions[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingProvisions") is IMessageView viewNoUnderlyingProvisions)
			{
				var count = viewNoUnderlyingProvisions.GroupCount();
				NoUnderlyingProvisions = new IOINoUnderlyingProvisions[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingProvisions[i] = new();
					((IFixParser)NoUnderlyingProvisions[i]).Parse(viewNoUnderlyingProvisions.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingProvisions":
					value = NoUnderlyingProvisions;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoUnderlyingProvisions = null;
		}
	}
}
