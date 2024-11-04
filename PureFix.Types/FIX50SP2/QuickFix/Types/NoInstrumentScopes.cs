using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoInstrumentScopes : IFixGroup
	{
		[TagDetails(Tag = 1535, Type = TagType.Int, Offset = 0, Required = false)]
		public int? InstrumentScopeOperator {get; set;}
		
		[Component(Offset = 1, Required = false)]
		public InstrumentScopeComponent? InstrumentScope {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (InstrumentScopeOperator is not null) writer.WriteWholeNumber(1535, InstrumentScopeOperator.Value);
			if (InstrumentScope is not null) ((IFixEncoder)InstrumentScope).Encode(writer);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			InstrumentScopeOperator = view.GetInt32(1535);
			if (view.GetView("InstrumentScope") is IMessageView viewInstrumentScope)
			{
				InstrumentScope = new();
				((IFixParser)InstrumentScope).Parse(viewInstrumentScope);
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "InstrumentScopeOperator":
					value = InstrumentScopeOperator;
					break;
				case "InstrumentScope":
					value = InstrumentScope;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			InstrumentScopeOperator = null;
			((IFixReset?)InstrumentScope)?.Reset();
		}
	}
}
