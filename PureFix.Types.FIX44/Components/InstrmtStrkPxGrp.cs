using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types;
using PureFix.Types.FIX44.Components;

namespace PureFix.Types.FIX44.Components
{
	public sealed partial class InstrmtStrkPxGrp : IFixComponent
	{
		public sealed partial class NoStrikes : IFixGroup
		{
			[Component(Offset = 0, Required = true)]
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
		[Group(NoOfTag = 428, Offset = 0, Required = true)]
		public NoStrikes[]? Strikes {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (Strikes is not null && Strikes.Length != 0)
			{
				writer.WriteWholeNumber(428, Strikes.Length);
				for (int i = 0; i < Strikes.Length; i++)
				{
					((IFixEncoder)Strikes[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoStrikes") is IMessageView viewNoStrikes)
			{
				var count = viewNoStrikes.GroupCount();
				Strikes = new NoStrikes[count];
				for (int i = 0; i < count; i++)
				{
					Strikes[i] = new();
					((IFixParser)Strikes[i]).Parse(viewNoStrikes.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoStrikes":
				{
					value = Strikes;
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
			Strikes = null;
		}
	}
}
