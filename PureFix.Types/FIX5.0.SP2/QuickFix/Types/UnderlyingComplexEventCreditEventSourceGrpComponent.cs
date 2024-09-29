using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingComplexEventCreditEventSourceGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41748, Offset = 0, Required = false)]
		public NoUnderlyingComplexEventCreditEventSources[]? NoUnderlyingComplexEventCreditEventSources {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingComplexEventCreditEventSources is not null && NoUnderlyingComplexEventCreditEventSources.Length != 0)
			{
				writer.WriteWholeNumber(41748, NoUnderlyingComplexEventCreditEventSources.Length);
				for (int i = 0; i < NoUnderlyingComplexEventCreditEventSources.Length; i++)
				{
					((IFixEncoder)NoUnderlyingComplexEventCreditEventSources[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingComplexEventCreditEventSources") is IMessageView viewNoUnderlyingComplexEventCreditEventSources)
			{
				var count = viewNoUnderlyingComplexEventCreditEventSources.GroupCount();
				NoUnderlyingComplexEventCreditEventSources = new NoUnderlyingComplexEventCreditEventSources[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingComplexEventCreditEventSources[i] = new();
					((IFixParser)NoUnderlyingComplexEventCreditEventSources[i]).Parse(viewNoUnderlyingComplexEventCreditEventSources.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingComplexEventCreditEventSources":
					value = NoUnderlyingComplexEventCreditEventSources;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoUnderlyingComplexEventCreditEventSources = null;
		}
	}
}
