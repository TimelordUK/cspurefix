using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class RelatedInstrumentGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 1647, Offset = 0, Required = false)]
		public NoRelatedInstruments[]? NoRelatedInstruments {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoRelatedInstruments is not null && NoRelatedInstruments.Length != 0)
			{
				writer.WriteWholeNumber(1647, NoRelatedInstruments.Length);
				for (int i = 0; i < NoRelatedInstruments.Length; i++)
				{
					((IFixEncoder)NoRelatedInstruments[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoRelatedInstruments") is IMessageView viewNoRelatedInstruments)
			{
				var count = viewNoRelatedInstruments.GroupCount();
				NoRelatedInstruments = new NoRelatedInstruments[count];
				for (int i = 0; i < count; i++)
				{
					NoRelatedInstruments[i] = new();
					((IFixParser)NoRelatedInstruments[i]).Parse(viewNoRelatedInstruments.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoRelatedInstruments":
					value = NoRelatedInstruments;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoRelatedInstruments = null;
		}
	}
}
