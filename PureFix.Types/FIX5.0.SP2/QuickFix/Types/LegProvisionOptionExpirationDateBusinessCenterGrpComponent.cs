using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegProvisionOptionExpirationDateBusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 40937, Offset = 0, Required = false)]
		public NoLegProvisionOptionExpirationDateBusinessCenters[]? NoLegProvisionOptionExpirationDateBusinessCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegProvisionOptionExpirationDateBusinessCenters is not null && NoLegProvisionOptionExpirationDateBusinessCenters.Length != 0)
			{
				writer.WriteWholeNumber(40937, NoLegProvisionOptionExpirationDateBusinessCenters.Length);
				for (int i = 0; i < NoLegProvisionOptionExpirationDateBusinessCenters.Length; i++)
				{
					((IFixEncoder)NoLegProvisionOptionExpirationDateBusinessCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegProvisionOptionExpirationDateBusinessCenters") is IMessageView viewNoLegProvisionOptionExpirationDateBusinessCenters)
			{
				var count = viewNoLegProvisionOptionExpirationDateBusinessCenters.GroupCount();
				NoLegProvisionOptionExpirationDateBusinessCenters = new NoLegProvisionOptionExpirationDateBusinessCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoLegProvisionOptionExpirationDateBusinessCenters[i] = new();
					((IFixParser)NoLegProvisionOptionExpirationDateBusinessCenters[i]).Parse(viewNoLegProvisionOptionExpirationDateBusinessCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegProvisionOptionExpirationDateBusinessCenters":
					value = NoLegProvisionOptionExpirationDateBusinessCenters;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoLegProvisionOptionExpirationDateBusinessCenters = null;
		}
	}
}
