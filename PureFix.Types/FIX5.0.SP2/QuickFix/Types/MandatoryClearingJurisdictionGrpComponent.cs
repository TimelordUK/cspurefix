using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class MandatoryClearingJurisdictionGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41312, Offset = 0, Required = false)]
		public NoMandatoryClearingJurisdictions[]? NoMandatoryClearingJurisdictions {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoMandatoryClearingJurisdictions is not null && NoMandatoryClearingJurisdictions.Length != 0)
			{
				writer.WriteWholeNumber(41312, NoMandatoryClearingJurisdictions.Length);
				for (int i = 0; i < NoMandatoryClearingJurisdictions.Length; i++)
				{
					((IFixEncoder)NoMandatoryClearingJurisdictions[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoMandatoryClearingJurisdictions") is IMessageView viewNoMandatoryClearingJurisdictions)
			{
				var count = viewNoMandatoryClearingJurisdictions.GroupCount();
				NoMandatoryClearingJurisdictions = new NoMandatoryClearingJurisdictions[count];
				for (int i = 0; i < count; i++)
				{
					NoMandatoryClearingJurisdictions[i] = new();
					((IFixParser)NoMandatoryClearingJurisdictions[i]).Parse(viewNoMandatoryClearingJurisdictions.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoMandatoryClearingJurisdictions":
					value = NoMandatoryClearingJurisdictions;
					break;
				default: return false;
			}
			return true;
		}
	}
}
