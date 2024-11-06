using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NestedInstrumentAttributeComponent : IFixComponent
	{
		[Group(NoOfTag = 1312, Offset = 0, Required = false)]
		public SecurityDefinitionNoNestedInstrAttrib[]? NoNestedInstrAttrib {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoNestedInstrAttrib is not null && NoNestedInstrAttrib.Length != 0)
			{
				writer.WriteWholeNumber(1312, NoNestedInstrAttrib.Length);
				for (int i = 0; i < NoNestedInstrAttrib.Length; i++)
				{
					((IFixEncoder)NoNestedInstrAttrib[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoNestedInstrAttrib") is IMessageView viewNoNestedInstrAttrib)
			{
				var count = viewNoNestedInstrAttrib.GroupCount();
				NoNestedInstrAttrib = new SecurityDefinitionNoNestedInstrAttrib[count];
				for (int i = 0; i < count; i++)
				{
					NoNestedInstrAttrib[i] = new();
					((IFixParser)NoNestedInstrAttrib[i]).Parse(viewNoNestedInstrAttrib.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoNestedInstrAttrib":
					value = NoNestedInstrAttrib;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoNestedInstrAttrib = null;
		}
	}
}
