using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingProvisionDateBusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 42190, Offset = 0, Required = false)]
		public NoUnderlyingProvisionDateBusinessCenters[]? NoUnderlyingProvisionDateBusinessCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingProvisionDateBusinessCenters is not null && NoUnderlyingProvisionDateBusinessCenters.Length != 0)
			{
				writer.WriteWholeNumber(42190, NoUnderlyingProvisionDateBusinessCenters.Length);
				for (int i = 0; i < NoUnderlyingProvisionDateBusinessCenters.Length; i++)
				{
					((IFixEncoder)NoUnderlyingProvisionDateBusinessCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingProvisionDateBusinessCenters") is IMessageView viewNoUnderlyingProvisionDateBusinessCenters)
			{
				var count = viewNoUnderlyingProvisionDateBusinessCenters.GroupCount();
				NoUnderlyingProvisionDateBusinessCenters = new NoUnderlyingProvisionDateBusinessCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingProvisionDateBusinessCenters[i] = new();
					((IFixParser)NoUnderlyingProvisionDateBusinessCenters[i]).Parse(viewNoUnderlyingProvisionDateBusinessCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingProvisionDateBusinessCenters":
					value = NoUnderlyingProvisionDateBusinessCenters;
					break;
				default: return false;
			}
			return true;
		}
	}
}
