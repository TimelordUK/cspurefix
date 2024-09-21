using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class PartyDetailGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 1671, Offset = 0, Required = false)]
		public NoPartyDetails[]? NoPartyDetails {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoPartyDetails is not null && NoPartyDetails.Length != 0)
			{
				writer.WriteWholeNumber(1671, NoPartyDetails.Length);
				for (int i = 0; i < NoPartyDetails.Length; i++)
				{
					((IFixEncoder)NoPartyDetails[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoPartyDetails") is IMessageView viewNoPartyDetails)
			{
				var count = viewNoPartyDetails.GroupCount();
				NoPartyDetails = new NoPartyDetails[count];
				for (int i = 0; i < count; i++)
				{
					NoPartyDetails[i] = new();
					((IFixParser)NoPartyDetails[i]).Parse(viewNoPartyDetails.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoPartyDetails":
					value = NoPartyDetails;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoPartyDetails = null;
		}
	}
}
