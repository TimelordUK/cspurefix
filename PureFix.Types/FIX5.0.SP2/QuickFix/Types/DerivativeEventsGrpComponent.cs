using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class DerivativeEventsGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 1286, Offset = 0, Required = false)]
		public NoDerivativeEvents[]? NoDerivativeEvents {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoDerivativeEvents is not null && NoDerivativeEvents.Length != 0)
			{
				writer.WriteWholeNumber(1286, NoDerivativeEvents.Length);
				for (int i = 0; i < NoDerivativeEvents.Length; i++)
				{
					((IFixEncoder)NoDerivativeEvents[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoDerivativeEvents") is IMessageView viewNoDerivativeEvents)
			{
				var count = viewNoDerivativeEvents.GroupCount();
				NoDerivativeEvents = new NoDerivativeEvents[count];
				for (int i = 0; i < count; i++)
				{
					NoDerivativeEvents[i] = new();
					((IFixParser)NoDerivativeEvents[i]).Parse(viewNoDerivativeEvents.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoDerivativeEvents":
					value = NoDerivativeEvents;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoDerivativeEvents = null;
		}
	}
}
