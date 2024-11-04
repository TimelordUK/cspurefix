using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingProvisionOptionExpirationDateBusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 42186, Offset = 0, Required = false)]
		public NoUnderlyingProvisionOptionExpirationDateBusinessCenters[]? NoUnderlyingProvisionOptionExpirationDateBusinessCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingProvisionOptionExpirationDateBusinessCenters is not null && NoUnderlyingProvisionOptionExpirationDateBusinessCenters.Length != 0)
			{
				writer.WriteWholeNumber(42186, NoUnderlyingProvisionOptionExpirationDateBusinessCenters.Length);
				for (int i = 0; i < NoUnderlyingProvisionOptionExpirationDateBusinessCenters.Length; i++)
				{
					((IFixEncoder)NoUnderlyingProvisionOptionExpirationDateBusinessCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingProvisionOptionExpirationDateBusinessCenters") is IMessageView viewNoUnderlyingProvisionOptionExpirationDateBusinessCenters)
			{
				var count = viewNoUnderlyingProvisionOptionExpirationDateBusinessCenters.GroupCount();
				NoUnderlyingProvisionOptionExpirationDateBusinessCenters = new NoUnderlyingProvisionOptionExpirationDateBusinessCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingProvisionOptionExpirationDateBusinessCenters[i] = new();
					((IFixParser)NoUnderlyingProvisionOptionExpirationDateBusinessCenters[i]).Parse(viewNoUnderlyingProvisionOptionExpirationDateBusinessCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingProvisionOptionExpirationDateBusinessCenters":
					value = NoUnderlyingProvisionOptionExpirationDateBusinessCenters;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoUnderlyingProvisionOptionExpirationDateBusinessCenters = null;
		}
	}
}
