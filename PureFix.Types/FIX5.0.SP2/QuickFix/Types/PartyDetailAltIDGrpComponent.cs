using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class PartyDetailAltIDGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 1516, Offset = 0, Required = false)]
		public NoPartyDetailAltID[]? NoPartyDetailAltID {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoPartyDetailAltID is not null && NoPartyDetailAltID.Length != 0)
			{
				writer.WriteWholeNumber(1516, NoPartyDetailAltID.Length);
				for (int i = 0; i < NoPartyDetailAltID.Length; i++)
				{
					((IFixEncoder)NoPartyDetailAltID[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoPartyDetailAltID") is IMessageView viewNoPartyDetailAltID)
			{
				var count = viewNoPartyDetailAltID.GroupCount();
				NoPartyDetailAltID = new NoPartyDetailAltID[count];
				for (int i = 0; i < count; i++)
				{
					NoPartyDetailAltID[i] = new();
					((IFixParser)NoPartyDetailAltID[i]).Parse(viewNoPartyDetailAltID.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoPartyDetailAltID":
					value = NoPartyDetailAltID;
					break;
				default: return false;
			}
			return true;
		}
	}
}
