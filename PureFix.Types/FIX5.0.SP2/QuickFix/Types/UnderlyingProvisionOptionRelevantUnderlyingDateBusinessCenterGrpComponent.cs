using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingProvisionOptionRelevantUnderlyingDateBusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 42188, Offset = 0, Required = false)]
		public NoUnderlyingProvisionOptionRelevantUnderlyingDateBusinessCenters[]? NoUnderlyingProvisionOptionRelevantUnderlyingDateBusinessCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingProvisionOptionRelevantUnderlyingDateBusinessCenters is not null && NoUnderlyingProvisionOptionRelevantUnderlyingDateBusinessCenters.Length != 0)
			{
				writer.WriteWholeNumber(42188, NoUnderlyingProvisionOptionRelevantUnderlyingDateBusinessCenters.Length);
				for (int i = 0; i < NoUnderlyingProvisionOptionRelevantUnderlyingDateBusinessCenters.Length; i++)
				{
					((IFixEncoder)NoUnderlyingProvisionOptionRelevantUnderlyingDateBusinessCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingProvisionOptionRelevantUnderlyingDateBusinessCenters") is IMessageView viewNoUnderlyingProvisionOptionRelevantUnderlyingDateBusinessCenters)
			{
				var count = viewNoUnderlyingProvisionOptionRelevantUnderlyingDateBusinessCenters.GroupCount();
				NoUnderlyingProvisionOptionRelevantUnderlyingDateBusinessCenters = new NoUnderlyingProvisionOptionRelevantUnderlyingDateBusinessCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingProvisionOptionRelevantUnderlyingDateBusinessCenters[i] = new();
					((IFixParser)NoUnderlyingProvisionOptionRelevantUnderlyingDateBusinessCenters[i]).Parse(viewNoUnderlyingProvisionOptionRelevantUnderlyingDateBusinessCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingProvisionOptionRelevantUnderlyingDateBusinessCenters":
					value = NoUnderlyingProvisionOptionRelevantUnderlyingDateBusinessCenters;
					break;
				default: return false;
			}
			return true;
		}
	}
}
