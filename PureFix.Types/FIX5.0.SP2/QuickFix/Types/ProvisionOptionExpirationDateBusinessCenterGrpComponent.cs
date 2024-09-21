using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class ProvisionOptionExpirationDateBusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 40955, Offset = 0, Required = false)]
		public NoProvisionOptionExpirationDateBusinessCenters[]? NoProvisionOptionExpirationDateBusinessCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoProvisionOptionExpirationDateBusinessCenters is not null && NoProvisionOptionExpirationDateBusinessCenters.Length != 0)
			{
				writer.WriteWholeNumber(40955, NoProvisionOptionExpirationDateBusinessCenters.Length);
				for (int i = 0; i < NoProvisionOptionExpirationDateBusinessCenters.Length; i++)
				{
					((IFixEncoder)NoProvisionOptionExpirationDateBusinessCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoProvisionOptionExpirationDateBusinessCenters") is IMessageView viewNoProvisionOptionExpirationDateBusinessCenters)
			{
				var count = viewNoProvisionOptionExpirationDateBusinessCenters.GroupCount();
				NoProvisionOptionExpirationDateBusinessCenters = new NoProvisionOptionExpirationDateBusinessCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoProvisionOptionExpirationDateBusinessCenters[i] = new();
					((IFixParser)NoProvisionOptionExpirationDateBusinessCenters[i]).Parse(viewNoProvisionOptionExpirationDateBusinessCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoProvisionOptionExpirationDateBusinessCenters":
					value = NoProvisionOptionExpirationDateBusinessCenters;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoProvisionOptionExpirationDateBusinessCenters = null;
		}
	}
}
