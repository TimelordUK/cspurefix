using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class DerivativeInstrumentAttributeComponent : IFixComponent
	{
		[Group(NoOfTag = 1311, Offset = 0, Required = false)]
		public NoDerivativeInstrAttrib[]? NoDerivativeInstrAttrib {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoDerivativeInstrAttrib is not null && NoDerivativeInstrAttrib.Length != 0)
			{
				writer.WriteWholeNumber(1311, NoDerivativeInstrAttrib.Length);
				for (int i = 0; i < NoDerivativeInstrAttrib.Length; i++)
				{
					((IFixEncoder)NoDerivativeInstrAttrib[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoDerivativeInstrAttrib") is IMessageView viewNoDerivativeInstrAttrib)
			{
				var count = viewNoDerivativeInstrAttrib.GroupCount();
				NoDerivativeInstrAttrib = new NoDerivativeInstrAttrib[count];
				for (int i = 0; i < count; i++)
				{
					NoDerivativeInstrAttrib[i] = new();
					((IFixParser)NoDerivativeInstrAttrib[i]).Parse(viewNoDerivativeInstrAttrib.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoDerivativeInstrAttrib":
					value = NoDerivativeInstrAttrib;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoDerivativeInstrAttrib = null;
		}
	}
}
