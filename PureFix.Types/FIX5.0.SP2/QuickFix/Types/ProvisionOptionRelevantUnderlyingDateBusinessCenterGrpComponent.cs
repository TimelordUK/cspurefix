using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class ProvisionOptionRelevantUnderlyingDateBusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 40956, Offset = 0, Required = false)]
		public NoProvisionOptionRelevantUnderlyingDateBusinessCenters[]? NoProvisionOptionRelevantUnderlyingDateBusinessCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoProvisionOptionRelevantUnderlyingDateBusinessCenters is not null && NoProvisionOptionRelevantUnderlyingDateBusinessCenters.Length != 0)
			{
				writer.WriteWholeNumber(40956, NoProvisionOptionRelevantUnderlyingDateBusinessCenters.Length);
				for (int i = 0; i < NoProvisionOptionRelevantUnderlyingDateBusinessCenters.Length; i++)
				{
					((IFixEncoder)NoProvisionOptionRelevantUnderlyingDateBusinessCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoProvisionOptionRelevantUnderlyingDateBusinessCenters") is IMessageView viewNoProvisionOptionRelevantUnderlyingDateBusinessCenters)
			{
				var count = viewNoProvisionOptionRelevantUnderlyingDateBusinessCenters.GroupCount();
				NoProvisionOptionRelevantUnderlyingDateBusinessCenters = new NoProvisionOptionRelevantUnderlyingDateBusinessCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoProvisionOptionRelevantUnderlyingDateBusinessCenters[i] = new();
					((IFixParser)NoProvisionOptionRelevantUnderlyingDateBusinessCenters[i]).Parse(viewNoProvisionOptionRelevantUnderlyingDateBusinessCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoProvisionOptionRelevantUnderlyingDateBusinessCenters":
					value = NoProvisionOptionRelevantUnderlyingDateBusinessCenters;
					break;
				default: return false;
			}
			return true;
		}
	}
}
