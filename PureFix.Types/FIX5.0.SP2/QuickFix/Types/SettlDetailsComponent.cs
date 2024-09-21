using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class SettlDetailsComponent : IFixComponent
	{
		[Group(NoOfTag = 1158, Offset = 0, Required = false)]
		public NoSettlDetails[]? NoSettlDetails {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoSettlDetails is not null && NoSettlDetails.Length != 0)
			{
				writer.WriteWholeNumber(1158, NoSettlDetails.Length);
				for (int i = 0; i < NoSettlDetails.Length; i++)
				{
					((IFixEncoder)NoSettlDetails[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoSettlDetails") is IMessageView viewNoSettlDetails)
			{
				var count = viewNoSettlDetails.GroupCount();
				NoSettlDetails = new NoSettlDetails[count];
				for (int i = 0; i < count; i++)
				{
					NoSettlDetails[i] = new();
					((IFixParser)NoSettlDetails[i]).Parse(viewNoSettlDetails.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoSettlDetails":
					value = NoSettlDetails;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoSettlDetails = null;
		}
	}
}
