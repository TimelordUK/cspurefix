using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types;
using PureFix.Types.FIX44.Components;

namespace PureFix.Types.FIX44.Components
{
	public sealed partial class InstrmtGrp : IFixComponent
	{
		public sealed partial class NoRelatedSym : IFixGroup
		{
			[Component(Offset = 0, Required = false)]
			public Instrument? Instrument {get; set;}
			
			
			bool IFixValidator.IsValid(in FixValidatorConfig config)
			{
				return true;
			}
			
			void IFixEncoder.Encode(IFixWriter writer)
			{
				if (Instrument is not null) ((IFixEncoder)Instrument).Encode(writer);
			}
			
			void IFixParser.Parse(IMessageView? view)
			{
				if (view is null) return;
				
				if (view.GetView("Instrument") is IMessageView viewInstrument)
				{
					Instrument = new();
					((IFixParser)Instrument).Parse(viewInstrument);
				}
			}
			
			bool IFixLookup.TryGetByTag(string name, out object? value)
			{
				value = null;
				switch (name)
				{
					case "Instrument":
					{
						value = Instrument;
						break;
					}
					default:
					{
						return false;
					}
				}
				return true;
			}
			
			void IFixReset.Reset()
			{
				((IFixReset?)Instrument)?.Reset();
			}
		}
		[Group(NoOfTag = 146, Offset = 0, Required = false)]
		public NoRelatedSym[]? RelatedSym {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (RelatedSym is not null && RelatedSym.Length != 0)
			{
				writer.WriteWholeNumber(146, RelatedSym.Length);
				for (int i = 0; i < RelatedSym.Length; i++)
				{
					((IFixEncoder)RelatedSym[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoRelatedSym") is IMessageView viewNoRelatedSym)
			{
				var count = viewNoRelatedSym.GroupCount();
				RelatedSym = new NoRelatedSym[count];
				for (int i = 0; i < count; i++)
				{
					RelatedSym[i] = new();
					((IFixParser)RelatedSym[i]).Parse(viewNoRelatedSym.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoRelatedSym":
				{
					value = RelatedSym;
					break;
				}
				default:
				{
					return false;
				}
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			RelatedSym = null;
		}
	}
}
